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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private ObservableCollection<Product> currentProducts;

        public MakeReceiptForMaintenanceAppointment()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            selectedMaintenance = (Maintenance_appointment)e.Parameter;
            currentProducts = new ObservableCollection<Product>();
            RefreshProductComboBox();
            productListView.ItemsSource = currentProducts;

        }
        private void RefreshProductComboBox()
        {
            using (var db = new AppDbContext())
            {
                productsComboBox.ItemsSource = db.Products.ToList();
            }
        }
        private void ProductRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var product = (Product)button.DataContext;

            currentProducts.Remove(product);
        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Product)productsComboBox.SelectedItem;

            if (selectedProduct == null)
            {
                return;
            }

            var existingProduct = currentProducts.FirstOrDefault(g => g.Id == selectedProduct.Id);

            if (existingProduct != null)
            {
                return;
            }

            if (int.TryParse(QuantityInputTextBox.Text, out int quantity))
            {
                selectedProduct.StockQuantity -= quantity;
            }
            currentProducts.Add(selectedProduct);
        }

        private async void finishButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WorkDescriptionTextBox.Text) || string.IsNullOrWhiteSpace(LaborHoursTextBox.Text))
            {
                await ShowErrorDialog("Fill in all fields.");
                return;
            }

            if (!decimal.TryParse(LaborHoursTextBox.Text, out var laborHours))
            {
                await ShowErrorDialog("Invalid decimal values for Labor Hours or Material Cost.");
                return;
            }

            using var db = new AppDbContext();

            var newReceipt = new Maintenance_Receipt
            {
                EmployeeId = Data.User.LoggedInUser.Id,
                CompanyId = selectedMaintenance.CompanyId,
                Maintenance_appointmentId = selectedMaintenance.Id,
                WorkDescription = WorkDescriptionTextBox.Text,
                LaborHours = decimal.TryParse(LaborHoursTextBox.Text, out laborHours) ? laborHours : 0.0m,
            };

            var maintenanceReceiptProducts = new List<Product>();
            foreach (var currentMaintenanceReceiptProduct in currentProducts)
            {
                var maintenanceReceiptProduct = db.Products.First(p => p.Id == currentMaintenanceReceiptProduct.Id);
                maintenanceReceiptProduct.StockQuantity = currentMaintenanceReceiptProduct.StockQuantity;
                maintenanceReceiptProducts.Add(maintenanceReceiptProduct);
            }
            newReceipt.Products = maintenanceReceiptProducts;

            db.MaintenanceReceipts.Add(newReceipt);
            db.SaveChanges();

            var getCurrentMaintenance = db.MaintenanceAppointments.FirstOrDefault(m => m.Id == selectedMaintenance.Id);
            getCurrentMaintenance.IsFinished = true;
            getCurrentMaintenance.Maintenance_ReceiptId = newReceipt.Id;
            db.SaveChanges();

            SendEmailToAdmin(newReceipt);

            this.Frame.GoBack();
        }
        void SendEmailToAdmin(Maintenance_Receipt receipt)
        {
            var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io")
            {
                Port = 587,
                Credentials = new NetworkCredential("fbeed68a24ce3c", "e3aa23f72b0da1"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                //dit moet eigenlijk email worden van de admin maar users heeft nog geen email weet ook niet of we dat willen toevoegen
                From = new MailAddress("brent.albers1999@gmail.com"),
                Subject = "Maintenance Receipt",
                Body = $"New Maintenance Receipt:\n\nEmployee ID: {receipt.EmployeeId}\nReceipt ID: {receipt.Id}\n\nDetails:\nWork Description: {receipt.WorkDescription}\nLabor Hours: {receipt.LaborHours}",
            };

            mailMessage.To.Add("brent.albers1999@gmail.com");

            smtpClient.Send(mailMessage);
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
