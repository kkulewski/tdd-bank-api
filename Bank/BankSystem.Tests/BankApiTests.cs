using BankSystem.Account;
using BankSystem.Authentication;
using BankSystem.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankSystem.Tests
{
    [TestClass]
    public class BankApiTests
    {
        private IAccountService _accountServiceDouble;
        private IUserStore _userStoreDouble;

        [TestInitialize]
        public void Initialize()
        {
            _accountServiceDouble = new Mock<IAccountService>().Object;
            _userStoreDouble = new Mock<IUserStore>().Object;
        }

        [TestMethod]
        public void SignIn_ReturnsTrue_WhenUserSigns_WithCorrectCredentials()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Authenticate("testlogin", "testpassword")).Returns(true);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            Assert.IsTrue(bank.SignIn("testlogin", "testpassword"));
        }

        [TestMethod]
        public void SignIn_ReturnsFalse_WhenUserSigns_WithIncorrectCredentials()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Authenticate("testlogin", "testpassword")).Returns(true);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            Assert.IsFalse(bank.SignIn("wronglogin", "wrongpassword"));
        }

        [TestMethod]
        public void SignOut_ReturnsFalse_WhenDeauthenticationFails()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Deauthenticate()).Returns(false);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            Assert.IsFalse(bank.SignOut());
        }

        [TestMethod]
        public void SignOut_ReturnsTrue_WhenDeauthenticationSucceeds()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Deauthenticate()).Returns(true);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            Assert.IsTrue(bank.SignOut());
        }

        [TestMethod]
        public void GetMyAccountBalance_ReturnsPredefinedAmount()
        {
            const decimal balance = 1000.0M;

            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.IsAuthenticated()).Returns(true);

            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(x => x.GetBalance(It.IsAny<IUser>())).Returns(balance);

            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, _userStoreDouble);

            Assert.AreEqual(balance, bank.GetMyAccountBalance());
        }
    }
}
