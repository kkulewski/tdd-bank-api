using BankSystem.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankSystem.Tests
{
    [TestClass]
    public class BankApiTests
    {
        [TestMethod]
        public void IsUserLoggedIn_ReturnsFalse_WhenNoUserIsLoggedIn()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(a => a.IsAuthenticated()).Returns(false);

            var bank = new BankApi(authServiceMock.Object);
            Assert.IsFalse(bank.IsUserLoggedIn());
        }
    }
}
