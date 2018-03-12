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
            var user = new User("someLogin", "somePassword", 0.0M);

            // ACT
            var result = userStore.Add(user);

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_ReturnsTrue_WhenAddingToNonEmptyList()
        {
            // ARRANGE
            IUserStore userStore = new InMemoryUserStore();
            var user1 = new User("someLogin", "somePassword", 0.0M);
            var user2 = new User("otherLogin", "otherPassword", 0.0M);

            // ACT
            userStore.Add(user1);
            var result = userStore.Add(user2);

            // ASSERT
            Assert.IsTrue(result);
        }
    }
}
