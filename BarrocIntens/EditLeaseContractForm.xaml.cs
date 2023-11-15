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
using BarrocIntens.Data;

namespace BarrocIntens
{
    public sealed partial class EditLeaseContractForm : Window
    {
        private LeaseContract selectedLeaseContract;

        public EditLeaseContractForm(LeaseContract leaseContract)
        {
            this.InitializeComponent();
            selectedLeaseContract = leaseContract;

            CustomerNameTextBox.Text = selectedLeaseContract.CustomerName;
            BkrCheckCheckBox.IsChecked = selectedLeaseContract.BkrCheckPassed;
            MonthlyInvoiceCheckBox.IsChecked = selectedLeaseContract.MonthlyInvoice;
            LeaseContractIdTextBox.Text = selectedLeaseContract.Id.ToString();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            selectedLeaseContract.CustomerName = CustomerNameTextBox.Text;
            selectedLeaseContract.BkrCheckPassed = BkrCheckCheckBox.IsChecked ?? false;
            selectedLeaseContract.MonthlyInvoice = MonthlyInvoiceCheckBox.IsChecked ?? false;

            int leaseContractId;
            if (int.TryParse(LeaseContractIdTextBox.Text, out leaseContractId))
            {
                selectedLeaseContract.Id = leaseContractId;
            }

            using (var dbContext = new AppDbContext())
            {
                dbContext.SaveChanges();
            }

            this.Close();
        }
    }
}
