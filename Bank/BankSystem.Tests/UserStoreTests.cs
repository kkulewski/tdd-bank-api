using BankSystem.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankSystem.Tests
{
    [TestClass]
    public class UserStoreTests
    {
        [TestMethod]
        public void GetUserByLogin_ReturnsNull_WhenUserListIsEmpty()
        {
            // ARRANGE
            IUserStore userStore = new InMemoryUserStore();

            // ACT
            var user = userStore.GetUserByLogin("someLogin");

            // ASSERT
            Assert.IsNull(user);
        }
    }
}
