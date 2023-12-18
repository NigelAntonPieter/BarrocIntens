using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class BaseAppointment
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
