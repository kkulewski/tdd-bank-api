using BankSystem.Model;

namespace BankSystem.Storage
{
    public interface IUserStore
    {
        IUser GetUserByLogin(string login);
    }
}
