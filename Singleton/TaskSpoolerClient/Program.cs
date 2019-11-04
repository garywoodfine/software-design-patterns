using System;
using System.Threading.Tasks;
using Threenine.Print;
using Threenine.Print.Simple;

namespace TaskSpoolerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new Task[5];
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                    Spooler.Instance.Queue.Add(new PrintQueueItem {DocumentName = $"Test{i.ToString()}.docx"}));
                tasks[i].Wait(1);

            }

            Task.WaitAll(tasks);

           
           
            foreach (var doc in Spooler.Instance.Queue)
            {
                Console.WriteLine(doc.DocumentName);
               
            }
        }
    }
}