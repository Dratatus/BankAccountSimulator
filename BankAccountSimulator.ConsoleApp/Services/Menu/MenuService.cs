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
            "Dodaj środki do konta",
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
            //if (isUserLogged)
            //{
            //    int optionCount = _optionsAfterLogin.Count;
            //    int option = _consoleService.GetIntegerWithinRange("\nWybierz opcję: ", 1, optionCount);
            //    return option;
            //}
            //else
            //{
            //    int optionCount = _options.Count;
            //    int option = _consoleService.GetIntegerWithinRange("\nWybierz opcję: ", 1, optionCount);
            //    return option;
            //}

            int optionCount = isUserLogged ? _optionsAfterLogin.Count : _options.Count;
            int option = _consoleService.GetIntegerWithinRange("\nWybierz opcję: ", 1, optionCount);
            return option;
        }

        public void DisplayError(string errorMessage)
        {
            Console.WriteLine("Wystąpił błąd");
            Console.WriteLine($"* {errorMessage} *");
        }

    }
}