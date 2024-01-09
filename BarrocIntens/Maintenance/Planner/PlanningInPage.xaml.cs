using BarrocIntens.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Maintenance.Planner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlanningInPage : Page
    {
        private readonly User _currentUser;
        public PlanningInPage()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();
            MaintenanceListView.ItemsSource = db.MaintenanceAppointments.Include(c => c.Company);
        }

        private void uitLogEL_Click(object sender, RoutedEventArgs e)
        {
            App.DashboardWindow.Close();
        }

        private async void DayListView_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (e.ClickedItem is Maintenance_appointment clickedMaintenance)
            {
                appointmentDialog.DataContext = clickedMaintenance;
                await appointmentDialog.ShowAsync();
            }
            else if (e.ClickedItem is Routine clickedRoutine)
            {
                await routineDialog.ShowAsync();
            }
        }


        private Dictionary<DateTime, List<string>> appointmentData = new Dictionary<DateTime, List<string>>();

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item is CalendarViewDayItem calendarItem)
            {
                using var db = new AppDbContext();
                var calendarItemDate = args.Item.Date.Date;

                var maintenanceAppointments = db.MaintenanceAppointments
                .Include(m => m.UserMaintenanceAppointments)
                .ThenInclude(uma => uma.User)
                .Include(m => m.Company)
                .Where(m => m.DateOfMaintenanceAppointment == DateOnly.FromDateTime(calendarItemDate))
                .ToList();

                var routineAppointments = db.Routines
                 .Include(m => m.UserRoutineAppointments)
                .ThenInclude(umr => umr.User)
                .Include(r => r.Company)
                .Where(r => r.DateOfRoutineAppointment == DateOnly.FromDateTime(calendarItemDate))
                .ToList();

                var allAppointments = new List<BaseAppointment>();
                allAppointments.AddRange(maintenanceAppointments);
                allAppointments.AddRange(routineAppointments);

                calendarItem.DataContext = allAppointments;

                if (allAppointments.Count() == 0)
                {
                    args.Item.IsBlackout = true;
                }
            }
        }

        private void MaintenanceListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedMaintenance = (Maintenance_appointment)e.ClickedItem;
            this.Frame.Navigate(typeof(AppointMaintenance), selectedMaintenance);
        }

        private void AddRoutine_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RoutineAppointmentPage));
        }

        private void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is Maintenance_appointment clickedMaintenance)
            {
                this.Frame.Navigate(typeof(AppointmentEditPage), clickedMaintenance);
            }
        }
        private List<int> deletedAppointmentIds = new List<int>();
        private async void ListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            // Het ophalen van het aangeklikte item is een beetje omslachtig. Dit is omdat (in tegenstelling
            // tot ItemClick) RightTapped geen ListView specifiek event is. Het werkt voor alle elementen.
            // Daarom moeten we eerst de ListViewItem vinden en dan daaruit de aangeklikte aap.
            var listViewItem = (FrameworkElement)e.OriginalSource;
            var selectedAppointment = (Maintenance_appointment)listViewItem.DataContext;

            // Als we rechtsklikken in de ListView, maar niet op een item, dan is er geen aap aangeklikt
            if (selectedAppointment == null)
            {
                return;
            }

            using var db = new AppDbContext();

            db.MaintenanceAppointments.Remove(selectedAppointment);


            try
            {
                await db.SaveChangesAsync();
                this.Frame.Navigate(typeof(PlanningInPage));
            }
            // Concurrency = gelijktijdigheid
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    // Wijzigingen die gemaakt moesten worden, maar waarbij een Exception gebeurde
                    var proposedValues = entry.CurrentValues;
                    // Gegevens zoals ze in de database staan
                    var databaseValues = entry.GetDatabaseValues();

                    // Geen databas egegevens? Dan was de aap al verwijdert en geven we simpelweg een melding
                    if (databaseValues == null)
                    {
                        // We tonen een melding
                        await databaseErrorDialog.ShowAsync();
                        // BUG:     https://github.com/microsoft/microsoft-ui-xaml/issues/1679
                        //          Wanneer al een ContentDialog geopent is, terwijl er nog een opent, dan
                        //          crasht de bovenstaande regel. Microsoft is bekend met het probleem en
                        //          werkt (erg langzaam) aan een oplossing.
                    }
                    else
                    {
                        // Anders hebben we een probleem waar we nog niks voor bedacht hebben.
                        throw ex;
                    }
                }
            }
        }

        private void appointmentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (appointmentDialog.DataContext is Maintenance_appointment selectedMaintenance)
            {
                // Navigeer naar de bewerkingspagina en geef de geselecteerde afspraak door
                this.Frame.Navigate(typeof(AppointmentEditPage), selectedMaintenance);
            }
        }
    } 
}
