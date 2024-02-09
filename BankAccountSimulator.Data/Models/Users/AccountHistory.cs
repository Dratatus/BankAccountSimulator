using System.Collections.Generic;

namespace BankAccountSimulator.Data.Models
{
    public class AccountHistory
    {
        public List<string> Operation { get; set; }

        public string OperationDate { get; set; }
    }
}
