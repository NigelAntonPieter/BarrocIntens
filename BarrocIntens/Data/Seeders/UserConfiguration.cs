using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new User { Id = 1, Name = "John Doe", UserName = "johndoe", Password = "password123", Role = "Sales" },
                new User { Id = 2, Name = "Jane Smith", UserName = "janesmith", Password = "password456", Role = "Maintenance" },
                new User { Id = 3, Name = "Nigel Pieter", UserName = "AnsjoNation", Password = "a", Role = "Purchase" },
                new User { Id = 4, Name = "Luna Smedes", UserName = "lunasmedes", Password = "n", Role = "Client" },
                new User { Id = 5, Name = "Merijn Sweep", UserName = "merijnsweep", Password = "m", Role = "Finance" },
                new User { Id = 6, Name = "Brent Albers", UserName = "balbers", Password = "n", Role = "MaintenanceAdmin" },
                new User { Id = 7, Name = "Jennet Smit", UserName = "jennetsmit", Password = "password456", Role = "Maintenance" }
            );
        }
    }
}
