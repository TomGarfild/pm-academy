using System;

namespace Task24
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 2.4");
            Console.WriteLine("Game more or less");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            Console.WriteLine("Rules:");
            Console.WriteLine("You should guess random number in your range.");
            Console.WriteLine("You will get tips is your number less or more than correct.");
            Console.WriteLine("Game is over when you guess number or type exit.\t");
            Console.WriteLine("Input low bound from 0 to 1.000.000");
            int low;
            while (!Int32.TryParse(Console.ReadLine(), out low) || low < 0 || low > 1000000)
            {
                Console.WriteLine("You should input non-negative integer and less than 1.000.000!");
            }
            Console.WriteLine($"Input high bound from {low} to 1.000.000");
            int high;
            while (!Int32.TryParse(Console.ReadLine(), out high) || high < low || high > 1000000)
            {
                Console.WriteLine("You should input non-negative integer which is more than low bound and less than 1.000.000!");
            }
            int points = 0;
            int attempts = 0;
            var random = new Random();
            var number = random.Next(low, high + 1);
            var startTime = DateTime.Now;
            do
            {
                Console.Write("Try to guess a number: ");
                int playerNumber;
                var input = Console.ReadLine();
                if (input == "exit") break;
                while (!Int32.TryParse(input, out playerNumber))
                {
                    Console.WriteLine("Input integer");
                    input = Console.ReadLine();
                }
                attempts++;
                if (playerNumber == number)
                {
                    int n = getN(high - low + 1);
                    points = (int)Math.Ceiling(100d * (n - attempts + 1) / n);
                    if (points < 1) points = 1;
                    break;
                }
                else if (playerNumber < number)
                {
                    Console.WriteLine("More");
                }
                else
                {
                    Console.WriteLine("Less");
                }
            } while (true);



            Console.WriteLine($"Points: {points}");
            Console.WriteLine($"Attempts: {attempts}");
            Console.WriteLine($"Game duration: {DateTime.Now.Subtract(startTime)}");
        }

        static int getN(int diff)
        {
            int min = Math.Abs(diff - 1);
            int n = 0;
            for (int i = 1; i <= 20; ++i)
            {
                int pow2i = (int)Math.Pow(2, i);
                if (Math.Abs(diff - pow2i) < min)
                {
                    min = Math.Abs(diff - pow2i);
                    n = i;
                }
            }

            return n;
        }
    }
}
