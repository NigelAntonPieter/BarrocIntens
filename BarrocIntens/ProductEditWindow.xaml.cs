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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductEditWindow : Window
    {
        private Product _clickedProduct;

        public ProductEditWindow(Product clickedProduct)
        {
            this.InitializeComponent();
            _clickedProduct = clickedProduct;

            using var db = new AppDbContext();
            db.Products.Attach(clickedProduct);
            NameTextBox.Text = clickedProduct.Name;
            DescriptionTextBox.Text = clickedProduct.Description;
            PriceTextBox.Text = clickedProduct.PriceFormatted;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var product = _clickedProduct;

            using var db = new AppDbContext();

            var clickedProduct = db.Products.Find(_clickedProduct.Id);

            // Werk de eigenschappen van het product bij met de waarden uit de tekstvakken
            clickedProduct.Name = NameTextBox.Text;
            clickedProduct.Description = DescriptionTextBox.Text;
            // Zorg ervoor dat je de prijs op de juiste manier bijwerkt, afhankelijk van het type in je database
            clickedProduct.Price = decimal.Parse(PriceTextBox.Text);

            // Sla de wijzigingen op in de database
            db.SaveChanges();

            this.Close();
        }
    }
}