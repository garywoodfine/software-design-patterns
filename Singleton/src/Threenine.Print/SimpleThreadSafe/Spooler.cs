namespace Threenine.Print.SimpleThreadSafe
{
    public sealed class Spooler : Spool
     {
        private static Spooler instance;
        private static readonly object threadlock = new object();
    
        
        public static Spooler Instance
        {
            get
            {
                lock (threadlock)
                {
                    return instance ??= new Spooler();
                }
            }
        }
    }
}