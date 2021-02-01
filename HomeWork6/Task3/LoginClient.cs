using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;

namespace Task3
{
    public class LoginClient
    {
        public static int Success = 0;
        public static int Failure = 0;

        public static string Login(string login, string password)
        {
            var random = new Random();
            Thread.Sleep((int)(random.NextDouble()*1000));

            if (random.Next(0, 2) == 0)
            {
                Interlocked.Increment(ref Failure);

                return null;
            }

            Interlocked.Increment(ref Success);

            return Guid.NewGuid().ToString();
        }
    }
}