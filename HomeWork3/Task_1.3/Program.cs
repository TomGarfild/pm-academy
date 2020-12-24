using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;

namespace Task_1._3
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.3");
            Console.WriteLine("Task on dictionary");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            Console.WriteLine("You will input data in console and see it after your actions");
            do
            {
                int N;
                Console.WriteLine("Enter number of elements");
                while (!Int32.TryParse(Console.ReadLine(), out N) || N <= 0)
                {
                    Console.WriteLine("Wrong input!");
                    Console.WriteLine("Enter number of elements");
                }

                Dictionary<Region, RegionSettings> regionDictionary = new Dictionary<Region, RegionSettings>();
                for (int i = 0; i < N; i++)
                {
                    GetBrandCountryWebSite(out var brand, out var country, out var webSite);
                    while (!regionDictionary.TryAdd(new Region(brand, country), new RegionSettings(webSite)))
                    {
                        Console.WriteLine("You have inputted duplicate. Try again");
                        GetBrandCountryWebSite(out brand, out country, out webSite);
                    }
                }

                PrintDictionary(regionDictionary);
                Console.WriteLine("If you want exit enter Y");
            } while (Console.ReadKey().Key != ConsoleKey.Y);

        }

        private static void GetBrandCountryWebSite(out string brand, out string country, out string webSite)
        {
            Console.WriteLine("Input brand");
            brand = Console.ReadLine();
            Console.WriteLine("Input country");
            country = Console.ReadLine();
            Console.WriteLine("Input WebSite");
            webSite = Console.ReadLine();
        }

        private static void PrintDictionary(Dictionary<Region, RegionSettings> regionDictionary)
        {
            Console.WriteLine("Region dictionary values:");
            foreach (var regionAndRegionSettings in regionDictionary)
            {
                Console.WriteLine(regionAndRegionSettings.Key + " " + regionAndRegionSettings.Value);
            }
        }
    }
}
