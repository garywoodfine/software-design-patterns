using System;
using System.Linq;

namespace StrategyClient.Rules
{
    public class ColoredTerminalRule : IRule
    {
        public bool Verify(string choice)
        {
            if (!choice.All(char.IsDigit)) return false;
            var answer = Convert.ToInt32(choice);
            return answer == 4;
        }

        public void Execute()
        {
            Console.ForegroundColor
                = ConsoleColor.Blue;
            
            Console.WriteLine("If you see blue you picked 4");
        }
    }
}