using System;
using Library;

namespace Task_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BetService betService = new BetService();
            for (int i = 0; i < 10; i++)
            {
                var odd = betService.GetOdds();
                var resultAmount = betService.Bet(100);
                Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {resultAmount}");
            }
            Console.WriteLine("-------------------------------------------------------------------------");
            for (int i = 0; i < 3; i++)
            {
                var odd = betService.GetOdds();
                while (odd <= 12f)
                {
                    odd = betService.GetOdds();
                }

                var resultAmount = betService.Bet(100);
                Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {resultAmount}");
            }
            Console.WriteLine("-------------------------------------------------------------------------");
            var balance = 10000m;
            var bet = 100m;
            while (balance != 0 && balance < 151000)
            {
                
                var odd = betService.GetOdds();
                if (odd >= 2 && odd <= 4)
                {
                    balance -= bet;
                    var result = betService.Bet(bet);
                    Console.WriteLine($"I’ve bet {bet} USD with the odd {odd} and I’ve earned {result}. My balance {balance + result}");
                    if (result == 0)
                    {
                        bet = Math.Min(2 * bet, balance);
                    }
                    else
                    {
                        bet = 100m;
                        balance += result;
                    }

                    
                }
            }

            Console.WriteLine($"Game over. My balance is {balance}");
        }
    }
}
