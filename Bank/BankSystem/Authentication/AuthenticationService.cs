using System;
using BankSystem.User;

namespace BankSystem.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUserStore _userStore;

        public IUser SignedUser { get; private set; }

        public AuthenticationService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }

        public bool Authenticate(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
