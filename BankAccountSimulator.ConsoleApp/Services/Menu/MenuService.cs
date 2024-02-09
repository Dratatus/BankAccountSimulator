using BankAccountSimulator.ConsoleApp.Services.Consoles;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BankAccountSimulator.ConsoleApp.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IConsoleService _consoleService;

        private readonly List<string> _options = new List<string>
        {
            "Login",
            "Create new Account",
            "Exit "
        };

        private readonly List<string> _optionsAfterLogin = new List<string>
        {
            "Deposit funds into the account",
            "Withdraw funds from the account",
            "Display account balance",
            "Display account history",
             "Convert your balance",
            "Log out"
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
            int option = _consoleService.GetIntegerWithinRange("\nChoose option: ", 1, optionCount);
            Console.Clear();

            return option;
        }

        public void DisplayError(string errorMessage)
        {
            Console.Clear();
            Console.WriteLine("An error occured: ");
            Console.Write($"* {errorMessage} *\n\n");
            Thread.Sleep(3000);
            Console.Clear();
        }

        public void DisplayBalance(decimal balance, string currency)
        {
            Console.Clear();

            bool isBalancePositive = balance >= 0;
            string message = isBalancePositive ? $"\nYour balance is {balance} {currency}\n" : $"\nYou have no funds in your account \n";
            Console.WriteLine(message);
        }

        public void DisplayLoginStatus(bool status, string message, string errorMessage)
        {
            Console.Clear();
            bool isRegisterSucces = status == true;

            string messageToDisplay = isRegisterSucces ? $"\n {message} \n" : $"\n {errorMessage}\n ";

            Console.WriteLine(messageToDisplay);
            Thread.Sleep(3000);
            Console.Clear();
        }   
        
        public void DisplayDepositStatus(string value, string message, string currency)
        {
            Console.Clear();
            Console.WriteLine($"{message} {value} {currency.ToUpper()}");
            Thread.Sleep(3000);
            Console.Clear();
        }
        public void DisplayWithdrawStatus(string value, string message, string currency)
        {
            Console.Clear();
            Console.WriteLine($" {message} {value} {currency.ToUpper()}");
            Thread.Sleep(3000);
            Console.Clear();
        }

        public void DisplayAccountHistory(List<string> operations, string operationDate)
        {
            Console.Clear();
            Console.WriteLine("Account history:");

            foreach (var operation in operations)
            {
                Console.WriteLine($"{operationDate} -{operation}");
            }
        }

    }
}