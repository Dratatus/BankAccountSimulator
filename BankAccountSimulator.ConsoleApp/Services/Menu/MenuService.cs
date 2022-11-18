using BankAccountSimulator.ConsoleApp.Services.Consoles;
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
            Console.WriteLine("Wystąpił błąd");
            Console.WriteLine($"* {errorMessage} *");
        }

        public void DisplayBalance(decimal balance)
        {
            bool isBalancePositive = balance >= 0;

            string message = isBalancePositive ? $"\nTwoje saldo wynosi {balance}\n" : $"\nNie posiadasz żadnych środków na koncie \n";

            Console.WriteLine(message);
        }

        public void DisplayRegisterStatus(bool status)
        {
            bool isRegisterSucces = status == true;

            string message = isRegisterSucces ? $"\n Pomyślnie zarejestrowano! " : $"\n Wystąpił błąd podczas rejstracji ";

            Console.WriteLine(message);
        }

        public void DisplayDepositStatus(string value)
        {
            Console.WriteLine($"Pomyślnie dodano do salda kwotę: {value}");
        }
        public void DisplayWithdrawStatus(string value)
        {
            Console.WriteLine($"Pomyślnie wypłacono z konta kwotę: {value}");
        }

    }
}