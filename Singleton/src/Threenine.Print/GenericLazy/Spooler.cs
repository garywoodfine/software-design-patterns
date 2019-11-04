using System;

namespace Threenine.Print.GenericLazy
{
    public class Spooler : Spool
    {
        private static readonly Lazy<Spooler>
            lazy =
                new Lazy<Spooler>
                    (() => new Spooler());

        public static Spooler Instance { get { return lazy.Value; } }

        private Spooler()
        {
        }
        
    }
}