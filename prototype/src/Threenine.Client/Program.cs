using System;
using Threenine.Employee;

namespace Threenine.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            var dev = new Developer
            {
                FirstName = "Gary",
                Lastname = "Woodfine",
                Skills = new []{"C#", "PHP", "SQL", "JavaScript"}
            };


            var dev2 = dev.Clone() as Developer; 
            
            Console.WriteLine($"The Cloned  Developer name is { dev2.FirstName }  { dev2.Lastname }");
            
            Console.WriteLine("The second developer has the following skills: ");


            foreach (var skill in dev2.Skills)
            {
                Console.WriteLine(skill);
            }
        }
    }
}