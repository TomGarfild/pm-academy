using System;
using System.ComponentModel;

namespace Library
{
    public class CreditCard : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        private Account Limit;
        private readonly decimal LimitAmount;
        private readonly string LimitCurrency;
        public CreditCard() : base("CreditCard")
        {
            LimitAmount = 3000;
            LimitCurrency = "UAH";
            Limit = new Account(LimitCurrency);
            Limit.Deposit(LimitAmount, LimitCurrency);
        }
        public void StartDeposit(decimal amount, string currency)
        {
            try
            {
                Limit.Withdraw(amount, currency);
                Limit.Deposit(LimitAmount - Limit.GetBalance(LimitCurrency), LimitCurrency);
            }
            catch (InvalidOperationException)
            {
                throw new LimitExceededException();
            }
            var number = GetCardNumber();
            Console.WriteLine("Enter expire date of your card, please");
            var expireDate = Console.ReadLine();
            int month, year;
            while (expireDate.Length != 5 || expireDate[2] != '/' ||
                   !Int32.TryParse(expireDate.Substring(0, 2), out month) || month < 1 || month > 12 ||
                   !Int32.TryParse(expireDate.Substring(3, 2), out year) || year < 0)
            {
                Console.WriteLine("Enter correct expire date of your card in format mm/yy, please");
                expireDate = Console.ReadLine();
            }
            Console.WriteLine("Enter CVV of your card, please");
            var CVV = Console.ReadLine();
            int cvv;
            while (CVV.Length != 3 || !Int32.TryParse(CVV, out cvv))
            {
                Console.WriteLine("Enter correct CVV of your card(3 digits), please");
                CVV = Console.ReadLine();
            }
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            try
            {
                Limit.Withdraw(amount, currency);
                Limit.Deposit(LimitAmount - Limit.GetBalance(LimitCurrency), LimitCurrency);
            }
            catch (InvalidOperationException)
            {
                throw new LimitExceededException();
            }
            var number = GetCardNumber();
        }

        private string GetCardNumber()
        {
            Console.WriteLine("Enter your credit number, please");
            var number = Console.ReadLine();
            while (number.Length != 16 || (number[0] != '5' && number[0] != '4'))
            {
                Console.WriteLine("Enter correct credit number, please.");
                Console.WriteLine("You must enter 16 digits and first must be 4 or 5");
                number = Console.ReadLine();
            }

            return number;
        }
        
    }
}