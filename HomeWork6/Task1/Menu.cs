using System;
using System.Diagnostics;

namespace Task1
{
    public class Menu
    {
        public static void Start()
        {
            Console.WriteLine("Task 1");
            Console.WriteLine("Prime numbers search.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");

            var stopwatch = new Stopwatch();
            while (true)
            {
                var command = GetInputCommand(new string[]
                    {"LINQ (consistent search)", "PLINQ (parallel search)", "Exit"});

                switch (command)
                {
                    case 1:
                    case 2:
                        var low = GetBound("Enter low bound of range: ");
                        var high = GetBound("Enter high bound of range: ");

                        stopwatch.Start();
                        int count = Primes.CountInRange(low, high, command);
                        stopwatch.Stop();

                        Console.WriteLine($"Primes in range [{low}; {high}]: {count}");
                        Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
                        stopwatch.Reset();
                        break;

                    case 3:
                        //Exit from application
                        return;
                }
            }
        }

        private static int GetBound(string message)
        {
            Console.Write(message);
            int number;

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("Wrong input. Try again: ");
            }

            return number;
        }

        private static int GetInputCommand(string[] commands)
        {
            Console.WriteLine("   Menu");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {commands[i]}");
            }

            Console.WriteLine("Enter number of command in menu");
            int command;
            while (!int.TryParse(Console.ReadLine(), out command) || command < 1 || command > commands.Length)
            {
                Console.WriteLine("\nWrong command\n");
                Console.WriteLine("   Menu");
                for (int i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {commands[i]}");
                }

                Console.WriteLine("Enter number of command in menu");
            }

            return command;
        }
    }
}