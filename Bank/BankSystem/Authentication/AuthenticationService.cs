using System;
using BankSystem.User;

namespace BankSystem.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserStore _userStore;

        public IUser SignedUser { get; private set; }

        public AuthenticationService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public bool IsAuthenticated()
        {
            return SignedUser != null;
        }

        public bool Authenticate(string login, string password)
        {
            var user = _userStore.GetUserByLogin(login);
            if (user.Password != password)
            {
                return false;
            }

            SignedUser = user;
            return true;
        }
    }
}
