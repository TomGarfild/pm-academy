using System;

namespace Library
{
    public class Privet48 : Bank
    {
        public Privet48() : base("Privet48", 10000m, "UAH",
                                                    Decimal.MaxValue, "UAH")
        {
            AvailableCards = new string[]{"Gold", "Platinum"};

            CardLimit = new Account(CardLimitCurrency);
            CardLimit.Deposit(CardLimitAmount, CardLimitCurrency);
        }
    }
}