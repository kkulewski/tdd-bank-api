using BankSystem.DAL;
using BankSystem.Models;
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

        [TestMethod]
        public void Add_ReturnsTrue_WhenAddingUserToEmptyList()
        {
            // ARRANGE
            IUserStore userStore = new InMemoryUserStore();

            // ACT
            var user = new User("someLogin", "somePassword", 0.0M);
            var result = userStore.Add(user);

            // ASSERT
            Assert.IsTrue(result);
        }
    }
}
