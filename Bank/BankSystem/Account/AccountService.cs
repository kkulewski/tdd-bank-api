using System;
using BankSystem.User;

namespace BankSystem.Account
{
    public class AccountService : IAccountService
    {
        public decimal GetBalance(IUser user)
        {
            return user.Balance;
        }

        public IMoneyTransfer CreateMoneyTransfer(IUser sender, IUser recipient, decimal amount)
        {
            if (amount <= 0.0M)
            {
                throw new AccountOperationException("Invalid amount.");
            }

            throw new NotImplementedException();
        }
    }
}
