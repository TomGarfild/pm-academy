using System;
using System.Collections.Generic;
using System.Globalization;

namespace Library
{
    public class BettingPlatformEmulator
    {
        private List<Player> Players;
        private Player ActivePlayer;
        private Account Account;
        private readonly BetService _betService;
        private readonly PaymentService _paymentService;
        public BettingPlatformEmulator()
        {
            Players = new List<Player>();
            Account = new Account("USD");
            _betService = new BetService();
            _paymentService = new PaymentService();
        }

        public void Start()
        {
            while (true)
            {
                if (ActivePlayer == null)
                {
                    var command = GetInputCommand(new string[] { "Register", "Login", "Stop" });
                    switch (command)
                    {
                        case 1:
                            Register();
                            break;
                        case 2:
                            Login();
                            break;
                        case 3:
                            Exit();
                            break;
                    }
                }
                else
                {
                    var command = GetInputCommand(new string[] { "Deposit", "Withdraw", "GetOdds", "Bet","Logout" });
                    switch (command)
                    {
                        case 1:
                            Deposit();
                            break;
                        case 2:
                            Withdraw();
                            break;
                        case 3:
                            var odd = _betService.GetOdds();
                            Console.WriteLine($"Odd on event is {odd}");
                            break;
                        case 4:
                            Bet();
                            break;
                        case 5:
                            Logout();
                            break;
                    }
                }
            }
        }

        private void Exit()
        {
            System.Environment.Exit(0);
        }

        private void Register()
        {
            Console.WriteLine("Enter your name, please");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter your Last name, please");
            var lastName = Console.ReadLine();
            Console.WriteLine("Enter your email, please");
            var email = Console.ReadLine();
            Console.WriteLine("Enter your password, please");
            var password = Console.ReadLine();
            Console.WriteLine("Enter your currency, please");
            var currency = Console.ReadLine();
            while (currency != "EUR" && currency != "USD" && currency != "UAH" )
            {
                Console.WriteLine("Enter correct currency(EUR, USD, UAH), please");
                currency = Console.ReadLine();
            }
            Players.Add(new Player(firstName, lastName, email, password, currency));
            Console.WriteLine("\nRegistration was successful.\nNow you can login");
        }

        private void Login()
        {
            Console.WriteLine("Enter your email, please");
            var email = Console.ReadLine();
            Console.WriteLine("Enter your password, please");
            var password = Console.ReadLine();
            foreach (var Player in Players)
            {
                if (Player.Email == email && Player.IsPasswordValid(password))
                {
                    ActivePlayer = Player;
                    Console.WriteLine("\nLogin was successful");
                    return;
                }
            }

            Console.WriteLine("\nSuch user doesn't exist. Try to register at first or input login information again!");
        }

        private void Logout()
        {
            ActivePlayer = null;
            Console.WriteLine("You was logged out");
        }

        private void Deposit()
        {
            
            Console.WriteLine("Enter amount of money you want to deposit");
            decimal amount;
            while (!Decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine("Wrong number!");
                Console.WriteLine("Enter amount of money you want to deposit");
            }
            Console.WriteLine("Enter in what currency you want to deposit");
            var currency = Console.ReadLine();
            try
            {
                _paymentService.StartDeposit(amount, currency);
                ActivePlayer.Deposit(amount, currency);
                Account.Deposit(amount, currency);
                Console.WriteLine("\nDeposit was successful");
            }
            catch (PaymentServiceException)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount or change the payment method");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong. Try again later...");
            }

        }

        private void Withdraw()
        {
            Console.WriteLine("Enter amount of money you want to withdraw");
            decimal amount;
            while (!Decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine("Wrong number!");
                Console.WriteLine("Enter amount of money you want to withdraw");
            }
            Console.WriteLine("Enter in what currency you want to withdraw");
            var currency = Console.ReadLine();
            
            try
            {
                ActivePlayer.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\nThere is insufficient funds on your account");
                return;
            }

            try
            {
                Account.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\nThere is some problem on the platform side. Please try it later");
                ActivePlayer.Deposit(amount, currency);
                return;
            }

            try
            {
                _paymentService.StartWithdrawal(amount, currency);
                Console.WriteLine("\nWithdrawal was successful!");
            }
            catch (PaymentServiceException)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount or change the payment method");
                Account.Deposit(amount, currency);
                ActivePlayer.Deposit(amount, currency);
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong. Try again later...");
                Account.Deposit(amount, currency);
                ActivePlayer.Deposit(amount, currency);
            }
            
        }

        private void Bet()
        {
            Console.WriteLine("Enter amount which you want to bet");
            decimal amount;
            while (!Decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine("Wrong number!");
                Console.WriteLine("Enter amount which you want to bet");
            }

            Console.WriteLine("Enter currency in which is your bet");
            var currency = Console.ReadLine();
            while (currency != "EUR" && currency != "USD" && currency != "UAH")
            {
                Console.WriteLine("Enter correct currency(EUR, USD, UAH), please");
                currency = Console.ReadLine();
            }

            try
            {
                ActivePlayer.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\nNot enough money on your balance");
                return;
            }
            
            var resultAmount = _betService.Bet(amount);
            if (resultAmount != 0)
            {
                ActivePlayer.Deposit(resultAmount, currency);
                Console.WriteLine($"\nYou have won {resultAmount}.");
            }
            else
            {
                Console.WriteLine($"\nYou have lost.");
            }
            

        }
        private int GetInputCommand(string[] commands)
        {
            Console.WriteLine("\n   Menu");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {commands[i]}");
            }

            Console.WriteLine("Enter number of command in menu");
            int command;
            while (!Int32.TryParse(Console.ReadLine(), out command) && command < 1 && command > commands.Length)
            {
                Console.WriteLine("\nWrong command");
                Console.WriteLine("   Menu");
                for (int i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {commands[i]}");
                }

                Console.WriteLine("Enter number of command in menu");
            } 

            return command;
        }
    }
}