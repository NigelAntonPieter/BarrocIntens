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
    public sealed partial class LoginWindow : Window
    {
        // De sleutel voor het opslaan van de gebruikersnaam in de lokale instellingen
        private const string LastUsernameKey = "LastUsername";

        private readonly IWindowFactory _windowFactory;

        public LoginWindow(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
            this.InitializeComponent();

            // Probeer de laatst ingevoerde gebruikersnaam op te halen en in te stellen
            string lastUsername = LoadLastUsername();
            if (!string.IsNullOrEmpty(lastUsername))
            {
                usernameTextbox.Text = lastUsername;
            }
        }

        private void LoginEl_Click(object sender, RoutedEventArgs e)
        {
            string enteredUsername = usernameTextbox.Text;
            string enteredPassword = passwordBox.Password;

            using (var db = new AppDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == usernameTextbox.Text && u.Password == enteredPassword);



                if (user != null)
                {
                    SaveLastUsername(usernameTextbox.Text);
                    ActivateWindow(user.Role);
                    this.Close();
                }
                else
                {
                    ErrorTextBlock.Text = "ongeldige inloggegvens";
                }

            }

        }
        private void ActivateWindow(string role)
        {
            var window = _windowFactory.CreateWindow(role);
            window.Activate();
        }

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