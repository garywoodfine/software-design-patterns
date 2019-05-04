using System;
using System.Text;

namespace BuilderPattern
{
   public class Person
   {
      public int Id { get; set; }

      public string Firstname { get; set; }

      public string Lastname { get; set; }

      public DateTime DateOfBirth { get; set; }

      public string Occupation { get; set; }

      public Gender Gender { get; set; }

      public override string ToString()=>
         new StringBuilder()
            .Append("Person with id: ")
            .Append(Id.ToString())
            .Append(" with date of birth ")
            .Append(DateOfBirth.ToLongDateString())
            .Append(" and name ")
            .Append(Firstname)
            .Append(" ")
            .Append(Lastname)
            .Append(" is a ")
            .Append(Occupation)
            .ToString();
      
        
   }
}
