using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using BarrocIntens.Date;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    public sealed partial class InstallationReceiptWindow : Window
    {
        public InstallationReceiptWindow()
        {
            this.InitializeComponent();
        }

        private void GenerateReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            string employeeName = EmployeeNameTextBox.Text;
            string machineModel = MachineModelTextBox.Text;
            DateTime installationDate = InstallationDatePicker.Date.DateTime;
            decimal connectionCosts = decimal.Parse(ConnectionCostsTextBox.Text);

            SaveReceiptToDatabase(employeeName, machineModel, installationDate, connectionCosts);

        }

        private void SaveReceiptToDatabase(string employeeName, string machineModel, DateTime installationDate, decimal connectionCosts)
        {

        }
    }
}
