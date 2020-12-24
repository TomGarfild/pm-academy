using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Channels;

namespace Task_2._1
{
    public class Program
    {
        private static List<Product> Products;
        private static List<Tag> Tags;
        private static List<Remainder> Inventory;
        static int Main(string[] args)
        {
            Console.WriteLine("Task 2.1");
            Console.WriteLine("ERP Reports Bot");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            Console.WriteLine("You will input command keys");
            try { 
                Products = File.ReadLines("Products.csv").Skip(1).Select(s => new Product(s.Split(';'))).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
                return -1;
            }
            try
            {
                Tags = File.ReadLines("Tags.csv").Skip(1).Select(s => new Tag(s.Split(';'))).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
                return -1;
            }
            try
            {
                Inventory = File.ReadLines("Inventory.csv").Skip(1).Select(s => new Remainder(s.Split(';'))).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
                return -1;
            }

            while (true)
            {
                var command = GetInputCommand(new string[] { "Exit", "Goods", "Remainders" });
                switch (command)
                {
                    case 1:
                        System.Environment.Exit(0);
                        break;
                    case 2:
                        Goods();
                        break;
                    case 3:
                        Remainders();
                        break;
                }
            }

            return 0;
        }

        private static void Goods()
        {
            
            while (true)
            {
                var command = GetInputCommand(new string[] {
                    "Return to general menu",
                    "Product search",
                    "List of all goods sorted by price in ascending order",
                    "List of all goods sorted by price in descending order"
                });
                switch (command)
                {
                    case 1:
                        return;
                    case 2:
                        ProductSearch();
                        break;
                    case 3:
                        var report = Products.OrderBy(x => x.Price).ToList();
                        int i = 1;
                        foreach (var x in report)
                        {
                            Console.WriteLine($"#{i++} {x}");
                        }
                        break;
                    case 4:
                        report = Products.OrderByDescending(x => x.Price).ToList();
                        i = 1;
                        foreach (var x in report)
                        {
                            Console.WriteLine($"#{i++} {x}");
                        }
                        break;
                }
            }
        }

        private static void ProductSearch()
        {
            Console.WriteLine("Input string for search");
            var s = Console.ReadLine()?.ToLower();
            var report = new List<string>();
            Products.Where(x => x.Id.ToLower() == s).ToList().ForEach(p =>
                report.Add($"{p} [{String.Join(", ", Tags.Where(t=> t.ProductId == p.Id).Select(t => t.TagValue))}]"));

            Products.Where(p => p.Brand.ToLower().Contains(s) || p.Model.ToLower().Contains(s)).ToList().ForEach(p => 
                report.Add($"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]"));

            Tags.Where(t => t.TagValue.ToLower().Contains(s)).ToList().ForEach(t => Products.Where(x => x.Id == t.ProductId).ToList().ForEach(p =>
                report.Add($"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]")));

            report = report.Distinct().ToList();
            if (report.Count == 0)
            {
                Console.WriteLine("Results wasn't found");
            }
            for (int i = 0; i < report.Count; i++)
            {
                Console.WriteLine($"#{i + 1} {report[i]}");
            }
        }
        private static void Remainders()
        {
            while (true)
            {
                var command = GetInputCommand(new string[] {
                    "Return to general menu",
                    "Unavailable goods",
                    "Remainders in ascending order",
                    "Remainders in descending order",
                    "Remainders by Id"
                });
                switch (command)
                {
                    case 1:
                        return;
                    case 2:
                        MissingGoods();
                        break;
                    case 3:
                        var productsAndAmount = Products.Select(p => new
                        {
                            product = $"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]",
                            amount = Inventory.Where(x => x.ProductId == p.Id).Select(x => x.RemainingAmount).Sum()
                        }).ToList();
                        productsAndAmount = productsAndAmount.OrderBy(x => x.amount).ToList();
                        int i = 1;
                        foreach(var x in productsAndAmount)
                        {
                            Console.WriteLine($"#{i++} {x.product}, {x.amount}");
                        }
                            
                        break;
                    case 4:
                        var productsAndAmountDescendingOrder = Products.Select(p => new
                        {
                            product = $"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]",
                            amount = Inventory.Where(x => x.ProductId == p.Id).Select(x => x.RemainingAmount).Sum()
                        }).ToList();
                        productsAndAmountDescendingOrder = productsAndAmountDescendingOrder.OrderByDescending(x => x.amount).ToList();
                        i = 1;
                        foreach (var x in productsAndAmountDescendingOrder)
                        {
                            Console.WriteLine($"#{i++} {x.product}, {x.amount}");
                        }
                        break;
                    case 5:
                        RemaindersById();
                        break;
                }
            }
        }

        private static void MissingGoods()
        {
            var report = Products.Where(p => Inventory.All(x => x.ProductId != p.Id || x.RemainingAmount == 0))
                .OrderBy(i => i.Id).ToList();
            for (int i = 0; i < report.Count; i++)
            {
                Console.WriteLine($"#{i + 1} {report[i]}");
            }
        }

        private static void RemaindersById()
        {
            Console.WriteLine("Input id of product for search");
            var id = Console.ReadLine()?.ToLower();
            var report = Inventory.Where(x => x.ProductId.ToLower() == id)
                .Select(x => new
                {
                    location = x.Location,
                    amount = x.RemainingAmount
                }).OrderByDescending(x => x.amount).ToList();
            if (report.Count == 0)
            {
                Console.WriteLine("Results wasn't found");
            }
            for (int i = 0; i < report.Count; i++)
            {
                Console.WriteLine($"#{i + 1} Location: {report[i].location}; Amount: {report[i].amount}");
            }
        }
        private static int GetInputCommand(string[] commands)
        {
            Console.WriteLine("\n   Menu");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {commands[i]}");
            }

            Console.WriteLine("Enter number of command in menu");
            int command;
            while (!Int32.TryParse(Console.ReadLine(), out command) || command < 1 || command > commands.Length)
            {
                Console.WriteLine("\nWrong command");
                Console.WriteLine("   Menu");
                for (int i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {commands[i]}");
                }

                Console.WriteLine("Enter number of command in menu");
            }

            return command;
        }
    }
}
