namespace BankAccountSimulator.ConsoleApp.Services.Consoles
{
    public interface IConsoleService
    {
        string GetString(string message);

        int GetIntegerWithinRange(string message, int rangeFrom, int rangeTo);
        decimal GetDecimal(string message, string errorMessage = null);
    }
}