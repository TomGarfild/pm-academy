using System;

namespace Task11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.1");
            Console.WriteLine("Calculate math expression.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            //birth year
            var b = 2003;
            //birth month
            var c = 9;
            //birth day
            var d = 4;
            Console.WriteLine($"(pow(e, a) + 4*lg({c}))/sqrt({b})*abs(arctg({d}))+5/sin(a)");
            Console.Write("Input a: ");
            var a = Double.Parse(Console.ReadLine());
            var y = f(a, b, c, d);
            Console.WriteLine($"y = {y}");
        }

        static double f(double a, int b, int c, int d)
        {
            return (Math.Pow(Math.E, a) + 4 * Math.Log10(c)) / Math.Sqrt(b) * Math.Abs(Math.Atan(d)) + 5 / Math.Sin(a);
        }
    }
}