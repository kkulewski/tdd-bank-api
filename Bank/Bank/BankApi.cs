using System;

namespace Bank
{
    public class BankApi
    {
        private IBankUser loggedUser;

        public bool IsUserLoggedIn()
        {
            return loggedUser != null;
        }
    }
}
