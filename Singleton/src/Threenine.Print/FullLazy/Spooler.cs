namespace Threenine.Print.FullLazy
{
    public class Spooler : Spool
    {
        
        private Spooler()
        {
        }

        public static Spooler Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Spooler instance = new Spooler();
        }
        
    }
}