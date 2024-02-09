using BankAccountSimulator.ConsoleApp.Services.Consoles;
using BankAccountSimulator.ConsoleApp.Services.Menu;
using BankAccountSimulator.Data.Repositories.Currencies;
using BankAccountSimulator.Data.Repositories.Users;
using BankAccountSimulator.Logic.Services.ExchangeRates;
using BankAccountSimulator.Logic.Services.Users;
using Ninject;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BankAccountSimulator.ConsoleApp
{
    public class Program
    {
        private static readonly IKernel _kernel = new StandardKernel();

        private static IMenuService _menuService;
        private static IUserService _userService;
        private static IConsoleService _consoleService;
        private static IExchangeRatesService _exchangeRatesService;

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

                    while (true)
                    {
                        try
                        {
                            login = _consoleService.GetString("\nEnter login: ");
                            string password = _consoleService.GetString("\nEnter password: ");

                            userFound = _userService.Login(login, password);
                            _menuService.DisplayLoginStatus(userFound, "Succesfully logged in! ", "An error occured");
                            break;
                        }
                        catch (Exception e)
                        {
                            _menuService.DisplayError(e.Message);
                            break;
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
                                    string valueToDeposit = _consoleService.GetString("Enter deposit amount: ");
                                    string currency = _consoleService.GetString("Specify the currency PLN/USD/EUR/GBP: ");
                                    _userService.DepositMoney(login, valueToDeposit, currency);
                                    _menuService.DisplayDepositStatus(valueToDeposit, "Succesfully deposited: ", currency);
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
                                    string valueToWithdraw = _consoleService.GetString("Enter whithdraw amount ");
                                    string currency = _consoleService.GetString("Specify the currency: PLN/USD/EUR/GBP: ");

                                    _userService.WithdrawMoney(login, valueToWithdraw, currency);
                                    _menuService.DisplayWithdrawStatus(valueToWithdraw, "Succesfully withdrawn: ", currency);
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
                                    string accountCurencyType = _exchangeRatesService.GetUserActualCurrencyAccountType(login);
                                    _menuService.DisplayBalance(userBlance, accountCurencyType);
                                }
                                catch (Exception e)
                                {

                                    _menuService.DisplayError(e.Message);
                                }

                            }
                            else if (optionAfterLogin == 4)
                            {
                                try
                                {
                                    List<string> operations = _userService.GetAccountHistory(login);
                                    string operationsDate = _userService.GetOperationDate();
                                    _menuService.DisplayAccountHistory(operations, operationsDate);
                                }
                                catch (Exception e)
                                {

                                    _menuService.DisplayError(e.Message);
                                }
                            }
                            else if (optionAfterLogin == 5)
                            {
                                try
                                {
                                    string userCurrency = _consoleService.GetString("Enter the currency code you wish to convert your balance to: PLN/USD/EUR/GBP :");
                                    string userAccountCurrencyType = _exchangeRatesService.GetUserActualCurrencyAccountType(login);
                                    _exchangeRatesService.SetUserBalance(login, userAccountCurrencyType, userCurrency);
                                }
                                catch (Exception e)
                                {

                                    _menuService.DisplayError(e.Message);
                                }
                            }
                            else if (optionAfterLogin == 6)
                            {
                                isUserLogged = false;
                                Console.Clear();
                                break;
                            }

                        }
                    }
                }
                else if (option == 2)
                {
                        try
                        {
                            string userLogin = _consoleService.GetString("\nCreate login ");
                            string userPassword = _consoleService.GetString("\nCreate password ");

                            bool registerStatus = _userService.AddNewUser(userLogin, userPassword);
                            _menuService.DisplayLoginStatus(registerStatus, "Succesfully registerd! ", "An error occured");
                        }

                        catch (Exception e)
                        {

                            _menuService.DisplayError(e.Message);
                        }
                    
                }
                else if (option == 3)
                {
                    Console.WriteLine("\nProgram finished.. ");
                    break;
                }
            }
        }

        private static void PerformDependencyInjectionBindings()
        {
            _kernel.Bind<IMenuService>().To<MenuService>();
            _kernel.Bind<IConsoleService>().To<ConsoleService>();
            _kernel.Bind<IUserService>().To<UserService>();

            _kernel.Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            _kernel.Bind<ICurrencyRepository>().To<CurrencyRepository>();
            _kernel.Bind<IExchangeRatesService>().To<ExchangeRatesService>();

            _menuService = _kernel.Get<IMenuService>();
            _consoleService = _kernel.Get<IConsoleService>();

            _userService = _kernel.Get<IUserService>();
            _exchangeRatesService = _kernel.Get<IExchangeRatesService>();
        }
    }
}