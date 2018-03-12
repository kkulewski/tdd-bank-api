using System.Collections.Generic;
using System.Linq;
using BankSystem.Models;

namespace BankSystem.DAL
{
    public class InMemoryUserStore : IUserStore
    {
        private static List<IUser> _users = new List<IUser>();

        public IUser GetUserByLogin(string login)
        {
            return _users.FirstOrDefault(x => x.Login == login);
        }
    }
}
