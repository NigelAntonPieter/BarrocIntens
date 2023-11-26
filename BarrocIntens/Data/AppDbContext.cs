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
        public DbSet<Maintenance_appointment> MaintenanceAppointments { get; set; }
        public DbSet<Product_category> ProductCategories { get; set; }
        public DbSet<LeaseContract> LeaseContracts { get; set; }
        public DbSet<InvoiceFinance> InvoicesFinance { get; set; }
        public DbSet<InstallationReceipt> InstallationReceipts { get; set; }

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
                new User { Id = 5, Name = "Merijn Sweep", UserName = "merijnsweep", Password = "m", Role = "Finance" },
                  new User { Id = 6, Name = "Brent Albers", UserName = "balbers", Password = "n", Role = "MaintenanceAdmin" }

            );
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Company1", Phone = "123-456-7890", Street = "123 Main Street" },
                new Company { Id = 2, Name = "Company2", Phone = "987-654-3210", Street = "456 Oak Avenue" },
                new Company { Id = 3, Name = "Company3", Phone = "555-123-4567", Street = "789 Pine Road" }
            );
            modelBuilder.Entity<Maintenance_appointment>().HasData(
                new Maintenance_appointment { Id = 1, CompanyId = 1, Remark = "Some seeded remark 1", DateAdded = DateTime.Now },
                new Maintenance_appointment { Id = 2, CompanyId = 2, Remark = "Some seeded remark 2", DateAdded = DateTime.Now }
            );
            modelBuilder.Entity<InstallationReceipt>().HasData(
         new InstallationReceipt { Id = 1, EmployeeName = "John", ProductId = "1", InstallationDate = DateTime.Now, ConnectionCosts = 500.00m },
         new InstallationReceipt { Id = 2, EmployeeName = "Jane", ProductId = "1", InstallationDate = DateTime.Now, ConnectionCosts = 700.00m }
         );

     modelBuilder.Entity<Product_category>().HasData(
                new Product_category { Id = 1, Name = "testforclient"},
                new Product_category { Id = 2, Name = "testforemployee" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = "S234FREKT", Name= "Barroc Intens Italian Light",Product_categoryId = 2, Is_employee_only = true, Description = "Light", Price = 499, StockQuantity = 100, IsOrdered = false},
                new Product { Id = "S234KNDPF", Name = "Barroc Intens Italian", Product_categoryId = 1, Is_employee_only = false, Description = "Italian", Price = 599, StockQuantity = 5, IsOrdered = true },
                new Product {Id = "S234NNBMV", Name = "Barroc Intens Italian Deluxe", Product_categoryId = 1, Is_employee_only = false, Description = "Deluxe", Price = 799, StockQuantity = 50, IsOrdered = false },
                new Product {Id = "S234MMPLA", Name = "Barroc Intens Italian Deluxe Special", Product_categoryId = 1, Is_employee_only = false, Description = "Special", Price = 999, StockQuantity = 200, IsOrdered = false },
                new Product { Id = "S234XYZ01", Name = "Barroc Intens Italian Special Espresso", Product_categoryId = 1, Is_employee_only = false, Description = "Exquisite and Unique Blend", Price = 999, StockQuantity = 20, IsOrdered = false },
                new Product { Id = "S234XYZ02", Name = "Barroc Intens Italian Dark Roast", Product_categoryId = 1, Is_employee_only = false, Description = "Intensely Bold and Rich", Price = 1099, StockQuantity = 15, IsOrdered = false },
                new Product { Id = "S234XYZ03", Name = "Barroc Intens Italian Light Brew", Product_categoryId = 1, Is_employee_only = false, Description = "Mild and Refreshing", Price = 899, StockQuantity = 25, IsOrdered = true },
                new Product { Id = "S234XYZ04", Name = "Barroc Intens Italian Deluxe Experience", Product_categoryId = 1, Is_employee_only = false, Description = "Luxurious and Indulgent", Price = 1299, StockQuantity = 18, IsOrdered = false },
                new Product { Id = "S234XYZ05", Name = "Barroc Intens Italian Special Dark Mocha", Product_categoryId = 1, Is_employee_only = false, Description = "Special Dark Chocolate Infusion", Price = 1199, StockQuantity = 22, IsOrdered = true },
                new Product { Id = "S234XYZ06", Name = "Barroc Intens Italian Light Vanilla Latte", Product_categoryId = 1, Is_employee_only = true, Description = "Light and Vanilla Delight", Price = 999, StockQuantity = 23, IsOrdered = false },
                new Product { Id = "S234XYZ07", Name = "Barroc Intens Italian Deluxe Caramel Macchiato", Product_categoryId = 1, Is_employee_only = true, Description = "Deluxe Caramel Indulgence", Price = 1399, StockQuantity = 14, IsOrdered = false },
                new Product { Id = "S234XYZ08", Name = "Barroc Intens Italian Dark Hazelnut Bliss", Product_categoryId = 1, Is_employee_only = true, Description = "Dark and Nutty Sensation", Price = 1299, StockQuantity = 16, IsOrdered = true },
                new Product { Id = "S234XYZ09", Name = "Barroc Intens Italian Special Iced Cappuccino", Product_categoryId = 1, Is_employee_only = true, Description = "Special Iced Bliss", Price = 1099, StockQuantity = 30, IsOrdered = false },
                new Product { Id = "S234XYZ10", Name = "Barroc Intens Italian Light Organic Euphoria", Product_categoryId = 1, Is_employee_only = true, Description = "Light and Organic Elegance", Price = 1499, StockQuantity = 12, IsOrdered = false },
                new Product { Id = "S234XYZ11", Name = "Barroc Intens Italian Dark Mint Chocolate Fusion", Product_categoryId = 1, Is_employee_only = true, Description = "Dark Minty Chocolate Blend", Price = 1199, StockQuantity = 17, IsOrdered = true },
                new Product { Id = "S234XYZ12", Name = "Barroc Intens Italian Deluxe Coconut Bliss", Product_categoryId = 1, Is_employee_only = true, Description = "Deluxe Tropical Indulgence", Price = 1299, StockQuantity = 21, IsOrdered = false },
                new Product { Id = "S234XYZ13", Name = "Barroc Intens Italian Special Cinnamon Spice Latte", Product_categoryId = 1, Is_employee_only = true, Description = "Special Warm and Spicy Experience", Price = 1399, StockQuantity = 19, IsOrdered = false },
                new Product { Id = "S234XYZ14", Name = "Barroc Intens Italian Dark Irish Cream Indulgence", Product_categoryId = 1, Is_employee_only = true, Description = "Dark and Creamy Irish Delight", Price = 1599, StockQuantity = 14, IsOrdered = true },
                new Product { Id = "S234XYZ15", Name = "Barroc Intens Italian Deluxe Caramelized Pecan Brew", Product_categoryId = 1, Is_employee_only = true, Description = "Deluxe Nutty and Sweet Sensation", Price = 1699, StockQuantity = 16, IsOrdered = false },
                new Product { Id = "S234XYZ16", Name = "Barroc Intens Italian Light Toffee Nut Latte", Product_categoryId = 1, Is_employee_only = true, Description = "Light Toffee Infusion", Price = 1499, StockQuantity = 21, IsOrdered = false },
                new Product { Id = "S234XYZ17", Name = "Barroc Intens Italian Special Maple Walnut Elegance", Product_categoryId = 1, Is_employee_only = true, Description = "Special Maple Walnut Harmony", Price = 1599, StockQuantity = 13, IsOrdered = true },
                new Product { Id = "S234XYZ18", Name = "Barroc Intens Italian Dark Pumpkin Spice Bliss", Product_categoryId = 1, Is_employee_only = true, Description = "Dark and Seasonal Favorite", Price = 1399, StockQuantity = 26, IsOrdered = false },
                new Product { Id = "S234XYZ19", Name = "Barroc Intens Italian Deluxe Almond Joyful Brew", Product_categoryId = 1, Is_employee_only = true, Description = "Deluxe Almond and Coconut Fusion", Price = 1699, StockQuantity = 11, IsOrdered = false },
                new Product { Id = "S234XYZ20", Name = "Barroc Intens Italian Special Raspberry Mocha", Product_categoryId = 1, Is_employee_only = true, Description = "Special Berry Chocolate Bliss", Price = 1599, StockQuantity = 24, IsOrdered = true }

            );

        }

    }
}
