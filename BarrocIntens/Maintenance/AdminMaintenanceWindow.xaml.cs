using BarrocIntens.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Maintenance
{
    public sealed partial class AdminMaintenanceWindow : Window
    {
        public AdminMaintenanceWindow(User user)
        {
            this.InitializeComponent();

            using (var dbContext = new AppDbContext())
            {
                var allMaintenanceAppointments = dbContext.MaintenanceAppointments.Include(ma => ma.UserMaintenanceAppointments).ToList();
                var finishedMaintenanceAppointments = allMaintenanceAppointments.Where(ma => ma.IsFinished).ToList();
                var unfinishedMaintenanceAppointments = allMaintenanceAppointments.Where(ma => !ma.IsFinished).ToList();


                // Example: Set the item source for two different controls
                FinishedMaintenanceListView.ItemsSource = finishedMaintenanceAppointments;
                UnfinishedMaintenanceListView.ItemsSource = unfinishedMaintenanceAppointments;
            }
        }

        private void MaintenanceListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedMaintenance = (Maintenance_appointment)e.ClickedItem;

            Maintenance.Frame.Navigate(typeof(MaintenanceDetailPage), selectedMaintenance);
        }
    }
}
