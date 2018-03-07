using BankSystem.User;

namespace BankSystem.Authentication
{
    public interface IAuthenticationService
    {
        IUser SignedUser { get; }

        bool IsAuthenticated();

        bool Authenticate(string login, string password);

        bool Deauthenticate();
    }
}
