using BarrocIntens.Data;
using Microsoft.EntityFrameworkCore;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            this.InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            var searchInput = searchTextbox.Text;

            using var db = new AppDbContext();
            productListView.ItemsSource = db.Products
                .Where(p => p.Name.Contains(searchInput) && (!p.Is_employee_only));


        }

        private void uitlogEl_Click(object sender, RoutedEventArgs e)
        {
            AppDbContext db = new AppDbContext();
            IWindowFactory windowFactory = new WindowFactory();
            var loginwindow = new LoginWindow(windowFactory, db);
            loginwindow.Activate();
            this.Close();
        }

        private async void productListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Product clickedProduct)
            {
                // Toon de Orderdialog
                ContentDialogResult result = await Orderdialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    if (int.TryParse(quantityTextBox.Text, out int quantity))
                    {
                        using var db = new AppDbContext();

                        // Controleer of het aantal beschikbaar is om te bestellen
                        if (clickedProduct.StockQuantity >= quantity)
                        {
                            // Werk de database bij met de nieuwe hoeveelheid
                            clickedProduct.StockQuantity -= quantity;
                            clickedProduct.IsOrdered = true; // Zet IsOrdered op true

                            // Sla het product op in de database
                            db.Products.Update(clickedProduct);
                            await db.SaveChangesAsync();

                            // Voeg een opmerking toe aan de bestelling (optioneel)
                            string comment = commentTextBox.Text;
                            // Hier kun je de logica toevoegen om de opmerking op te slaan in de database, indien nodig.
                        }
                        else
                        {
                            // Melding als de gevraagde hoeveelheid niet beschikbaar is
                            _ = QuantityLevelDialog.ShowAsync();
                        }
                    }
                    else
                    {
                        // Melding als de gebruiker geen geldige numerieke waarde heeft ingevoerd
                       _ = QuantityParameterDialog.ShowAsync();
                    }
                }
            }
        }


    }
}