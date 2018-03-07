using System;
using BankSystem.Account;
using BankSystem.Authentication;

namespace BankSystem
{
    public class BankApi
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountService _accountService;

        public BankApi(IAuthenticationService authenticationService, IAccountService accountService)
        {
            _authenticationService = authenticationService;
            _accountService = accountService;
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

        public decimal GetMyAccountBalance()
        {
            if (!IsUserSignedIn())
            {
                throw new Exception("You are not signed in.");
            }

            var user = _authenticationService.SignedUser;
            return _accountService.GetBalance(user);
        }
    }
}
