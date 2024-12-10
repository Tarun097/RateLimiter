using System;

namespace ConsoleApp1
{
    internal class RequestRateLimiter : IRateLimiter
    {
        private readonly int capacity;
        long[] requestTimestamps;
        int currentRequestIndex = 0;

        public RequestRateLimiter(int capacity)
        {
            this.capacity = capacity;
            requestTimestamps = new long[capacity];
        }

        public void MadeRequest()
        {
            currentRequestIndex = currentRequestIndex % capacity;
            requestTimestamps[currentRequestIndex] = DateTime.Now.Ticks;
            currentRequestIndex++;
        }

        public bool IsRequestAllowed()
        {
            if (currentRequestIndex < capacity)
            {
                return true;
            }
            else
            {
                long oldestRequest = requestTimestamps[currentRequestIndex % capacity];
                long currentTime = DateTime.Now.Ticks;

                if ((currentTime - oldestRequest) > TimeSpan.TicksPerSecond * 60)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
