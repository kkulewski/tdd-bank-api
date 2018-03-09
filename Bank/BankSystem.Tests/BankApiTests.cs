using System;
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

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetMyAccountBalance_Throws_WhenUserIsNotAuthenticated()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.IsAuthenticated()).Returns(false);

            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);
            bank.GetMyAccountBalance();
        }

        [TestMethod]
        public void SendMoneyTransfer_ReturnsFalse_WhenRecipientDoesNotExist()
        {
            const string invalidRecipientLogin = "invalid_recipient_login";
            const decimal exampleAmount = 100.0M;

            var authServiceMock = new Mock<IAuthenticationService>();
            var accountServiceMock = new Mock<IAccountService>();

            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, _userStoreDouble);

            Assert.IsFalse(bank.SendMoneyTransfer(invalidRecipientLogin, exampleAmount));
        }

        [TestMethod]
        public void SendMoneyTransfer_ReturnsFalse_WhenUserIsNotAuthenticated()
        {
            const string recipientLogin = "recipient_login";
            const decimal exampleAmount = 100.0M;

            var authServiceMock = new Mock<IAuthenticationService>();
            var accountServiceMock = new Mock<IAccountService>();
            
            var recipientMock = new Mock<IUser>();
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(recipientLogin)).Returns(recipientMock.Object);

            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, userStoreMock.Object);

            Assert.IsFalse(bank.SendMoneyTransfer(recipientLogin, exampleAmount));
        }

        [TestMethod]
        public void SendMoneyTransfer_ReturnsTrue_WhenTransferSucceeds()
        {
            var userMock = new Mock<IUser>();

            var authServiceMock = new Mock<IAuthenticationService>();

            authServiceMock
                .Setup(x => x.IsAuthenticated())
                .Returns(true);

            authServiceMock
                .Setup(x => x.SignedUser)
                .Returns(userMock.Object);

            var userStoreMock = new Mock<IUserStore>();

            userStoreMock
                .Setup(x => x.GetUserByLogin(It.IsAny<string>()))
                .Returns(userMock.Object);

            var moneyTransferMock = new Mock<IMoneyTransfer>();

            var accountServiceMock = new Mock<IAccountService>();

            accountServiceMock
                .Setup(x => x.CreateMoneyTransfer(It.IsAny<IUser>(), It.IsAny<IUser>(), It.IsAny<decimal>()))
                .Returns(moneyTransferMock.Object);

            accountServiceMock
                .Setup(x => x.ExecuteMoneyTransfer(It.IsAny<IMoneyTransfer>()))
                .Returns(true);

            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, userStoreMock.Object);

            Assert.IsTrue(bank.SendMoneyTransfer("RecipientLogin", 100.0M));
        }
    }
}
