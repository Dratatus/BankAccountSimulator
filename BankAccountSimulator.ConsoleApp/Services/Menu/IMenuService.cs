namespace BankAccountSimulator.ConsoleApp.Services.Menu
{
    public interface IMenuService
    {
        void DisplayOptions(bool isUserLogged);

        int GetOption(bool isUserLogged);
    }
}
