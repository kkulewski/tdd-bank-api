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

        public bool IsUserLoggedIn()
        {
            return _authenticationService.IsAuthenticated();
        }
    }
}
