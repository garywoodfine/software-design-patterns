using System;
using Threenine.Print;
using Threenine.Print.Simple;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Spooler.Instance.Queue.Add(new PrintQueueItem{ DocumentName = "poo"});
            
            Spooler.Instance.Queue.Add(new PrintQueueItem{ DocumentName = "poo2"});

          
            foreach (var queueItem in Spooler.Instance.Queue)
            {
                Console.WriteLine(queueItem.DocumentName);
            }
        }
    }
}