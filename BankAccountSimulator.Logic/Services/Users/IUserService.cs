using BankAccountSimulator.Data.Models;

namespace BankAccountSimulator.Logic.Services.Users
{
    public interface IUserService
    {
        bool Login(string username, string password);

        bool UserExists(string username, string password);

        decimal GetUserBalance(string login);

        void AddNewUser(string username, string password);
    }
}