using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Channels;

namespace Library
{
    public abstract class Bank : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        protected string[] AvailableCards;
        protected readonly decimal GeneralLimitAmount;
        protected readonly string GeneralLimitCurrency;
        protected static Dictionary<string, Account> CurrentAmount;
        protected Account CardLimit;
        protected readonly decimal CardLimitAmount;
        protected readonly string CardLimitCurrency;

        static Bank()
        {
            CurrentAmount = new Dictionary<string, Account>();
        }
        
        protected Bank(string name,
            decimal generalLimitAmount, string generalLimitCurrency,
            decimal cardLimitAmount, string cardLimitCurrency) : base(name)
        {
            GeneralLimitAmount = generalLimitAmount;
            GeneralLimitCurrency = generalLimitCurrency;
            CardLimitAmount = cardLimitAmount;
            CardLimitCurrency = cardLimitCurrency;
            CardLimit = new Account(CardLimitCurrency);
            CardLimit.Deposit(CardLimitAmount, CardLimitCurrency);
        }

        public void StartDeposit(decimal amount, string currency)
        {
            try
            {
                CardLimit.Withdraw(amount, currency);
                CardLimit.Deposit(CardLimitAmount-CardLimit.GetBalance(CardLimitCurrency), CardLimitCurrency);
            }
            catch (InvalidOperationException)
            {
                throw new LimitExceededException();
            }
            WithdrawDepositChat("withdraw", amount, currency);
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            try
            {
                CardLimit.Withdraw(amount, currency);
                CardLimit.Deposit(CardLimitAmount - CardLimit.GetBalance(CardLimitCurrency), CardLimitCurrency);
            }
            catch (InvalidOperationException)
            {
                throw new LimitExceededException();
            }
            WithdrawDepositChat("deposit", amount, currency);
        }

        private void WithdrawDepositChat(string transaction, decimal amount, string currency)
        {
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}!");
            Console.WriteLine("Please, enter your login");
            var login = Console.ReadLine();
            Console.WriteLine("Please, enter your password");
            var password = Console.ReadLine();
            if (!CurrentAmount.ContainsKey(login))
            {
                CurrentAmount.Add(login, new Account(GeneralLimitCurrency));
                CurrentAmount[login].Deposit(GeneralLimitAmount, GeneralLimitCurrency);
            }

            try
            {
                CurrentAmount[login].Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                throw new InsufficientFundsException();
            }
            Console.WriteLine($"Hello Mr {login}. Pick a card to proceed the transaction");
            for (int i = 0; i < AvailableCards.Length; i++)
            {
                Console.WriteLine($"{i}. {AvailableCards[i]}");
            }

            int n;
            while (!Int32.TryParse(Console.ReadLine(), out n) || n < 0 || n >= AvailableCards.Length)
            {
                Console.WriteLine("Wrong number!");
                Console.WriteLine("Try again");
            }

            Console.Write($"You’ve {transaction} {amount} {currency}");
            Console.Write(transaction == "deposit" ? " to " : " from ");
            Console.WriteLine($"{AvailableCards[n]} card successfully");
        }
    }
}