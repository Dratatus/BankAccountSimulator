namespace BankAccountSimulator.ConsoleApp.Services.Consoles
{
    public interface IConsoleService
    {
        string GetString(string message);

        int GetIntegerWithinRange(string message, int rangeFrom, int rangeTo);

        //decimal DepositMoney(string message, string message2, string errorMessage);

        //string GetLogin(string loginMessage);

        //string GetPassword(string passwdMessage);

        //void AddNewUser(string loginMessage, string paswwdMessage);
    }
}