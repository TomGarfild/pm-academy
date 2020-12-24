using System;
using System.Linq;

namespace Task_1._1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Task 1.1");
            Console.WriteLine("Array statistics.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            Console.WriteLine("After entering array you will get statistics about it.");
            Console.WriteLine("Enter string with integers and separator is comma");
            var input = Console.ReadLine()?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            while (!input!.Any() || !input.All(x => Int32.TryParse(x, out _)) )
            {
                Console.WriteLine("Wrong input!");
                Console.WriteLine("Try again to enter correct string with integers and separator comma");
                input = Console.ReadLine()?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            }

            var array = input.Select(int.Parse).ToArray();
            Console.WriteLine($"Min element: {array.Min()}");
            Console.WriteLine($"Max element: {array.Max()}");
            Console.WriteLine($"Sum of elements: {array.Sum()}");
            var average = array.Average();
            Console.WriteLine($"Average element: {average}");
            var standardDeviation = Math.Sqrt(array.Select(x => (x - average) * (x - average)).Sum() / array.Count());
            Console.WriteLine($"Standard deviation: {standardDeviation}");
            Console.WriteLine(String.Join(" ", array.Distinct().OrderBy(x => x)));
        }
    }
}
