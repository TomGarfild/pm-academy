using System;

namespace Task3
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                //dialog mode
                Console.WriteLine("Task 3");
                Console.WriteLine("Calculator");
                Console.WriteLine("Author: Safroniuk Oleksii\n");
                Console.WriteLine("You can input math expression.");
                Console.WriteLine("You expression contains binary or unary operators, and one or two operands.");
                Console.WriteLine("Type exit if you want to stop program, or help if you need it in any case.");
                do
                {
                    Console.Write("Input math expression or command: ");
                    var expression = Console.ReadLine().ToLower();
                    expression = String.Concat(expression.Split(' '));
                    if (expression == "exit") break;
                    else if (expression == "help") PrintHelp();
                    else
                    {
                        try
                        {
                            DoCalculation(expression);
                        }

                        catch (DivideByZeroException)
                        {
                            Console.WriteLine("Division by zero is forbidden!");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Operands should be positive when you use bit operators!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Factorial is too big!");
                        }
                        catch (ArithmeticException)
                        {
                            Console.WriteLine("Wrong input! You can use command help.");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("You should buy PRO version for such math expressions!");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error occurred. Application will stop.");
                            break;
                        }
                    }
                } while (true);
            }
            else
            {
                //command mode
                var expression = String.Concat(args);
                try
                {
                     DoCalculation(expression);
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            return 0;
        }

        static void DoCalculation(string expression)
        {
            int index = 0;
            double result = 0;
            int countOperands = 0;
            int length = expression.Length;

            while (index < length)
            {
 
                if (expression[index] == ' ')
                {
                    index++;
                }
                else if (Char.IsDigit(expression[index]))
                {
                    GetResult(ref result, expression, ref index, '+');

                    countOperands++;
                    if (index == length)
                    {
                        Console.WriteLine(result);
                    }
                }
                else if (IsOperator(expression[index]))
                {
                    index++;
                    if (index == length || (countOperands == 0 && expression[index - 1] != '-'))
                    {
                        throw new ArithmeticException();
                    }

                    GetResult(ref result, expression, ref index, expression[index - 1]);
                    
                    countOperands++;
                    if (countOperands <= 2 && index == length)
                    {
                        Console.WriteLine(result);
                    }
                    else if (countOperands >= 2)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                else if (IsBinaryBitOperator(expression[index]))
                {
                    if (countOperands == 1)
                    {
                        index++;
                        if (index == length)
                        {
                            throw new ArithmeticException();
                        }

                        GetResult(ref result, expression, ref index, expression[index - 1]);

                        if (index == length)
                        {
                            Console.WriteLine((int)result);
                        }
                        else
                        {
                            throw new IndexOutOfRangeException();
                        }
                    }
                    else if (countOperands == 0)
                    {
                        throw new ArithmeticException();
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                    countOperands++;
                }
                else if (expression[index] == '!')
                {
                    if (countOperands == 1 )
                    {
                        if (++index != length)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        //factorial
                        if (result < 0)
                        {
                            throw new ArithmeticException();
                        }

                        Console.WriteLine(GetFactorial((int)result));

                    }
                    else if (countOperands == 0 )
                    {
                        if (index + 1 == length)
                        {
                            throw new ArithmeticException();
                        }

                        index++;
                        GetResult(ref result, expression, ref index, '!');
                        if (index != length)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        Console.WriteLine(result);
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                else if (expression.IndexOf("pow") == index)
                {
                    index += 3;
                    if (index == length || countOperands == 0)
                    {
                        throw new ArithmeticException();
                    }
                    GetResult(ref result, expression, ref index, 'p');

                    if (index == length)
                    {
                        Console.WriteLine(result);
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                else
                {
                    throw new ArithmeticException();
                }
            }
        }
        static void PrintHelp()
        {
            Console.WriteLine("You must type only one or two operands.");
            Console.WriteLine("Binary operators: a+b, a-b, a*b, axb, a/b, a\\b, a%b, a pow b, where a,b - double values.");
            Console.WriteLine("Binary bit operators: a&b, a|b, a^b, where operands are only positive.");
            Console.WriteLine("Unary bit operator: !a, where operand is only positive.");
            Console.WriteLine("Unary operators: a!(factorial), a(echo mode), -a");
        }

        static bool IsOperator(char c)
        {
            if (c == '+' || c == '-' ||
                c == '*' || c == 'x' ||
                c == '/' || c == '\\' ||
                c == '%')
            {
                return true;
            }
            return false;
        }

        static bool IsBinaryBitOperator(char c)
        {
            if (c == '&' || c == '|' || c == '^')
            {
                return true;
            }
            return false;
        }

        static void GetResult(ref double result, string expression, ref int index, char Operator)
        {
            string number = "";
            double currentOperand;
            do
            {
                if (expression[index] == '+')
                {
                    throw new ArithmeticException();
                }
                number += expression[index];
                ++index;
            } while (index < expression.Length &&
                     (Char.IsDigit(expression[index]) || expression[index] == '.'));

            if (!Double.TryParse(number, out currentOperand))
            {
                throw new ArithmeticException();
            }
            result = DoOperation(result, currentOperand, Operator);
        }

        static double DoOperation(double Operand1, double Operand2, char Operator)
        {
            if (Operator == '+')
            {
                return Operand1 + Operand2;
            }
            else if (Operator == '-')
            {
                return Operand1 - Operand2;
            }
            else if (Operator == '*' || Operator == 'x')
            {
                return Operand1 * Operand2;
            }
            else if (Operator == '\\' || Operator == '/')
            {
                if (Operand2 == 0)
                {
                    throw new DivideByZeroException();
                }
                return Operand1 / Operand2;
            }
            else if (Operator == '%')
            {
                return Operand1 % Operand2;
            }
            else if (Operator == '&') 
            {
                if (Operand1 <= 0 || Operand2 <= 0)
                {
                    throw new ArgumentException();
                }
                return (int)Operand1 & (int)Operand2;
            }
            else if (Operator == '|')
            {
                if (Operand1 <= 0 || Operand2 <= 0)
                {
                    throw new ArgumentException();
                }
                return (int)Operand1 | (int)Operand2;
            }
            else if (Operator == '^')
            {
                if (Operand1 <= 0 || Operand2 <= 0)
                {
                    throw new ArgumentException();
                }
                return (int)Operand1 ^ (int)Operand2;
            }
            else if (Operator == '!')
            {
                if (Operand2 <= 0)
                {
                    throw new ArgumentException();
                }
                return ~(int) Operand2;
            }
            else if (Operator == 'p')
            {
                return Math.Pow(Operand1, Operand2);
            }
            else
            {
                throw new Exception();
            }
        }

        static Int64 GetFactorial(int n)
        {
            checked
            {
                Int64 result = 1;
                for (int i = 2; i <= n; ++i) result *= i;
                return result;
            }
            
        }
    }
}
