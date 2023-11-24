using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Maintenance_appointment
    {
        public int Id {  get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Column(TypeName = "longtext")]public string Remark { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
