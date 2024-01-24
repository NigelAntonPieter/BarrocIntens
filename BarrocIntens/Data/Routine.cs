using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Routine: BaseAppointment
    {
        public int Id { get; set; }
       
        public string Location { get; set; }
        public DateTime DateAdded { get; set; }

        public DateOnly DateOfRoutineAppointment { get; set; }

        public ICollection<UserRoutineAppointment> UserRoutineAppointments { get; set; }
        public string GetUserName => UserRoutineAppointments.FirstOrDefault()?.User?.UserName ?? "Geen medewerker";
    }

    public class UserRoutineAppointment
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoutineId { get; set; }
        public Routine Routine { get; set; }
    }
        
}
