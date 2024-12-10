using System;

namespace ConsoleApp1
{
    internal class Connector
    {
        private readonly IRateLimiter _rateLimiter;

        public Connector(IRateLimiter rateLimiter)
        {
            _rateLimiter = rateLimiter;
        }

        public void MakeRequest(int requestNumber)
        {
            if (_rateLimiter.IsRequestAllowed())
            {
                Console.WriteLine($"Making request {requestNumber}");

                _rateLimiter.MadeRequest();
            }
            else
            {
                Console.WriteLine($"Request {requestNumber} denied. Capacity reached");
            }
        }
    }
}
