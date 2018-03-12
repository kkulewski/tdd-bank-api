using System.Collections.Generic;
using BankSystem.Account;

namespace BankSystem.User
{
    public class FakeUser : IUser
    {
        public string Login { get; }
        public string Password { get; }
        public decimal Balance { get; set; }

        public List<IMoneyTransfer> PendingTransfers { get; }
        public List<IMoneyTransfer> CompletedTransfers { get; }

        public FakeUser(string login, string password, decimal balance)
        {
            Login = login;
            Password = password;
            Balance = balance;
            PendingTransfers = new List<IMoneyTransfer>();
            CompletedTransfers = new List<IMoneyTransfer>();
        }
    }
}
