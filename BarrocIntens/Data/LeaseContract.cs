    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

public class LeaseContract
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CustomerName { get; set; }

    public bool BkrCheckPassed { get; set; }

    public bool MonthlyInvoice { get; set; }

    public virtual ICollection<InvoiceFinance> Invoices { get; set; }
}
