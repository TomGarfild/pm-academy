using System;
using System.Collections.Generic;
using System.Data;

namespace Library
{
    public class GiftVoucher : PaymentMethodBase, ISupportDeposit
    {
        private static Dictionary<String, int> GiftVouchers;

        static GiftVoucher()
        {
            GiftVouchers = new Dictionary<string, int>();
        }
        public GiftVoucher() : base("GiftVoucher")
        {
            GiftVouchers = new Dictionary<string, int>();
        }

        public void StartDeposit(decimal amount, string currency)
        {
            if (amount != 100 && amount != 500 && amount != 1000)
            {
                Console.WriteLine("Wrong gift voucher. Denomination are only 100/500/1000.");
                Console.WriteLine("Try to input correct value.");
            }
            else
            {
                string number;
                Int64 Number;
                do
                {
                    Console.WriteLine("Enter number of your gift voucher(10 digits)");
                    number = Console.ReadLine();
                } while (number.Length != 10 || !Int64.TryParse(number, out Number));

                if (GiftVouchers.ContainsKey(number))
                {
                    throw new InsufficientFundsException();
                }

                GiftVouchers[number] = 1;
            }

        }
    }
}