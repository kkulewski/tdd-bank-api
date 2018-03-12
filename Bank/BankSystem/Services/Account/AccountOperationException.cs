using System;

namespace BankSystem.Services.Account
{
    public class AccountOperationException : Exception
    {
        public AccountOperationException(string message) : base(message)
        {
        }
    }
}
