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


namespace BarrocIntens
{
    public sealed partial class LeaseContractManagementWindow : Window
    {
        private AppDbContext dbContext;

        public LeaseContractManagementWindow()
        {
            this.InitializeComponent();
            dbContext = new AppDbContext();
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
        private void EditLeaseContractButton_Click(object sender, RoutedEventArgs e)
        {
            LeaseContract selectedLeaseContract = GetSelectedLeaseContract();

            if (selectedLeaseContract != null)
            {
                var editForm = new EditLeaseContractForm(selectedLeaseContract);
                editForm.ShowDialog();
            }
        }

        private void DeleteLeaseContractButton_Click(object sender, RoutedEventArgs e)
        {
            LeaseContract selectedLeaseContract = GetSelectedLeaseContract();

            if (selectedLeaseContract != null)
            {
                dbContext.LeaseContracts.Remove(selectedLeaseContract);
                dbContext.SaveChanges();
            }
        }

        private void GenerateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            // Add code to generate an invoice for a lease contract

            LeaseContract selectedLeaseContract = GetSelectedLeaseContract();

            if (selectedLeaseContract != null)
            {
                // Create a new invoice based on the selected lease contract
                InvoiceFinance newInvoice = new InvoiceFinance
                {

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
            //if (leaseContractsListView.SelectedItem is LeaseContract selectedLeaseContract)
            //{
            //    return selectedLeaseContract;
            //}
            return null;

        }
        private InvoiceFinance GetSelectedInvoice()
        {
            // Add code to retrieve the selected invoice from the UI
            return null;
        }
    }
}
