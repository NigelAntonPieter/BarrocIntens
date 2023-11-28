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
        public DbSet<Product> Products { get; set; }
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

            modelBuilder.Entity<User>()
               .HasMany(u => u.Companys)
               .WithMany(c => c.Users)
               .UsingEntity<Note>();

            modelBuilder.Entity<Note>()
               .HasOne(n => n.Company)
               .WithMany(c => c.Notes)
               .HasForeignKey(n => n.CompanyId);

        modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "John Doe", UserName = "johndoe", Password = "password123", Role = "Sales" },
                new User { Id = 2, Name = "Jane Smith", UserName = "janesmith", Password = "password456", Role = "Maintenance"},
                 new User { Id = 3, Name = "Nigel Pieter", UserName = "AnsjoNation", Password = "password4321", Role = "client"}
            );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1, Name= "Balonga", Description = "kapot lekker"}
            );
        }

    }
}
