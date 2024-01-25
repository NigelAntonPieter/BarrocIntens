using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.Design;
using System.Reflection.PortableExecutable;

namespace BarrocIntens.Data.Seeders
{
    public class LeaseContractConfiguration : IEntityTypeConfiguration<LeaseContract>
    {
        public void Configure(EntityTypeBuilder<LeaseContract> builder)
        {
            builder
                .HasOne(u => u.Company)
                .WithMany(c => c.LeaseContracts)
                .HasForeignKey(u => u.CompanyId);

            builder.HasData(
                new LeaseContract
                {
                    Id = 1,
                    CustomerName = "Customer1",
                    BkrCheckPassed = true,
                    MonthlyInvoice = true,
                    PeriodicInvoice = false,
                    DateCreated = DateTime.Now,
                    Amount = 15.00m,
                    IsPaid = false,
                    MachineId = 1,
                    CompanyId = 1,
                    SignedContract = true
                },
                new LeaseContract{
                    Id = 2,
                    CustomerName = "Customer1",
                    BkrCheckPassed = true,
                    MonthlyInvoice = true,
                    PeriodicInvoice = false,
                    DateCreated = DateTime.Now,
                    Amount = 20.00m,
                    IsPaid = true,
                    MachineId = 1,
                    CompanyId = 1,
                    SignedContract = true

                //},
                //new LeaseContract
                //{
                //    Id = 2,
                //    CustomerName = "Customer2",
                //    BkrCheckPassed = false,
                //    MonthlyInvoice = false,
                //    PeriodicInvoice = true,
                //    DateCreated = DateTime.Now,
                //    Amount = 20.00m,
                //    IsPaid = true,
                //    MachineId = 2,
                //    SignedContract = false
                }
                // Add more LeaseContract seeding data as needed
            );
        }
    }
}
