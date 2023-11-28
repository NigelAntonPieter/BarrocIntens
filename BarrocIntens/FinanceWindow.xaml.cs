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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    public sealed partial class FinanceWindow : Window
    {
        private AppDbContext dbContext;

        public FinanceWindow(User user)
        {
            this.InitializeComponent();
            dbContext = new AppDbContext();

            EmployeeComboBox.ItemsSource = dbContext.Users.ToList();
            ProductComboBox.ItemsSource = dbContext.Products.ToList();
            ProductComboBox.DisplayMemberPath = "Name";
            ProductComboBox.SelectedValuePath = "Id";
        }

        private void SaveLeaseContractButton_Click(object sender, RoutedEventArgs e)
        {
            LeaseContract newLeaseContract = new LeaseContract
            {
                CustomerName = CustomerNameTextBox.Text,
                BkrCheckPassed = BkrCheckCheckBox.IsChecked ?? false,
                MonthlyInvoice = MonthlyInvoiceCheckBox.IsChecked ?? false,
            };

            dbContext.LeaseContracts.Add(newLeaseContract);
            dbContext.SaveChanges();
        }

        private void ViewLeaseContractsButton_Click(object sender, RoutedEventArgs e)
        {
            var leaseContractOverview = new LeaseContractOverviewWindow();
            leaseContractOverview.Activate();
            this.Close();
        }
        private void GenerateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            LeaseContract selectedLeaseContract = GetSelectedLeaseContract();

            if (selectedLeaseContract != null)
            {
                // Create a new invoice based on the selected lease contract
                InvoiceFinance newInvoice = new InvoiceFinance
                {
                    // Add properties for the invoice based on your logic
                };

                dbContext.InvoicesFinance.Add(newInvoice);
                dbContext.SaveChanges();
            }
        }

        private void SendInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            // Add code to send an invoice to the customer
        }

        private void MarkAsPaidButton_Click(object sender, RoutedEventArgs e)
        {
            InvoiceFinance selectedInvoice = GetSelectedInvoice();

            if (selectedInvoice != null)
            {
                selectedInvoice.IsPaid = true;

                dbContext.SaveChanges();
            }
        }

        private LeaseContract GetSelectedLeaseContract()
        {
            // Implement the logic to get the selected lease contract
            return null;
        }

        private InvoiceFinance GetSelectedInvoice()
        {
            // Implement the logic to get the selected invoice
            return null;
        }

        private void GenerateReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorMessages();

            string employeeName = (EmployeeComboBox.SelectedItem as User)?.Name;

            if (string.IsNullOrEmpty(employeeName))
            {
                ShowErrorMessage("Please select an employee.");
                return;
            }

            if (ProductComboBox.SelectedValue == null)
            {
                ShowErrorMessage("Please select a product.");
                return;
            }

            // Directly use the selected value as a string (assuming it's a string)
            string selectedProductId = ProductComboBox.SelectedValue.ToString();

            DateTime installationDate = InstallationDatePicker.Date.DateTime;
            if (installationDate == DateTime.MinValue)
            {
                ShowErrorMessage("Please select a valid installation date.");
                return;
            }

            if (!decimal.TryParse(ConnectionCostsTextBox.Text, out decimal connectionCosts))
            {
                ShowErrorMessage("Error converting connection costs.");
                return;
            }

            SaveReceiptToDatabase(employeeName, selectedProductId, installationDate, connectionCosts);
        }

        private void ClearErrorMessages()
        {
            ErrorMessageTextBlock.Text = string.Empty;
        }

        private void ShowErrorMessage(string errorMessage)
        {
            ErrorMessageTextBlock.Text = errorMessage;
        }

        private void SaveReceiptToDatabase(string employeeName, string selectedProductId, DateTime installationDate, decimal connectionCosts)
        {
            User selectedUser = dbContext.Users.FirstOrDefault(u => u.Name == employeeName);

            if (selectedUser != null)
            {
                InstallationReceipt newReceipt = new InstallationReceipt
                {
                    EmployeeName = selectedUser.Name,
                    ProductId = selectedProductId, // Use the selectedProductId directly as a string
                    InstallationDate = installationDate,
                    ConnectionCosts = connectionCosts
                };

                dbContext.InstallationReceipts.Add(newReceipt);
                dbContext.SaveChanges();
            }
        }
    }
}
 