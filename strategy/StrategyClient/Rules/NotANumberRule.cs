using System;
using System.Linq;

namespace StrategyClient.Rules
{
    public class NotANumberRule : IRule
    {
        public bool Verify(string choice)
        {
            return !choice.All(char.IsDigit);
        }

        public void Execute()
        {
            Console.WriteLine("We said enter a number ");
        }
    }
}