using System;
using Library;

namespace Task_1._4
{
    class Program
    {
        static void Main(string[] args)
        {
            //This sort will work O(n^2) because Accounts are sorted in almost every case
            //But I checked and this algorithm works pretty quickly when Id's are random
            Account[] accounts = new Account[1_000_000];
            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i] = new Account("UAH");
            }

            GetSortedAccountsByQuickSort(accounts);
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

        static void GetSortedAccountsByQuickSort(Account[] accounts)
        {
            QuickSort(accounts, 0, accounts.Length - 1);
        }

        static int Partition(Account[] accounts, int low, int high)
        {
            var pivot = low - 1;
            for (var i = low; i < high; i++)
            {
                if (accounts[i].Id < accounts[high].Id)
                {
                    pivot++;
                    Swap(ref accounts[pivot], ref accounts[i]);
                }
            }

            pivot++;
            Swap(ref accounts[pivot], ref accounts[high]);
            return pivot;
        }


        static void QuickSort(Account[] accounts, int low, int high)
        {
            if (low < high)
            {
                var Random = new Random();
                var randomNumber = low + Random.Next() % (high - low);
                Swap(ref accounts[randomNumber], ref accounts[low]);
                var pivotIndex = Partition(accounts, low, high);
                QuickSort(accounts, low, pivotIndex - 1);
                QuickSort(accounts, pivotIndex + 1, high);
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
