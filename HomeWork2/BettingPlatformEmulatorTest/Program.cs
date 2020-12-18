using System;
using Library;

namespace BettingPlatformEmulatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BettingPlatformEmulator platform = new BettingPlatformEmulator();
            platform.Start();
        }
    }
}
