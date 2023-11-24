using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarrocIntens.Data;
using BarrocIntensTestlLibrary;

namespace BarrocIntens
{
    public class AppAuthenticationService : IAuthenticationService
    {
        public bool Authenticate(string username, string password)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);
                return user != null;
            }
        }
    }
}
