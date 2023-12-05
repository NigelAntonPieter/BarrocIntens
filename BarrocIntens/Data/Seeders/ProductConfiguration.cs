using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data.Seeders
{
    public class Product_categoryConfiguration : IEntityTypeConfiguration<Product_category>
    {
        public void Configure(EntityTypeBuilder<Product_category> builder)
        {
            builder.HasData(
                new Product_category { Id = 1, Name = "testforclient" },
                new Product_category { Id = 2, Name = "testforemployee" }
            );
        }
    }
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
            new Product { Id = 1, Code = "S234FREKT", Name = "Barroc Intens Italian Light", Product_categoryId = 2, Is_employee_only = true, Description = "Light", Price = 499, StockQuantity = 100, IsOrdered = false },
            new Product { Id = 2, Code = "S234KNDPF", Name = "Barroc Intens Italian", Product_categoryId = 1, Is_employee_only = false, Description = "Italian", Price = 599, StockQuantity = 5, IsOrdered = true },
            new Product { Id = 3, Code = "S234NNBMV", Name = "Barroc Intens Italian Deluxe", Product_categoryId = 1, Is_employee_only = false, Description = "Deluxe", Price = 799, StockQuantity = 50, IsOrdered = false },
            new Product { Id = 4, Code = "S234MMPLA", Name = "Barroc Intens Italian Deluxe Special", Product_categoryId = 1, Is_employee_only = false, Description = "Special", Price = 999, StockQuantity = 200, IsOrdered = false },
            new Product { Id = 5, Code = "S234XYZ01", Name = "Barroc Intens Italian Special Espresso", Product_categoryId = 1, Is_employee_only = false, Description = "Exquisite and Unique Blend", Price = 999, StockQuantity = 20, IsOrdered = false },
            new Product { Id = 6, Code = "S234XYZ02", Name = "Barroc Intens Italian Dark Roast", Product_categoryId = 1, Is_employee_only = false, Description = "Intensely Bold and Rich", Price = 1099, StockQuantity = 15, IsOrdered = false },
            new Product { Id = 7, Code = "S234XYZ03", Name = "Barroc Intens Italian Light Brew", Product_categoryId = 1, Is_employee_only = false, Description = "Mild and Refreshing", Price = 899, StockQuantity = 25, IsOrdered = true },
            new Product { Id = 8, Code = "S234XYZ04", Name = "Barroc Intens Italian Deluxe Experience", Product_categoryId = 1, Is_employee_only = false, Description = "Luxurious and Indulgent", Price = 1299, StockQuantity = 18, IsOrdered = false },
            new Product {Id = 9, Code = "S234XYZ05", Name = "Barroc Intens Italian Special Dark Mocha", Product_categoryId = 1, Is_employee_only = false, Description = "Special Dark Chocolate Infusion", Price = 1199, StockQuantity = 22, IsOrdered = true },
            new Product {Id = 10, Code = "S234XYZ06", Name = "Barroc Intens Italian Light Vanilla Latte", Product_categoryId = 1, Is_employee_only = true, Description = "Light and Vanilla Delight", Price = 999, StockQuantity = 23, IsOrdered = false },
            new Product {Id = 11, Code = "S234XYZ07", Name = "Barroc Intens Italian Deluxe Caramel Macchiato", Product_categoryId = 1, Is_employee_only = true, Description = "Deluxe Caramel Indulgence", Price = 1399, StockQuantity = 14, IsOrdered = false },
            new Product {Id = 12, Code = "S234XYZ08", Name = "Barroc Intens Italian Dark Hazelnut Bliss", Product_categoryId = 1, Is_employee_only = true, Description = "Dark and Nutty Sensation", Price = 1299, StockQuantity = 16, IsOrdered = true },
            new Product {Id = 13, Code = "S234XYZ09", Name = "Barroc Intens Italian Special Iced Cappuccino", Product_categoryId = 1, Is_employee_only = true, Description = "Special Iced Bliss", Price = 1099, StockQuantity = 30, IsOrdered = false },
            new Product {Id = 14, Code = "S234XYZ10", Name = "Barroc Intens Italian Light Organic Euphoria", Product_categoryId = 1, Is_employee_only = true, Description = "Light and Organic Elegance", Price = 1499, StockQuantity = 12, IsOrdered = false },
            new Product {Id = 15, Code = "S234XYZ11", Name = "Barroc Intens Italian Dark Mint Chocolate Fusion", Product_categoryId = 1, Is_employee_only = true, Description = "Dark Minty Chocolate Blend", Price = 1199, StockQuantity = 17, IsOrdered = true },
            new Product {Id = 16, Code = "S234XYZ12", Name = "Barroc Intens Italian Deluxe Coconut Bliss", Product_categoryId = 1, Is_employee_only = true, Description = "Deluxe Tropical Indulgence", Price = 1299, StockQuantity = 21, IsOrdered = false },
            new Product {Id = 17, Code = "S234XYZ13", Name = "Barroc Intens Italian Special Cinnamon Spice Latte", Product_categoryId = 1, Is_employee_only = true, Description = "Special Warm and Spicy Experience", Price = 1399, StockQuantity = 19, IsOrdered = false },
            new Product {Id = 18, Code = "S234XYZ14", Name = "Barroc Intens Italian Dark Irish Cream Indulgence", Product_categoryId = 1, Is_employee_only = true, Description = "Dark and Creamy Irish Delight", Price = 1599, StockQuantity = 14, IsOrdered = true },
            new Product {Id = 19, Code = "S234XYZ15", Name = "Barroc Intens Italian Deluxe Caramelized Pecan Brew", Product_categoryId = 1, Is_employee_only = true, Description = "Deluxe Nutty and Sweet Sensation", Price = 1699, StockQuantity = 16, IsOrdered = false },
            new Product {Id = 20, Code = "S234XYZ16", Name = "Barroc Intens Italian Light Toffee Nut Latte", Product_categoryId = 1, Is_employee_only = true, Description = "Light Toffee Infusion", Price = 1499, StockQuantity = 21, IsOrdered = false },
            new Product {Id = 21, Code = "S234XYZ17", Name = "Barroc Intens Italian Special Maple Walnut Elegance", Product_categoryId = 1, Is_employee_only = true, Description = "Special Maple Walnut Harmony", Price = 1599, StockQuantity = 13, IsOrdered = true },
            new Product {Id = 22, Code = "S234XYZ18", Name = "Barroc Intens Italian Dark Pumpkin Spice Bliss", Product_categoryId = 1, Is_employee_only = true, Description = "Dark and Seasonal Favorite", Price = 1399, StockQuantity = 26, IsOrdered = false },
            new Product {Id = 23, Code = "S234XYZ19", Name = "Barroc Intens Italian Deluxe Almond Joyful Brew", Product_categoryId = 1, Is_employee_only = true, Description = "Deluxe Almond and Coconut Fusion", Price = 1699, StockQuantity = 11, IsOrdered = false },
            new Product {Id = 24, Code = "S234XYZ20", Name = "Barroc Intens Italian Special Raspberry Mocha", Product_categoryId = 1, Is_employee_only = true, Description = "Special Berry Chocolate Bliss", Price = 1599, StockQuantity = 24, IsOrdered = true }
            );
        }
    }

}
