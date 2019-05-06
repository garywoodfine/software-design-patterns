using System;
using BuilderPattern;

namespace BuilderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var person = new PersonBuilder()
                .Create("Gary", "Woodfine")
                .Gender(Gender.Male)
                .DateOfBirth(DateTime.Now)
                .Occupation("Freelance Full-Stack Developer")
                .Build();
            
            
            Console.WriteLine(person.ToString());
            
            
            Console.ReadLine();
        }
    }
}