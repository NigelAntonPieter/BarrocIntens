using BarrocIntens.Data;
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
        static ObservableCollection<Maintenance_appointment> AllAppointments = new ObservableCollection<Maintenance_appointment>();
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

        private void DayListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(PurchaseWindow));
        }

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            var calendarItemDate = args.Item.Date;
            var relevantCalendarItems = AllAppointments.Where(item => item.DateAdded.Date == calendarItemDate.Date);

            // De DataContext is vanuit de xaml te benaderen met {Binding}
            args.Item.DataContext = relevantCalendarItems;

            if (relevantCalendarItems.Count() == 0)
            {
                args.Item.IsBlackout = true;
            }
        }
    }
}
