using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntensTestlLibrary
{
    public class AuthenticationManager
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationManager(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        public bool Authenticate(string enteredUsername, string enteredPassword)
        {
            // Use the injected authentication service
            return _authenticationService.Authenticate(enteredUsername, enteredPassword);
        }
    }
}
