using BankAccountSimulator.Data.Models;

namespace BankAccountSimulator.Logic.Services.Users
{
    public interface IUserService
    {
        bool Login(string username, string password);

        bool UserExists(string username);

        decimal GetUserBalance(string login);

        bool AddNewUser(string login, string password);
        decimal DepositMoney(string login, string userAmountOfMoney);
        decimal WithdrawMoney(string login, string aomuntOfMoney);
        string GetOperationDate();
        void AddAccountHistory(string typeOperation, string login, string message);
    }
}