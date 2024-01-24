using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class MaintenanceReceiptProduct
    {
        public int ProductId { get; set; }
        public int Maintenance_ReceiptId { get; set; }
        public int QuantityUsed { get; set; }

        public Product Product { get; set; }
    }
}
