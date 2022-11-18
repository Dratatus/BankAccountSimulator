using BankAccountSimulator.ConsoleApp.Services.Consoles;
using BankAccountSimulator.ConsoleApp.Services.Menu;
using BankAccountSimulator.Data.Repositories.Users;
using BankAccountSimulator.Logic.Services.RulesOfCorrectnes;
using BankAccountSimulator.Logic.Services.Users;
using Ninject;
using System;

namespace BankAccountSimulator.ConsoleApp
{
    public class Program
    {
        private static readonly IKernel _kernel = new StandardKernel();

        // WSZELKIE rzeczy i operacje typowe dla Twojego programu (wiedza biznesowa) czyli np... podaj login, podaj hasł, wpłać pieniądze
        private static IMenuService _menuService;
        private static IUserService _userService;

        // Typowe operacje konsolowe (GetInt, GetString, itp... + jakaś walidacja, np GetIntFromRange(x, y) )
        private static IConsoleService _consoleService;
        private static IRuleOfCorrectnesService _ruleOfCorrectnesProvider;

        public static void Main(string[] args)
        {
            PerformDependencyInjectionBindings();

            bool isUserLogged = false;

            while (true)
            {
                _menuService.DisplayOptions(isUserLogged);
                int option = _menuService.GetOption(isUserLogged);

                if (option == 1)
                {
                    bool userFound = false;
                    string login = null;

                    // Nie sugeruj się tym co poniżej, to tylko przykład
                    while (true)
                    {
                        try
                        {
                            login = _consoleService.GetString("\nPodaj login: ");
                            string password = _consoleService.GetString("\nPodaj hasło: ");

                            userFound = _userService.Login(login, password);

                            break;
                        }
                        catch (Exception e)
                        {
                            _menuService.DisplayError(e.Message);
                        }
                    }

                    if (userFound)
                    {
                        while (true)
                        {

                            isUserLogged = true;

                            _menuService.DisplayOptions(isUserLogged);
                            int optionAfterLogin = _menuService.GetOption(isUserLogged);

                            if (optionAfterLogin == 1)
                            {
                                try
                                {
                                    string valueToDeposit = _consoleService.GetString("Podaj kwotę wpłaty z dokładnością do dwóch miejsc po przecinku: ");
                                    _userService.DepositMoney(login, valueToDeposit);
                                    _menuService.DisplayDepositStatus(valueToDeposit);
                                }
                                catch (Exception e)
                                {

                                    _menuService.DisplayError(e.Message);
                                }
                            }
                            else if (optionAfterLogin == 2)
                            {
                                try
                                {
                                    string valueToWithdraw = _consoleService.GetString("Podaj kwotę wpłaty z dokładnością do dwóch miejsc po przecinku: ");
                                    _userService.WithdrawMoney(login, valueToWithdraw);
                                    _menuService.DisplayWithdrawStatus(valueToWithdraw);
                                }
                                catch (Exception e)
                                {

                                    _menuService.DisplayError(e.Message);
                                }
                               
                            }
                            else if (optionAfterLogin == 3)
                            {
                                try
                                {
                                    decimal userBlance = _userService.GetUserBalance(login);
                                    _menuService.DisplayBalance(userBlance);
                                }
                                catch (Exception e)
                                {

                                    _menuService.DisplayError(e.Message);
                                }
                                
                            }

                        }
                    }
                }
                else if (option == 2)
                {
                    string userLogin = _consoleService.GetString("\nUtwórz login: ");
                    string userPassword = _consoleService.GetString("\nUtwórz hasło: ");

                    bool registerStatus = _userService.AddNewUser(userLogin, userPassword);
                    _menuService.DisplayRegisterStatus(registerStatus);

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
            _kernel.Bind<IConsoleService>().To<ConsoleService>();
            _kernel.Bind<IRuleOfCorrectnesService>().To<RuleOfCorrectnesService>();
            _kernel.Bind<IUserService>().To<UserService>();

            _kernel.Bind<IUserRepository>().To<UserRepository>();

            _menuService = _kernel.Get<IMenuService>();
            _consoleService = _kernel.Get<IConsoleService>();
            _ruleOfCorrectnesProvider = _kernel.Get<IRuleOfCorrectnesService>();
            _userService = _kernel.Get<IUserService>();
        }
    }
}