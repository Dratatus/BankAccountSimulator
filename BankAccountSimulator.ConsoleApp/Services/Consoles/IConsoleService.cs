namespace BankAccountSimulator.ConsoleApp.Services.Consoles
{
    public  interface IConsoleService
    {
        int GetIntegerWithinRange(string message, int rangeFrom, int rangeTo);
    }
}
