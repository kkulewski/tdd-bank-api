using BankSystem.DAL;
using BankSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankSystem.Tests
{
    [TestClass]
    public class UserStoreTests
    {
        private IUserStore _userStore;
        private IUser _someUser;
        private IUser _otherUser;
        private IUser _someUserDuplicate;

        [TestInitialize]
        public void Initialize()
        {
            _userStore = new InMemoryUserStore();
            _someUser = new User("someLogin", "somePassword", 0.0M);
            _otherUser = new User("otherLogin", "otherPassword", 0.0M);
            _someUserDuplicate = new User("someLogin", "somePassword", 0.0M);
        }

        [TestMethod]
        public void GetUserByLogin_ReturnsNull_WhenUserListIsEmpty()
        {
            // ACT
            var user = _userStore.GetUserByLogin("someLogin");

            // ASSERT
            Assert.IsNull(user);
        }

        [TestMethod]
        public void Add_ReturnsTrue_WhenAddingUserToEmptyList()
        {
            // ACT
            var result = _userStore.Add(_someUser);

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_ReturnsTrue_WhenAddingToNonEmptyList()
        {
            // ARRANGE
            _userStore.Add(_someUser);

            // ACT
            var result = _userStore.Add(_otherUser);

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_ReturnsFalse_WhenAddingDuplicateUser()
        {
            // ARRANGE
            _userStore.Add(_someUser);

            // ACT
            var result = _userStore.Add(_someUserDuplicate);

            // ASSERT
            Assert.IsFalse(result);
        }
    }
}
