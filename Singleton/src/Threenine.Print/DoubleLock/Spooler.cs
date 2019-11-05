namespace Threenine.Print.DoubleLock
{
    
    /// <summary>
    /// Do not use this pattern
    /// </summary>
    public class Spooler : Spool
    {
      
            private static Spooler instance;
            private static readonly object padlock = new object();

            public static Spooler Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (padlock)
                        {
                            if (instance == null)
                            {
                                instance = new Spooler();
                            }
                        }
                    }
                    return instance;
                }
            }
        }
        
    }
