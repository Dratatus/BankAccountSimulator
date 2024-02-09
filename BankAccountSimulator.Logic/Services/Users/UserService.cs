using BankAccountSimulator.Data.Models;
using BankAccountSimulator.Data.Models.Currencies;
using BankAccountSimulator.Data.Repositories.Currencies;
using BankAccountSimulator.Data.Repositories.Users;
using BankAccountSimulator.Logic.Services.ExchangeRates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountSimulator.Logic.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExchangeRatesService _exchangeRatesService;
        public UserService(IUserRepository userRepository, ICurrencyRepository currencyRepository, IExchangeRatesService exchangeRatesService)
        {
            _currencyRepository = currencyRepository;
            _userRepository = userRepository;
            _exchangeRatesService = exchangeRatesService;
        }

        public bool Login(string username, string password)
        {
            bool userExist = _userRepository.UserExists(username);

            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Login not provided! ");
            }

            else if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password not provided! ");
            }
            else if (password.Length < 4)
            {
                throw new Exception("Password must be at least 5 characters! ");
            }
            else if (username.Length < 4)
            {
                throw new Exception("Username must be at least 5 characters! ");
            }
            else if (!userExist)
            {
                throw new Exception("User not found ");
            }


            return true;
        }

        public bool UserExists(string username)
        {
            bool userExists = _userRepository.UserExists(username);

            return userExists;
        }

        public decimal GetUserBalance(string username)
        {
            var users = _userRepository.GetUsers();
            var user = _userRepository.GetUserByLogin(username);

            if (user == null)
            {
                throw new Exception($"User with the login: {username} not found");
            }

            return user.Balance;
        }

        public bool AddNewUser(string login, string password)
        {
            if (login == null || login.Length <= 4)
            {
                throw new Exception("Username cannot be less than 5 characters! ");
            }

            if (password == null || password.Length <= 4)
            {
                throw new Exception("Password cannot be less than 5 characters! ");
            }

            bool userExists = _userRepository.UserExists(login);

            if (userExists)
            {
                throw new Exception($"User with the login {login} already exists! ");
            }

            var user = new User
            {
                Username = login,
                Password = password,
                AccountHistory_ = new AccountHistory { Operation = new List<string>(), OperationDate = "" },
                Currency_ = new Currency { Code = "PLN" }
                
            };

            _userRepository.AddNew(user);

            return true;
        }

        public decimal DepositMoney(string login, string amountOfMoney, string currency)
        {
            string operationType = "deposit";

            var loggedUser = _userRepository.GetUserByLogin(login);
            bool isCurrencyExist = _exchangeRatesService.CurrencyExists(currency);
            string accountCurrency = _exchangeRatesService.GetUserActualCurrencyAccountType(login);

            bool isCorretType = decimal.TryParse(amountOfMoney, out decimal convertedMoney);

            if (!isCorretType)
            {
                throw new Exception("Invalid data type! ");
            }
            else if (isCorretType)
            {
                if (convertedMoney <= 0)
                {
                    throw new Exception("Incorrect amount! ");
                }
            }
            if (!isCurrencyExist)
            {
                throw new Exception("Incorrect currency! ");
            }
            _exchangeRatesService.ConvertMoneyToBalance(login, accountCurrency, currency,operationType, convertedMoney);

            AddAccountHistory(operationType, login, $"-- Deposit -- amount: {convertedMoney} {currency.ToUpper()}");

            return loggedUser.Balance;
        }

        public decimal WithdrawMoney(string login, string aomuntOfMoney, string currency)
        {
            string operationType = "withdraw";

            var loggedUser = _userRepository.GetUserByLogin(login);
            bool isCurrencyExist = _exchangeRatesService.CurrencyExists(currency);
            string accountCurrency = _exchangeRatesService.GetUserActualCurrencyAccountType(login);

            bool isCorretType = decimal.TryParse(aomuntOfMoney, out decimal convertedMoney);

            if (!isCorretType)
            {
                throw new Exception("Invalid amount format! ");
            }
            else if (isCorretType)
            {
                if (convertedMoney <= 0)
                {
                    throw new Exception("Incorrect amount! ");
                }
                else if (convertedMoney > loggedUser.Balance)
                {
                    throw new Exception("Insufficient funds in the account!");
                }
            }
            if (!isCurrencyExist)
            {
                throw new Exception("Incorrect currency! ");
            }
            _exchangeRatesService.ConvertMoneyToBalance(login, accountCurrency, currency, operationType, convertedMoney);

            AddAccountHistory(operationType, login, $"-- Withdraw -- amount: {convertedMoney} {currency.ToUpper()}");

            return loggedUser.Balance;
        }
        public string GetOperationDate()
        {
            string operationDate = DateTime.Now.ToString();

            return operationDate;
        }

        public void AddAccountHistory(string typeOperation, string username, string message)
        {
            var users = _userRepository.GetUsers();
            string operationDate = GetOperationDate();

            var loggedUser = _userRepository.GetUserByLogin(username);

            if (typeOperation == "deposit")
            {
                loggedUser.AccountHistory_.OperationDate = operationDate;
                loggedUser.AccountHistory_.Operation.Add(message);

            }
            else if (typeOperation == "withdraw")
            {
                loggedUser.AccountHistory_.OperationDate = operationDate;
                loggedUser.AccountHistory_.Operation.Add(message);

            }
        }

        public string GetAccountHistoryOperationDate(string username)
        {
            var users = _userRepository.GetUsers();
            var loggedUser = _userRepository.GetUserByLogin(username);

            return loggedUser.AccountHistory_.OperationDate;
        }
        public List<string> GetAccountHistory(string username)
        {
            var users = _userRepository.GetUsers();
            User loggedUser = _userRepository.GetUserByLogin(username); ;

            bool isUserHvaeAccountHistory = string.IsNullOrEmpty(loggedUser.AccountHistory_.OperationDate) || loggedUser.AccountHistory_.Operation == null;

            if (isUserHvaeAccountHistory)
            {
                throw new Exception("Account history is empty!");
            }

            return loggedUser.AccountHistory_.Operation;
        }



    }
}