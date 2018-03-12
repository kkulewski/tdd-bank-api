using System.Collections.Generic;
using System.Linq;
using BankSystem.Models;

namespace BankSystem.DAL
{
    public class InMemoryUserStore : IUserStore
    {
        private readonly List<IUser> _users = new List<IUser>();

        public IUser GetUserByLogin(string login)
        {
            return _users.FirstOrDefault(x => x.Login == login);
        }

        public bool Add(IUser user)
        {
            if (_users.Any(x => x.Login == user.Login))
            {
                return false;
            }

            _users.Add(user);
            return true;
        }
    }
}
