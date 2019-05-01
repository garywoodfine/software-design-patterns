using System;

namespace BuilderPattern
{
    public class PersonBuilder
    {
        private readonly Person _person;

        public PersonBuilder()
        {
            _person = new Person();
        }

        public PersonBuilder Id(int id)
        {
            _person.Id = id;
            return this;
        }

        public PersonBuilder Firstname(string firstName)
        {
            _person.Firstname = firstName;
            return this;
        }

        public PersonBuilder Lastname(string lastname)
        {
            _person.Lastname = lastname;
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

    