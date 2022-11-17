using BankAccountSimulator.Data.Models;
using System.Collections.Generic;

namespace BankAccountSimulator.Data.Repositories.Users
{
    public interface IUserRepository
    {
        List<User> GetUsers();

        bool UserExists(string login, string password);

        void AddNew(User user);
    }
}