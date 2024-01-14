using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Animation;
using BarrocIntens.Data;

namespace BarrocIntens
{
    public sealed partial class LeaseContractEditWindow : Window
    {
        private LeaseContract _editedLeaseContract;

        public LeaseContractEditWindow(LeaseContract leaseContract)
        {
            this.InitializeComponent();
            _editedLeaseContract = leaseContract;
            DataContext = _editedLeaseContract; 
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate and save changes to the LeaseContract
            // You may want to add validation logic here before saving
            // For simplicity, validation is not included in this example

            // Save changes to the database using DbContext
            using (var dbContext = new AppDbContext())
            {
                dbContext.Update(_editedLeaseContract);
                dbContext.SaveChanges();
            }

            // Close the window or provide feedback to the user
            Close();
        }
    }
}

