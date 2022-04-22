using System;

namespace StrategyClient.Rules
{
    public class ProfoundStatementRule : IRule
    {
        public bool Verify(string choice)
        {
            if (choice == "poo") return true;
            return false;
        }

        public void Execute()
        {
           Console.WriteLine("Bagels are lush");
        }
    }
}