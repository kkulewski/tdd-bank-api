using BankSystem.User;

namespace BankSystem.Account
{
    public interface IAccountService
    {
        decimal GetBalance(IUser user);
    }
}
