using System;
using Xunit;
using SimpleFactory;


namespace SimpleFactoryTest
{
    public class UsernameFactoryTests
    {
        [Fact]
        public void ShouldGetFirstNameFirst()
        {
            //arrange
            string username = "Gary Woodfine";


            //act
            var username = UsernameFactory.GetUsername(username);


            //assert
            

        }
    }
}
