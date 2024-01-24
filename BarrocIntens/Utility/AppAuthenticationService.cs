using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarrocIntens.Data;
using BarrocIntensTestlLibrary.LoginWindow;

namespace BarrocIntens.Utility
{
    public class AppAuthenticationService : IAuthenticationService
    {
        public bool Authenticate(string username, string password)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    bool isValidPassword = SecureHasher.Verify(password, user.Password);
                    if (isValidPassword)
                    {
                        User.LoggedInUser = user;
                    }
                    return isValidPassword;
                }

                return false;
            }
        }
    }
}
