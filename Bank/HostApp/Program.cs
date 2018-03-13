using System;
using BankSystem;
using BankSystem.DAL;
using BankSystem.Models;
using BankSystem.Services.Account;
using BankSystem.Services.Authentication;

namespace HostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bank = GetApi();
        }

        private static BankApi GetApi()
        {
            var userFactory = new UserFactory();
            var userStore = new InMemoryUserStore();
            var authService = new AuthenticationService(userStore, userFactory);
            var accountService = new AccountService();
            return new BankApi(authService, accountService, userStore);
        }
    }
}
