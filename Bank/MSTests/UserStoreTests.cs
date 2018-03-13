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
            IUser userStub = new StubIUser();

            // ACT
            var result = userStore.Add(userStub);

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUserByLogin_ReturnsUser_WithCorrectLogin()
        {
            // ARRANGE
            var userStore = new InMemoryUserStore();

            var userLogin = "someLogin";
            IUser userStub = new StubIUser
            {
                LoginGet = () => userLogin
            };

            userStore.Add(userStub);

            // ACT
            var result = userStore.GetUserByLogin(userLogin);

            // ASSERT
            StringAssert.StartsWith(result.Login, userLogin);
        }

        [TestMethod]
        public void GetUserByLogin_ReturnsUser_WithCorrectPassword()
        {
            // ARRANGE
            var userStore = new InMemoryUserStore();

            var userLogin = "someLogin";
            var userPassword = "somePassword";
            IUser userStub = new StubIUser
            {
                LoginGet = () => userLogin,
                PasswordGet = () => userPassword
            };

            userStore.Add(userStub);

            // ACT
            var result = userStore.GetUserByLogin(userLogin);

            // ASSERT
            StringAssert.StartsWith(result.Password, userPassword);
        }
    }
}
