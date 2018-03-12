using System;
using System.Collections.Generic;
using BankSystem.Account;
using BankSystem.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankSystem.Tests
{
    [TestClass]
    public class AccountServiceTests
    {
        [TestMethod]
        public void GetBalance_ReturnsUserBalance()
        {
            decimal userBalance = 1000.0M;

            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Balance).Returns(userBalance);
            IUser user = userMock.Object;

            IAccountService accountService = new AccountService();

            Assert.AreEqual(userBalance, accountService.GetBalance(user));
        }

        [TestMethod]
        [ExpectedException(typeof(AccountOperationException))]
        public void CreateMoneyTransfer_Throws_WhenAmountInvalid()
        {
            var invalidAmount = -100.0M;
            var senderMock = new Mock<IUser>();
            var recipientMock = new Mock<IUser>();

            IAccountService accountService = new AccountService();

            Assert.IsNotNull(accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, invalidAmount));
        }

        [TestMethod]
        [ExpectedException(typeof(AccountOperationException))]
        public void CreateMoneyTransfer_Throws_WhenSenderBalanceIsLowerThanTransferAmount()
        {
            var validAmount = 100.0M;
            var senderBalance = 50.0M;

            var senderMock = new Mock<IUser>();
            senderMock.Setup(x => x.Balance).Returns(senderBalance);

            var recipientMock = new Mock<IUser>();
            IAccountService accountService = new AccountService();

            Assert.IsNotNull(accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, validAmount));
        }

        [TestMethod]
        public void CreateMoneyTransfer_CreatesMoneyTransfer_WithCorrectAmount()
        {
            var amount = 100.0M;
            var senderBalance = amount;

            var senderMock = new Mock<IUser>();
            senderMock.Setup(x => x.Balance).Returns(senderBalance);
            senderMock.Setup(x => x.PendingTransfers).Returns(new List<IMoneyTransfer>());
            var recipientMock = new Mock<IUser>();

            IAccountService accountService = new AccountService();
            IMoneyTransfer transfer = accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, amount);

            Assert.AreEqual(amount, transfer.Amount);
        }

        [TestMethod]
        public void CreateMoneyTransfer_SubtractsCorrectAmount_FromSenderBalance()
        {
            var senderInitialBalance = 200.0M;
            var transferAmount = 100.0M;

            var recipientMock = new Mock<IUser>();
            IUser sender = new FakeUser("Sender", "SenderPass", senderInitialBalance);

            IAccountService accountService = new AccountService();
            var _ = accountService.CreateMoneyTransfer(sender, recipientMock.Object, transferAmount);

            var senderExpectedBalance = senderInitialBalance - transferAmount;
            Assert.AreEqual(senderExpectedBalance, sender.Balance);
        }

        //[TestMethod]
        //public void CreateMoneyTransfer_CreatesMoneyTransfer_WithCorrectCreatedOnDate()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        var amount = 100.0M;
        //        var senderBalance = amount;
                
        //        var expectedDate = new DateTime(2018, 1, 1);
        //        System.Fakes.ShimDateTime.NowGet = () => { return expectedDate; };

        //        var senderMock = new Mock<IUser>();
        //        senderMock.Setup(x => x.Balance).Returns(senderBalance);
        //        var recipientMock = new Mock<IUser>();

        //        IAccountService accountService = new AccountService();
        //        IMoneyTransfer transfer = accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, amount);

        //        Assert.AreEqual(expectedDate.Date, transfer.CreatedOn.Date);
        //    }
        //}

        [TestMethod]
        public void ExecuteMoneyTransfer_ReturnsFalse_WhenTransferIsAlreadyCompleted()
        {
            var transferMock = new Mock<IMoneyTransfer>();
            transferMock.Setup(x => x.Completed).Returns(true);

            IAccountService accountService = new AccountService();
            Assert.IsFalse(accountService.ExecuteMoneyTransfer(transferMock.Object));
        }

        [TestMethod]
        public void ExecuteMoneyTransfer_ReturnsTrue_WhenTransferSucceeded()
        {
            var amount = 100.0M;
            var senderBalance = amount;

            var senderMock = new Mock<IUser>();
            senderMock.Setup(x => x.Balance).Returns(senderBalance);
            senderMock.Setup(x => x.PendingTransfers).Returns(new List<IMoneyTransfer>());
            var recipientMock = new Mock<IUser>();

            IAccountService accountService = new AccountService();
            IMoneyTransfer transfer = accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, amount);

            Assert.IsTrue(accountService.ExecuteMoneyTransfer(transfer));
        }

        [TestMethod]
        public void ExecuteMoneyTransfer_AddsCorrectAmount_ToRecipientBalance()
        {
            var senderInitialBalance = 100.0M;
            var recipientInitialBalance = 0.0M;
            var transferAmount = 100.0M;

            IUser sender = new FakeUser("Sender", "SenderPass", senderInitialBalance);
            IUser recipient = new FakeUser("Recipient", "RecipientPass", recipientInitialBalance);

            IAccountService accountService = new AccountService();
            IMoneyTransfer transfer = accountService.CreateMoneyTransfer(sender, recipient, transferAmount);
            accountService.ExecuteMoneyTransfer(transfer);

            var recipientExpectedBalance = recipientInitialBalance + transferAmount;
            Assert.AreEqual(recipientExpectedBalance, recipient.Balance);
        }

        [TestMethod]
        public void CreateMoneyTransfer_AddsNewTransfer_ToSenderPendingTransfers()
        {
            var senderInitialBalance = 100.0M;
            var recipientInitialBalance = 0.0M;
            var transferAmount = 100.0M;

            IUser sender = new FakeUser("Sender", "SenderPass", senderInitialBalance);
            IUser recipient = new FakeUser("Recipient", "RecipientPass", recipientInitialBalance);

            IAccountService accountService = new AccountService();
            var transferCountBeforeNewTransfer = sender.PendingTransfers.Count;
            var _ = accountService.CreateMoneyTransfer(sender, recipient, transferAmount);

            Assert.AreEqual(transferCountBeforeNewTransfer + 1, sender.PendingTransfers.Count);
        }
    }
}
