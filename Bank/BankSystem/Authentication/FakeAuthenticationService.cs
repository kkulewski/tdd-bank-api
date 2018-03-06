namespace BankSystem.Authentication
{
    public class FakeAuthenticationService : IAuthenticationService
    {
        public bool IsAuthenticated()
        {
            return false;
        }
    }
}
