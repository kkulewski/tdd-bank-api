using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bank.Tests
{
    [TestClass]
    public class BankApiTests
    {
        [TestMethod]
        public void IsUserLoggedIn_ReturnsFalse_WhenNoUserIsLoggedIn()
        {
            var bank = new BankApi();
            Assert.IsFalse(bank.IsUserLoggedIn());
        }
    }
}
