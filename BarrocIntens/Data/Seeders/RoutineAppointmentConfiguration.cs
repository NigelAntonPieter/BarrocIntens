using BarrocIntens.Data;
using BarrocIntens.Maintenance.Planner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data.Seeders
{
    internal class RoutineAppointmentConfiguration : IEntityTypeConfiguration<UserRoutineAppointment>
    {
        public void Configure(EntityTypeBuilder<UserRoutineAppointment> builder)
        {
            
            builder.HasKey(ua => new { ua.UserId, ua.RoutineId });

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserRoutineAppointments)
                .HasForeignKey(ua => ua.UserId);

            builder.HasOne(ua => ua.Routine)
                .WithMany(m => m.UserRoutineAppointments)
                .HasForeignKey(ua => ua.RoutineId);

            builder.HasData(
                new UserRoutineAppointment { UserId = 2, RoutineId = 1 },
                new UserRoutineAppointment { UserId = 7, RoutineId = 1 }
            );
           
        }
    }
    internal class RoutineConfiguration : IEntityTypeConfiguration<Routine>
    {
        public void Configure(EntityTypeBuilder<Routine> builder)
        {
            builder.HasData(
               new Routine { Id = 1, CompanyId = 1, Location = "Breda", DateAdded = DateTime.Now, IsFinished = false, DateOfRoutineAppointment = new DateOnly(2023, 12, 5) }
           );

        }
    }
}

