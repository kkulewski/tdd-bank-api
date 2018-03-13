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
            Console.WriteLine("## CREATING ACCOUNTS");
            Console.WriteLine(bank.SignUp("JohnDoe", "secret"));
            Console.WriteLine(bank.SignUp("JohnDoe", "other"));
            Console.WriteLine(bank.SignUp("KateDoe", "pass"));
            Console.WriteLine(bank.SignUp("DaveSmith", "dave"));

            Console.WriteLine("## SIGNING IN");
            Console.WriteLine(bank.SignIn("wrong", "wrong"));
            Console.WriteLine(bank.SignIn("JohnDoe", "wrong"));
            Console.WriteLine(bank.SignIn("JohnDoe", "secret"));
            
            Console.WriteLine("## CHECKING BALANCE");
            Console.WriteLine(bank.GetMyAccountBalance());

            Console.WriteLine("## SENDING TRANSFER");
            Console.WriteLine(bank.SendMoneyTransfer("KateDoe", 1200.0M));
            Console.WriteLine(bank.SendMoneyTransfer("wrong", 200.0M));
            Console.WriteLine(bank.SendMoneyTransfer("KateDoe", -200.0M));
            Console.WriteLine(bank.SendMoneyTransfer("KateDoe", 200.0M));

            Console.WriteLine("## CHECKING BALANCE");
            Console.WriteLine(bank.GetMyAccountBalance());

            Console.WriteLine("## SIGNING OUT");
            Console.WriteLine(bank.SignOut());
            Console.WriteLine(bank.SignOut());
            
            Console.WriteLine("## SENDING TRANSFER");
            Console.WriteLine(bank.SendMoneyTransfer("KateDoe", 1200.0M));
            Console.WriteLine(bank.SendMoneyTransfer("wrong", 200.0M));
            Console.WriteLine(bank.SendMoneyTransfer("KateDoe", -200.0M));
            Console.WriteLine(bank.SendMoneyTransfer("KateDoe", 200.0M));

            Console.ReadKey();
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
