
using System;
using Xunit;
using SimpleFactory;

namespace SimpleFactoryTest
{
    public class FirstNameFirstTest
    {
       
        [Fact]
        public void ShouldSplitStringOnSpace()
        {
            var theName = "Gary Woodfine"; 
            
            var  splitname = new FirstNameFirst(theName);
            
            Assert.Equal("Gary", splitname.FirstName);
            Assert.Equal("Woodfine", splitname.LastName);
            
        }

        [Fact]
        public void FailSplitOnStringOnComma()
        {
            var theName = "Gary,Woodfine"; 
            
            var  splitname = new FirstNameFirst(theName);
            
            Assert.Null(splitname.FirstName);
            Assert.Null(splitname.LastName);
            
        }
        
    }
}