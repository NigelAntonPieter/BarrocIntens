using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data.Seeders
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company { Id = 1, Name = "Company1", Phone = "123-456-7890", Street = "123 Main Street" },
                new Company { Id = 2, Name = "Company2", Phone = "987-654-3210", Street = "456 Oak Avenue" },
                new Company { Id = 3, Name = "Company3", Phone = "555-123-4567", Street = "789 Pine Road" }
            );
        }
    }
}
