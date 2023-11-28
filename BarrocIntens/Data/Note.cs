using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class Note
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string AppointmentTitle { get; set; }
        public DateTimeOffset AppointmentDate { get; set; }
        //public int Author_id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public User User { get; set; }
        public Company Company { get; set; }
    }
}
