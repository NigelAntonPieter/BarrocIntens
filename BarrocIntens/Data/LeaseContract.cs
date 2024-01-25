using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace BarrocIntens.Data
{
    public class LeaseContract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public bool BkrCheckPassed { get; set; }

        public bool MonthlyInvoice { get; set; }
        public bool PeriodicInvoice { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public int MachineId { get; set; }
        [ForeignKey("MachineId")]

        public bool SignedContract { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual ICollection<InvoicesFinance> Invoices { get; set; }
    }

}
