using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

public class LeaseContract
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CustomerName { get; set; }

    public bool BkrCheckPassed { get; set; }

    public bool MonthlyInvoice { get; set; }

    public int MachineId { get; set; }

    [ForeignKey("MachineId")]
    public virtual Machine Machine { get; set; }

    public virtual ICollection<InvoiceFinance> Invoices { get; set; }
}
