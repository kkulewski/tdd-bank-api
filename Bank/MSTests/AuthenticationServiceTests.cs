using BankSystem.DAL;
using BankSystem.DAL.Fakes;
using BankSystem.Models;
using BankSystem.Models.Fakes;
using BankSystem.Services.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        [TestMethod]
        public void Authenticate_ReturnsFalse_WhenUserWithGivenLogin_IsNotFound()
        {
            // ARRANGE
            IUserStore userStoreStub = new StubIUserStore
            {
                GetUserByLoginString = login => null
            };

            IUserFactory userFactoryStub = new StubIUserFactory();
            IAuthenticationService authService = new AuthenticationService(userStoreStub, userFactoryStub);

            // ACT
            bool authResult = authService.Authenticate("someLogin", "somePassword");

            // ASSERT
            Assert.IsFalse(authResult);
        }
    }
}
