using System;
using System.Threading;
using Library;

namespace Task_4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new LimitExceededException();
            }
            catch (InsufficientFundsException)
            {
                Console.WriteLine($"{nameof(InsufficientFundsException)} was handled.");
            }
            catch (LimitExceededException)
            {
                Console.WriteLine($"{nameof(LimitExceededException)} was handled.");
            }
            catch (PaymentServiceException)
            {
                Console.WriteLine($"{nameof(PaymentServiceException)} was handled.");
            }
            catch (Exception)
            {
                Console.WriteLine($"{nameof(Exception)} was handled.");
            }
        }
    }
}
