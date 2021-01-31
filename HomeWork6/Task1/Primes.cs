using System;
using System.Linq;

namespace Task1
{
    public class Primes
    {
        public static int CountInRange(int low, int high, int type)
        {

            if (high < low) return 0;

            low = Math.Max(low, 2);
            int count;

            if (type == 1)
            {
                count = Enumerable.Range(low, high - low + 1)
                    .Count(n => Enumerable.Range(2, n - 2).All(d => n % d != 0));
            }
            else
            {
                count = Enumerable.Range(low, high - low + 1)
                    .AsParallel()
                    .Count(n => Enumerable.Range(2, n - 2).All(d => n % d != 0));
            }

            return count;
        }
    }
}