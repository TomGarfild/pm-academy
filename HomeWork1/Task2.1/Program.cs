using System;
using System.Collections.Generic;

namespace Task21
{

    class Program
    {
        private static List<string> statistics;
        static void Main(string[] args)
        {
            Console.WriteLine("Task 2.1");
            Console.WriteLine("Game Stone-Scissors-Paper.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            PlayGame();
        }

        static void PlayGame()
        {
            Console.WriteLine("Stone-Scissors-Paper");
            Console.WriteLine("Rules:");
            Console.WriteLine("You are playing with computer.\nYour main aim is to win more times.");
            Console.WriteLine("Victorious combinations:\n\tstone > scissors\n\tscissors > paper\n\tpaper > stone");
            PrintCommands();
            statistics = new List<string>();
            while (true)
            {
                Console.Write("Input command: ");
                var command = Console.ReadLine().ToLower();

                string result;
                switch (command)
                {
                    case "stone":
                        Game(0);
                        break;
                    case "scissors":
                        Game(1);
                        break;
                    case "paper":
                        Game(2);
                        break;
                    case "exit":
                        PrintGameStatistic(statistics);
                        return;
                    default:
                        Console.WriteLine("Your input is wrong.");
                        PrintCommands();
                        Console.WriteLine("Try again.");
                        break;
                }

            }
        }

        static void Game(int command)
        {
            var computer = ComputerChoice();
            PrintComputerChoice(computer);
            var result = GetGameResult(command, computer);
            PrintGameResult(result);
            statistics.Add(result);
        }
        static void PrintCommands()
        {
            Console.WriteLine("Available commands(input in any case):");
            Console.WriteLine("\tstone");
            Console.WriteLine("\tscissors");
            Console.WriteLine("\tpaper");
            Console.WriteLine("\texit\n");
        }

        static int ComputerChoice()
        {
            var random = new Random();
            var figure = random.Next(3);
            return figure;
        }

        static string GetGameResult(int person, int computer)
        {
            //0 - computer wins, 1 - person wins, 2 - draw
            person = (person + 1) % 3;
            computer = (computer + 1) % 3;
            if (person == computer) return "Draw.";
            else if ((computer + 1) % 3 == person) return "Computer wins.";
            else return "You win!!!";
        }

        static void PrintComputerChoice(int comp)
        {
            Console.Write($"Computer's choice: ");
            if (comp == 0) Console.WriteLine("stone");
            else if (comp == 1) Console.WriteLine("scissors");
            else Console.WriteLine("paper");
        }
        static string PrintGameResult(string result)
        {
            Console.WriteLine($"Game result: {result}\n");
            return result;
        }

        static void PrintGameStatistic(List<string> statistics)
        {
            var size = statistics.Count;
            Console.Write($"\nYou have played {size} game");
            if (size != 1) Console.Write("s");
            Console.WriteLine();
            for (int i = 0; i < size; ++i)
            {
                Console.WriteLine($"Game #{i + 1} result: {statistics[i]}");
            }
        }
    }
}
