
using System;
using Threenine.Print.Simple;
using Xunit;

namespace Threenine.Print.Tests
{
    public class DoubleLockTests
    {
        
        [Fact]
        public void ShouldCreateOneInstance()
        {
            
            var firstItem = new PrintQueueItem {DocumentName = "firstItem"};
            var secondItem = new PrintQueueItem {DocumentName = "secondItem"};
            
            var instance1 = Spooler.Instance;
            var instance2 = Spooler.Instance;
            
            Assert.Same(instance1, instance2);
            
            instance1.Queue.Add(firstItem);
            
            Assert.Equal(instance1.Queue.Count, instance2.Queue.Count);
            
            instance2.Queue.Add(secondItem);
            
            Assert.Equal(instance1.Queue.Count, instance2.Queue.Count);
            
            Assert.Same(instance1, instance2);

            
            

        }
        
    }
}