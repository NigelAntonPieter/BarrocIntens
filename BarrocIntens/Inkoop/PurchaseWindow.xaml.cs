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
            productListView.ItemsSource = db.Products.OrderBy(p => p.Id);
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
            productListView.ItemsSource = db.Products.OrderByDescending(p => p.Id);
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productListView.SelectedItem is Product selectedProduct)
            {
                if (selectedProduct.IsOrdered)
                {
                    // Toon de dialog omdat het product al besteld is
                    _ = IsOrderdDialog.ShowAsync();
                }
                else
                {
                    // Voer de verwijderingslogica uit als het product niet als besteld is gemarkeerd
                    using var db = new AppDbContext();
                    db.Products.Remove(selectedProduct);
                    db.SaveChanges();

                    productListView.ItemsSource = db.Products.ToList();
                }
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
            productListView.ItemsSource = db.Products.OrderByDescending(p => p.Id);
        }

        private void uitlogEL_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Activate();
            this.Close();
        }

        private async void AddQuantity_Click(object sender, RoutedEventArgs e)
        {
            // Controleren of er een product is geselecteerd
            if (productListView.SelectedItem is Product selectedProduct)
            {
                // Openen van de ContentDialog
                ContentDialogResult result = await QuantityInputDialog.ShowAsync();

                // Controleren of de gebruiker op 'Ok' heeft geklikt in de ContentDialog
                if (result == ContentDialogResult.Primary)
                {
                    // Controleren of het ingevoerde cijfer geldig is
                    if (int.TryParse(QuantityTextBox.Text, out int quantity))
                    {
                        // De hoeveelheid van het geselecteerde product bijwerken
                        using var db = new AppDbContext();
                        selectedProduct.StockQuantity += quantity;
                        db.Update(selectedProduct);
                        db.SaveChanges();

                        // Productlijst vernieuwen
                        productListView.ItemsSource = db.Products.OrderByDescending(p => p.Id);
                    }
                    else
                    {
                         await QuantityInputDialog.ShowAsync();
                    }
                }
            }
            else
            {
                await noProductClicked.ShowAsync();
            }
        }

        private void stockStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using var db = new AppDbContext();

            if (stockStatusComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "Momenteel leverbaar")
                {
                    productListView.ItemsSource = db.Products.Where(p => p.StockQuantity > 1).OrderBy(p => p.Id).ToList();
                }
                else if (selectedItem.Content.ToString() == "Uit voorraad")
                {
                    productListView.ItemsSource = db.Products.Where(p => p.StockQuantity <= 0).OrderBy(p => p.Id).ToList();
                }
            }
        }
    }
}