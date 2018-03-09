using System;
using BankSystem.User;

namespace BankSystem.Account
{
    public class MoneyTransfer : IMoneyTransfer
    {
        public IUser Sender { get; }
        public IUser Recipient { get; }
        public decimal Amount { get; }
        public bool Complete { get; private set; }
        public DateTime CreatedOn { get; }
        public DateTime CommitedOn { get; private set; }

        public MoneyTransfer(IUser sender, IUser recipient, decimal amount)
        {
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
            Complete = false;
            CreatedOn = DateTime.Now;
        }
    }
}
