using BankAccountSimulator.Data.Models;
using BankAccountSimulator.Data.Models.Currencies;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountSimulator.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            
            new User { Username = "admin", Password = "1qaz", Balance = 100.00M, Currency_ = new Currency { Code = "PLN" }, AccountHistory_ = new AccountHistory { Operation = new List<string>(), OperationDate = "" } }
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

        public void AddConvertedBalance(User user, decimal newBalance)
        {
            user.Balance = newBalance;
        }

        public User GetUserByLogin(string username)
        {
            var user = _users.Single(u => u.Username == username);

            return user;
        }
    }
}