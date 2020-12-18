using System;

namespace Library
{
    public class Stereobank : Bank
    {
        public Stereobank() : base("Stereobank", 7000m, "UAH",
            3000, "UAH")
        {
            AvailableCards = new string[] {"Black", "White", "Iron"};

            CardLimit = new Account(CardLimitCurrency);
            CardLimit.Deposit(CardLimitAmount, CardLimitCurrency);
        }
    }
}