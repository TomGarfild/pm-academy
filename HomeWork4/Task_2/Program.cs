using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Task_2
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Task 2");
            Console.WriteLine("Currency converter");
            Console.WriteLine("Author: Safroniuk Oleksii\n");

            Console.WriteLine("Enter initial currency");
            var initialCurrency = Console.ReadLine()?.Trim().ToUpper();
            while (String.IsNullOrWhiteSpace(initialCurrency) || initialCurrency.Length != 3)
            {
                Console.WriteLine("Wrong input. You should enter string length 3");
                initialCurrency = Console.ReadLine()?.Trim().ToUpper();
            }
            Console.WriteLine("Enter desired currency");
            var desiredCurrency = Console.ReadLine()?.Trim().ToUpper();
            while (String.IsNullOrWhiteSpace(desiredCurrency) || desiredCurrency.Length != 3)
            {
                Console.WriteLine("Wrong input. You should enter string length 3");
                desiredCurrency = Console.ReadLine()?.Trim();
            }

            decimal sum;
            do
            {
                Console.WriteLine("Enter non-negative decimal number");
            } while (!Decimal.TryParse(Console.ReadLine(), out sum) || sum < 0);

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"https://bank.gov.ua/NBUStatService/v1/");
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            var response = await httpClient.GetAsync("statdirectory/exchange?json");

            try
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                await File.WriteAllTextAsync("cache.json", json);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Updating currency rate was unsuccessful");
            }

            List<Currency> currencies;
            try
            {
                var json = await File.ReadAllTextAsync("cache.json");
                currencies = JsonConvert.DeserializeObject<List<Currency>>(json);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File with currency rates wasn't found");
                return;
            }
            var date = currencies[0].ExchangeDate;
            currencies.Add(new Currency() { Cc = "UAH", ExchangeDate = date, Rate = 1 });

            decimal initialCurrencyRate;
            try
            {
                initialCurrencyRate = currencies.Where(c => c.Cc == initialCurrency).ToArray()[0].Rate;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"{initialCurrency} doesn't exist in our base");
                return;
            }

            decimal desiredCurrencyRate;
            try
            {
                desiredCurrencyRate = currencies.Where(c => c.Cc == desiredCurrency).ToArray()[0].Rate;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"{desiredCurrency} doesn't exist in our base");
                return;
            }
            decimal rate = Decimal.Round(initialCurrencyRate / desiredCurrencyRate, 4);
            Console.WriteLine($"{sum} {initialCurrency} x {rate} = {rate * sum} {desiredCurrency} ({date})", 2);
        }
    }
}