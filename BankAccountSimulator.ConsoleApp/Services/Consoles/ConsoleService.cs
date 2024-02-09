using System;

namespace BankAccountSimulator.ConsoleApp.Services.Consoles
{
    public class ConsoleService : IConsoleService
    {
        public string GetString(string message)
        {
            Console.Write(message);
            string stringFromUser = Console.ReadLine();
            return stringFromUser;
        }

        public int GetIntegerWithinRange(string message, int rangeFrom, int rangeTo)
        {
            while (true)
            {
                Console.Write($"{message} ({rangeFrom}-{rangeTo}) : ");

                string potentialInteger = Console.ReadLine();
                bool parsingSuccess = int.TryParse(potentialInteger, out int parsedInteger);
                bool isIntegerWithinRange = parsedInteger >= rangeFrom && parsedInteger <= rangeTo;

                if (parsingSuccess && isIntegerWithinRange)
                {
                    return parsedInteger;
                }
            }
        }

        public decimal GetDecimal(string message, string errorMessage = null)
        {
            while (true)
            {
                Console.Write(message);
                string stringFromUser = Console.ReadLine();
                bool parsingResult = decimal.TryParse(stringFromUser, out decimal decimalFromUser);
                bool errorMessageWasPassed = errorMessage != null;

                if (parsingResult)
                {
                    return decimalFromUser;
                }
                else if (errorMessageWasPassed)
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }
    }
}