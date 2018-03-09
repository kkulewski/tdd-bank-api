﻿using System;
using BankSystem.User;

namespace BankSystem.Account
{
    public class MoneyTransfer : IMoneyTransfer
    {
        public IUser Sender { get; }
        public IUser Recipient { get; }
        public decimal Amount { get; }
        public bool Completed { get; private set; }
        public DateTime CreatedOn { get; }
        public DateTime CompletedOn { get; private set; }

        public MoneyTransfer(IUser sender, IUser recipient, decimal amount)
        {
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
            Completed = false;
            CreatedOn = DateTime.Now;
        }
    }
}
