using System.Collections.Generic;

namespace BankAccountSimulator.ConsoleApp.Services.Menu
{
    public interface IMenuService
    {
        void DisplayOptions(bool isUserLogged);

        int GetOption(bool isUserLogged);

        void DisplayError(string errorMessage);

        void DisplayBalance(decimal balance, string currency);

        void DisplayAccountHistory(List<string> operations, string operationDate);

        void DisplayLoginStatus(bool status, string message, string errorMessage);

        void DisplayDepositStatus(string value, string message, string currency);

        void DisplayWithdrawStatus(string value, string message, string currency);
    }
}