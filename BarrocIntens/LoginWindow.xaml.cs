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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            this.InitializeComponent();
        }

        private void LoginEl_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new AppDbContext())
            {
                var user = db.Users.Where(u => u.UserName == usernameTextbox.Text).FirstOrDefault();

                if (user != null)
                {
                    
                    if (user.Role == "Sales")
                    {
                        var salesWindow = new SalesWindow();
                        salesWindow.Activate();
                    }
                    else if(user.Role == "Maintenance")
                    {
                        var maintenanceWindow = new Maintenance();
                        maintenanceWindow.Activate();
                    }
                    else if (user.Role == "Finance")
                    {
                        var financeWindow = new FinanceWindow();
                        financeWindow.Activate();
                    }
                    else if (user.Role == "Purchase")
                    {
                        var purchaseWindow = new PurchaseWindow();
                        purchaseWindow.Activate();
                    }
                    else
                    {
                        var clientWindow = new ClientWindow();
                        clientWindow.Activate();
                    }
                }
            }
            this.Close();
        }

    }
}



        
            
        

        





