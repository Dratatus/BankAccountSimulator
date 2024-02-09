using BankAccountSimulator.Data.Models;
using System.Collections.Generic;

namespace BankAccountSimulator.Data.Repositories.Users
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        bool UserExists(string login);
        void AddNew(User user);
        void AddConvertedBalance(User user, decimal newBalance);
        User GetUserByLogin(string username);
    }
}