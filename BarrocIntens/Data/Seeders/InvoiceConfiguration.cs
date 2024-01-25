using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data.Seeders
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<InvoicesFinance>
    {
        public void Configure(EntityTypeBuilder<InvoicesFinance> builder)
        {
            builder.HasData(
                new InvoicesFinance
                {
                    Id = 9,
                    MonthlyInvoice = true,
                    PeriodicInvoice = false,
                    DateCreated = DateTime.Now,
                    Amount = 15.00m,
                    IsPaid = false,
                    CustomerName = "colin",
                },

                    new InvoicesFinance
                    {
                        Id = 8,
                        MonthlyInvoice = false,
                        PeriodicInvoice = true,
                        DateCreated = DateTime.Now,
                        Amount = 15.00m,
                        IsPaid = false,
                        CustomerName = "colin",
                    },
                    new InvoicesFinance
                    {
                        Id = 10,
                        MonthlyInvoice = false,
                        PeriodicInvoice = true,
                        DateCreated = DateTime.Now,
                        Amount = 15.00m,
                        IsPaid = false,
                        CustomerName = "colin",
                    },
                    new InvoicesFinance
                    {
                        Id = 7,
                        MonthlyInvoice = false,
                        PeriodicInvoice = true,
                        DateCreated = DateTime.Now,
                        Amount = 15.00m,
                        IsPaid = false,
                        CustomerName = "colin",
                    }
            );
        }
    }
}

