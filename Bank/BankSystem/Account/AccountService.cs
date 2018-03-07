using BankSystem.User;

namespace BankSystem.Account
{
    public class AccountService : IAccountService
    {
        public decimal GetBalance(IUser user)
        {
            return user.Balance;
        }
    }
}
