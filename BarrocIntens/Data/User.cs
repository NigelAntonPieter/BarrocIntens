using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class User
    {

        public static User LoggedInUser {  get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string SessionToken { get; set; }
          public ICollection<UserMaintenanceAppointment> UserMaintenanceAppointments { get; set; }
        public ICollection<UserRoutineAppointment> UserRoutineAppointments { get; set; }
        public ICollection<Company> Companys { get; set; }



    }
}   
