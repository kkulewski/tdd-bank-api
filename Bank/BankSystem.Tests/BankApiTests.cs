using System;
using BankSystem.DAL;
using BankSystem.Models;
using BankSystem.Services.Account;
using BankSystem.Services.Authentication;
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
            // ARRANGE
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Authenticate("testlogin", "testpassword")).Returns(true);
            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            // ACT
            var result = bank.SignIn("testlogin", "testpassword");

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SignIn_ReturnsFalse_WhenUserSigns_WithIncorrectCredentials()
        {
            // ARRANGE
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Authenticate("testlogin", "testpassword")).Returns(true);
            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            // ACT
            var result = bank.SignIn("wronglogin", "wrongpassword");

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SignOut_ReturnsFalse_WhenDeauthenticationFails()
        {
            // ARRANGE
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Deauthenticate()).Returns(false);
            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            // ACT
            var result = bank.SignOut();

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SignOut_ReturnsTrue_WhenDeauthenticationSucceeds()
        {
            // ARRANGE
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.Deauthenticate()).Returns(true);
            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            // ACT
            var result = bank.SignOut();

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetMyAccountBalance_ReturnsPredefinedAmount()
        {
            // ARRANGE
            const decimal expectedBalance = 1000.0M;
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.IsAuthenticated()).Returns(true);
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(x => x.GetBalance(It.IsAny<IUser>())).Returns(expectedBalance);
            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, _userStoreDouble);

            // ACT
            var balance = bank.GetMyAccountBalance();

            // ASSERT
            Assert.AreEqual(expectedBalance, balance);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetMyAccountBalance_Throws_WhenUserIsNotAuthenticated()
        {
            // ARRANGE
            const decimal expectedBalance = 1000.0M;
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.IsAuthenticated()).Returns(false);
            var bank = new BankApi(authServiceMock.Object, _accountServiceDouble, _userStoreDouble);

            // ACT
            var balance = bank.GetMyAccountBalance();

            // ASSERT
            Assert.AreEqual(expectedBalance, balance);
        }

        [TestMethod]
        public void SendMoneyTransfer_ReturnsFalse_WhenRecipientDoesNotExist()
        {
            // ARRANGE
            const string invalidRecipientLogin = "invalid_recipient_login";
            const decimal exampleAmount = 100.0M;
            var authServiceMock = new Mock<IAuthenticationService>();
            var accountServiceMock = new Mock<IAccountService>();
            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, _userStoreDouble);

            // ACT
            bool result = bank.SendMoneyTransfer(invalidRecipientLogin, exampleAmount);

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SendMoneyTransfer_ReturnsFalse_WhenUserIsNotAuthenticated()
        {
            // ARRANGE
            const string recipientLogin = "recipient_login";
            const decimal exampleAmount = 100.0M;
            var authServiceMock = new Mock<IAuthenticationService>();
            var accountServiceMock = new Mock<IAccountService>();
            var recipientMock = new Mock<IUser>();
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(recipientLogin)).Returns(recipientMock.Object);
            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, userStoreMock.Object);

            // ACT
            var result = bank.SendMoneyTransfer(recipientLogin, exampleAmount);

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SendMoneyTransfer_ReturnsTrue_WhenTransferSucceeds()
        {
            // ARRANGE
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

            // ACT
            var result = bank.SendMoneyTransfer("RecipientLogin", 100.0M);

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SignUp_ReturnFalse_WhenSignUpFails()
        {
            // ARRANGE
            var login = "someLogin";
            var password = "somePassword";

            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.SignUp(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var accountServiceMock = new Mock<IAccountService>();
            var userStoreMock = new Mock<IUserStore>();
            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, userStoreMock.Object);

            // ACT
            var result = bank.SignUp(login, password);

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SignUp_ReturnTrue_WhenSignUpSucceeds()
        {
            // ARRANGE
            var login = "someLogin";
            var password = "somePassword";

            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.SignUp(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var accountServiceMock = new Mock<IAccountService>();
            var userStoreMock = new Mock<IUserStore>();
            var bank = new BankApi(authServiceMock.Object, accountServiceMock.Object, userStoreMock.Object);

            // ACT
            var result = bank.SignUp(login, password);

            // ASSERT
            Assert.IsTrue(result);
        }
    }
}
