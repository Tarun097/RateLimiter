namespace ConsoleApp1
{
    internal interface IRateLimiter
    {
        bool IsRequestAllowed(); 
        void MadeRequest();
    }
}
