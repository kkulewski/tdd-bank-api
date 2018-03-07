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
            var userStoreMock = new Mock<IUserStore>();
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            Assert.IsFalse(authService.IsAuthenticated());
        }

        [TestMethod]
        public void Authenticate_ReturnsTrue_WhenUserSuccessfullyAuthenticated()
        {
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            bool authResult = authService.Authenticate(_testUserValidLogin, _testUserValidPassword);

            Assert.IsTrue(authResult);
        }

        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenUserAuthenticates_WithIncorrectPassword()
        {
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            bool authResult = authService.Authenticate(_testUserValidLogin, _testUserInvalidPassword);

            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenSpecifiedUserDoesNotExists()
        {
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            bool authResult = authService.Authenticate(_testUserInvalidLogin, _testUserInvalidPassword);

            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsTrue_WhenUserSuccessfullyAuthenticated()
        {
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            authService.Authenticate(_testUserValidLogin, _testUserValidPassword);
            
            Assert.IsTrue(authService.IsAuthenticated());
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_WhenUserAuthenticationFailed()
        {
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            authService.Authenticate(_testUserValidLogin, _testUserInvalidPassword);

            Assert.IsFalse(authService.IsAuthenticated());
        }

        [TestMethod]
        public void Deauthenticate_ReturnsFalse_WhenNoUserIsAuthenticated()
        {
            var userStoreMock = new Mock<IUserStore>();
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            Assert.IsFalse(authService.Deauthenticate());
        }

        [TestMethod]
        public void Deauthenticate_ReturnsTrue_WhenDeauthenticationSucceeds_WithAuthenticatedUser()
        {
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            authService.Authenticate(_testUserValidLogin, _testUserValidPassword);

            Assert.IsTrue(authService.Deauthenticate());
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_AfterSuccessfullDeauthentication()
        {
            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_testUserDouble);

            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);
            authService.Authenticate(_testUserValidLogin, _testUserValidPassword);
            authService.Deauthenticate();

            Assert.IsFalse(authService.IsAuthenticated());
        }
    }
}
