using System;
using Library;
namespace Task_1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var EURAccount = new Account("EUR");
            var USDAccount = new Account("USD");
            var UAHAccount = new Account("UAH");
            EURAccount.Deposit(10, "EUR");
            EURAccount.Withdraw(3, "UAH");
            UAHAccount.Deposit(121, "USD");
            try
            {
                USDAccount.Withdraw(5, "USD");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Balance on your account is less than amount to withdraw!");
            }

            try
            {
                var PLNAccount = new Account("PLN");
            }
            catch
            {
                Console.WriteLine("Wrong currency");
            }

            Console.WriteLine("Account with currency {0} has {1} balance", "EUR", EURAccount.GetBalance("EUR"));
            Console.WriteLine("Account with currency {0} has {1} balance", "USD", USDAccount.GetBalance("USD"));
            Console.WriteLine("Account with currency {0} has {1} balance", "UAH", UAHAccount.GetBalance("UAH"));
        }
    }
}
