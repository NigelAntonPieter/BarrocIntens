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
using BarrocIntens.Date;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductAddWindow : Window
    {
        

        public ProductAddWindow()
        {
            this.InitializeComponent();
            
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using var db = new AppDbContext();
            db.Products.Add(new Product
            {
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text,
            });
            db.SaveChanges();
        }
    }
}
    

    
        
       

        

