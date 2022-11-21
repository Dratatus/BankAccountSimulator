using System.Collections.Generic;
using BankAccountSimulator.Data.Models.Currencies;

namespace BankAccountSimulator.Data.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }

        public AccountHistory AccountHistory_ { get; set; }

        public Currency Currency_ { get; set; }

        public User()
        {
        }
    }
}