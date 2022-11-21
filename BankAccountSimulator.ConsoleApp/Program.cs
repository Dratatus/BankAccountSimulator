using BankAccountSimulator.ConsoleApp.Services.Consoles;
using BankAccountSimulator.ConsoleApp.Services.Menu;
using BankAccountSimulator.Data.Repositories.Currencies;
using BankAccountSimulator.Data.Repositories.Users;
using BankAccountSimulator.Logic.Services.ExchangeRates;
using BankAccountSimulator.Logic.Services.RulesOfCorrectnes;
using BankAccountSimulator.Logic.Services.Users;
using Ninject;
using System;
using System.Collections.Generic;

namespace BankAccountSimulator.ConsoleApp
{
    public class Program
    {
        private static readonly IKernel _kernel = new StandardKernel();

        private static IMenuService _menuService;
        private static IUserService _userService;
        private static IConsoleService _consoleService;
        private static IRuleOfCorrectnesService _ruleOfCorrectnesProvider;
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
                                    string valueToDeposit = _consoleService.GetString("Podaj kwotę wpłaty: ");
                                    string currency = _consoleService.GetString("Podaj walutę depozytu PLN/USD/EUR/GBP: ");
                                    _userService.DepositMoney(login, valueToDeposit, currency);
                                    _menuService.DisplayDepositStatus(valueToDeposit, "Pomyślnie wpłacono: ", currency);
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
                                    string valueToWithdraw = _consoleService.GetString("Podaj kwotę wYpłaty: ");
                                    string currency = _consoleService.GetString("Podaj walutę depozytu PLN/USD/EUR/GBP: ");

                                    _userService.WithdrawMoney(login, valueToWithdraw, currency);
                                    _menuService.DisplayWithdrawStatus(valueToWithdraw, "Pomyślnie wypłacono: ", currency);
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
                                    string userCurrency = _consoleService.GetString("Podaj kod waluty na którą chcesz przewalutować swoje saldo PLN/USD/EUR/GBP :");
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
                            string userLogin = _consoleService.GetString("\nUtwórz login: ");
                            string userPassword = _consoleService.GetString("\nUtwórz hasło: ");

                            bool registerStatus = _userService.AddNewUser(userLogin, userPassword);
                            _menuService.DisplayRegisterStatus(registerStatus, "Pomyślnie zarejestrowano! ", "Wysąpił błąd przy rejstracji");
                        }

                        catch (Exception e)
                        {

                            _menuService.DisplayError(e.Message);
                        }
                    
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

            // Singleton to wzorzez projektowy, gdzie uniemożliwia się teworzenie więcej niż 1 instancji danej klasy
            // Kontenery wstrzykiwania zależności mają to "wbudowane"
            _kernel.Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            _kernel.Bind<ICurrencyRepository>().To<CurrencyRepository>();
            _kernel.Bind<IExchangeRatesService>().To<ExchangeRatesService>();



            _menuService = _kernel.Get<IMenuService>();
            _consoleService = _kernel.Get<IConsoleService>();
            _ruleOfCorrectnesProvider = _kernel.Get<IRuleOfCorrectnesService>();

            _userService = _kernel.Get<IUserService>();
            _exchangeRatesService = _kernel.Get<IExchangeRatesService>();
        }
    }
}