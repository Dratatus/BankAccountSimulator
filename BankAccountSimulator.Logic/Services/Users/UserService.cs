using BankAccountSimulator.Data.Models;
using BankAccountSimulator.Data.Models.Currencies;
using BankAccountSimulator.Data.Repositories.Currencies;
using BankAccountSimulator.Data.Repositories.Users;
using BankAccountSimulator.Logic.Services.ExchangeRates;
using BankAccountSimulator.Logic.Services.RulesOfCorrectnes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountSimulator.Logic.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRuleOfCorrectnesService _ruleOfCorrectnesService;
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
                throw new Exception("Nie podano loginu! ");
            }

            else if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Nie podano hasła! ");
            }
            else if (password.Length < 4)
            {
                throw new Exception("Hasło musi mieć minimum 5 znaków! ");
            }
            else if (username.Length < 4)
            {
                throw new Exception("Login musi mieć minimum 5 znaków! ");
            }
            else if (!userExist)
            {
                throw new Exception("Nie znaleziono użytokownika ");
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
                throw new Exception($"Nie znaleziono użkownika o loginie {username}");
            }

            return user.Balance;
        }

        public bool AddNewUser(string login, string password)
        {
            if (login == null || login.Length <= 4)
            {
                throw new Exception("nazwa użytkownika nie może mieć mniej niż 5 znaków! ");
            }

            if (password == null || password.Length <= 4)
            {
                throw new Exception("hasło nie może mieć mniej niż 5 znaków! ");
            }

            bool userExists = _userRepository.UserExists(login);

            if (userExists)
            {
                throw new Exception($"Użytkownik o loginie {login} już istnieje! ");
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
                throw new Exception("Niepoprawny typ danych! ");
            }
            else if (isCorretType)
            {
                if (convertedMoney <= 0)
                {
                    throw new Exception("Błędna kwota! ");
                }
            }
            if (!isCurrencyExist)
            {
                throw new Exception("Błędna waluta!");
            }
            _exchangeRatesService.ConvertMoneyToBalance(login, accountCurrency, currency,operationType, convertedMoney);

            AddAccountHistory(operationType, login, $"Dokonano wypłaty - kwota: {convertedMoney} {currency.ToUpper()}");

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
                throw new Exception("Niepoprawny format kwoty!  ");
            }
            else if (isCorretType)
            {
                if (convertedMoney <= 0)
                {
                    throw new Exception("Błędna kwota! ");
                }
                else if (convertedMoney > loggedUser.Balance)
                {
                    throw new Exception("Nie posiadasz wystarczających środków na koncie!");
                }
            }
            if (!isCurrencyExist)
            {
                throw new Exception("Błędna waluta! ");
            }
            _exchangeRatesService.ConvertMoneyToBalance(login, accountCurrency, currency, operationType, convertedMoney);

            AddAccountHistory(operationType, login, $"Dokonano wypłaty - kwota: {convertedMoney} {currency.ToUpper()}");

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
                throw new Exception("Historia konta jest pusta!");
            }

            return loggedUser.AccountHistory_.Operation;
        }



    }
}