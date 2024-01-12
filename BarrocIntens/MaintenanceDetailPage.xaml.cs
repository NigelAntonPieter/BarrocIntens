using BarrocIntens.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
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

namespace BarrocIntens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MaintenanceDetailPage : Page
    {
        private Maintenance_appointment selectedMaintenance;

        public MaintenanceDetailPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            selectedMaintenance = (Maintenance_appointment)e.Parameter;
            using (var db = new AppDbContext())
            {
                Maintenance_Receipt receipt = null;

                if (db.MaintenanceReceipts.Any(r => r.Maintenance_appointmentId == selectedMaintenance.Id))
                {
                    receipt = db.MaintenanceReceipts
                        .Include(r => r.Company)
                        .FirstOrDefault(r => r.Maintenance_appointmentId == selectedMaintenance.Id);

                    CompanyTextBlock.Text = receipt.Company.Name;
                    WorkDescriptionTextBlock.Text = receipt.WorkDescription;
                    MaintenanceReceiptDetailsPanel.Visibility = Visibility.Visible;
                    MarkAsFinishedButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    selectedMaintenance = db.MaintenanceAppointments
                        .Include(ma => ma.UserMaintenanceAppointments)
                        .FirstOrDefault(ma => ma.Id == selectedMaintenance.Id);

                    MaintenanceReceiptDetailsPanel.Visibility = Visibility.Collapsed;
                    if (selectedMaintenance.UserMaintenanceAppointments != null &&
                        selectedMaintenance.UserMaintenanceAppointments.Any(uma => uma.UserId == Data.User.LoggedInUser.Id))
                    {
                        MarkAsFinishedButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MarkAsFinishedButton.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void MarkAsFinishedButton_Click(object sender, RoutedEventArgs e)
        {
                this.Frame.Navigate(typeof(MakeReceiptForMaintenanceAppointment), selectedMaintenance);
            
        }
    }
}
