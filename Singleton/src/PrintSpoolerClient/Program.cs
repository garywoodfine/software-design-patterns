using System;
using System.Threading;
using System.Threading.Tasks;
using Threenine.Print;
using Threenine.Print.GenericLazy;

namespace PrintSpoolerClient
{
    class Program
    {
        static void Main(string[] args)
        {
          ThreadStart test1 = new Program().AddToQueue1;
          ThreadStart test2 = new Program().AddToQueue2;
          
          Thread[] threads = new Thread[2];
          
          threads[0] = new Thread(test1);
          threads[1] = new Thread(test2);

          foreach (var thread in threads)
          {
              thread.Start();
          }

          Thread.Sleep(20000);
          foreach (var item in Spooler.Instance.Queue)
          {
              Console.WriteLine(item.DocumentName);
          }
           
        }

        public void AddToQueue1()
        {
            for (int a = 0; a < 2; a++)
            {
                Spooler.Instance.Queue.Add(new PrintQueueItem {DocumentName = $"TestThread-1-{a.ToString()}.docx"});
               
            }
        }
        
        public void AddToQueue2()
        {
            for (int b = 0; b < 2; b++)
            {
                Spooler.Instance.Queue.Add(new PrintQueueItem {DocumentName = $"TestThread-2-{b.ToString()}.docx"});
               
            }
        }
    }
}