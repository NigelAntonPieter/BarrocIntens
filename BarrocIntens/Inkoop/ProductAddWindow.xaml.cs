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
using System.Collections.ObjectModel;
using BarrocIntens.Data;
using System.Diagnostics;
using Windows.Gaming.UI;
using Windows.Storage.Pickers;
using WinRT.Interop;
using Windows.Storage;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductAddWindow : Window
    {
        private StorageFile copiedFile;
        public ObservableCollection<Product_category> ProductCategories { get; set; }
        public ProductAddWindow()
        {
            this.InitializeComponent();
            

            using (var dbContext = new AppDbContext())
            {
                ProductCategories = new ObservableCollection<Product_category>(dbContext.ProductCategories.ToList());
            }

        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrWhiteSpace(CodeTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                 string.IsNullOrWhiteSpace(QuantityTextBox.Text) ||
                copiedFile == null)
            {

                await dialog.ShowAsync();
            }
            else
            {
                var productCategoryId = ProductCategoryComboBox.SelectedItem as Product_category;
                using var db = new AppDbContext();
                db.Products.Add(new Product
                {
                    Id = CodeTextBox.Text,
                    Name = NameTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    StockQuantity = int.Parse(QuantityTextBox.Text),
                    Product_categoryId = productCategoryId.Id,
                    ImagePath = copiedFile.Path
                });
                db.SaveChanges();

                this.Close();
            }
        }

        private async void fileButton_Click(object sender, RoutedEventArgs e)
        {
            await SelectAndCopyFileAsync();
        }

        private async Task SelectAndCopyFileAsync()
        {
            var fileopenPicker = new FileOpenPicker()
            {
                FileTypeFilter = { ".jpg", ".jpeg", ".png", ".gif" }
            };

            var windowHandle = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(fileopenPicker, windowHandle);

            var file = await fileopenPicker.PickSingleFileAsync();

            if (file == null)
            {
                return;
            }

            var localFolder = ApplicationData.Current.LocalFolder;

            var fileExtension = file.FileType;

            var currentTime = DateTime.Now;
            var renamedFileName = $"{currentTime.ToFileTime()}{fileExtension}";

            copiedFile = await file.CopyAsync(localFolder, renamedFileName);
        }
    }
}