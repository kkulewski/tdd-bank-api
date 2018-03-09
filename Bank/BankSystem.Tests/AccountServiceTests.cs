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
            var senderBalance = 100.0M;

            var senderMock = new Mock<IUser>();
            senderMock.Setup(x => x.Balance).Returns(senderBalance);
            var recipientMock = new Mock<IUser>();

            IAccountService accountService = new AccountService();
            IMoneyTransfer transfer = accountService.CreateMoneyTransfer(senderMock.Object, recipientMock.Object, amount);
            Assert.AreEqual(amount, transfer.Amount);
        }
    }
}
