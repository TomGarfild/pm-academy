using System;
using Library;

namespace Task_4._2
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentService paymentService = new PaymentService();
            for (int i = 0; i < 100; i++)
            {
                paymentService.StartDeposit(1, "UAH");
            }
            paymentService.StartDeposit(3001, "UAH");
            paymentService.StartDeposit(10000, "UAH");
            paymentService.StartDeposit(7000, "UAH");
            paymentService.StartWithdrawal(3001, "UAH");
            paymentService.StartDeposit(500, "USD");
            paymentService.StartDeposit(500, "USD");
            
        }
    }
}
