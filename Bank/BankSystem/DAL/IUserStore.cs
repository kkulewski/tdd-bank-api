using BankSystem.Models;

namespace BankSystem.DAL
{
    public interface IUserStore
    {
        IUser GetUserByLogin(string login);
    }
}
