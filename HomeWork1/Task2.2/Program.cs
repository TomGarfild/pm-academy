using System;

namespace Task22
{
    class Program
    {
        static int Main(string[] args)
        {
            int n = args.Length;
            if (n == 0)
            {
                Console.WriteLine("Task 2.2");
                Console.WriteLine("Area calculator");
                Console.WriteLine("Author: Safroniuk Oleksii\n");
                Console.WriteLine("You should input name of the figure and parameters to calculate area.");
                PrintCommands();
                while (true)
                {
                    Console.WriteLine("Input your command in any case");
                    var command = Console.ReadLine().ToLower();
                    if (command == "circle")
                    {
                        Console.Write("Input radius of the circle: ");
                        double radius;
                        if (!Double.TryParse(Console.ReadLine(), out radius) || radius < 0 || radius > 1e150)
                        {
                            Console.WriteLine("Radius is a non-negative double value and not too large.");
                            continue;
                        }

                        Console.WriteLine($"Area of the CIRCLE with radius {radius} = {radius * radius * Math.PI}");
                    }
                    else if (command == "square")
                    {
                        Console.Write("Input length of the square's side: ");
                        double side;
                        if (!Double.TryParse(Console.ReadLine(), out side) || side < 0 || side > 1e150)
                        {
                            Console.WriteLine("Side of the square is a non-negative double value and not too large.");
                            continue;
                        }

                        Console.WriteLine($"Area of the SQUARE with side {side} = {side * side}");
                    }
                    else if (command == "rectangle")
                    {
                        Console.Write("Input rectangle's height: ");
                        double height;
                        if (!Double.TryParse(Console.ReadLine(), out height) || height < 0 || height > 1e150)
                        {
                            Console.WriteLine("Rectangle's height is a non-negative double value and not too large.");
                            continue;
                        }
                        Console.Write("Input rectangle's width: ");
                        double width;
                        if (!Double.TryParse(Console.ReadLine(), out width) || width < 0 || width > 1e150)
                        {
                            Console.WriteLine("Rectangle's width is a non-negative double value and not too large.");
                            continue;
                        }

                        Console.WriteLine($"Area of the RECTANGLE with height {height} and width {width} = {height * width}");
                    }
                    else if (command == "triangle")
                    {
                        Console.Write("Input triangle's side: ");
                        double side;
                        if (!Double.TryParse(Console.ReadLine(), out side) || side < 0 || side > 1e150)
                        {
                            Console.WriteLine("Rectangle's side is a non-negative double value and not too large.");
                            continue;
                        }
                        Console.Write("Input rectangle's perpendicular to this side: ");
                        double perpendicular;
                        if (!Double.TryParse(Console.ReadLine(), out perpendicular) || perpendicular < 0 || perpendicular > 1e150)
                        {
                            Console.WriteLine("Rectangle's perpendicular is a non-negative double value and not too large.");
                            continue;
                        }

                        Console.WriteLine($"Area of the TRIANGLE with side {side} and perpendicular {perpendicular} = {side * perpendicular / 2}");
                    }
                    else if (command == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Your command is wrong.");
                        PrintCommands();
                    }
                }
            }
            else
            {
                var command = args[0].ToLower();
                if (command == "circle")
                {
                    if (n != 2) return -1;
                    double radius;
                    if (!Double.TryParse(args[1], out radius) || radius < 0 || radius > 1e150)
                        return -1;
                    Console.WriteLine(radius * radius * Math.PI);
                }
                else if (command == "square")
                {
                    if (n != 2) return -1;
                    double side;
                    if (!Double.TryParse(args[1], out side) || side < 0 || side > 1e150)
                        return -1;
                    Console.WriteLine(side * side);
                }
                else if (command == "rectangle")
                {
                    if (n != 3) return -1;
                    double height;
                    if (!Double.TryParse(args[1], out height) || height < 0 || height > 1e150)
                        return -1;
                    double width;
                    if (!Double.TryParse(args[2], out width) || width < 0 || width > 1e150)
                        return -1;
                    Console.WriteLine(height * width);
                }
                else if (command == "triangle")
                {
                    if (n != 3) return -1;
                    double side;
                    if (!Double.TryParse(args[1], out side) || side < 0 || side > 1e150)
                        return -1;
                    double perpendicular;
                    if (!Double.TryParse(args[2], out perpendicular) || perpendicular < 0 ||
                        perpendicular > 1e150)
                        return -1;

                    Console.WriteLine(side * perpendicular / 2);
                }
                else return -1;
                Console.ReadKey();
            }

            return 0;
        }

        static void PrintCommands()
        {
            Console.WriteLine("Commands: ");
            Console.WriteLine("-circle;");
            Console.WriteLine("-square;");
            Console.WriteLine("-rectangle;");
            Console.WriteLine("-triangle;");
            Console.WriteLine("-exit.");
        }
    }
}
