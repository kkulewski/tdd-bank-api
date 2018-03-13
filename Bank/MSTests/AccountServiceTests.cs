using System.Collections.Generic;
using BankSystem.Models;
using BankSystem.Models.Fakes;
using BankSystem.Services.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests
{
    [TestClass]
    public class AccountServiceTests
    {
        [TestMethod]
        public void CreateMoneyTransfer_AddsCorrectTransfers_ToSenderPendingTransfers()
        {
            // ARRANGE
            IUser sender = new User("sender", "sender", 100.0M);
            IUser recipient = new User("sender", "sender", 0.0M);
            IAccountService accountService = new AccountService();
            
            // ACT
            var transfer1 = accountService.CreateMoneyTransfer(sender, recipient, 50.0M);
            var transfer2 = accountService.CreateMoneyTransfer(sender, recipient, 50.0M);

            // ASSERT
            var expectedTransfers = new List<IMoneyTransfer> { transfer1, transfer2 };
            var senderTransfers = sender.PendingTransfers;
            CollectionAssert.AreEqual(expectedTransfers, senderTransfers);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountOperationException))]
        public void CreateMoneyTransfer_Throws_WhenAmountInvalid()
        {
            // ARRANGE
            decimal invalidAmount = -100.0M;
            IUser senderStub = new StubIUser();
            IUser recipientStub = new StubIUser();
            IAccountService accountService = new AccountService();

            // ACT
            var transfer = accountService.CreateMoneyTransfer(senderStub, recipientStub, invalidAmount);

            // ASSERT
            Assert.IsNotNull(transfer);
        }
    }
}
