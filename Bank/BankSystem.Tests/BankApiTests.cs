using BankSystem.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankSystem.Tests
{
    [TestClass]
    public class BankApiTests
    {
        [TestMethod]
        public void IsUserSignedIn_ReturnsFalse_WhenUserIsNotSignedIn()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(a => a.IsAuthenticated()).Returns(false);

            var bank = new BankApi(authServiceMock.Object);
            Assert.IsFalse(bank.IsUserSignedIn());
        }

        [TestMethod]
        public void IsUserSignedIn_ReturnsTrue_WhenUserIsSignedIn()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(a => a.IsAuthenticated()).Returns(true);

            var bank = new BankApi(authServiceMock.Object);
            Assert.IsTrue(bank.IsUserSignedIn());
        }

        [TestMethod]
        public void SignIn_ReturnsTrue_WhenUserSigns_WithCorrectCredentials()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(a => a.Authenticate("testlogin", "testpassword")).Returns(true);

            var bank = new BankApi(authServiceMock.Object);
            Assert.IsTrue(bank.SignIn("testlogin", "testpassword"));
        }

        [TestMethod]
        public void SignIn_ReturnsFalse_WhenUserSigns_WithIncorrectCredentials()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(a => a.Authenticate("testlogin", "testpassword")).Returns(true);

            var bank = new BankApi(authServiceMock.Object);
            Assert.IsFalse(bank.SignIn("wronglogin", "wrongpassword"));
        }
    }
}
