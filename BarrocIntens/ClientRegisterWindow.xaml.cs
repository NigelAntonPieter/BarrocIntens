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
using BarrocIntens.Date;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientRegisterWindow : Window
    {
        public ClientRegisterWindow()
        {
            this.InitializeComponent();
        }

        private void registerEl_Click(object sender, RoutedEventArgs e)
        {
            string name = nameEl.Text;
            string username = usernameEl.Text;
            string password = PasswordEl.Password;
            string role = "client";

            var context = new AppDbContext();
            var existingUser = context.Users.FirstOrDefault(u => u.UserName == username);

            if (existingUser != null)
            {
                return;
            }

            var newUser = new User
            {
                UserName = username,
                Name = name,
                Role = role,
            };

            newUser.SetPassword(password);

            context.Users.Add(newUser);
            context.SaveChanges();

            this.Close();
        }
    }
}
