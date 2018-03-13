using System;
using System.Collections.Generic;
using BankSystem.Models;
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
    }
}
