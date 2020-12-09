using System;

namespace Task14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.4");
            Console.WriteLine("Find prime numbers.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            Console.Write("Input low bound: ");
            var lowBound = Int32.Parse(Console.ReadLine());
            Console.Write("Input high bound: ");
            var highBound = Int32.Parse(Console.ReadLine());
            for (var i = lowBound; i <= highBound; ++i)
            {
                var isPrime = true;
                if (i < 2) continue;
                for (var k = 2; k * k <= i; ++k)
                {
                    if (i % k == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) Console.WriteLine($"{i} is prime");
            }
        }
    }
}