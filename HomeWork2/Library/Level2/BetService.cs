using System;

namespace Library
{
    public class BetService
    {
        private decimal Odd;
        private readonly Random Random;

        public BetService()
        {
            Random = new Random();
            GetOdds();
        }

        public float GetOdds()
        {
            
            Odd = Random.Next(101, 2500) / 100m;
            return (float)Odd;
        }

        private bool IsWon()
        {
            int chanse = (int)(100 / Odd);
            if (Random.Next(1, 101) <= chanse)
            {
                return true;
            }
            return false;
        }

        public decimal Bet(decimal amount)
        {
            if (IsWon())
            {
                return amount * Odd;
            }
            return 0;
        }
    }
}