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
               new Product { Id = 1, Name= "Balonga", Description = "kapot lekker", Price = 5},
                new Product { Id = 2, Name = "spaans", Description = "niet lekker", Price = 50 },
                 new Product { Id = 3, Name = "portaal", Description = "zwaar", Price = 2 },
                  new Product { Id = 4, Name = "kortaa", Description = "wisa",Price = 5 },
                   new Product { Id = 5, Name = "hans", Description = "lang" , Price = 5 },
                    new Product { Id = 6, Name = "portyu", Description = "kapot lekker" , Price = 20 }
            );
        }

    }
}
