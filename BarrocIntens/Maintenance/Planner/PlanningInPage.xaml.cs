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
            MaintenanceListView.ItemsSource = db.MaintenanceAppointments;
        }

        private void uitLogEL_Click(object sender, RoutedEventArgs e)
        {
            App.DashboardWindow.Close();
        }

        private async void DayListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Maintenance_appointment clickedMaintenance)
            {
                var dialog = new ContentDialog()
                {
                    Title = "MaintenanceAppointment",
                    Content = $"\nLocation: {clickedMaintenance.Location}, \nCompany: {clickedMaintenance.Company.Name}",
                    CloseButtonText = "Close",
                    XamlRoot = this.XamlRoot,
                };

                await dialog.ShowAsync();
            }
            else if (e.ClickedItem is Routine clickedRoutine)
            {
                var dialog = new ContentDialog()
                {
                    Title = "RoutineBezoek",
                    Content = $"\nLocation: {clickedRoutine.Location}, \nCompany: {clickedRoutine.Company.Name}",
                    CloseButtonText = "Close",
                    XamlRoot = this.XamlRoot,
                };

                await dialog.ShowAsync();
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
                .Include(m => m.Company)
                .Where(m => m.DateOfMaintenanceAppointment == DateOnly.FromDateTime(calendarItemDate))
                .ToList();

                var routineAppointments = db.Routines
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

    }
}
