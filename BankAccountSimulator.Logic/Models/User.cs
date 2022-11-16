namespace BankAccountSimulator.Logic.Models
{
    public class User
    {
        public string login { get; set; }

        public string passwd { get; set; }

        public decimal balance { get; set; }

        public List<string> AccountHistory = new List<string>();
    }
}
