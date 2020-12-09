using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.3");
            Console.WriteLine("Calculate sum of math series.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            int i = 1;
            int birthYear = 2003;
            double epsilon = 1d / birthYear;
            Console.WriteLine($"birth year: {birthYear};\nepsilon: {epsilon};\nsum (1/i*(i+1)) i from {i} to INFINITY\n");
            double last = 1;
            double sum = 0;
            while (last >= epsilon)
            {
                last = 1d / (i * (i + 1));
                sum += last;
                i++;
            }

            Console.WriteLine($"Result: {sum}");
            Console.ReadKey();
        }
    }
}