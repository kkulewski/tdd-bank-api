using System.Collections.Generic;

namespace BankSystem.Model
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
