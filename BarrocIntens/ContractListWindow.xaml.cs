// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
using BarrocIntens.Data;
using System.Security.Claims;
using Windows.System;
using System.Data.Entity;

namespace BarrocIntens
{
    public sealed partial class ContractListWindow : Window
    {
        private string userName = Data.User.LoggedInUser.UserName;
        private List<LeaseContract> leaseContracts;
        public ContractListWindow()
        {
            this.InitializeComponent();
            LoadUserContracts();
        }
        private void LoadUserContracts()
        {
            using (var db = new AppDbContext())
            {
                leaseContracts = db.LeaseContracts.Where(n => n.CustomerName == userName)
                    .Include(m => m.Machine)
                    .ToList();

                notesGridView.ItemsSource = leaseContracts;
            }
        }
    }
}
