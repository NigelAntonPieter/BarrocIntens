using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data.Seeders
{
    public class Maintenance_appointmentConfiguration : IEntityTypeConfiguration<Maintenance_appointment>
    {
        public void Configure(EntityTypeBuilder<Maintenance_appointment> builder)
        {
            builder.HasOne(ma => ma.Maintenance_Receipt)
                .WithOne(mr => mr.Maintenance_appointment)
                .HasForeignKey<Maintenance_Receipt>(mr => mr.Maintenance_appointmentId)
                .IsRequired(false);
            builder.HasData(
                new Maintenance_appointment { Id = 1, CompanyId = 1, Remark = "Updated remark 1", DateAdded = DateTime.Now, DateOfMaintenanceAppointment = new DateOnly(2023, 12, 5), IsFinished = true, Maintenance_ReceiptId = null, Location = "Breda" },
                new Maintenance_appointment { Id = 2, CompanyId = 2, Remark = "Updated remark 2", DateAdded = DateTime.Now, DateOfMaintenanceAppointment = new DateOnly(2023, 12, 25), IsFinished = false, Maintenance_ReceiptId = null, Location = "Eindhoven" },
                new Maintenance_appointment { Id = 3, CompanyId = 3, Remark = "Updated remark 3", DateAdded = DateTime.Now, DateOfMaintenanceAppointment = new DateOnly(2023, 12, 19), IsFinished = false, Maintenance_ReceiptId = null, Location = "Rotterdam" },
                 new Maintenance_appointment { Id = 4, CompanyId = 4, Remark = "Updated remark 4", DateAdded = DateTime.Now, DateOfMaintenanceAppointment = new DateOnly(2023, 12, 21), IsFinished = false, Maintenance_ReceiptId = null, Location = "Tilburg" }
            );

        }
    }
}
