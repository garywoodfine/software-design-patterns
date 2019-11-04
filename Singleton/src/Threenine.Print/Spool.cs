using System.Collections.Generic;

namespace Threenine.Print
{
    public abstract class Spool
    {
       public List<PrintQueueItem> Queue { get; } = new List<PrintQueueItem>();
    }
}