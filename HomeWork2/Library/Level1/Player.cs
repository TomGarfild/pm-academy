using System;

namespace Library
{
    public class Player
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        private readonly string Password;
        private readonly Account Account;
        private static int LastId;
        private static readonly int MIN_ID;
        private static readonly int MAX_ID;
        static Player()
        {
            MIN_ID = 100000;
            MAX_ID = 99999999;
            var Random = new Random();
            LastId = Random.Next(MIN_ID, MAX_ID);
        }
        public Player(string firstName, string lastName, string email, string password, string Currency)
        {
            Id = LastId++;
            if (LastId > MAX_ID) LastId = MIN_ID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Account = new Account(Currency);
        }

        public bool IsPasswordValid(string password) => Password == password;
        public void Deposit(decimal amount, string Currency) => Account.Deposit(amount, Currency);
        public void Withdraw(decimal amount, string Currency) => Account.Withdraw(amount, Currency);
    }
}
