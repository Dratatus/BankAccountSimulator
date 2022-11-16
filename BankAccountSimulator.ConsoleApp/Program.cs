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

                if (option == 2)
                {
                    _userProvider.AddNewUser("Pomyślnie utworzono konto!");

                }
                

                else if (option == 2)
                {
                    Console.WriteLine("Program zakończony.. ");
                    break;
                }

                else if (option == 3)
                {
                    Console.WriteLine("Program zakończony.. ");
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