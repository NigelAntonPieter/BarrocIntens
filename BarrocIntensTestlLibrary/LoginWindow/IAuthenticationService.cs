using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntensTestlLibrary.LoginWindow
{

    public interface IAuthenticationService
    {
        bool Authenticate(string username, string password);
    }
}
