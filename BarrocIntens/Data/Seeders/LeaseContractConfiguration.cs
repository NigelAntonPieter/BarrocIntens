using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BarrocIntens.Data.Seeders
{
    public class LeaseContractConfiguration : IEntityTypeConfiguration<LeaseContract>
    {
        public void Configure(EntityTypeBuilder<LeaseContract> builder)
        {
            builder.HasData(
                new LeaseContract
                {
                    Id = 1,
                    CustomerName = "colin",
                    BkrCheckPassed = true,
                    MonthlyInvoice = true,
                    PeriodicInvoice = false,
                    DateCreated = DateTime.Now,
                    Amount = 15.00m,
                    IsPaid = false,
                    MachineId = 1,
                    SignedContract = true

                },
                    new LeaseContract
                    {
                        Id = 2,
                        CustomerName = "colin",
                        BkrCheckPassed = false,
                        MonthlyInvoice = false,
                        PeriodicInvoice = true,
                        DateCreated = DateTime.Now,
                        Amount = 20.00m,
                        IsPaid = true,
                        MachineId = 2,
                        SignedContract = false
                    }
                // Add more LeaseContract seeding data as needed
            );
        }
    }
}
