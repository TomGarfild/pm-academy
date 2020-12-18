using System;
using Library;

namespace Task_1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Account[] accounts = new Account[1_000_000];
            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i] = new Account("UAH");
            }
            
            GetSortedAccounts(accounts);
            Console.WriteLine("First ten accounts are:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(accounts[i].Id);
            }
            Console.WriteLine("\nLast ten accounts are:");
            for (int i = accounts.Length - 10; i < accounts.Length; i++)
            {
                Console.WriteLine(accounts[i].Id);
            }

        }

        static void GetSortedAccounts(Account[] accounts)
        {
            for (int i = 0; i < accounts.Length-1; i++)
            {
                bool changed = false;
                for (int j = 0; j < accounts.Length - i - 1; j++)
                {
                    if (accounts[j].Id > accounts[j + 1].Id)
                    {
                        Swap(ref accounts[j], ref accounts[j+1]);
                        changed = true;
                    }
                }
                if (!changed) break;
            }
        }

        static void Swap(ref Account firstAccount, ref Account secondAccount)
        {
            var tempAccount = firstAccount;
            firstAccount = secondAccount;
            secondAccount = tempAccount;
        }
    }
}
