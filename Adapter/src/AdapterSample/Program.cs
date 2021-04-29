using System;

namespace AdapterSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var transport = new Transport();
            transport.Commute();
        }
    }
}