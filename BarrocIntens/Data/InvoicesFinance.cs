using System;
using System.ComponentModel.DataAnnotations;

public class InvoiceFinance
{
    [Key]
    public int Id { get; set; }

    public int LeaseContractId { get; set; }
    public bool MonthlyInvoice { get; set; }
    public bool PeriodicInvoice { get; set; }
    public DateTime DateCreated { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }


    public virtual LeaseContract LeaseContract { get; set; }
}
