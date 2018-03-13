using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankSystem.Tests
{
    [TestClass]
    public class UserFactoryTests
    {
        private IUserFactory _factory;
        private string _login;
        private string _password;
        private decimal _balance;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new UserFactory();
            _login = "someLogin";
            _password = "somePassword";
            _balance = 1000.0M;
        }

        [TestMethod]
        public void Create_CreatesNewUser_WithCorrectLogin()
        {
            // ACT
            var user = _factory.Create(_login, _password);

            // ASSERT
            Assert.AreEqual(_login, user.Login);
        }

        [TestMethod]
        public void Create_CreatesNewUser_WithCorrectPassword()
        {
            // ACT
            var user = _factory.Create(_login, _password);

            // ASSERT
            Assert.AreEqual(_password, user.Password);
        }

        [TestMethod]
        public void Create_CreatesNewUser_WithCorrectBalance()
        {
            // ACT
            var user = _factory.Create(_login, _password);

            // ASSERT
            Assert.AreEqual(_balance, user.Balance);
        }
    }
}
