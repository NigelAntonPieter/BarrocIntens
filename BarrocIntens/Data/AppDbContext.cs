using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BarrocIntens.Date
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LeaseContract> LeaseContracts { get; set; }
        public DbSet<InvoiceFinance> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                ConfigurationManager.ConnectionStrings["BarrocIntens"].ConnectionString,
                ServerVersion.Parse("5.7.33-winx64"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "John Doe", UserName = "johndoe", Password = "password123", Role = "Sales" },
                new User { Id = 2, Name = "Jane Smith", UserName = "janesmith", Password = "password456", Role = "Maintenance"}
            );
            modelBuilder.Entity<LeaseContract>().HasData(
          new LeaseContract { Id = 1, CustomerName = "Customer A", BkrCheckPassed = true, MonthlyInvoice = true },
          new LeaseContract { Id = 2, CustomerName = "Customer B", BkrCheckPassed = false, MonthlyInvoice = false }
            );

            modelBuilder.Entity<InvoiceFinance>().HasData(
                new InvoiceFinance { Id = 1, LeaseContractId = 1, DueDate = DateTime.Now.AddDays(30), Amount = 100.00m, IsPaid = false },
                new InvoiceFinance { Id = 2, LeaseContractId = 2, DueDate = DateTime.Now.AddDays(45), Amount = 150.00m, IsPaid = true }
            );

        }


    }
}
