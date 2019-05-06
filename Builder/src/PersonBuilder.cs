using System;

namespace BuilderPattern
{
    public class PersonBuilder
    {
        private Person _person;

       public PersonBuilder Create(string firstName, string lastName)
        {
            _person = new Person();
            _person.Firstname = firstName;
            _person.Lastname = lastName;
            _person.Id = Guid.NewGuid();
            return this;

        }
        public PersonBuilder DateOfBirth( DateTime dob)
        {
            _person.DateOfBirth = dob;
            return this;
        }

        public PersonBuilder Gender(Gender gender)
        {
            _person.Gender = gender;
            return this;
        }

        public PersonBuilder Occupation(string occupation)
        {
            _person.Occupation = occupation;
            return this;
        }
        
        public Person Build()
        {
            return _person;
        }
    }
}

    