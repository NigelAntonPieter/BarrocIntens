using BarrocIntens.Data;
using Microsoft.Extensions.Primitives;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MakeReceiptForMaintenanceAppointment : Page
    {
        private Maintenance_appointment selectedMaintenance;

        public MakeReceiptForMaintenanceAppointment()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            selectedMaintenance = (Maintenance_appointment)e.Parameter;
            
        }

        private async void finishButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServiceTypeTextBox.Text) ||
                string.IsNullOrWhiteSpace(WorkDescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(MaterialsUsedTextBox.Text) ||
                string.IsNullOrWhiteSpace(LaborHoursTextBox.Text) ||
                string.IsNullOrWhiteSpace(MaterialCostTextBox.Text))
            {

                await ShowErrorDialog("Fill in all fields.");
                return;
            }


            if (!decimal.TryParse(LaborHoursTextBox.Text, out var laborHours) ||
                !decimal.TryParse(MaterialCostTextBox.Text, out var materialCost))
            {

                await ShowErrorDialog("Invalid decimal values for Labor Hours or Material Cost.");
                return;
            }
            using var db = new AppDbContext();
            var newReceipt = new Maintenance_Receipt
            {
                EmployeeId = Data.User.LoggedInUser.Id,
                VisitDate = selectedMaintenance.DateOfMaintenanceAppointment,
                CompanyId = selectedMaintenance.Company.Id,
                CustomerLocation = selectedMaintenance.Location,
                Maintenance_appointmentId = selectedMaintenance.Id,
                ServiceType = ServiceTypeTextBox.Text,
                WorkDescription = WorkDescriptionTextBox.Text,
                MaterialsUsed = MaterialsUsedTextBox.Text,
                LaborHours = decimal.TryParse(LaborHoursTextBox.Text, out laborHours) ? laborHours : 0.0m,
                MaterialCost = decimal.TryParse(MaterialCostTextBox.Text, out  materialCost) ? materialCost : 0.0m
            };
            db.MaintenanceReceipts.Add(newReceipt);
            
            db.SaveChanges();
            selectedMaintenance.IsFinished = true;
            selectedMaintenance.Maintenance_ReceiptId = newReceipt.Id;

            this.Frame.GoBack();
        }
        private async Task ShowErrorDialog(string errorMessage)
        {
            ErrorMessageText.Text = errorMessage;
            await errorDialog.ShowAsync();
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
