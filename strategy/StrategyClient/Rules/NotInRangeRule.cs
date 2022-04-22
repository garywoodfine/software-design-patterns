namespace StrategyClient.Rules
{
    public class NotInRangeRule : IRule
    {
        public bool Verify(string choice)
        {
            return false;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}