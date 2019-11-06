using System;

namespace Threenine.Print.DoubleLock
{
    /// <summary>
    /// Do not use this pattern
    /// </summary>
    public sealed class Spooler : Spool
    {
       
        private static volatile Spooler instance;
        private static readonly object threadLock = new object();

        public static Spooler Instance
        {
            get
            {
                if (instance != null) return instance;

                lock (threadLock)
                {
                    if (instance == null)
                    {
                        instance = new Spooler();
                    }
                }

                return instance;
            }
        }
    }
}