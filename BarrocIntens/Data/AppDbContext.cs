using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BarrocIntens.Data.Seeders;

namespace BarrocIntens.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Maintenance_appointment> MaintenanceAppointments { get; set; }
        public DbSet<Maintenance_Receipt> MaintenanceReceipts { get; set; }
        public DbSet<Product_category> ProductCategories { get; set; }
        public DbSet<LeaseContract> LeaseContracts { get; set; }
        public DbSet<InvoiceFinance> InvoicesFinance { get; set; }

        public DbSet<UserMaintenanceAppointment> UserMaintenanceAppointments { get; set; }
        public DbSet<InstallationReceipt> InstallationReceipts { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Company> Companies { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
               ConfigurationManager.ConnectionStrings["BarrocIntens"].ConnectionString,
               ServerVersion.Parse("5.7.33-winx64"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
           .HasOne(n => n.Company)
           .WithMany(c => c.Notes)
           .HasForeignKey(n => n.CompanyId);

            modelBuilder.ApplyConfiguration(new Product_categoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new Maintenance_ReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new Maintenance_appointmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserMaintenanceAppointmentConfiguration());
        }

    }
}
