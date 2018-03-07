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

        [TestInitialize]
        public void Initialize()
        {
            _accountServiceDouble = new Mock<IAccountService>().Object;
        }

        [TestMethod]
        public void IsUserSignedIn_ReturnsFalse_WhenUserIsNotSignedIn()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.IsAuthenticated()).Returns(false);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble);

            Assert.IsFalse(bank.IsUserSignedIn());
        }

        [TestMethod]
        public void IsUserSignedIn_ReturnsTrue_WhenUserIsSignedIn()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.IsAuthenticated()).Returns(true);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble);

            Assert.IsTrue(bank.IsUserSignedIn());
        }

        [TestMethod]
        public void SignIn_ReturnsTrue_WhenUserSigns_WithCorrectCredentials()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Authenticate("testlogin", "testpassword")).Returns(true);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble);

            Assert.IsTrue(bank.SignIn("testlogin", "testpassword"));
        }

        [TestMethod]
        public void SignIn_ReturnsFalse_WhenUserSigns_WithIncorrectCredentials()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Authenticate("testlogin", "testpassword")).Returns(true);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble);

            Assert.IsFalse(bank.SignIn("wronglogin", "wrongpassword"));
        }

        [TestMethod]
        public void SignOut_ReturnsFalse_WhenDeauthenticationFails()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Deauthenticate()).Returns(false);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble);

            Assert.IsFalse(bank.SignOut());
        }

        [TestMethod]
        public void SignOut_ReturnsTrue_WhenDeauthenticationSucceeds()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Deauthenticate()).Returns(true);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble);

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

            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object);

            Assert.AreEqual(balance, bank.GetMyAccountBalance());
        }
    }
}
