using System;

namespace SimpleFactory
{
    public class UsernameFactory 
    {
       public UsernameFactory ()
       {
           
       }

       public UserName GetUserName(string name)
           {
               if(name.Contains(","))
               {
                   return new LastNameFirst(name);

               }
               else
               {
                   return new FirstNameFirst(name);

               }

           }
    }
}