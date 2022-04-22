namespace StrategyClient
{
    public interface IRule
    {
        bool Verify(string choice);
        void Execute();
    }
}