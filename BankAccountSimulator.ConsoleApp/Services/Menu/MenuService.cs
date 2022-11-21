using BankAccountSimulator.ConsoleApp.Services.Consoles;
using BankAccountSimulator.Data.Repositories.Currencies;
using System;
using System.Collections.Generic;

namespace BankAccountSimulator.ConsoleApp.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IConsoleService _consoleService;

        private readonly List<string> _options = new List<string>
        {
            "Zaloguj się",
            "Utwórz nowe konto",
            "Zakończ program"
        };

        private readonly List<string> _optionsAfterLogin = new List<string>
        {
            "Wpłać środki na konto",
            "Wyciągnij środki z konta",
            "Wyświetl saldo konta",
            "Wyświetl historię konta",
            "Przewalutuj swoje saldo",
            "Wyloguj"
        };

        public MenuService(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public void DisplayOptions(bool isUserLogged)
        {
            int number = 1;
            if (isUserLogged)
            {
                Console.WriteLine();
                foreach (var option in _optionsAfterLogin)
                {
                    Console.WriteLine($"{number}. {option}");
                    number++;
                }
            }
            else
            {
                foreach (var option in _options)
                {
                    Console.WriteLine($"{number}. {option}");
                    number++;
                }
            }
        }

        public int GetOption(bool isUserLogged)
        {
            int optionCount = isUserLogged ? _optionsAfterLogin.Count : _options.Count;
            int option = _consoleService.GetIntegerWithinRange("\nWybierz opcję: ", 1, optionCount);

            return option;
        }

        public void DisplayError(string errorMessage)
        {
            Console.Clear();
            Console.WriteLine("Wystąpił błąd: ");
            Console.Write($"* {errorMessage} *\n");
        }

        public void DisplayBalance(decimal balance, string currency)
        {
            Console.Clear();
            bool isBalancePositive = balance >= 0;

            string message = isBalancePositive ? $"\nTwoje saldo wynosi {balance} {currency}\n" : $"\nNie posiadasz żadnych środków na koncie \n";

            Console.WriteLine(message);
        }

        public void DisplayRegisterStatus(bool status, string message, string errorMessage)
        {
            Console.Clear();
            bool isRegisterSucces = status == true;

            string messageToDisplay = isRegisterSucces ? $"\n {message} \n" : $"\n {errorMessage}\n ";

            Console.WriteLine(messageToDisplay);
        }

        public void DisplayDepositStatus(string value,string message, string currency)
        {
            Console.Clear();
            Console.WriteLine($"{message} {value} {currency.ToUpper()}");
        }
        public void DisplayWithdrawStatus(string value, string message, string currency)
        {
            Console.Clear();
            Console.WriteLine($" {message} {value} {currency.ToUpper()}");
        }

        public void DisplayAccountHistory(List<string> operations, string operationDate)
        {
            Console.Clear();
            Console.WriteLine("Historia konta:");

            foreach (var operation in operations)
            {
                Console.WriteLine($"{operationDate} - {operation}");
            }
        }

    }
}