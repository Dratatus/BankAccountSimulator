using BankAccountSimulator.Data.Models.Currencies;

namespace BankAccountSimulator.Logic.Services.ExchangeRates
{
    public interface IExchangeRatesService
    {
        void ConvertMoneyToBalance(string login, string accountCurrency, string depositCurrency, string operation, decimal value);
        bool CurrencyExists(string currencyCode);
        decimal GetAmonuntOfExchangingMoney(string currencyToPurchase, string currencyToSell, decimal amountOfmoney);
        string GetCurrencySymbol(string currencyCode);
        decimal GetExchangeRate(string currencyToPurchase, string currencyToSell);
        string GetUserActualCurrencyAccountType(string username);
        void SetUserBalance(string username, string baseCurrency, string currencyToGet);

    }
}
