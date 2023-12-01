using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data.Seeders
{
    public class InstallationReceiptConfiguration : IEntityTypeConfiguration<InstallationReceipt>
    {
        public void Configure(EntityTypeBuilder<InstallationReceipt> builder)
        {
            builder.HasData(
                 new InstallationReceipt { Id = 1, EmployeeName = "John", ProductId = "S234XYZ03", InstallationDate = DateTime.Now, ConnectionCosts = 500.00m },
                 new InstallationReceipt { Id = 2, EmployeeName = "Jane", ProductId = "S234NNBMV", InstallationDate = DateTime.Now, ConnectionCosts = 700.00m }
            );
        }
    }
}
