using BankSystem.User;

namespace BankSystem.Account
{
    public interface IAccountService
    {
        decimal GetBalance(IUser user);

        IMoneyTransfer CreateMoneyTransfer(IUser sender, IUser recipient, decimal amount);

        bool ExecuteMoneyTransfer(IMoneyTransfer transfer);
    }
}
