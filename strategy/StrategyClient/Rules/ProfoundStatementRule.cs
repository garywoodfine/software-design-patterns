using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyClient.Rules
{
    public class ProfoundStatementRule : IRule
    {
        public bool Verify(string choice)
        {
            if (!choice.All(char.IsDigit)) return false;
            var answer = Convert.ToInt32(choice);
            return answer == 3;
        }

        public void Execute()
        {
            var index = new Random().Next(responses.Count);
            Console.WriteLine(responses[index]);
        }
        
        private List<string> responses => new List<string>()
        {
            "All endings are also beginnings. We just don't know it at the time.",
            "Art and love are the same thing: It’s the process of seeing yourself in things that are not you.",
            "The reason birds can fly and we can't is simply because they have perfect faith, for to have faith is to have wings.",
            "Till this moment I never knew myself.",
            "At the center of your being you have the answer; you know who you are and you know what you want.",
            "We can never know what to want, because, living only one life, we can neither compare it with our previous lives nor perfect it in our lives to come.",
            "I told him I believed in hell, and that certain people, like me, had to live in hell before they died, to make up for missing out on it after death, since they didn't believe in life after death, and what each person believed happened to him when he died.",
            "How satisfying it is to leave a mark on a blank surface. To make a map of my movement - no matter how temporary."
            
            
        };
    }
}