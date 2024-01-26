// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatsWindow : Window
    {
        private decimal totalAmountThisMonth;
        private int currentYear = DateTime.Now.Year;
        private List<InvoicesFinance> invoices;
        public StatsWindow()
        {
            this.InitializeComponent();
            this.LoadUserContracts();
        }

        private void LoadUserContracts()
        {
            using (var db = new AppDbContext())
            {
                invoices = db.InvoiceFinances.Where(n => n.DateCreated.Year == currentYear)
                .ToList();

                totalAmountThisMonth = invoices.Sum(i => i.Amount);

                InvoicesGridView.ItemsSource = invoices;

                TotalAmountTextBlock.Text = $"€ {totalAmountThisMonth:F2}";
            }
        }

        private void ChangeStats_Click(object sender, RoutedEventArgs e)
        {
            var MonthyStatsSwitch = new StatsWindowMonthly();
            MonthyStatsSwitch.Activate();
        }
    }
}
