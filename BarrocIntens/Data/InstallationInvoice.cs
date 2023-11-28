using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class InstallationInvoice
    {
        [Key]
        public int Id { get; set; }

        public int LeaseContractId { get; set; }

        public DateTime DueDate { get; set; }

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }
        public virtual LeaseContract LeaseContract { get; set; }
    }
}
