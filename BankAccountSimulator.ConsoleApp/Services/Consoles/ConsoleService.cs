using BankAccountSimulator.Data.Models;
using BankAccountSimulator.Logic.Services.Users;
using System;

namespace BankAccountSimulator.ConsoleApp.Services.Consoles
{
    public class ConsoleService : IConsoleService
    {
        // Do usunięcia
        private readonly IUserService _userProvider;
        public string GetString(string message)
        {
            Console.Write(message);
            string stringFromUser = Console.ReadLine();
            return stringFromUser;
        }

        public int GetIntegerWithinRange(string message, int rangeFrom, int rangeTo)
        {
            while (true)
            {
                Console.Write($"{message} ({rangeFrom}-{rangeTo}) : ");

                string potentialInteger = Console.ReadLine();
                bool parsingSuccess = int.TryParse(potentialInteger, out int parsedInteger);
                bool isIntegerWithinRange = parsedInteger >= rangeFrom && parsedInteger <= rangeTo;

                if (parsingSuccess && isIntegerWithinRange)
                {
                    return parsedInteger;
                }
            }
        }

        //public void AddNewUser(string login, string paswwd)
        //{
        //    var users = _userProvider.GetUsers();
        //    var userToRegister = _userProvider.GetUserToLogin();

        //    userToRegister = new User
        //    {
        //        Login = login,
        //        Password = paswwd
        //    };

        //    users.Add(userToRegister);
        //}

        //public string GetLogin(string loginMessage)
        //{
        //    Console.Write(loginMessage);
        //    string login = Console.ReadLine();

        //    return login;
        //}

        //public string GetPassword(string passwdMessage)
        //{
        //    Console.Write(passwdMessage);
        //    string passwd = Console.ReadLine();

        //    return passwd;
        //}

        //public decimal DepositMoney(string message, string message2, string errorMessage)
        //{
        //    var userData = _userProvider.GetUserToLogin();
        //    var users = _userProvider.GetUsers();

        //    //User user = users.Single(u => u.login == userData.login && u.passwd == userData.passwd);

        //    Console.WriteLine(message);
        //    string userAmountOfMoney = Console.ReadLine();
        //    bool isCorretType = decimal.TryParse(userAmountOfMoney, out decimal ConvertedMoney);

        //    if (isCorretType)
        //    {
        //        Console.WriteLine(message2 + $" {userData.Balance}");
        //        return userData.Balance;
        //    }
        //    else
        //    {
        //        Console.WriteLine(errorMessage);
        //        return 0;
        //    }
        //}

        //public void DisplayBalance(decimal balance)
        //{
        //    bool isBalancePositive = balance >= 0;

        //    string message = isBalancePositive ? $"\nTwoje saldo wynosi {balance}\n" : $"\nNie posiadasz żadnych środków na koncie \n";

        //    Console.WriteLine(message);
        //}
    }
}