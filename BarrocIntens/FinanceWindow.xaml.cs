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

        public FinanceWindow(User user)
        {
            this.InitializeComponent();
            dbContext = new AppDbContext();

            EmployeeComboBox.ItemsSource = dbContext.Users.ToList();
            ProductComboBox.ItemsSource = dbContext.Products.ToList();
            ProductComboBox.DisplayMemberPath = "Name";
            ProductComboBox.SelectedValuePath = "Id";
            LeaseContractComboBox.ItemsSource = dbContext.LeaseContracts.ToList();
            LeaseContractComboBox.DisplayMemberPath = "CustomerName";
            LeaseContractComboBox.SelectedValuePath = "Id";
            MachineComboBox.ItemsSource = dbContext.Products.ToList();
            MachineComboBox.DisplayMemberPath = "Name";
            MachineComboBox.SelectedValuePath = "Id";
        }

        private void RefreshComboBoxes()
        {
            EmployeeComboBox.ItemsSource = dbContext.Users.ToList();

            ProductComboBox.ItemsSource = dbContext.Products.ToList();

            LeaseContractComboBox.ItemsSource = dbContext.LeaseContracts.ToList();

            MachineComboBox.ItemsSource = dbContext.Products.ToList();
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

            // Check if MachineComboBox is empty
            if (MachineComboBox.SelectedValue == null)
            {
                ShowLeaseContractErrorMessage("Please select a Machine.");
                return;
            }

            // Check if BkrCheckCheckBox is not checked
            if (BkrCheckCheckBox.IsChecked == null || !BkrCheckCheckBox.IsChecked.Value)
            {
                ShowLeaseContractErrorMessage("BKR check must be passed.");
                return;
            }

            // All fields are valid, proceed to create and save LeaseContract
            LeaseContract newLeaseContract = new LeaseContract
            {
                CustomerName = CustomerNameTextBox.Text,
                BkrCheckPassed = BkrCheckCheckBox.IsChecked ?? false,
                // Retrieve the selected machine from the database
                MachineId = int.Parse(MachineComboBox.SelectedValue.ToString())
            };

            dbContext.LeaseContracts.Add(newLeaseContract);
            dbContext.SaveChanges();

            RefreshComboBoxes();
        }

        private void ShowLeaseContractErrorMessage(string errorMessage)
        {
            LeaseContractErrorMessageTextBlock.Text = errorMessage;
        }

        private void ClearErrorMessages()
        {
            LeaseContractErrorMessageTextBlock.Text = string.Empty;
            InvoiceErrorMessageTextBlock.Text = string.Empty;
            ReceiptErrorMessageTextBlock.Text = string.Empty;
        }

        private void ViewLeaseContractsButton_Click(object sender, RoutedEventArgs e)
        {
            var leaseContractOverview = new LeaseContractOverviewWindow();
            leaseContractOverview.Activate();
            this.Close();
        }

        private void CreateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorMessages();

            // Check if LeaseContractComboBox is empty
            if (LeaseContractComboBox.SelectedValue == null)
            {
                ShowInvoiceErrorMessage("Please select a Lease Contract.");
                return;
            }

            if (DueDatePicker.Date == default(DateTimeOffset))
            {
                ShowInvoiceErrorMessage("Please select a valid Due Date.");
                return;
            }

            if (!decimal.TryParse(AmountTextBox.Text, out decimal amount))
            {
                ShowInvoiceErrorMessage("Please enter a valid Amount.");
                return;
            }

            if ((MonthlyInvoiceCheckBox.IsChecked ?? false) && (PeriodicInvoiceCheckBox.IsChecked ?? false))
            {
                ShowInvoiceErrorMessage("Please select either Monthly Invoice or Periodic Payment, not both.");
                return;
            }

            InvoiceFinance newInvoice = new InvoiceFinance
            {
                LeaseContractId = int.Parse(LeaseContractComboBox.SelectedValue.ToString()),
                DueDate = DueDatePicker.Date.DateTime,
                Amount = amount,
                IsPaid = IsPaidCheckBox.IsChecked ?? false,
                MonthlyInvoice = MonthlyInvoiceCheckBox.IsChecked ?? false,
                PeriodicInvoice = PeriodicInvoiceCheckBox.IsChecked ?? false,
            };
            CreateAndSaveInvoice(newInvoice);
        }


        private void CreateAndSaveInvoice(InvoiceFinance newInvoice)
        {
            dbContext.InvoicesFinance.Add(newInvoice);
            dbContext.SaveChanges();
        }

        private void ShowInvoiceErrorMessage(string errorMessage)
        {
            InvoiceErrorMessageTextBlock.Text = errorMessage;
        }

        private void ClearInvoiceErrorMessages()
        {
            InvoiceErrorMessageTextBlock.Text = string.Empty;
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

            Product selectedProduct = dbContext.Products.FirstOrDefault(p => p.Code == selectedProductId);

            if (selectedProduct == null)
            {
                ShowErrorMessage("Selected product not found.");
                return;
            }

            decimal machinePrice = selectedProduct.Price;
            decimal vatRate = 0.21m; // Example VAT rate (21%)

            decimal totalPrice = machinePrice + connectionCosts + (machinePrice + connectionCosts) * vatRate;

            string receiptText = $"Receipt for Coffee Machine Installation\n\n" +
                                 $"Employee: {employeeName}\n" +
                                 $"Product: {selectedProduct.Name}\n" +
                                 $"Machine Price: {machinePrice:C}\n" +
                                 $"Installation Cost: {connectionCosts:C}\n" +
                                 $"VAT (btw): {(machinePrice + connectionCosts) * vatRate:C}\n" +
                                 $"Total Price: {totalPrice:C}\n" +
                                 $"Installation Date: {installationDate.ToShortDateString()}";

            ShowReceipt(receiptText);

            SaveReceiptToDatabase(employeeName, selectedProductId, installationDate, connectionCosts, totalPrice);
        }

        private void ShowReceipt(string receiptText)
        {
            ReceiptTextBlock.Text = receiptText;
        }

        private void ClearReceipt()
        {
            ReceiptTextBlock.Text = string.Empty;
        }

        private void ShowErrorMessage(string errorMessage)
        {
            ReceiptErrorMessageTextBlock.Text = errorMessage;
        }

        private void SaveReceiptToDatabase(string employeeName, string selectedProductId, DateTime installationDate, decimal connectionCosts, decimal totalPrice)
        {
            User selectedUser = dbContext.Users.FirstOrDefault(u => u.Name == employeeName);
            Product selectedProduct = dbContext.Products.FirstOrDefault(p => p.Code == selectedProductId);

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
        private void PeriodicalPaymentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedOption = PeriodicalPaymentComboBox.SelectedItem as ComboBoxItem;
            if (selectedOption != null)
            {
                int months = Convert.ToInt32(selectedOption.Tag);
                PeriodicalPaymentComboBox.Text = months.ToString();
            }
        }
    }
}

