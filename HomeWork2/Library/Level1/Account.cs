using System;

namespace Library
{
    public class Account
    {
        public int Id { get; }
        private readonly string Currency;
        private decimal Amount;
        private static int LastId;
        private static readonly int MIN_ID;
        private static readonly int MAX_ID;

        private readonly decimal USD_UAH;
        private readonly decimal EUR_UAH;
        private readonly decimal EUR_USD;
        static Account()
        {
            MIN_ID = 100000;
            MAX_ID = 99999999;
            var Random = new Random();
            LastId =  Random.Next(MIN_ID, MAX_ID);
        }
        public Account (string BaseCurrency)
        {
            Id = LastId++;
            if (LastId > MAX_ID) LastId = MIN_ID;

            if (BaseCurrency != "EUR" && BaseCurrency != "USD" && BaseCurrency != "UAH")
            {
                throw new NotSupportedException();
            }
            Currency = BaseCurrency;
            Amount = 0;
            USD_UAH = 28.36m;
            EUR_UAH = 33.63m;
            EUR_USD = 1.19m;
        }

        public void Deposit(decimal amount, string Currency)
        {
            Amount += ConvertToAccountCurrency(amount, Currency);
        }

        public void Withdraw(decimal amount, string Currency)
        {
            var reducerAmount = ConvertToAccountCurrency(amount, Currency);
            if (reducerAmount > Amount) throw new InvalidOperationException();
            Amount -= reducerAmount;
        }

        public decimal GetBalance(string currency)
        {
            switch (Currency)
            {
                case "EUR":
                    if (currency == "USD")
                    {
                        return Amount * EUR_USD;
                    }

                    if (currency == "UAH")
                    {
                        return Amount * EUR_UAH;
                    }

                    break;
                case "USD":
                    if (currency == "EUR")
                    {
                        return Amount / EUR_USD;
                    }

                    if (currency == "UAH")
                    {
                        return Amount * USD_UAH;
                    }

                    break;
                case "UAH":
                    if (currency == "USD")
                    {
                        return Amount / USD_UAH;
                    }

                    if (currency == "EUR")
                    {
                        return Amount / EUR_UAH;
                    }

                    break;
            }
            return Amount;
        }

        private decimal ConvertToAccountCurrency(decimal amount, string Currency)
        {
            switch (this.Currency)
            {
                case "EUR":
                    if (Currency == "USD")
                    {
                        return amount / EUR_USD;
                    }

                    if (Currency == "UAH")
                    {
                        return amount / EUR_UAH;
                    }

                    break;
                case "USD":
                    if (Currency == "EUR")
                    {
                        return amount * EUR_USD;
                    }

                    if (Currency == "UAH")
                    {
                        return amount / USD_UAH;
                    }

                    break;
                case "UAH":
                    if (Currency == "USD")
                    {
                        return amount * USD_UAH;
                    }

                    if (Currency == "EUR")
                    {
                        return amount * EUR_UAH;
                    }

                    break;
            }

            return amount;
        }
    }
}
