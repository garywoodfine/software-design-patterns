using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyClient.Rules
{
    public class SnarkyCommentRule : IRule
    {
        public bool Verify(string choice)
        {
            if (!choice.All(char.IsDigit)) return false;
            var answer = Convert.ToInt32(choice);
            return answer == 2;
        }

        public void Execute()
        {
            var index = new Random().Next(responses.Count);
            Console.WriteLine(responses[index]);
        }

        private List<string> responses => new List<string>()
        {
            "To err is human to choose number 2 is plain stupid",
            "Of all the numbers to choose you picked 2. Why?",
            "A bird in the hand is worth 2 in the bush",
            "You picked a couple of ones",
            "Once bitten twice shy",
            "An arch bishop Desmond",
            "A vicar in a two two",
        };
    }
}