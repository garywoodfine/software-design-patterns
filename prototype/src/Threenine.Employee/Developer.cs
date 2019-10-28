using System;

namespace Threenine.Employee
{
    public class Developer : ICloneable
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string[] Skills { get; set; }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}