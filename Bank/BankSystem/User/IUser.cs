using System.Collections.Generic;
using BankSystem.Account;

namespace BankSystem.User
{
    public interface IUser
    {
        string Login { get; }
        string Password { get; }

        decimal Balance { get; set; }

        List<IMoneyTransfer> PendingTransfers { get; }
        List<IMoneyTransfer> CompletedTransfers { get; }
    }
}
