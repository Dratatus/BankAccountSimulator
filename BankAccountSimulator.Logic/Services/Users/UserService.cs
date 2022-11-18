using BankAccountSimulator.Data.Models;
using BankAccountSimulator.Data.Repositories.Users;
using BankAccountSimulator.Logic.Services.RulesOfCorrectnes;
using System;
using System.Linq;

namespace BankAccountSimulator.Logic.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRuleOfCorrectnesService _ruleOfCorrectnesService;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string username, string password)
        {
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
            var user = users.Single(u => u.Username == username);

            if (user == null)
            {
                throw new Exception($"Nie znaleziono użkownika o loginie {username}");
            }
            return user.Balance;
        }

        public bool AddNewUser(string login, string password)
        {
            if (login == null || login.Length < 4)
            {
                throw new Exception("nazwa użytkownika nie może mieć mniej niż 5 znaków! ");
            }

            if (password == null || password.Length < 4)
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
                Password = password
            };

            _userRepository.AddNew(user);

            return true;
        }

        public decimal DepositMoney(string login, string amountOfMoney)
        {
            var users = _userRepository.GetUsers();

            User loggedUser = users.Single(u => u.Username == login);

            bool isCorretType = decimal.TryParse(amountOfMoney, out decimal ConvertedMoney);
            if (!isCorretType)
            {
                throw new Exception("Proszę podać kwotę do wpłaty z dokładnością do dwóch miejsc po przecinku! ");
            }
            else if (isCorretType)
            {
                if (ConvertedMoney <= 0)
                {
                    throw new Exception("Błędna kwota! ");
                }
            }
            string operationDate = GetOperationDate();

            loggedUser.Balance += ConvertedMoney;
            loggedUser.AccountHistory.Operation.Add($"{operationDate}");

            return loggedUser.Balance;
        }

        public decimal WithdrawMoney(string login, string aomuntOfMoney)
        {
            var users = _userRepository.GetUsers();

            User loggedUser = users.Single(u => u.Username == login);

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
            loggedUser.Balance -= convertedMoney;

            return loggedUser.Balance;

        }
        public string GetOperationDate()
        {
            string operationDate = DateTime.Now.ToString();

            return operationDate;
        }

        public void AddAccountHistory(string typeOperation, string login, string message)
        {
            var users = _userRepository.GetUsers();
            string operationDate = GetOperationDate();

            User loggedUser = users.Single(u => u.Username == login);



            if (typeOperation == "deposit")
            {
                loggedUser.AccountHistory.OperationDate = operationDate;
                loggedUser.AccountHistory.Operation.Add(message);

            }
            else if (typeOperation == "withdraw")
            {
                loggedUser.AccountHistory.OperationDate = operationDate;
                loggedUser.AccountHistory.Operation.Add(message);

            }
        }
    }
}