using BankSystem.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankSystem.Tests
{
    [TestClass]
    public class BankApiTests
    {
        [TestMethod]
        public void IsUserLoggedIn_ReturnsFalse_WhenNoUserIsLoggedIn()
        {
            var fakeAuthenticationService = new FakeAuthenticationService();
            var bank = new BankApi(fakeAuthenticationService);

            Assert.IsFalse(bank.IsUserLoggedIn());
        }
    }
}
