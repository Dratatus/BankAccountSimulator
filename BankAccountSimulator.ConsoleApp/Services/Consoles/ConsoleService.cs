using System;
using System.Collections.Generic;
namespace BankAccountSimulator.ConsoleApp.Services.Consoles
 
{
    public class ConsoleService: IConsoleService
    {
        public  int GetIntegerWithinRange(string message, int rangeFrom, int rangeTo)
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
    }
}
