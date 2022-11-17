using BankAccountSimulator.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountSimulator.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Login = "admin", Password = "1qaz", Balance = 999999 }
        };

        public List<User> GetUsers()
        {
            return _users.ToList();
        }

        public bool UserExists(string login, string passwd)
        {
            bool isUserExist = _users.Any(us => us.Login == login && us.Password == passwd);

            return isUserExist;
        }

        public void AddNew(User user)
        {
            _users.Add(user);
        }
    }
}