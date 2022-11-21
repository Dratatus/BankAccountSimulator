namespace BankAccountSimulator.Data.Models.Currencies
{
    public class ExchangeRate
    {
        public Currency BaseCurrency { get; set; }

        public Currency CurrencyToGet { get; set; }

        public decimal Rate { get; set; }

    }
}
