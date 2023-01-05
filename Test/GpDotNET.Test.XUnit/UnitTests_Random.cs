using System;
using Xunit;
using System.Linq;
using GPdotNet.Core;
namespace gpdotnet.xunit
{
    public class Random_UnitTest : BaseTestClass
    {

        
        [Fact]
        public void ThreadLocal_Test()
        {

            ThreadSafeRandom rand = new ThreadSafeRandom();
            rand.Next();
            rand.Next();
            rand.Next();
            rand.Next();
            rand.Next();
            rand.Next();
            rand.Next();

        }

       


    }
}
