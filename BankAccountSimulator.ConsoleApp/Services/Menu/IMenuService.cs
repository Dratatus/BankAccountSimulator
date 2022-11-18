namespace BankAccountSimulator.ConsoleApp.Services.Menu
{
    public interface IMenuService
    {
        void DisplayOptions(bool isUserLogged);

        int GetOption(bool isUserLogged);

        void DisplayError(string errorMessage);
        void DisplayBalance(decimal balance);
        void DisplayRegisterStatus(bool status);
        void DisplayDepositStatus(string balance);
        void DisplayWithdrawStatus(string value);
    }
}