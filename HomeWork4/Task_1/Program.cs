using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Task_1
{
    class Program
    {
        static void Main()
        {
            Result result;
            try
            {
                var json = File.ReadAllText("settings.json");
                var settings = JsonSerializer.Deserialize<Settings>(json);

                var start = DateTime.UtcNow;
                var primes = GetPrimes(settings.PrimesFrom, settings.PrimesTo);

                result = new Result()
                {
                    Success = true,
                    Error = null,
                    Duration = (DateTime.UtcNow - start).ToString(),
                    Primes = primes
                };

            }
            catch (Exception e)
            {
                string errorMessage;
                if (e is FileNotFoundException) errorMessage = "settings.json is missing";
                else if (e is JsonException) errorMessage = "settings.json is corrupted";
                else errorMessage = "Something went wrong";

                result = new Result
                {
                    Success = false,
                    Error = errorMessage,
                    Duration = "0:00:00",
                    Primes = null

                };
            }
            var jsonSerialize = JsonSerializer.Serialize(result);
            File.WriteAllText("result.json", jsonSerialize);
        }

        static int[] GetPrimes(int low, int high)
        {
            //if low is less than 2 we will start from 2 else from low
            low = Math.Max(low, 2);
            var primes = new List<int>();
            for (int i = low; i < high; i++)
            {
                var isPrime = true;
                for (int j = 2; j * j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) primes.Add(i);
            }
            return primes.ToArray();
        }
    }
}