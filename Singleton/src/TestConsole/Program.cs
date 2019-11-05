using System;
using System.Threading.Tasks;
using Threenine.Print;
using Threenine.Print.GenericLazy;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            for (int i = 0; i < 12; i++)
            {
                Spooler.Instance.Queue.Add(new PrintQueueItem{ DocumentName = $"test-document-{i}"});
               
            }

          
            foreach (var queueItem in Spooler.Instance.Queue)
            {
                Console.WriteLine(queueItem.DocumentName);
            }
        }
    }
}