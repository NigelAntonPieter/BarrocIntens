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
using System.Threading.Tasks;
using BarrocIntens.Data;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Inkoop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPurchasePage : Page
    {
        private static TaskCompletionSource<bool> adminApprovalCompletionSource;
        public AdminPurchasePage()
        {
            this.InitializeComponent();
            adminApprovalCompletionSource = new TaskCompletionSource<bool>();
        }

        public void Initialize(Dictionary<string, object> parameters)
        {
            if (parameters.TryGetValue("Product", out object product) && parameters.TryGetValue("Quantity", out object quantity))
            {
                if (product is Product productObj && quantity is int quantityObj)
                {
                   
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Dictionary<string, object> parameters)
            {
                if (parameters.TryGetValue("Product", out object product) && parameters.TryGetValue("Quantity", out object quantity))
                {
                    if (product is Product productObj && quantity is int quantityObj)
                    {
                        // Show UI with product and quantity details
                        // ...
                    }
                }
            }

            base.OnNavigatedTo(e);
        }

        public Task<bool> GetAdminDecision()
        {
            return adminApprovalCompletionSource.Task;
        }

        private void ApproveButtonClick(object sender, RoutedEventArgs e)
        {
            adminApprovalCompletionSource.SetResult(true);
            this.Frame.Navigate(typeof(ProductListPage));
        }

        private void uitlogEL_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}


   

