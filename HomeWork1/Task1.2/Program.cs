using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.2");
            Console.WriteLine("Calculate margin and teams chances' to win.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            Console.Write("Input home team name: ");
            var name1 = Console.ReadLine();
            Console.Write("Input guest team name: ");
            var name2 = Console.ReadLine();
            Console.Write($"Coefficient on {name1}'s victory: ");
            var w1 = Double.Parse(Console.ReadLine());
            Console.Write("Coefficient on draw: ");
            var x = Double.Parse(Console.ReadLine());
            Console.Write($"Coefficient on {name2}'s victory: ");
            var w2 = Double.Parse(Console.ReadLine());
            var firstChance = 1 / w1;
            var drawChance = 1 / x;
            var secondChance = 1 / w2;
            var margin = 1 - 1 / (firstChance + drawChance + secondChance);
            Console.WriteLine();
            Console.WriteLine($"Chance of {name1}'s victory: {Math.Round(100 * (firstChance), 1)}%");
            Console.WriteLine($"Chance of {name2}'s victory: {Math.Round(100 * (secondChance), 1)}%");
            Console.WriteLine($"Chance of draw: {Math.Round(100 * (drawChance), 1)}%");
            Console.WriteLine($"Margin: {Math.Round(100 * margin, 1)}%");
            Console.ReadKey();
        }
    }
}