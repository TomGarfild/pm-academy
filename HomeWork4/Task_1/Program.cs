using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Channels;

namespace Task_1
{
    class Program
    {
        static void Main()
        {
            var start = DateTime.UtcNow;
            Result result;
            try
            {
                var json = File.ReadAllText("settings.json");
                var settings = JsonSerializer.Deserialize<Settings>(json);
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
                    Duration = (DateTime.UtcNow - start).ToString(),
                    Primes = null

                };
            }
            var jsonSerialize = JsonSerializer.Serialize(result);
            File.WriteAllText("result.json", jsonSerialize);
        }

        static int[] GetPrimes(int low, int high)
        {
            var primes = new List<int>();
            for (int i = low; i < high; i++)
            {
                if (i < 2) continue;
                var isPrime = true;
                for (int j = 2; j*j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if(isPrime) primes.Add(i);
            }
            return primes.ToArray();
        }
    }
}
