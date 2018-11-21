using System;

namespace SimpleFactory
{
    public class FirstNameFirst : UserName
    {
        public FirstNameFirst(string username)
        {
            var index = username.Trim().IndexOf(" ", StringComparison.Ordinal);

            if (index <= 0) return;

            FirstName = username.Substring(0,index).Trim();
            LastName = username.Substring(index + 1).Trim();
        }
    }
}