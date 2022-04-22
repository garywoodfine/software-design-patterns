using System;
using System.Linq;

namespace StrategyClient.Rules
{
    public class NotInRangeRule : IRule
    {
        public bool Verify(string choice)
        {
            if (choice.All(char.IsDigit))
            {
                var answer = Convert.ToInt32(choice);

                if (answer is < 0 or > 10) return true;

            }
                
            return false;
        }

        public void Execute()
        {
            Console.WriteLine("We said a number between 1 and 10!");
        }
    }
}