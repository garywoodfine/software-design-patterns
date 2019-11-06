using System;

namespace Threenine.Print.GenericLazy
{
    public sealed class Spooler : Spool 
    {
        private bool _disposed;
        private static volatile Lazy<Spooler>
            lazy =
                new Lazy<Spooler>
                    (() => new Spooler());

        public static Spooler Instance => lazy.Value;

        private Spooler()
        {
        }

       
        
    }
}