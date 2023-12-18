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
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeeMaintenancePage : Page
    {
        private object userId;

        public EmployeeMaintenancePage()
        {
            this.InitializeComponent();

            LoadMaintenanceAppointments();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        private void LoadMaintenanceAppointments()
        {
            using (var dbContext = new AppDbContext())
            {

                var maintenanceAppointments = dbContext.UserMaintenanceAppointments
                    .Where(uma => uma.UserId == Data.User.LoggedInUser.Id)
                    .Join(
                        dbContext.MaintenanceAppointments,
                        uma => uma.MaintenanceAppointmentId,
                        ma => ma.Id,
                        (uma, ma) => ma
                    )
                    .ToList();


                MaintenanceListView.ItemsSource = maintenanceAppointments;
            }
        }
        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item is CalendarViewDayItem calendarItem)
            {
                using var db = new AppDbContext();

                var calendarItemDate = args.Item.Date.Date;
                var maintenanceAppointments = db.UserMaintenanceAppointments
                    .Where(uma => uma.UserId == Data.User.LoggedInUser.Id)
                    .Join(
                        db.MaintenanceAppointments,
                        uma => uma.MaintenanceAppointmentId,
                        ma => ma.Id,
                        (uma, ma) => ma
                    )
                    .Include(m => m.Company)
                    .Where(m => m.DateOfMaintenanceAppointment == DateOnly.FromDateTime(calendarItemDate))
                    .ToList();
    

                var allAppointments = new List<BaseAppointment>();
                allAppointments.AddRange(maintenanceAppointments);

                calendarItem.DataContext = allAppointments;

                if (allAppointments.Count == 0)
                {
                    args.Item.IsBlackout = true;
                }
            }
        } 

        private async void DayListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Maintenance_appointment clickedMaintenance)
            {
                var dialog = new ContentDialog()
                {
                    Title = "MaintenanceAppointment",
                    CloseButtonText = "Close",
                    XamlRoot = this.XamlRoot,
                };

                var stackPanel = new StackPanel();

                stackPanel.Children.Add(new TextBlock
                {
                    Text = $"Location: {clickedMaintenance.Location}, \nCompany: {clickedMaintenance.Company.Name}",
                    Margin = new Thickness(0, 0, 0, 10),
                });

                var button = new Button
                {
                    Content = "Meer",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                button.Click += (sender, args) =>
                {
                    this.Frame.Navigate(typeof(MaintenanceDetailPage), clickedMaintenance);
                    dialog.Hide();
                };

                stackPanel.Children.Add(button);

                dialog.Content = stackPanel;

                await dialog.ShowAsync();
            }
        }

        private void MaintenanceListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedMaintenance = (Maintenance_appointment)e.ClickedItem;

            this.Frame.Navigate(typeof(MaintenanceDetailPage), selectedMaintenance);
        }

    }

}
