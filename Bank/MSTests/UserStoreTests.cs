using BankSystem.DAL;
using BankSystem.Models;
using BankSystem.Models.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests
{
    [TestClass]
    public class UserStoreTests
    {
        [TestMethod]
        public void GetUserByLogin_ReturnsNull_WhenUserListIsEmpty()
        {
            // ARRANGE
            var userStore = new InMemoryUserStore();

            // ACT
            var user = userStore.GetUserByLogin("someLogin");

            // ASSERT
            Assert.IsNull(user);
        }

        [TestMethod]
        public void Add_ReturnsTrue_WhenAddingUserToEmptyList()
        {
            // ARRANGE
            var userStore = new InMemoryUserStore();
            IUser user = new StubIUser();

            // ACT
            var result = userStore.Add(user);

            // ASSERT
            Assert.IsTrue(result);
        }
    }
}
