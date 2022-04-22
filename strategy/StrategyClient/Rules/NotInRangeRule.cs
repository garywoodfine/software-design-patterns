using System.Linq;

namespace StrategyClient.Rules
{
    public class NotInRangeRule : IRule
    {
        public bool Verify(string choice)
        {
            if (choice.All(char.IsDigit))
            {
                
            }
                
            return false;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}