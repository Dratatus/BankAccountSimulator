using BankAccountSimulator.Data.Models;

namespace BankAccountSimulator.Logic.Services.RulesOfCorrectnes
{
    public class RuleOfCorrectnesService : IRuleOfCorrectnesService
    {
        public decimal GetCorrectBalnceValue(User loggedUser)
        {
            string balnceToString= loggedUser.Balance.ToString();

            bool isCorrectValue = decimal.TryParse(balnceToString, out decimal checkedAndConvertedBalance);

            if (isCorrectValue)
            {
                return checkedAndConvertedBalance;
            }
            else
            {
                return 0;
            }
        }
    }
}