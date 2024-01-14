using BarrocIntens.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

namespace BarrocIntens
{
    public sealed partial class LeaseContractOverviewWindow : Window
    {
        public LeaseContractOverviewWindow(User user)
        {
            this.InitializeComponent();


            using (var dbContext = new AppDbContext())
            {
                var LeaseContracts = dbContext.LeaseContracts
                    .Include(lc => lc.Machine)
                    .Include(lc => lc.Invoices)
                    .ToList();

                LeaseContractListView.ItemsSource = LeaseContracts;
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
            // Implement logic to send an invoice (e.g., create a .txt file)
            // You can use selected LeaseContract properties to generate the invoice
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
