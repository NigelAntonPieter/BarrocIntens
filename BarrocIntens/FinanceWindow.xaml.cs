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

        public FinanceWindow()
        {
            this.InitializeComponent();
            dbContext = new AppDbContext();
        }

        private void SaveLeaseContractButton_Click(object sender, RoutedEventArgs e)
        {
            // Your existing logic for saving lease contracts
        }

        // Handle other events and logic from both sections

        private LeaseContract GetSelectedLeaseContract()
        {
            // Implement this method if needed
            return null;
        }

        private InvoiceFinance GetSelectedInvoice()
        {
            // Implement this method if needed
            return null;
        }
    }
}

