using BankAccountSimulator.Data.Models;
using System.Collections.Generic;

namespace BankAccountSimulator.Logic.Services.Users
{
    public interface IUserService
    {
        bool Login(string username, string password);

        bool UserExists(string username);

        decimal GetUserBalance(string login);

        bool AddNewUser(string login, string password);

        string GetOperationDate();

        void AddAccountHistory(string typeOperation, string login, string message);

        string GetAccountHistoryOperationDate(string username);

        List<string> GetAccountHistory(string username);
        decimal DepositMoney(string login, string amountOfMoney, string currency);
        decimal WithdrawMoney(string login, string aomuntOfMoney, string currency);
    }
}