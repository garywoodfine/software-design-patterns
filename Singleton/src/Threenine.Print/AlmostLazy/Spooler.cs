namespace Threenine.Print.AlmostLazy
{
    public class Spooler : Spool
    {
        private static readonly Spooler instance = new Spooler();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Spooler()
        {
        }

        private Spooler()
        {
        }

        public static Spooler Instance => instance;
    }
}