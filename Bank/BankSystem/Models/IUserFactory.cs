namespace BankSystem.Models
{
    public interface IUserFactory
    {
        IUser Create(string login, string password);
    }
}
