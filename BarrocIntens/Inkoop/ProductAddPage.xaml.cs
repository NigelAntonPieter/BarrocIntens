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
using Windows.Storage.Pickers.Provider;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductAddPage : Page
    {
        private ObservableCollection<Product> allProducts {  get; set; }

        private StorageFile copiedFile;
        public ObservableCollection<Product_category> ProductCategories { get; set; }
        public ProductAddPage()
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
                bool isCodeValid = !int.TryParse(CodeTextBox.Text, out _);
                bool isNameValid = !int.TryParse(NameTextBox.Text, out _);
                bool isDescriptionValid = !int.TryParse(DescriptionTextBox.Text, out _);
                bool isPriceValid = decimal.TryParse(PriceTextBox.Text, out decimal price);
                bool isQuantityValid = int.TryParse(QuantityTextBox.Text, out int quantity);

                if (isCodeValid && isNameValid && isDescriptionValid && isPriceValid && isQuantityValid)
                {
                    var productCategoryId = ProductCategoryComboBox.SelectedItem as Product_category;
                    using var db = new AppDbContext();
                    var newProduct = new Product
                    {
                        Code = CodeTextBox.Text,
                        Name = NameTextBox.Text,
                        Description = DescriptionTextBox.Text,
                        Price = price, // Gebruik de eerder gecontroleerde geconverteerde waarde
                        StockQuantity = quantity, // Gebruik de eerder gecontroleerde geconverteerde waarde
                        Product_categoryId = productCategoryId.Id,
                        ImagePath = copiedFile.Path
                    };
                    db.Products.Add(newProduct);
                    db.SaveChanges();
                    
                    this.Frame.Navigate(typeof(PurchaseWindow));
                }
                else
                {
                    await priceDialog.ShowAsync(); // Toon foutmelding als invoer ongeldig is
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null && e.Parameter is Product newProduct)
            {
                if (allProducts == null)
                {
                    allProducts = new ObservableCollection<Product>();
                }
                allProducts.Add(newProduct);
                // Use allProducts in your page accordingly
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

            //var windowHandle = WindowNative.GetWindowHandle(this);
            //InitializeWithWindow.Initialize(fileopenPicker, windowHandle);

            //var windowHandle = GetWindowHandle();

            ////var filePickerInterop = fileopenPicker.As<IInitializeWithWindow>();
            //filePickerInterop.Initialize(windowHandle);

            nint windowHandle = WindowNative.GetWindowHandle(App.ParentWindow);
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