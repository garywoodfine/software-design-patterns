using System;

namespace SimpleFactory
{
    public class LastNameFirst : UserName
    {
       public LastNameFirst(string username)
       {
            var index = username.Trim().IndexOf(",");
            if(index > 0)
            {
               lastName = username.Substring(0,index).Trim();
               firstName = username.Substring(index + 1).Trim();

            }
       }
    }
}