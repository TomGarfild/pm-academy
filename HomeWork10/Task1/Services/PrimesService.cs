using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task1.Services
{
    public class PrimesService : IPrimesService
    {
        public async Task<bool> IsPrimeAsync(int number)
        {
            if (number < 2) return false;

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        public async Task<IList<int>> GetPrimesAsync(int @from, int to)
        {
            var primes = new List<int>();
            for (int i = from; i <= to; i++)
            {
                var isPrime = await IsPrimeAsync(i);
                if (isPrime)
                {
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}