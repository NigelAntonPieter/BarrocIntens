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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchaseWindow : Window
    {
        public PurchaseWindow()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();
            productListView.ItemsSource = db.Products;
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            var productAddWindow = new ProductAddWindow();
            productAddWindow.Activate();

            productAddWindow.Closed += ProductAddWindow_Closed;
        }
        private void ProductAddWindow_Closed(object sender, WindowEventArgs args)
        {
            using var db = new AppDbContext();
            productListView.ItemsSource = db.Products;
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productListView.SelectedItem is Product selectedproduct)
            {
                using var db = new AppDbContext();
                db.Products.Remove(selectedproduct);
                db.SaveChanges();

                productListView.ItemsSource = db.Products.ToList();
            }
        }

        private void productListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is Product clickedProduct)
            {
                var productEditWindow = new ProductEditWindow(clickedProduct);
                productEditWindow.Closed += ProductEditWindow_Closed;
                productEditWindow.Activate();
            }

        }
        private void ProductEditWindow_Closed(object sender, WindowEventArgs args)
        {
            using var db = new AppDbContext();
            productListView.ItemsSource = db.Products;
        }

        private void uitlogEL_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Activate();
            this.Close();
        }
    }
}