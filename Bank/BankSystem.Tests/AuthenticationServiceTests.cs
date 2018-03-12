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

        private IUser _userMock;
        private IAuthenticationService _authServiceWithMockUserStore;


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
            _userMock = userMock.Object;

            var userStoreMock = new Mock<IUserStore>();
            userStoreMock.Setup(x => x.GetUserByLogin(_testUserValidLogin)).Returns(_userMock);
            _authServiceWithMockUserStore = new AuthenticationService(userStoreMock.Object);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_WhenThereWasNoAttemptToAuthenticateYet()
        {
            // ACT
            var result = _authServiceWithMockUserStore.IsAuthenticated();

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Authenticate_ReturnsTrue_WhenUserSuccessfullyAuthenticated()
        {
            // ACT
            bool authResult = _authServiceWithMockUserStore.Authenticate(_testUserValidLogin, _testUserValidPassword);

            // ASSERT
            Assert.IsTrue(authResult);
        }

        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenUserAuthenticates_WithIncorrectPassword()
        {
            // ACT
            bool authResult = _authServiceWithMockUserStore.Authenticate(_testUserValidLogin, _testUserInvalidPassword);

            // ASSERT
            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenSpecifiedUserDoesNotExists()
        {
            // ACT
            bool authResult = _authServiceWithMockUserStore.Authenticate(_testUserInvalidLogin, _testUserInvalidPassword);

            // ASSERT
            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsTrue_WhenUserSuccessfullyAuthenticated()
        {
            // ARRANGE
            _authServiceWithMockUserStore.Authenticate(_testUserValidLogin, _testUserValidPassword);

            // ACT
            bool authResult = _authServiceWithMockUserStore.IsAuthenticated();

            // ASSERT
            Assert.IsTrue(authResult);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_WhenUserAuthenticationFailed()
        {
            // ARRANGE
            _authServiceWithMockUserStore.Authenticate(_testUserValidLogin, _testUserInvalidPassword);

            // ACT
            bool authResult = _authServiceWithMockUserStore.IsAuthenticated();

            // ASSERT
            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public void Deauthenticate_ReturnsFalse_WhenNoUserIsAuthenticated()
        {
            //ACT
            bool result = _authServiceWithMockUserStore.Deauthenticate();

            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Deauthenticate_ReturnsTrue_WhenDeauthenticationSucceeds_WithAuthenticatedUser()
        {
            // ARRANGE
            _authServiceWithMockUserStore.Authenticate(_testUserValidLogin, _testUserValidPassword);

            // ACT
            bool result = _authServiceWithMockUserStore.Deauthenticate();

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_AfterSuccessfullDeauthentication()
        {
            // ARRANGE
            _authServiceWithMockUserStore.Authenticate(_testUserValidLogin, _testUserValidPassword);
            _authServiceWithMockUserStore.Deauthenticate();

            // ACT
            bool result = _authServiceWithMockUserStore.IsAuthenticated();

            // ASSERT
            Assert.IsFalse(result);
        }
    }
}
