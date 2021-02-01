using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Task2
{
    public class Program
    {
        private static CountdownEvent _waiter;
        private static readonly ThreadSafeHashSet Primes = new ThreadSafeHashSet();
        public static void Main()
        {
            Result result;
            try
            {
                var json = File.ReadAllText("settings.json");
                var settings = JsonSerializer.Deserialize<Settings[]>(json).Where(s => s != null).ToArray();

                _waiter = new CountdownEvent(settings.Length);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                foreach (var s in settings)
                {
                    var thread = new Thread(GetPrimes);
                    thread.Start(new Range(Math.Max(2, s.PrimesFrom), Math.Max(1, s.PrimesTo)));
                }

                _waiter.Wait();

                stopwatch.Stop();

                Primes.Sort();
                result = new Result()
                {
                    Success = true,
                    Error = null,
                    Duration = stopwatch.Elapsed.ToString(),
                    Primes = Primes.ToArray()
                };
            }
            catch (Exception e)
            {
                var errorMessage = e switch
                {
                    FileNotFoundException _ => "settings.json is missing",
                    JsonException _ => "settings.json is corrupted",
                    _ => "Something went wrong"
                };

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

        private static void GetPrimes(object obj)
        {
            var range = (Range) obj;
            GetPrimes(range.Start.Value, range.End.Value);
        }
        private static void GetPrimes(int low, int high)
        {
            for (var i = low; i < high; i++)
            {
                var isPrime = true;
                for (var j = 2; j * j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) Primes.SyncAdd(i);
            }

            _waiter.Signal();
        }
    }
}
