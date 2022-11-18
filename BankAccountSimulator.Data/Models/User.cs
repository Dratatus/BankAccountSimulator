using System.Collections.Generic;

namespace BankAccountSimulator.Data.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }

        public AccountHistory accountHistory { get; set; }

        public User()
        {
        }
    }
}