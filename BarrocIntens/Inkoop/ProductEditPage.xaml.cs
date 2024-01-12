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
using BarrocIntens.Data;
using System.Collections.ObjectModel;
using BarrocIntens.Inkoop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductEditPage : Page
    {

        private Product _clickedProduct;
        public ObservableCollection<Product_category> ProductCategories { get; set; }

        public ProductEditPage()
        {   
            this.InitializeComponent();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Product clickedProduct)
            {
                using (var dbContext = new AppDbContext())
                {
                    ProductCategories = new ObservableCollection<Product_category>(dbContext.ProductCategories.ToList());
                }

                _clickedProduct = clickedProduct;

                using var db = new AppDbContext();
                db.Products.Attach(clickedProduct);
                NameTextBox.Text = clickedProduct.Name;
                DescriptionTextBox.Text = clickedProduct.Description;
                PriceTextBox.Text = clickedProduct.PriceFormatted;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var product = _clickedProduct;

            using var db = new AppDbContext();

            var clickedProduct = db.Products.Find(_clickedProduct.Id);
            var selectedCategory = ProductCategoryComboBox.SelectedItem as Product_category;
            if (selectedCategory != null)
            {
                clickedProduct.Product_categoryId = selectedCategory.Id;
            }
            else
            {
                clickedProduct.Product_categoryId = clickedProduct.Product_categoryId;
            }

            // Validatie voor prijs
            if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                await EditDialog.ShowAsync();
            }
            clickedProduct.Price = price;

            // Validatie voor naam
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                await EditDialog.ShowAsync();
            }
            clickedProduct.Name = NameTextBox.Text;

            // Validatie voor beschrijving (optioneel)
            clickedProduct.Description = DescriptionTextBox.Text;

            // Sla de wijzigingen op in de database
            db.SaveChanges();

            this.Frame.GoBack();

            
        }

    }
}