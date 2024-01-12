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
using System.Threading.Tasks;
using BarrocUser = BarrocIntens.Data.User;


namespace BarrocIntens
{
    public sealed partial class LoginPage : Page
    {
        // De sleutel voor het opslaan van de gebruikersnaam in de lokale instellingen
        private const string LastUsernameKey = "LastUsername";
        public bool IsLoggedIn { get; private set; }
        public string UserRole { get; private set; }

        public LoginPage()
        {
            this.InitializeComponent();

            // Probeer de laatst ingevoerde gebruikersnaam op te halen en in te stellen
            string lastUsername = UserSettingsManager.LoadLastUsername();
            if (!string.IsNullOrEmpty(lastUsername))
            {
                usernameTextbox.Text = lastUsername;
            }

            // Voeg key event handlers toe voor de tekstvakken
            usernameTextbox.KeyUp += UsernameTextbox_KeyUp;
            passwordBox.KeyUp += PasswordBox_KeyUp;
        }

        private void UsernameTextbox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                AttemptLogin();
            }
        }

        private void PasswordBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                AttemptLogin();
            }
        }

        private async void AttemptLogin()
        {
            string enteredUsername = usernameTextbox.Text;
            string enteredPassword = passwordBox.Password;

            using (var db = new AppDbContext())
            {
                var authManager = new AuthenticationManager(new AppAuthenticationService());
                if (authManager.Authenticate(enteredUsername, enteredPassword))
                {
                    UserSettingsManager.SaveLastUsername(enteredUsername);
                    string sessionToken = GenerateSessionToken.SessionTokenGenerator(32); // Generate session token
                    db.Attach(BarrocUser.LoggedInUser);
                    SaveSessionTokenToFile(BarrocUser.LoggedInUser.Id, sessionToken);

                    BarrocUser.LoggedInUser.SessionToken = sessionToken; // Set session token for the user in the database
                    db.SaveChanges(); // Save changes to persist the session token
                    ActivateWindow(BarrocUser.LoggedInUser);
                }
                else
                {
                    await inlogDialog.ShowAsync();
                }
            }
        }


        private void ActivateWindow(Data.User user)
        {
            var windowFactory = new WindowFactory();
            var newWindow = windowFactory.CreateWindow(user);
            newWindow.Activate();
            App.DashboardWindow = newWindow;
        }

        private async void SaveSessionTokenToFile(int userId, string sessionToken)
        {
            string fileName = $"_sessionToken.txt";
            string sessionTokenPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
            File.WriteAllText(sessionTokenPath, sessionToken);
        }

    }
}

