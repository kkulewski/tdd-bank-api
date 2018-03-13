namespace BankSystem.Models
{
    public class UserFactory : IUserFactory
    {
        public IUser Create(string login, string password)
        {
            return new User(login, password, 1000.0M);
        }
    }
}
