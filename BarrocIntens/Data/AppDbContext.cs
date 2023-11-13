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
                 new User { Id = 3, Name = "Nigel Pieter", UserName = "AnsjoNation", Password = "password4321", Role = "Purchase" }
            );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = "S234FREKT", Name= "Barroc Intens Italian Light", Description = "Light", Price = 499},
                new Product { Id = "S234KNDPF", Name = "Barroc Intens Italian", Description = "Italian", Price = 599 },
                 new Product { Id = "S234NNBMV", Name = "Barroc Intens Italian Deluxe", Description = "Deluxe", Price = 799 },
                  new Product { Id = "S234MMPLA", Name = "Barroc Intens Italian Deluxe Special", Description = "Special",Price = 999}
            );
        }

    }
}
