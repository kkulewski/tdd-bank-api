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
    }
}
