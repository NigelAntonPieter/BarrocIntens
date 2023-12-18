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
using System.Drawing;
using System.Reflection.Emit;
using Windows.UI.Input.Spatial;
using Microsoft.UI.Xaml.Automation;
using BarrocIntensTestlLibrary.LoginWindow;
using System.Threading.Tasks;
using Windows.Storage;
using BarrocUser = BarrocIntens.Data.User;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainBarrocpage : Page
    {
        //internal static User loggedInUser;
       
       
        public MainBarrocpage()
        {
            this.InitializeComponent();
            using (var db = new AppDbContext())
            {
               db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

        }

        private async void LoginPage_Click(object sender, RoutedEventArgs e)
        {
            //await GetUserFromSessionToken();
            this.Frame.Navigate(typeof(LoginPage));
        }

        private async Task<BarrocUser> GetUserFromSessionToken()
        {
            string lastUsername = UserSettingsManager.LoadLastUsername();
            string sessionTokenFileName = $"_sessionToken.txt";
            string sessionTokenPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, sessionTokenFileName);

            if (File.Exists(sessionTokenPath))
            {
                string sessionToken = await FileIO.ReadTextAsync(await StorageFile.GetFileFromPathAsync(sessionTokenPath));

                using (var db = new AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.SessionToken == sessionToken);
                    return user; // Return the user if found
                }
            }
            return null; // Geen sessietoken gevonden of fout bij lezen bestand
        }
    }
}

