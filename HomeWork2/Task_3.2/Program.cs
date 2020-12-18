using System;
using Library;

namespace Task_3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentService paymentService = new PaymentService();
            paymentService.StartDeposit(50, "USD");
            paymentService.StartWithdrawal(50, "USD");
            paymentService.StartDeposit(50, "USD");
            paymentService.StartWithdrawal(50, "USD");
            paymentService.StartDeposit(50, "USD");
            paymentService.StartDeposit(500, "USD");
        }
    }
}
