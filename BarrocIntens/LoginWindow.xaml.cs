using BarrocIntens.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using BarrocIntensTestlLibrary.LoginWindow;
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
        public bool IsLoggedIn { get; private set; }
        public string UserRole { get; private set; }

        private readonly IWindowFactory _windowFactory;

        public LoginWindow(IWindowFactory windowFactory, AppDbContext @object)
        {
            _windowFactory = windowFactory;
            this.InitializeComponent();

            // Probeer de laatst ingevoerde gebruikersnaam op te halen en in te stellen
            string lastUsername = UserSettingsManager.LoadLastUsername();
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
                var user = db.Users.SingleOrDefault(u => u.UserName == enteredUsername && u.Password == enteredPassword);

                // Use the AuthenticationManager to authenticate the user
                var authManager = new AuthenticationManager(new AppAuthenticationService());
                if (user != null && authManager.Authenticate(enteredUsername, enteredPassword))
                {
                    // Authentication successful
                    UserSettingsManager.SaveLastUsername( enteredUsername);
                    ActivateWindow(user.Role);
                    IsLoggedIn = true;
                    UserRole = user.Role;
                    this.Close();
                }
                else
                {
                    // Authentication failed
                    ErrorTextBlock.Text = "Ongeldige inloggegevens";
                }
            }
        }
        private void ActivateWindow(string role)
        {
            var window = _windowFactory.CreateWindow(role);
            window.Activate();
        }

    }
}