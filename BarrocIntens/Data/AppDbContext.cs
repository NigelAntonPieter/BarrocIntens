using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BarrocIntens.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_category> ProductCategories { get; set; }

        public DbSet<LeaseContract> LeaseContracts { get; set; }
        public DbSet<InvoiceFinance> InvoicesFinance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
               ConfigurationManager.ConnectionStrings["BarrocIntens"].ConnectionString,
               ServerVersion.Parse("5.7.33-winx64"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Companys)
               .WithMany(c => c.Users)
               .UsingEntity<Note>();

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "John Doe", UserName = "johndoe", Password = "password123", Role = "Sales" },
                new User { Id = 2, Name = "Jane Smith", UserName = "janesmith", Password = "password456", Role = "Maintenance"},
                new User { Id = 3, Name = "Nigel Pieter", UserName = "AnsjoNation", Password = "a", Role = "Purchase" },
                new User { Id = 4, Name = "Luna Smedes", UserName = "lunasmedes", Password = "n", Role = "Client" },
                new User { Id = 5, Name = "Merijn Sweep", UserName = "merijnsweep", Password = "m", Role = "Finance" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = "S234FREKT", Name= "Barroc Intens Italian Light", Description = "Light", Price = 499, StockQuantity = 100, IsOrdered = false},
                new Product { Id = "S234KNDPF", Name = "Barroc Intens Italian", Description = "Italian", Price = 599, StockQuantity = 5, IsOrdered = true },
                new Product {Id = "S234NNBMV", Name = "Barroc Intens Italian Deluxe", Description = "Deluxe", Price = 799, StockQuantity = 50, IsOrdered = false },
                new Product {Id = "S234MMPLA", Name = "Barroc Intens Italian Deluxe Special", Description = "Special", Price = 999, StockQuantity = 200, IsOrdered = false },
                new Product { Id = "S234XYZ01", Name = "Barroc Intens Italian Special Espresso", Description = "Exquisite and Unique Blend", Price = 999, StockQuantity = 20, IsOrdered = false },
                new Product { Id = "S234XYZ02", Name = "Barroc Intens Italian Dark Roast", Description = "Intensely Bold and Rich", Price = 1099, StockQuantity = 15, IsOrdered = false },
                new Product { Id = "S234XYZ03", Name = "Barroc Intens Italian Light Brew", Description = "Mild and Refreshing", Price = 899, StockQuantity = 25, IsOrdered = true },
                new Product { Id = "S234XYZ04", Name = "Barroc Intens Italian Deluxe Experience", Description = "Luxurious and Indulgent", Price = 1299, StockQuantity = 18, IsOrdered = false },
                new Product { Id = "S234XYZ05", Name = "Barroc Intens Italian Special Dark Mocha", Description = "Special Dark Chocolate Infusion", Price = 1199, StockQuantity = 22, IsOrdered = true },
                new Product { Id = "S234XYZ06", Name = "Barroc Intens Italian Light Vanilla Latte", Description = "Light and Vanilla Delight", Price = 999, StockQuantity = 23, IsOrdered = false },
                new Product { Id = "S234XYZ07", Name = "Barroc Intens Italian Deluxe Caramel Macchiato", Description = "Deluxe Caramel Indulgence", Price = 1399, StockQuantity = 14, IsOrdered = false },
                new Product { Id = "S234XYZ08", Name = "Barroc Intens Italian Dark Hazelnut Bliss", Description = "Dark and Nutty Sensation", Price = 1299, StockQuantity = 16, IsOrdered = true },
                new Product { Id = "S234XYZ09", Name = "Barroc Intens Italian Special Iced Cappuccino", Description = "Special Iced Bliss", Price = 1099, StockQuantity = 30, IsOrdered = false },
                new Product { Id = "S234XYZ10", Name = "Barroc Intens Italian Light Organic Euphoria", Description = "Light and Organic Elegance", Price = 1499, StockQuantity = 12, IsOrdered = false },
                new Product { Id = "S234XYZ11", Name = "Barroc Intens Italian Dark Mint Chocolate Fusion", Description = "Dark Minty Chocolate Blend", Price = 1199, StockQuantity = 17, IsOrdered = true },
                new Product { Id = "S234XYZ12", Name = "Barroc Intens Italian Deluxe Coconut Bliss", Description = "Deluxe Tropical Indulgence", Price = 1299, StockQuantity = 21, IsOrdered = false },
                new Product { Id = "S234XYZ13", Name = "Barroc Intens Italian Special Cinnamon Spice Latte", Description = "Special Warm and Spicy Experience", Price = 1399, StockQuantity = 19, IsOrdered = false },
                new Product { Id = "S234XYZ14", Name = "Barroc Intens Italian Dark Irish Cream Indulgence", Description = "Dark and Creamy Irish Delight", Price = 1599, StockQuantity = 14, IsOrdered = true },
                new Product { Id = "S234XYZ15", Name = "Barroc Intens Italian Deluxe Caramelized Pecan Brew", Description = "Deluxe Nutty and Sweet Sensation", Price = 1699, StockQuantity = 16, IsOrdered = false },
                new Product { Id = "S234XYZ16", Name = "Barroc Intens Italian Light Toffee Nut Latte", Description = "Light Toffee Infusion", Price = 1499, StockQuantity = 21, IsOrdered = false },
                new Product { Id = "S234XYZ17", Name = "Barroc Intens Italian Special Maple Walnut Elegance", Description = "Special Maple Walnut Harmony", Price = 1599, StockQuantity = 13, IsOrdered = true },
                new Product { Id = "S234XYZ18", Name = "Barroc Intens Italian Dark Pumpkin Spice Bliss", Description = "Dark and Seasonal Favorite", Price = 1399, StockQuantity = 26, IsOrdered = false },
                new Product { Id = "S234XYZ19", Name = "Barroc Intens Italian Deluxe Almond Joyful Brew", Description = "Deluxe Almond and Coconut Fusion", Price = 1699, StockQuantity = 11, IsOrdered = false },
                new Product { Id = "S234XYZ20", Name = "Barroc Intens Italian Special Raspberry Mocha", Description = "Special Berry Chocolate Bliss", Price = 1599, StockQuantity = 24, IsOrdered = true }

            );
        }

    }
}
