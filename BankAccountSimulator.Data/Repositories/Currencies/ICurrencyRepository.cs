using BankAccountSimulator.Data.Models.Currencies;
using System.Collections.Generic;

namespace BankAccountSimulator.Data.Repositories.Currencies
{
    public interface ICurrencyRepository
    {
        List<Currency> GetCurrencies();
    }
}
