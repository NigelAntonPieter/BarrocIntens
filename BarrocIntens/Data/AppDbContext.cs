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
        public DbSet<UserMaintenanceAppointment> UserMaintenanceAppointments { get; set; }
        public DbSet<InstallationReceipt> InstallationReceipts { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<UserRoutineAppointment> UserRoutineAppoinments { get; set; }

        public User GetUserBySessionToken(string sessionToken)
        {
            return Users.FirstOrDefault(u => u.SessionToken == sessionToken);
            // Hier wordt ervan uitgegaan dat 'SessionToken' een eigenschap van de 'User'-klasse is waarin het sessietoken wordt opgeslagen.
            // Pas deze code aan op basis van hoe je het sessietoken in je gebruikerstableau hebt opgeslagen.
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseMySql(
               ConfigurationManager.ConnectionStrings["BarrocIntens"].ConnectionString,
               ServerVersion.Parse("5.7.33-winx64"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maintenance_Receipt>()
                .HasMany(maintenance_receipt => maintenance_receipt.Products)
                .WithMany(product => product.Maintenance_Receipts)
                .UsingEntity<MaintenanceReceiptProduct>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Product_categoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new Maintenance_ReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new Maintenance_appointmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserMaintenanceAppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new RoutineConfiguration());
            modelBuilder.ApplyConfiguration(new RoutineAppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new LeaseContractConfiguration());
        }

    }
}
