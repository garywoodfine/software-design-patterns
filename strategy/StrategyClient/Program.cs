using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StrategyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string val;
            Console.Write("Enter a number between 1 & 10 : ");
            val = Console.ReadLine();
            ProcessInput(val);
           
        }

        internal static void ProcessInput(string input)
        {
             
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!typeof(IRule).IsAssignableFrom(type) || !type.IsPublic || type.IsInterface) continue;
                var rule = (IRule)Assembly.GetExecutingAssembly().CreateInstance(type.FullName, false);
                if (rule.Verify(input)) rule.Execute();
            }
        }
    }
}