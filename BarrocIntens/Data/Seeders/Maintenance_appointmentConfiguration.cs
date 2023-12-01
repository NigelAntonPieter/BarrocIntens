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
                new Maintenance_appointment { Id = 1, CompanyId = 1, Remark = "Some seeded remark 1", DateAdded = DateTime.Now },
                new Maintenance_appointment { Id = 2, CompanyId = 2, Remark = "Some seeded remark 2", DateAdded = DateTime.Now }
            );
        }
    }
}
