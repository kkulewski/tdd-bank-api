using System;
using BankSystem.User;

namespace BankSystem.Account
{
    public interface IMoneyTransfer
    {
        IUser Sender { get; }
        IUser Recipient { get; }
        decimal Amount { get; }
        bool Complete { get; }
        DateTime CreatedOn { get; }
        DateTime CommitedOn { get; }
    }
}
