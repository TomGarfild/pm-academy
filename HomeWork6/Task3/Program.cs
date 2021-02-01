using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Task3
{
    public class Program
    {
        private static CountdownEvent _waiter;
        public static void Main()
        {
            Console.WriteLine("Task 3");
            Console.WriteLine("Service delivery unique logins.");
            Console.WriteLine("Author: Safroniuk Oleksii\n");

            Console.Write("Enter number of threads: ");
            var numberThreads = int.Parse(Console.ReadLine() ?? string.Empty);

            var storage = ReadData("logins.csv");

            MultiThreadLogin(numberThreads, storage);

            var json = JsonSerializer.Serialize(new Result(LoginClient.Success, LoginClient.Failure));
            File.WriteAllText("result.json", json);
        }

        public static ConcurrentQueue<User> ReadData(string path)
        {
            var storage = new ConcurrentQueue<User>();
            var file = File.ReadAllLines(path);

            foreach (var line in file)
            {
                storage.Enqueue(new User(line.Split(';')));
            }

            return storage;
        }

        public static void MultiThreadLogin(int number, ConcurrentQueue<User> storage)
        {
            _waiter = new CountdownEvent(number);

            for (var i = 0; i < number; i++)
            {
                ThreadPool.QueueUserWorkItem(SingleThreadLogin, storage);
            }

            _waiter.Wait();
        }

        public static void SingleThreadLogin(object storage)
        {
            var data = (ConcurrentQueue<User>) storage;

            while (data.TryDequeue(out var user))
            {
                LoginClient.Login(user.Login, user.Password);
            }

            _waiter.Signal();
        }
    }
}
