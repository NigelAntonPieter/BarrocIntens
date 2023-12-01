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

namespace BarrocIntens
{
    public sealed partial class FinanceWindow : Window
    {
        private AppDbContext dbContext;

        public FinanceWindow()
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
            ClearErrorMessages();

            // Check if CustomerNameTextBox is empty
            if (string.IsNullOrEmpty(CustomerNameTextBox.Text))
            {
                ShowLeaseContractErrorMessage("Please enter customer name.");
                return;
            }

            // Check if BkrCheckCheckBox is not checked
            if (BkrCheckCheckBox.IsChecked == null || !BkrCheckCheckBox.IsChecked.Value)
            {
                ShowLeaseContractErrorMessage("BKR check must be passed.");
                return;
            }

            // Check if MonthlyInvoiceCheckBox is not checked
            if (MonthlyInvoiceCheckBox.IsChecked == null || !MonthlyInvoiceCheckBox.IsChecked.Value)
            {
                ShowLeaseContractErrorMessage("Monthly invoice must be selected.");
                return;
            }

            // All fields are valid, proceed to create and save LeaseContract
            LeaseContract newLeaseContract = new LeaseContract
            {
                CustomerName = CustomerNameTextBox.Text,
                BkrCheckPassed = BkrCheckCheckBox.IsChecked ?? false,
                MonthlyInvoice = MonthlyInvoiceCheckBox.IsChecked ?? false,
            };

            dbContext.LeaseContracts.Add(newLeaseContract);
            dbContext.SaveChanges();
        }

        private void ShowLeaseContractErrorMessage(string errorMessage)
        {
            LeaseContractErrorMessageTextBlock.Text = errorMessage;
        }

        private void ClearLeaseContractErrorMessages()
        {
            LeaseContractErrorMessageTextBlock.Text = string.Empty;
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

            // Get the selected product from the database
            Product selectedProduct = dbContext.Products.FirstOrDefault(p => p.Id == selectedProductId);

            if (selectedProduct == null)
            {
                ShowErrorMessage("Selected product not found.");
                return;
            }

            // Calculate total price including machine price, installation cost, and VAT
            decimal machinePrice = selectedProduct.Price;
            decimal vatRate = 0.21m; // Example VAT rate (21%)

            decimal totalPrice = machinePrice + connectionCosts + (machinePrice + connectionCosts) * vatRate;

            // Display receipt information on the UI
            string receiptText = $"Receipt for Coffee Machine Installation\n\n" +
                                 $"Employee: {employeeName}\n" +
                                 $"Product: {selectedProduct.Name}\n" +
                                 $"Machine Price: {machinePrice:C}\n" +
                                 $"Installation Cost: {connectionCosts:C}\n" +
                                 $"VAT (btw): {(machinePrice + connectionCosts) * vatRate:C}\n" +
                                 $"Total Price: {totalPrice:C}\n" +
                                 $"Installation Date: {installationDate.ToShortDateString()}";

            ShowReceipt(receiptText);

            // Save receipt to the database
            SaveReceiptToDatabase(employeeName, selectedProductId, installationDate, connectionCosts, totalPrice);
        }

        private void ShowReceipt(string receiptText)
        {
            // Display receipt information on the UI (assuming you have a TextBlock named ReceiptTextBlock)
            ReceiptTextBlock.Text = receiptText;
        }

        private void ClearReceipt()
        {
            // Clear the receipt information on the UI
            ReceiptTextBlock.Text = string.Empty;
        }

        private void ClearErrorMessages()
        {
            ErrorMessageTextBlock.Text = string.Empty;
        }

        private void ShowErrorMessage(string errorMessage)
        {
            ErrorMessageTextBlock.Text = errorMessage;
        }

        private void SaveReceiptToDatabase(string employeeName, string selectedProductId, DateTime installationDate, decimal connectionCosts, decimal totalPrice)
        {
            User selectedUser = dbContext.Users.FirstOrDefault(u => u.Name == employeeName);
            Product selectedProduct = dbContext.Products.FirstOrDefault(p => p.Id == selectedProductId);

            if (selectedUser != null && selectedProduct != null)
            {
                InstallationReceipt newReceipt = new InstallationReceipt
                {
                    EmployeeName = selectedUser.Name,
                    ProductId = selectedProductId,
                    InstallationDate = installationDate,
                    ConnectionCosts = connectionCosts,
                    TotalPrice = totalPrice
                };

                dbContext.InstallationReceipts.Add(newReceipt);
                dbContext.SaveChanges();
            }
        }
    }
}
