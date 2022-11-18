using BankAccountSimulator.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountSimulator.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "1qaz", Balance = 100.00M }
        };

        public List<User> GetUsers()
        {
            return _users.ToList();
        }

        public bool UserExists(string login)
        {
            bool isUserExist = _users.Any(us => us.Username == login);

            return isUserExist;
        }

        public void AddNew(User user)
        {
            _users.Add(user);
        }
    }
}