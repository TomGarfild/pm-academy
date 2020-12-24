using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;


namespace Task_1._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.4");
            Console.WriteLine("Brackets sequence.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            Console.WriteLine("Enter non-empty expression with brackets (), {}, [], <>");
            var expression = Console.ReadLine();
            while (string.IsNullOrEmpty(expression))
            {
                Console.WriteLine("Enter non-empty expression with brackets (), {}, [], <>");
                expression = Console.ReadLine();
            }

            CheckBracketSequenceAndPrintResult(expression);
        }

        static void CheckBracketSequenceAndPrintResult(string expression)
        {
            var stack = new Stack<(char, int)>();
            for (int i = 0; i < expression.Length; i++)
            {
                var character = expression[i];
                if (character == '<' ||
                    character == '(' ||
                    character == '{' ||
                    character == '[')
                {
                    stack.Push((character, i));
                }
                else if (character == '>' ||
                         character == ')' ||
                         character == '}' ||
                         character == ']')
                {
                    if (!stack.TryPop(out var lastOpenBracketAndIndex) ||
                        (lastOpenBracketAndIndex.Item1 + 1 != character && lastOpenBracketAndIndex.Item1 + 2 != character))
                    {
                        // (+1 = );
                        // <,[,{ + 2 = >,],}.
                        Console.WriteLine($"Error at position {i} - bracket {character} doesn't have pair");
                        return;
                    }
                }
            }
            if (stack.Count != 0)
            {
                Console.WriteLine(
                    $"Error at position {stack.Peek().Item2} - bracket {stack.Peek().Item1} doesn't have pair");
                return;
            }
            else
            {
                Console.WriteLine("Expression is correct");
                return;
            }
            
        }
    }
}