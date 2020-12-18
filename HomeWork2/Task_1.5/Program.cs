using System;
using Library;

namespace Task_1._5
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            var player = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "USD");
            //2
            Console.WriteLine($"Login with login {player.Email} and password {"TheP@$$w0rd"} is {player.IsPasswordValid("TheP@$$w0rd")}");
            //3
            Console.WriteLine($"Login with login {player.Email} and password {"TheP@$$wOrd"} is {player.IsPasswordValid("TheP@$$wOrd")}");
            //4
            player.Deposit(100, "USD");
            //5
            player.Withdraw(50, "EUR");
            //6
            try
            {
                player.Withdraw(1000, "USD");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Huge amount of money to withdraw!");
            }
            //7
            try
            {
                var player2 = new Player("Pol", "Bet", "pol777@gmail.com", "TheP@SSW0rd", "PLN");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Wrong currency!");
            }
        }
    }
}
