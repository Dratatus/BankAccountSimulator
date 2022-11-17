using System.Collections.Generic;

namespace BankAccountSimulator.Data.Models
{
    public class User
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }

        public List<string> AccountHistory { get; set; }

        public User()
        {
            AccountHistory = new List<string>();
        }
    }
}