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
        public void IsAuthenticated_ReturnsFalse_WhenNoAuthenticatedUser()
        {
            var userStoreMock = new Mock<IUserStore>();
            IAuthenticationService authService = new AuthenticationService(userStoreMock.Object);

            Assert.IsFalse(authService.IsAuthenticated());
        }
    }
}
