using System;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int capacity = 100;
            IRateLimiter rateLimiter = new RequestRateLimiter(capacity);
            Connector connector = new Connector(rateLimiter);

            Console.WriteLine("Test 1: Making 101 rapid requests");
            for (int i = 0; i < 101; i++)
            {
                connector.MakeRequest(i+1);
            }

            Console.WriteLine("\nTest 2: Retrying request number 101 again after 1 minute");
            Thread.Sleep(60000); // Sleep for 60 seconds, this will clear out all requests in time window
            connector.MakeRequest(101);
            //time windows now holds 1 request

            Console.WriteLine("\nTest 3: Making additional 9 requests within 1 minute, each after 1 second");
            for (int i = 0; i < 9; i++)
            {
                Thread.Sleep(1000); // Sleep for 1 second
                connector.MakeRequest(i+102); 
            }

            //time windows now holds 10 requests

            Console.WriteLine("\nTest 4: Making a burst of more 100 requests");
            Console.WriteLine("90 should be allowed and 10 should be denied");
            for (int i = 0; i < 100; i++)
            {
                connector.MakeRequest(i+111);
            }

            Console.ReadLine();
        }
    }

}
