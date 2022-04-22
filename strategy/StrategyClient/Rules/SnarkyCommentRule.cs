using System;

namespace StrategyClient.Rules
{
    public class SnarkyCommentRule : IRule
    {
        public bool Verify(string choice)
        {
            return false;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}