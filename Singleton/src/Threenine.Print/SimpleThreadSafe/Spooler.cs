namespace Threenine.Print.SimpleThreadSafe
{
    public sealed class Spooler : Spool
     {
        private static Spooler instance;
        private static readonly object padlock = new object();
    
        
        public static Spooler Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ??= new Spooler();
                }
            }
        }
    }
}