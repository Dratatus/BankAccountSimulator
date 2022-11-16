using BankAccountSimulator.ConsoleApp.Services.Consoles;
using BankAccountSimulator.ConsoleApp.Services.Menu;
using BankAccountSimulator.Logic.Services.UsersProvider;
using Ninject;
using System.Threading.Tasks;

namespace BankAccountSimulator.ConsoleApp
{
    public class Program
    {
        private static readonly IKernel _kernel = new StandardKernel();

        private static IMenuService _menuService;
        private static IUsersProviderService _userProvider;
        private static IConsoleService _consoleService;
        static void Main(string[] args)
        {
            PerformDependencyInjectionBindings();

            bool isUserLogged = false;


            while (true)
            {
                _menuService.DisplayOptions(isUserLogged);
                int option = _menuService.GetOption(isUserLogged);

                if (option == 1)
                {
                    _userProvider.GetUserDataToLogin("\npodaj login: ", "\npodaj hasło: ");
                    _userProvider.IsUserExist();

                    if (_userProvider.IsUserExist())
                    {
                        isUserLogged = true;

                        _menuService.DisplayOptions(isUserLogged);
                        int optionAfterLogin = _menuService.GetOption(isUserLogged);

                        if (optionAfterLogin == 3)
                        {
                            _userProvider.CheckUserBalance();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nBłędne dane! \n");
                    }

                }
                else if (option == 2)
                {
                    _userProvider.AddNewUser("\nPomyślnie utworzono konto!\n");

                }

                else if (option == 3)
                {
                    Console.WriteLine("\nProgram zakończony.. ");
                    break;
                }

            }

        }

        private static void PerformDependencyInjectionBindings()
        {
            _kernel.Bind<IMenuService>().To<MenuService>();
            _kernel.Bind<IUsersProviderService>().To<HardcodedUsersProvider>();
            _kernel.Bind<IConsoleService>().To<ConsoleService>();


            _menuService = _kernel.Get<IMenuService>();
            _userProvider = _kernel.Get<IUsersProviderService>();
            _consoleService = _kernel.Get<IConsoleService>();
        }
    }
}