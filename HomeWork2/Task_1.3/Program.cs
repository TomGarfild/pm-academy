using System;
using Library;

namespace Task_1._3
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
            GetAccount(111178, accounts);

        }

        static void GetSortedAccounts(Account[] accounts)
        {
            for (int i = 0; i < accounts.Length - 1; i++)
            {
                bool changed = false;
                for (int j = 0; j < accounts.Length - i - 1; j++)
                {
                    if (accounts[j].Id > accounts[j + 1].Id)
                    {
                        Swap(ref accounts[j], ref accounts[j + 1]);
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

        static void GetAccount(int Id, Account[] accounts)
        {
            int left = 0, right = accounts.Length-1;
            int attempts = 0;
            while (right >= left)
            {
                attempts++;
                int mid = left + (right - left) / 2;
                if (accounts[mid].Id == Id)
                {
                    Console.WriteLine($"{Id} was found at index {mid} by {attempts} tries");
                    return;
                }

                if (accounts[mid].Id > Id)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            Console.WriteLine($"There is no account {Id} in the list");
        }
    }
}
