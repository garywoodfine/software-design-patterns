using System;
using System.Threading;
using System.Threading.Tasks;
using Threenine.Print;
using Threenine.Print.FullLazy;

namespace PrintSpoolerClient
{
    class Program
    {
        static void Main(string[] args)
        {
          Parallel.Invoke(
              () => AddToQueue1(),
              () => AddToQueue2()
          );
      
          Console.WriteLine($"There are {Spooler.Instance.Queue.Count}");
          
          foreach (var item in Spooler.Instance.Queue)
          {
              Console.WriteLine(item.DocumentName);
          }

          Console.Read();

        }

        static void AddToQueue1()
        {
            for (int a = 0; a < 2; a++)
            {
                Task.Factory.StartNew( ()=>
                    Spooler.Instance.Queue.Add(new PrintQueueItem {DocumentName = $"TestThread-1-{a}.docx"}),CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).Wait();
               
              //  Spooler.Instance.Queue.Add(new PrintQueueItem {DocumentName = $"TestThread-1-{a.ToString()}.docx"});
               
            }
        }
        
        static void AddToQueue2()
        {
            for (int b = 0; b < 2; b++)
            {
                
                Task.Factory.StartNew( ()=>
                    Spooler.Instance.Queue.Add(new PrintQueueItem {DocumentName = $"TestThread-2-{b}.docx"}),CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).Wait();
               
               // Spooler.Instance.Queue.Add(new PrintQueueItem {DocumentName = $"TestThread-2-{b.ToString()}.docx"});
               
            }
        }
    }
}