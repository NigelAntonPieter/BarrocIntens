using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Date
{
    internal class Note
    {
        public int NotesId { get; set; }
        public string Comments { get; set; }
        public string Appointments { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
