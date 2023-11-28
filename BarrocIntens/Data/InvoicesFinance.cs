using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

public class InvoiceFinance
{
    [Key]
    public int Id { get; set; }

    public int LeaseContractId { get; set; }

    public DateTime DueDate { get; set; }

    public decimal Amount { get; set; }

    public bool IsPaid { get; set; }
    public virtual LeaseContract LeaseContract { get; set; }
}

