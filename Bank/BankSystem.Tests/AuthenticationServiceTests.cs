using BankSystem.Authentication;
using BankSystem.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankSystem.Tests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_WhenThereWasNoAttemptToAuthenticateYet()
        {
            var userStoreMock = new Mock<IUserStore>();
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            Assert.IsFalse(authService.IsAuthenticated());
        }

        [TestMethod]
        public void Authenticate_ReturnsTrue_WhenUserSuccessfullyAuthenticated()
        {
            var testUserLogin = "testlogin";
            var testUserPassword = "testpassword";

            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Login).Returns(testUserLogin);
            userMock.Setup(x => x.Password).Returns(testUserPassword);

            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(testUserLogin)).Returns(userMock.Object);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            bool authResult = authService.Authenticate(testUserLogin, testUserPassword);

            Assert.IsTrue(authResult);
        }

        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenUserAuthenticates_WithIncorrectPassword()
        {
            var testUserLogin = "testlogin";
            var testUserPassword = "testpassword";

            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Login).Returns(testUserLogin);
            userMock.Setup(x => x.Password).Returns(testUserPassword);

            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(testUserLogin)).Returns(userMock.Object);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            bool authResult = authService.Authenticate(testUserLogin, "wrongpassword");

            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenSpecifiedUserDoesNotExists()
        {
            var testUserLogin = "testlogin";
            var testUserPassword = "testpassword";

            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Login).Returns(testUserLogin);
            userMock.Setup(x => x.Password).Returns(testUserPassword);

            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(testUserLogin)).Returns(userMock.Object);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            bool authResult = authService.Authenticate("wronglogin", "wrongpassword");

            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsTrue_WhenUserSuccessfullyAuthenticated()
        {
            var testUserLogin = "testlogin";
            var testUserPassword = "testpassword";

            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Login).Returns(testUserLogin);
            userMock.Setup(x => x.Password).Returns(testUserPassword);

            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(testUserLogin)).Returns(userMock.Object);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            authService.Authenticate(testUserLogin, testUserPassword);
            
            Assert.IsTrue(authService.IsAuthenticated());
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_WhenUserAuthenticationFailed()
        {
            var testUserLogin = "testlogin";
            var testUserPassword = "testpassword";

            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Login).Returns(testUserLogin);
            userMock.Setup(x => x.Password).Returns(testUserPassword);

            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(testUserLogin)).Returns(userMock.Object);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            authService.Authenticate(testUserLogin, "wrongpassword");

            Assert.IsFalse(authService.IsAuthenticated());
        }
    }
}
