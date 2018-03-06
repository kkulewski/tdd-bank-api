namespace BankSystem.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated();

        bool Authenticate(string login, string password);
    }
}
