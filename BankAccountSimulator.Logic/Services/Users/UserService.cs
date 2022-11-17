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

        private User _loggedInUser;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Nie podano loginu!");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Nie podano hasła!");
            }

            // _loggedInUser = ...
            return true; // lub false
        }

        public bool UserExists(string username, string password)
        {
            bool userExists = _userRepository.UserExists(username, password);

            return userExists;
        }

        public decimal GetUserBalance(string login)
        {
            // czy login istnieje w bazie
            //if (user == null)
            //{
            //    // wyjątek
            //}

            var users = _userRepository.GetUsers();

            var user = users.Single(u => u.Login == login);

            return user.Balance;
        }

        public void AddNewUser(string login, string password)
        {
            if (login == null)
            {
                // wyjątek
            }

            if (password == null)
            {
                // wyjątek
            }

            if (login.Length < 5)
            {
                // wyjątek
            }

            // TODO: naprawić, tylko login chyba powinien być unikalny
            bool userExists = _userRepository.UserExists(login, password);

            if (userExists)
            {
                throw new Exception($"Użytkownik o loginie {login} już istnieje");
            }

            var user = new User
            {
                Login = login,
                Password = password
            };

            _userRepository.AddNew(user);
        }
    }
}