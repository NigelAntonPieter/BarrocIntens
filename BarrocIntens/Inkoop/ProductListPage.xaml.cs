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
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Inkoop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductListPage : Page
    {
        public static ProductListPage Instance { get; private set; }
        public ObservableCollection<Product> allProducts { get; private set; }

        public ProductListPage()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();
            var products = db.Products.ToList();
            allProducts = new ObservableCollection<Product>(products);
            productListView.ItemsSource = allProducts;
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProductAddPage));

        }


        private void productListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is Product clickedProduct)
            {
                this.Frame.Navigate(typeof(ProductEditPage), clickedProduct);
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using var db = new AppDbContext();
            productListView.ItemsSource = db.Products.OrderByDescending(p => p.Id);
            productListView.ScrollIntoView(allProducts);
        }

        private void uitlogEL_Click(object sender, RoutedEventArgs e)
        {
            App.DashboardWindow.Close();
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

        private async void productListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var listViewItem = (FrameworkElement)e.OriginalSource;
            var selectedProduct = (Product)listViewItem.DataContext;

            // Als we rechtsklikken in de ListView, maar niet op een item, dan is er geen aap aangeklikt
            if (selectedProduct == null)
            {
                return;
            }

            using var db = new AppDbContext();

            db.Products.Remove(selectedProduct);
            allProducts.Remove(selectedProduct);

            try
            {
                db.SaveChanges();
            }
            // Concurrency = gelijktijdigheid
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    // Wijzigingen die gemaakt moesten worden, maar waarbij een Exception gebeurde
                    var proposedValues = entry.CurrentValues;
                    // Gegevens zoals ze in de database staan
                    var databaseValues = entry.GetDatabaseValues();

                    // Geen databas egegevens? Dan was de aap al verwijdert en geven we simpelweg een melding
                    if (databaseValues == null)
                    {
                        // We tonen een melding
                        //await databaseErrorDialog.ShowAsync();
                        // BUG:     https://github.com/microsoft/microsoft-ui-xaml/issues/1679
                        //          Wanneer al een ContentDialog geopent is, terwijl er nog een opent, dan
                        //          crasht de bovenstaande regel. Microsoft is bekend met het probleem en
                        //          werkt (erg langzaam) aan een oplossing.
                    }
                    else
                    {
                        // Anders hebben we een probleem waar we nog niks voor bedacht hebben.
                        throw ex;
                    }
                }
            }

        }
        private void searchTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchInput = searchTextbox.Text;

            using var db = new AppDbContext();
            productListView.ItemsSource = db.Products
                .Where(p => p.Name.Contains(searchInput) && (!p.Is_employee_only));
        }
    }
}
