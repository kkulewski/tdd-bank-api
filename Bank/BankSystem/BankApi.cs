using BankSystem.Authentication;

namespace BankSystem
{
    public class BankApi
    {
        private readonly IAuthenticationService _authenticationService;

        public BankApi(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public bool IsUserSignedIn()
        {
            return _authenticationService.IsAuthenticated();
        }

        public bool SignIn(string login, string password)
        {
            return _authenticationService.Authenticate(login, password);
        }

        public bool SignOut()
        {
            return _authenticationService.Deauthenticate();
        }
    }
}
