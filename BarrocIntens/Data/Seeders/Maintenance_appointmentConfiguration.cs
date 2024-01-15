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
            builder.HasData(
                new Maintenance_appointment { Id = 1, CompanyId = 1, Remark = "Updated remark 1", DateAdded = DateTime.Now, DateOfMaintenanceAppointment = null, IsFinished = false, Maintenance_ReceiptId = 1, Location = "Breda" },
                new Maintenance_appointment { Id = 2, CompanyId = 2, Remark = "Updated remark 2", DateAdded = DateTime.Now, DateOfMaintenanceAppointment = null, IsFinished = false, Maintenance_ReceiptId = 1, Location = "Eindhoven" },
                new Maintenance_appointment { Id = 3, CompanyId = 3, Remark = "Updated remark 3", DateAdded = DateTime.Now, DateOfMaintenanceAppointment = null, IsFinished = false, Maintenance_ReceiptId = 1, Location = "Rotterdam" },
                 new Maintenance_appointment { Id = 4, CompanyId = 4, Remark = "Updated remark 4", DateAdded = DateTime.Now, DateOfMaintenanceAppointment = new DateOnly(2023, 12, 21), IsFinished = false, Maintenance_ReceiptId = 1, Location = "Tilburg" }
                
            );

            builder.HasOne(m => m.Company)
           .WithMany()
           .HasForeignKey(m => m.CompanyId);
        }
    }
}   
