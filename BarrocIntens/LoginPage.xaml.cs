using BarrocIntens;
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
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;


namespace BarrocIntens
{
    public sealed partial class LoginPage : Page
    {
        // De sleutel voor het opslaan van de gebruikersnaam in de lokale instellingen
        private const string LastUsernameKey = "LastUsername";
        

        public LoginPage()
        {
            this.InitializeComponent();

            // Probeer de laatst ingevoerde gebruikersnaam op te halen en in te stellen
            string lastUsername = LoadLastUsername();
            if (!string.IsNullOrEmpty(lastUsername))
            {
                usernameTextbox.Text = lastUsername;
            }
        }

        private async void LoginEl_Click(object sender, RoutedEventArgs e)
        {
            string enteredUsername = usernameTextbox.Text;
            string enteredPassword = passwordBox.Password;

            using (var db = new AppDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == usernameTextbox.Text && u.Password == enteredPassword);

                if (user != null)
                {
                    // Sla de laatst ingevoerde gebruikersnaam op
                    SaveLastUsername(usernameTextbox.Text);

                    if (user.Role == "Sales")
                    {
                        var salesWindow = new SalesWindow();
                        salesWindow.Activate();
                    }
                    else if (user.Role == "Maintenance")
                    {
                        var maintenanceWindow = new Maintenance();
                        maintenanceWindow.Activate();
                    }
                    else if (user.Role == "MaintenanceAdmin")
                    {
                        var AdminMaintenanceWindow = new AdminMaintenanceWindow();
                        AdminMaintenanceWindow.Activate();
                    }
                    else if (user.Role == "Finance")
                    {
                        var financeWindow = new FinanceWindow();
                        financeWindow.Activate();
                    }
                    else if (user.Role == "Purchase")
                    {
                        this.Frame.Navigate(typeof(PurchasePage));
                    }
                    else
                    {
                        var clientWindow = new ClientWindow();
                        clientWindow.Activate();
                    }
                    
                }
                else
                {
                    await inlogDialog.ShowAsync();
                }
                
            }
           
        }
        

        // Methode om de laatst ingevoerde gebruikersnaam op te slaan in lokale instellingen
        private void SaveLastUsername(string username)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[LastUsernameKey] = username;
        }

        // Methode om de laatst ingevoerde gebruikersnaam op te halen uit lokale instellingen
        private string LoadLastUsername()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(LastUsernameKey, out object value) && value is string lastUsername)
            {
                return lastUsername;
            }

            return string.Empty;
        }
    }
}