using System;
using System.Collections.Generic;
using System.Linq;
using Task_1._2.Comparers;

namespace Task_1._2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.2");
            Console.WriteLine("Players statistics");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            List<Player> players = new List<Player>
            {
                new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain),
                new Player(19, "Peter", "Petrenko", PlayerRank.Private),
                new Player(59, "Ivan", "Ivanov", PlayerRank.General),
                new Player(52, "Ivan", "Snezko", PlayerRank.Lieutenant),
                new Player(34, "Alex", "Zeshko", PlayerRank.Colonel),
                new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain),
                new Player(19, "Peter", "Petrenko", PlayerRank.Private),
                new Player(34, "Vasiliy", "Sokol", PlayerRank.Major),
                new Player(31, "Alex", "Alexeenko", PlayerRank.Major),
            };
            players = players.Distinct(new PlayerEqualityComparer()).ToList();

            Console.WriteLine("Sorted list by Full Name with distinct values");
            players.Sort(new NameComparer());
            players.ForEach(Console.WriteLine);

            Console.WriteLine("\nSorted list by Age with distinct values");
            players.Sort(new AgeComparer());
            players.ForEach(Console.WriteLine);

            Console.WriteLine("\nSorted list by Rank with distinct values");
            players.Sort(new RankComparer());
            players.ForEach(Console.WriteLine);
        }
    }
}
