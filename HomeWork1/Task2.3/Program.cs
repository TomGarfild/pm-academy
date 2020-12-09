using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task23
{
    class Program
    {
        static int Main(string[] args)
        {
            int n = args.Length;
            int[] array;
            bool commandMode = false;
            if (n == 0)
            {
                Console.WriteLine("Task 2.3");
                Console.WriteLine("Array statistics");
                Console.WriteLine("Author: Safroniuk Oleksii\n");
                do
                {
                    Console.WriteLine("Input array length (positive integer)");
                }
                while (!Int32.TryParse(Console.ReadLine(), out n) || n <= 0);
                array = new int[n];
                Console.WriteLine("Now you can input array elements");
                for (int i = 0; i < n; ++i)
                {
                    while (!Int32.TryParse(Console.ReadLine(), out array[i]))
                    {
                        Console.WriteLine("Input integer");
                    }
                }
            }
            else
            {
                commandMode = true;
                array = new int[n];
                for (int i = 0; i < n; ++i)
                {
                    if (!Int32.TryParse(args[i], out array[i]))
                    {
                        return -1;
                    }
                }
            }
            PrintResult(array, commandMode);
            return 0;
        }

        static int GetMin(int[] array)
        {
            int min = array[0];
            for (int i = 1; i < array.Length; ++i)
            {
                if (array[i] < min) min = array[i];
            }
            return min;
        }
        static int GetMax(int[] array)
        {
            int max = array[0];
            for (int i = 1; i < array.Length; ++i)
            {
                if (array[i] > max) max = array[i];
            }
            return max;
        }
        static int GetSum(int[] array)
        {
            int sum = 0;
            foreach (var element in array)
            {
                sum += element;
            }

            return sum;
        }

        static double GetAverage(int[] array)
        {
            return (double)GetSum(array) / array.Length;
        }

        static double GetStandardDeviation(int[] array)
        {
            var average = GetAverage(array);
            var S = 0d;
            foreach (var element in array)
            {
                S += (element - average) * (element - average);
            }

            return Math.Sqrt(S / array.Length);
        }

        static int[] Sort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; ++i)
            {
                for (int j = i + 1; j < array.Length; ++j)
                {
                    if (array[i] > array[j])
                    {
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

            return array;
        }

        static void PrintResult(int[] array, bool commandMode)
        {
            if (!commandMode) Console.Write("Min element: ");
            Console.WriteLine(GetMin(array));
            if (!commandMode) Console.Write("Max element: ");
            Console.WriteLine(GetMax(array));
            if (!commandMode) Console.Write("Sum of elements: ");
            Console.WriteLine(GetSum(array));
            if (!commandMode) Console.Write("Average: ");
            Console.WriteLine(GetAverage(array));
            if (!commandMode) Console.Write("Standard Deviation: ");
            Console.WriteLine(GetStandardDeviation(array));
            if (!commandMode) Console.WriteLine("Sorted array:");
            var sortedArray = Sort(array);
            foreach (var element in sortedArray) Console.Write($"{element} ");
            Console.ReadKey();
        }
    }

}
