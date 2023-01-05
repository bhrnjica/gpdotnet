using System;

using System.Linq;
using GPdotNet.Core;
using GPdotNet.Data;
using Xunit;

namespace gpdotnet.xunit
{
    public class Random_Minibatch : BaseTestClass
    {

        
        [Fact]
        public void Minibatch_Test()
        {
           
            int miniBatch = 7;
            int tlengthData = trainingDataSet.Length;
            int it = 0;
            var retVal = GPdotNet.Core.Extensions.CalculateMinibatch(tlengthData, it, miniBatch);
            Assert.True(retVal.skip==0 && retVal.size== miniBatch);

            it = 5;
            retVal = GPdotNet.Core.Extensions.CalculateMinibatch(tlengthData, it, miniBatch);
            Assert.True(retVal.skip == 35 && retVal.size == miniBatch);

            it = 6;
            retVal = GPdotNet.Core.Extensions.CalculateMinibatch(tlengthData, it, miniBatch);
            Assert.True(retVal.skip == 42 && retVal.size == 1);

            it = 8;
            retVal = GPdotNet.Core.Extensions.CalculateMinibatch(tlengthData, it, miniBatch);
            Assert.True(retVal.skip == 7 && retVal.size == miniBatch);

            it = 10;
            retVal = GPdotNet.Core.Extensions.CalculateMinibatch(tlengthData, it, miniBatch);
            Assert.True(retVal.skip == 21 && retVal.size == miniBatch);

            it = 13;
            retVal = GPdotNet.Core.Extensions.CalculateMinibatch(tlengthData, it, miniBatch);
            Assert.True(retVal.skip == 42 && retVal.size == 1);

            it = 14;
            retVal = GPdotNet.Core.Extensions.CalculateMinibatch(tlengthData, it, miniBatch);
            Assert.True(retVal.skip == 0 && retVal.size == miniBatch);

            it = 15;
            retVal = GPdotNet.Core.Extensions.CalculateMinibatch(tlengthData, it, miniBatch);
            Assert.True(retVal.skip == 7 && retVal.size == miniBatch);


        }

        
       


    }
}
