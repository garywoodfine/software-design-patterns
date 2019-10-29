using System;
using System.Collections.Generic;

namespace Threenine.Employee
{
    public class Developer : ICloneable
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public List<string> Skills { get; set; }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}