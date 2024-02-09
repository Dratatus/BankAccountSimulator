using BankAccountSimulator.Data.Models.Currencies;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountSimulator.Data.Repositories.Currencies
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly List<Currency> _currencies = new List<Currency>
        {
            new Currency { Name = "złoty", Code = "PLN", Symbol = "zł"},
            new Currency { Name = "dolar", Code = "USD", Symbol = "$"},
            new Currency { Name = "British funt szterling", Code = "GBP", Symbol = "£"},
            new Currency { Name = "euro", Code = "EUR", Symbol = "€"}
        };

        public  List<Currency> GetCurrencies()
        {

            var currencies = _currencies.ToList();

            return currencies;
        }

    }
}
