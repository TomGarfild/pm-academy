using System;

namespace Library
{
    public class PaymentService
    {
        private PaymentMethodBase[] AvailablePaymentMethod;
        public PaymentService()
        {
            AvailablePaymentMethod = new PaymentMethodBase[]{new CreditCard(), new Privet48(),
                                                            new Stereobank(), new GiftVoucher()};
        }

        public void StartDeposit(decimal amount, string currency)
        {
            
            int index = 1;
            foreach (var availablePaymentMethod in AvailablePaymentMethod)
            {
                if (availablePaymentMethod is ISupportDeposit)
                {
                    Console.WriteLine($"{index++}. {availablePaymentMethod.Name}");
                }
            }
            int number;
            do
            {
                Console.WriteLine("Enter number of payment method you have chosen");
            } while (!Int32.TryParse(Console.ReadLine(), out number) || number < 1 || number >= index);

            var depositPaymentMethod = ChoseDepositPaymentMethod(number);
            var Random = new Random();
            if (Random.Next(100) < 2)
            {
                throw new Exception();
            }
            depositPaymentMethod?.StartDeposit(amount, currency);
        }
        public void StartWithdrawal(decimal amount, string currency)
        {
            
            int index = 1;
            foreach (var availablePaymentMethod in AvailablePaymentMethod)
            {
                if (availablePaymentMethod is ISupportWithdrawal)
                {
                    Console.WriteLine($"{index++}. {availablePaymentMethod.Name}");
                }
            }
            int number;
            do
            {
                Console.WriteLine("Enter number of payment method you have chosen");
            } while (!Int32.TryParse(Console.ReadLine(), out number) || number < 1 || number >= index);
            var  withdrawalPaymentMethod = ChoseWithdrawalPaymentMethod(number);
            var Random = new Random();
            if (Random.Next(100) < 2)
            {
                throw new Exception();
            }
            withdrawalPaymentMethod?.StartWithdrawal(amount, currency);
        }

        private ISupportDeposit ChoseDepositPaymentMethod(int index)
        {
            int currentIndex = 1;
            foreach (var availablePaymentMethod in AvailablePaymentMethod)
            {
                if (availablePaymentMethod is ISupportDeposit && currentIndex++ == index)
                {
                    return (ISupportDeposit)availablePaymentMethod;
                }
            }

            return null;
        }
        private ISupportWithdrawal ChoseWithdrawalPaymentMethod(int index)
        {
            int currentIndex = 1;
            foreach (var availablePaymentMethod in AvailablePaymentMethod)
            {
                if (availablePaymentMethod is ISupportWithdrawal && currentIndex++ == index)
                {
                    return (ISupportWithdrawal)availablePaymentMethod;
                }
            }

            return null;
        }
    }
}