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
        private IAccountService _accountService;
        private decimal _defaultTransferAmount;
        private IUser _senderDouble;
        private IUser _recipientDouble;

        [TestInitialize]
        public void Initialize()
        {
            _accountService = new AccountService();

            _defaultTransferAmount = 100.0M;
            var senderInitialBalance = 100.0M;
            var recipientInitialBalance = 0.0M;

            _senderDouble = new FakeUser("Sender", "SenderPass", senderInitialBalance);
            _recipientDouble = new FakeUser("Recipient", "RecipientPass", recipientInitialBalance);
        }

        [TestMethod]
        public void GetBalance_ReturnsUserBalance()
        {
            // ARRANGE
            decimal expectedBalance = 1000.0M;
            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Balance).Returns(expectedBalance);
            IUser user = userMock.Object;

            // ACT
            decimal balance = _accountService.GetBalance(user);

            // ASSERT
            Assert.AreEqual(expectedBalance, balance);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountOperationException))]
        public void CreateMoneyTransfer_Throws_WhenAmountInvalid()
        {
            // ARRANGE
            var invalidAmount = -100.0M;
            var senderMock = new Mock<IUser>();
            var recipientMock = new Mock<IUser>();

            // ACT
            var transfer = _accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, invalidAmount);

            // ASSERT
            Assert.IsNotNull(transfer);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountOperationException))]
        public void CreateMoneyTransfer_Throws_WhenSenderBalanceIsLowerThanTransferAmount()
        {
            // ARRANGE
            var validAmount = 100.0M;
            var senderBalance = 50.0M;
            var senderMock = new Mock<IUser>();
            senderMock.Setup(x => x.Balance).Returns(senderBalance);
            var recipientMock = new Mock<IUser>();

            // ACT
            var transfer = _accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, validAmount);

            // ASSERT
            Assert.IsNotNull(transfer);
        }

        [TestMethod]
        public void CreateMoneyTransfer_CreatesMoneyTransfer_WithCorrectAmount()
        {
            // ARRANGE
            var amount = 100.0M;
            var senderBalance = amount;
            var senderMock = new Mock<IUser>();
            senderMock.Setup(x => x.Balance).Returns(senderBalance);
            senderMock.Setup(x => x.PendingTransfers).Returns(new List<IMoneyTransfer>());
            var recipientMock = new Mock<IUser>();

            // ACT
            IMoneyTransfer transfer = _accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, amount);

            // ASSERT
            Assert.AreEqual(amount, transfer.Amount);
        }

        [TestMethod]
        public void CreateMoneyTransfer_SubtractsCorrectAmount_FromSenderBalance()
        {
            // ARRANGE
            var senderInitialBalance = _senderDouble.Balance;
            
            // ACT
            _accountService.CreateMoneyTransfer(_senderDouble, _recipientDouble, _defaultTransferAmount);

            // ASSERT
            Assert.AreEqual(senderInitialBalance - _defaultTransferAmount, _senderDouble.Balance);
        }

        [TestMethod]
        public void ExecuteMoneyTransfer_ReturnsFalse_WhenTransferIsAlreadyCompleted()
        {
            // ARRANGE
            var transferMock = new Mock<IMoneyTransfer>();
            transferMock.Setup(x => x.Completed).Returns(true);
            
            // ACT
            var result = _accountService.ExecuteMoneyTransfer(transferMock.Object);

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExecuteMoneyTransfer_ReturnsTrue_WhenTransferSucceeded()
        {
            // ARRANGE
            var amount = 100.0M;
            var senderBalance = amount;
            var senderMock = new Mock<IUser>();
            senderMock.Setup(x => x.Balance).Returns(senderBalance);
            senderMock.Setup(x => x.PendingTransfers).Returns(new List<IMoneyTransfer>());
            var recipientMock = new Mock<IUser>();
            IMoneyTransfer transfer = _accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, amount);

            // ACT
            var result = _accountService.ExecuteMoneyTransfer(transfer);

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExecuteMoneyTransfer_AddsCorrectAmount_ToRecipientBalance()
        {
            // ARRANGE
            var recipientInitialBalance = _recipientDouble.Balance;
            IMoneyTransfer transfer = _accountService.CreateMoneyTransfer(_senderDouble, _recipientDouble, _defaultTransferAmount);

            // ACT
            _accountService.ExecuteMoneyTransfer(transfer);

            // ASSERT
            Assert.AreEqual(recipientInitialBalance + _defaultTransferAmount, _recipientDouble.Balance);
        }

        [TestMethod]
        public void CreateMoneyTransfer_AddsNewTransfer_ToSenderPendingTransfers()
        {
            // ACT
            var transferCountBeforeNewTransfer = _senderDouble.PendingTransfers.Count;
            var _ = _accountService.CreateMoneyTransfer(_senderDouble, _recipientDouble, _defaultTransferAmount);

            // ASSERT
            Assert.AreEqual(transferCountBeforeNewTransfer + 1, _senderDouble.PendingTransfers.Count);
        }

        [TestMethod]
        public void CreateMoneyTransfer_AddsTransferWithCorrectSender_ToSenderPendingTransfers()
        {   
            // ACT
            IMoneyTransfer transfer = _accountService.CreateMoneyTransfer(_senderDouble, _recipientDouble, _defaultTransferAmount);

            // ASSERT
            Assert.AreEqual(_senderDouble, transfer.Sender);
        }

        [TestMethod]
        public void CreateMoneyTransfer_AddsTransferWithCorrectRecipient_ToSenderPendingTransfers()
        {   
            // ACT
            IMoneyTransfer transfer = _accountService.CreateMoneyTransfer(_senderDouble, _recipientDouble, _defaultTransferAmount);

            // ASSERT
            Assert.AreEqual(_recipientDouble, transfer.Recipient);
        }

        [TestMethod]
        public void CreateMoneyTransfer_AddsTransferWithCorrectAmount_ToSenderPendingTransfers()
        {
            // ACT
            IMoneyTransfer transfer = _accountService.CreateMoneyTransfer(_senderDouble, _recipientDouble, _defaultTransferAmount);

            // ASSERT
            Assert.AreEqual(_defaultTransferAmount, transfer.Amount);
        }
    }
}
