using BarrocIntens.Data;
using BarrocIntens.Inkoop;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace BarrocIntens
{
    public sealed partial class LeaseContractOverviewWindow : Window
    {
        public static LeaseContract LeaseContract { get; private set; }

        public LeaseContractOverviewWindow(User user)
        {

            this.InitializeComponent();
            using (var dbContext = new AppDbContext())
            {
                LeaseContractListView.ItemsSource = dbContext.LeaseContracts.Include(lc => lc.Invoices).ToList(); ;
    }

    LeaseContractListView.SelectionChanged += LeaseContractListView_SelectionChanged;
        }

        private void LeaseContractListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LeaseContractListView.SelectedItem != null)
            {
                MarkAsPaidButton.IsEnabled = true;
                EditButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
        }

        private void MarkAsPaidButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeaseContractListView.SelectedItem != null)
            {
                LeaseContract selectedContract = (LeaseContract)LeaseContractListView.SelectedItem;

                selectedContract.IsPaid = true;

                using (var dbContext = new AppDbContext())
                {
                    dbContext.Update(selectedContract);
                    dbContext.SaveChanges();
                }

                RefreshList();
            }
        }
        
        private void SendInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeaseContractListView.SelectedItem != null)
            {
                LeaseContract selectedContract = (LeaseContract)LeaseContractListView.SelectedItem;

                using var db = new AppDbContext();
                var newInvoice = new InvoicesFinance
                {
                    /*LeaseContractId = selectedContract.Id*/
                    CustomerName = selectedContract.CustomerName,
                    Amount = selectedContract.Amount,
                    DateCreated = DateTime.Now,
                };
                db.InvoiceFinances.Add(newInvoice); 
                db.SaveChanges();

                string fileName = $"Invoice_{selectedContract.Id}_{DateTime.Now:yyyyMMddHHmmss}.txt";
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                System.IO.File.WriteAllText(filePath, $"Invoice Details:\n\nCustomer Name: {newInvoice.CustomerName}\nAmount: {newInvoice.Amount:C}\nDate: {newInvoice.DateCreated}\n");

                System.Diagnostics.Process.Start("notepad.exe", filePath);
            }
        }
        

        private void GenerateInvoice(LeaseContract leaseContract)
        {
            var invoice = new InvoicesFinance
            {
                CustomerName = leaseContract.CustomerName,
                Amount = leaseContract.Amount,
                DateCreated = DateTime.Now,
            };

            using (var dbContext = new AppDbContext())
            {
                dbContext.Add(invoice);
                dbContext.SaveChanges();
            }

            string fileName = $"Invoice_{leaseContract.Id}_{DateTime.Now:yyyyMMddHHmmss}.txt";
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            System.IO.File.WriteAllText(filePath, $"Invoice Details:\n\nCustomer Name: {invoice.CustomerName}\nAmount: {invoice.Amount:C}\nDate: {invoice.DateCreated}\n");

            System.Diagnostics.Process.Start("notepad.exe", filePath);
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeaseContractListView.SelectedItem != null)
            {
                LeaseContract selectedContract = (LeaseContract)LeaseContractListView.SelectedItem;

                var editWindow = new LeaseContractEditWindow(selectedContract);
                editWindow.Activate();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeaseContractListView.SelectedItem != null)
            {
                LeaseContract selectedContract = (LeaseContract)LeaseContractListView.SelectedItem;

                using (var dbContext = new AppDbContext())
                {
                    dbContext.Remove(selectedContract);
                    dbContext.SaveChanges();
                }

                RefreshList();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            using (var dbContext = new AppDbContext())
            {
                var LeaseContracts = dbContext.LeaseContracts
                    .Include(lc => lc.Machine)
                    .Include(lc => lc.Invoices)
                    .ToList();

                LeaseContractListView.ItemsSource = LeaseContracts;

                LeaseContractListView.SelectedItem = null;
                MarkAsPaidButton.IsEnabled = false;
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }
    }
}
