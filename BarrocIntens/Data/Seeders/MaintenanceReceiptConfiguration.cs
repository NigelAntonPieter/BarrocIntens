using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data.Seeders
{
    public class Maintenance_ReceiptConfiguration : IEntityTypeConfiguration<Maintenance_Receipt>
    {
        public void Configure(EntityTypeBuilder<Maintenance_Receipt> builder)
        {
              builder.HasData(
                new Maintenance_Receipt
                {
                    Id = 1,
                    EmployeeId = 123,
                    CompanyId = 1 ,
                    WorkDescription = "Work Description 1",
                    LaborHours = 5.5m,
                    Maintenance_appointmentId = 1
                });

        }
    }
}
