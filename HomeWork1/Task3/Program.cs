using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 3");
            Console.WriteLine("Calculator");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            Console.WriteLine("You can input math expression.");
            Console.WriteLine("You expression contains binary or unary operators, and one or two operands");
            Console.WriteLine("Type exit if you want to stop program, or help if you need it.");
            do
            {
                Console.Write("Input math expression: ");
                var expression = Console.ReadLine();
                if (expression == "exit") break;
                else if (expression == "help") PrintHelp();
                else
                {
                    int index = 0;
                    double result = 0;
                    int countOperands = 0;
                    while (index < expression.Length)
                    {

                        if (Char.IsDigit(expression[index]))
                        {
                            try
                            {
                                GetResult(ref result, expression, ref index, '+');
                            }
                            catch (InvalidCastException)
                            {
                                Console.WriteLine("Your input consists wrong number.");
                                break;
                            }

                            countOperands++;
                        }
                        else if (IsOperator(expression[index]))
                        {
                            index++;
                            try
                            {
                                GetResult(ref result, expression, ref index, expression[index - 1]);
                            }
                            catch (InvalidCastException)
                            {
                                Console.WriteLine("Your input has wrong number.");
                                break;
                            }
                            catch (DivideByZeroException)
                            {
                                Console.WriteLine("You cannot divide by zero!");
                                break;
                            }
                            countOperands++;
                            if (countOperands == 2 && index == expression.Length) Console.WriteLine(result);
                            else
                            {
                                Console.WriteLine("Input is wrong. You should buy PRO version of the application for such actions!");
                                break;
                            }
                        }
                        else if (IsBinaryBitOperator(expression[index]))
                        {
                            if (countOperands == 1)
                            {
                                index++;
                                GetResult(ref result, expression, ref index, expression[index - 1]);
                                if (index == expression.Length)
                                {
                                    Console.WriteLine((int)result);
                                }
                                else
                                {
                                    Console.WriteLine("Wrong Input!");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Wrong input!");
                                break;
                            }
                            countOperands++;
                        }
                    }
                }
            } while (true);
            Console.ReadKey();
        }

        static void PrintHelp()
        {
            Console.WriteLine("You must type only one or two operands.");
            Console.WriteLine("Binary operators: a+b, a-b, a*b, axb, a/b, a\\b, a%b, a pow b, where a,b - integers.");
            Console.WriteLine("Binary bit operators: a&b, a|b, a^b, where operands are only positive.");
            Console.WriteLine("Unary bit operator: !a, where operand is only positive.");
            Console.WriteLine("Unary operators: a!(factorial), a(echo mode), -a");
        }

        static bool IsOperator(char c)
        {
            if (c == '+' || c == '-' ||
                c == '*' || c == 'x' ||
                c == '/' || c == '\\' ||
                c == '%') return true;
            return false;
        }

        static bool IsBinaryBitOperator(char c)
        {
            if (c == '&' || c == '|' || c == '^') return true;
            return false;
        }
        static void GetResult(ref double result, string expression, ref int index, char Operator)
        {
            string number = "";
            double currentOperand;
            do
            {
                number += expression[index];
                ++index;
            } while (index < expression.Length &&
                     (Char.IsDigit(expression[index]) || expression[index] == '.'));
            if (!Double.TryParse(number, out currentOperand))
                throw new InvalidCastException();

            result = DoOperation(result, currentOperand, Operator);
        }
        static double DoOperation(double Operand1, double Operand2, char Operator)
        {
            if (Operator == '+') return Operand1 + Operand2;
            else if (Operator == '-') return Operand1 - Operand2;
            else if (Operator == '*' || Operator == 'x') return Operand1 * Operand2;
            else if (Operator == '\\' || Operator == '/') return Operand1 * Operand2;
            else if (Operator == '%') return Operand1 % Operand2;
            else if (Operator == '&') return (int)Operand1 & (int)Operand1;
            else if (Operator == '|') return (int)Operand1 | (int)Operand2;
            else if (Operator == '^') return (int)Operand1 ^ (int)Operand2;
            else return 0;
        }
    }

}
