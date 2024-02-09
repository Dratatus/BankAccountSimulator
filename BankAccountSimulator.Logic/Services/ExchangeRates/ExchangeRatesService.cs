using BankAccountSimulator.Data.Models.Currencies;
using BankAccountSimulator.Data.Repositories.Currencies;
using BankAccountSimulator.Data.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountSimulator.Logic.Services.ExchangeRates
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserRepository _userRepository;

        private readonly List<ExchangeRate> _exchangeRates;

        public ExchangeRatesService(ICurrencyRepository currencyRepository, IUserRepository userRepository)
        {
            _currencyRepository = currencyRepository;
            _userRepository = userRepository;

            var currencyPLN = GetCurrencyByCode("PLN");
            var currencyEUR = GetCurrencyByCode("EUR");
            var currencyGBP = GetCurrencyByCode("GBP");
            var currencyUSD = GetCurrencyByCode("USD");

            _exchangeRates = new List<ExchangeRate>
            {
                new ExchangeRate { BaseCurrency = currencyPLN, CurrencyToGet = currencyEUR, Rate = 0.21M },
                new ExchangeRate { BaseCurrency = currencyPLN, CurrencyToGet = currencyUSD, Rate = 0.22M },
                new ExchangeRate { BaseCurrency = currencyPLN, CurrencyToGet = currencyGBP, Rate = 0.19M },
                new ExchangeRate { BaseCurrency = currencyPLN, CurrencyToGet = currencyPLN, Rate = 1.00M },

                new ExchangeRate { BaseCurrency = currencyEUR, CurrencyToGet = currencyPLN, Rate = 4.22M },
                new ExchangeRate { BaseCurrency = currencyEUR, CurrencyToGet = currencyGBP, Rate = 0.87M },
                new ExchangeRate { BaseCurrency = currencyEUR, CurrencyToGet = currencyEUR, Rate = 1.00M },
                new ExchangeRate { BaseCurrency = currencyEUR, CurrencyToGet = currencyUSD, Rate = 1.03M },

                new ExchangeRate { BaseCurrency = currencyUSD, CurrencyToGet = currencyPLN, Rate = 4.55M },
                new ExchangeRate { BaseCurrency = currencyUSD, CurrencyToGet = currencyUSD, Rate = 1.00M },
                new ExchangeRate { BaseCurrency = currencyUSD, CurrencyToGet = currencyEUR, Rate = 0.98M },
                new ExchangeRate { BaseCurrency = currencyUSD, CurrencyToGet = currencyGBP, Rate = 0.84M },

                new ExchangeRate { BaseCurrency = currencyGBP, CurrencyToGet = currencyPLN, Rate = 5.60M },
                new ExchangeRate { BaseCurrency = currencyGBP, CurrencyToGet = currencyEUR, Rate = 1.15M },
                new ExchangeRate { BaseCurrency = currencyGBP, CurrencyToGet = currencyGBP, Rate = 1.00M },
                new ExchangeRate { BaseCurrency = currencyGBP, CurrencyToGet = currencyUSD, Rate = 1.19M },

            };
        }

        public decimal GetExchangeRate(string currencyToPurchase, string currencyToSell)
        {
            string currencyToPurchaseUpper = currencyToPurchase.ToUpper();
            string currencyToSellUpper = currencyToSell.ToUpper();

            var exchangeRate = _exchangeRates.Single(er => er.BaseCurrency.Code == currencyToPurchaseUpper && er.CurrencyToGet.Code == currencyToSellUpper);

            return exchangeRate.Rate;
        }


        public decimal GetAmonuntOfExchangingMoney(string currencyToPurchase, string currencyToSell, decimal amountOfmoney)
        {
            decimal exchangeRate = GetExchangeRate(currencyToPurchase, currencyToSell);

            decimal amonuntOfExchangingMoney = exchangeRate * amountOfmoney;

            return amonuntOfExchangingMoney;
        }

        public bool CurrencyExists(string currencyCode)
        {
            string currencyCodeToUpper = currencyCode.ToUpper();
            bool currencyExists = _exchangeRates.Any(er => er.BaseCurrency.Code == currencyCodeToUpper);

            return currencyExists;
        }

        public string GetCurrencySymbol(string currencyCode)
        {
            var currencies = _currencyRepository.GetCurrencies();

            string currencyCodeUpper = currencyCode.ToUpper();

            var currency = currencies.Single(c => c.Code == currencyCodeUpper);

            return currency.Symbol;
        }

        private Currency GetCurrencyByCode(string currencyCode)
        {
            var currencies = _currencyRepository.GetCurrencies();

            var currency = currencies.Single(c => c.Code == currencyCode);

            return currency;
        }

        public void SetUserBalance(string username, string baseCurrency, string currencyToGet)
        {
            var user = _userRepository.GetUserByLogin(username);
            bool isCurrencyExist = CurrencyExists(currencyToGet);

            if (string.IsNullOrEmpty(currencyToGet))
            {
                throw new Exception("You must provide value! ");
            }
            else if (!isCurrencyExist)
            {
                throw new Exception("Incorrect currency! ");
            }

            var exchangeRate = _exchangeRates.Single(er => er.BaseCurrency.Code == baseCurrency.ToUpper() && er.CurrencyToGet.Code == currencyToGet.ToUpper());

            decimal exchangeRateToSet = exchangeRate.Rate;
            decimal convertedBalance = user.Balance * exchangeRateToSet;

            user.Balance = Math.Round(convertedBalance, 2);
            user.Currency_.Code = currencyToGet.ToUpper();
        }

        public void ConvertMoneyToBalance(string login, string accountCurrency, string depositCurrency, string operation, decimal value)
        {
            var user = _userRepository.GetUserByLogin(login);

            var exchangeRate = _exchangeRates.Single(er => er.BaseCurrency.Code == depositCurrency.ToUpper() && er.CurrencyToGet.Code == accountCurrency.ToUpper());

            decimal exchangeRateToSet = exchangeRate.Rate;
            decimal valueTooperate = value * exchangeRateToSet;

            if (operation == "deposit")
            {
                user.Balance += Math.Round(valueTooperate, 2);

            }
            else if (operation == "withdraw")
            {
                bool isUserHaveEnoughtMoney = user.Balance - valueTooperate >= 0;
                if (isUserHaveEnoughtMoney)
                {
                    user.Balance -= Math.Round(valueTooperate, 2);
                }
                else
                {
                    throw new Exception("You have no funds in your account! ");
                }
            }
        }

        public string GetUserActualCurrencyAccountType(string username)
        {
            var user = _userRepository.GetUserByLogin(username);

            return user.Currency_.Code.ToUpper();
        }
    }
}