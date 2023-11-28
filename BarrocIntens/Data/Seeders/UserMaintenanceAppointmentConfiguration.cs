using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data.Seeders
{
    internal class UserMaintenanceAppointmentConfiguration : IEntityTypeConfiguration<UserMaintenanceAppointment>
    {
        public void Configure(EntityTypeBuilder<UserMaintenanceAppointment> builder)
        {
            builder.HasKey(ua => new { ua.UserId, ua.MaintenanceAppointmentId });

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserMaintenanceAppointments)
                .HasForeignKey(ua => ua.UserId);

            builder.HasOne(ua => ua.MaintenanceAppointment)
                .WithMany(m => m.UserMaintenanceAppointments)
                .HasForeignKey(ua => ua.MaintenanceAppointmentId);

            builder.HasData(
                new UserMaintenanceAppointment { UserId = 2, MaintenanceAppointmentId = 1 },
                new UserMaintenanceAppointment { UserId = 7, MaintenanceAppointmentId = 1 }
            );

        }
    }
}
