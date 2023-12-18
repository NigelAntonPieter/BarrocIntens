using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarrocIntens.Utility;

namespace BarrocIntens.Data.Seeders
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Companys)
          .WithMany(c => c.Users).UsingEntity<Note>();

            builder.HasData(
                new User { Id = 1, Name = "John Doe", UserName = "johndoe", Password = SecureHasher.Hash("password123"), Role = "Sales" },
                new User { Id = 2, Name = "Jane Smith", UserName = "janesmith", Password = SecureHasher.Hash("password456"), Role = "Maintenance" },
                new User { Id = 3, Name = "Nigel Pieter", UserName = "AnsjoNation", Password = SecureHasher.Hash("a"), Role = "Purchase" },
                new User { Id = 4, Name = "Luna Smedes", UserName = "lunasmedes", Password = SecureHasher.Hash("n"), Role = "Client" },
                new User { Id = 5, Name = "Merijn Sweep", UserName = "merijnsweep", Password = SecureHasher.Hash("m"), Role = "Finance" },
                new User { Id = 6, Name = "Brent Albers", UserName = "balbers", Password = SecureHasher.Hash("n"), Role = "MaintenanceAdmin" },
                new User { Id = 7, Name = "Jennet Smit", UserName = "jennetsmit", Password = SecureHasher.Hash("password456"), Role = "Maintenance" },
                new User { Id = 8, Name = "Britt Lips", UserName = "brittlips", Password = SecureHasher.Hash("kl"), Role = "Planner" },
                new User { Id = 9, Name = "Liever Niet", UserName = "lieverniet", Password = SecureHasher.Hash("password654"), Role = "Maintenance" },
                new User { Id = 10, Name = "Sam Smit", UserName = "samsmit", Password = SecureHasher.Hash("password789"), Role = "Maintenance" },
                new User { Id = 11, Name = "Bart Smit", UserName = "bartsmit", Password = SecureHasher.Hash("password987"), Role = "Maintenance" }
            );
        }
    }
}
