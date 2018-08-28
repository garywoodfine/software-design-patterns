using System;

namespace SimpleFactory
{
    public class FirstNameFirst : UserName
    {
        public FirstNameFirst(string username)
        {
            var index = username.Trim().IndexOf(" ");
            if(index > 0)
            {
               firstName = username.Substring(0,index).Trim();
               lastName = username.Substring(index + 1).Trim();

            }
        }
    }
}