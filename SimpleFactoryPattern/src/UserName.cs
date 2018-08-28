using System;

namespace SimpleFactory
{
    public class UserName
    {
       protected string firstName, lastName;

       public string getFirstName(){
           return firstName;
       }

       public string getLastName(){
           return lastName;
       }

    }

}