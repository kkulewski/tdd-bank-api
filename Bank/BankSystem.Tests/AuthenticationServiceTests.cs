using BankSystem.Authentication;
using BankSystem.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankSystem.Tests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private string _testUserValidLogin;
        private string _testUserValidPassword;

        private string _testUserInvalidLogin;
        private string _testUserInvalidPassword;

        private IUser _testUserDouble;

        [TestInitialize]
        public void Initialize()
        {
            _testUserValidLogin = "valid_login";
            _testUserValidPassword = "valid_password";

            _testUserInvalidLogin = "invalid_login";
            _testUserInvalidPassword = "invalid_password";

            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Login).Returns(_testUserValidLogin);
            userMock.Setup(x => x.Password).Returns(_testUserValidPassword);
            _testUserDouble = userMock.Object;
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_WhenThereWasNoAttemptToAuthenticateYet()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            // ACT
            var result = authService.IsAuthenticated();

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Authenticate_ReturnsTrue_WhenUserSuccessfullyAuthenticated()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            // ACT
            bool authResult = authService.Authenticate(_testUserValidLogin, _testUserValidPassword);

            // ASSERT
            Assert.IsTrue(authResult);
        }

        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenUserAuthenticates_WithIncorrectPassword()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            // ACT
            bool authResult = authService.Authenticate(_testUserValidLogin, _testUserInvalidPassword);

            // ASSERT
            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenSpecifiedUserDoesNotExists()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            // ACT
            bool authResult = authService.Authenticate(_testUserInvalidLogin, _testUserInvalidPassword);

            // ASSERT
            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsTrue_WhenUserSuccessfullyAuthenticated()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            // ACT
            authService.Authenticate(_testUserValidLogin, _testUserValidPassword);
            bool authResult = authService.IsAuthenticated();

            // ASSERT
            Assert.IsTrue(authResult);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_WhenUserAuthenticationFailed()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            // ACT
            authService.Authenticate(_testUserValidLogin, _testUserInvalidPassword);
            bool authResult = authService.IsAuthenticated();

            // ASSERT
            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void Deauthenticate_ReturnsFalse_WhenNoUserIsAuthenticated()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            //ACT
            bool result = authService.Deauthenticate();

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Deauthenticate_ReturnsTrue_WhenDeauthenticationSucceeds_WithAuthenticatedUser()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            // ACT
            authService.Authenticate(_testUserValidLogin, _testUserValidPassword);
            bool result = authService.Deauthenticate();

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_AfterSuccessfullDeauthentication()
        {
            // ARRANGE
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            // ACT
            authService.Authenticate(_testUserValidLogin, _testUserValidPassword);
            authService.Deauthenticate();
            bool result = authService.IsAuthenticated();

            // ASSERT
            Assert.IsFalse(result);
        }
    }
}
