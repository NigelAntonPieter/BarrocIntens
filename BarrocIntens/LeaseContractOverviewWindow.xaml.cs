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
    public partial class LeaseContractOverviewWindow : Window
    {
        private AppDbContext dbContext;

        public LeaseContractOverviewWindow()
        {
            InitializeComponent();
            dbContext = new AppDbContext();

            LeaseContractsListView.ItemsSource = dbContext.LeaseContracts.ToList();
        }

        private LeaseContract GetSelectedLeaseContract()
        {
            //if (leaseContractsListView.SelectedItem is LeaseContract selectedLeaseContract)
            //{
            //    return selectedLeaseContract;
            //}
            return null;
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

    }
}
