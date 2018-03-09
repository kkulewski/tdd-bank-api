using System;

namespace BankSystem.Account
{
    public class AccountOperationException : Exception
    {
        public AccountOperationException(string message) : base(message)
        {
        }
    }
}
