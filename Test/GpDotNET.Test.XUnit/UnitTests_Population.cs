using System;
using Xunit;
using System.Linq;
using GPdotNet.Core;
namespace gpdotnet.xunit
{
    public class PopulationUnitTest : BaseTestClass
    {

        
        [Fact]
        public void PopulationPersistant_Test01()
        {
            Population pop = new Population();
            for(int i=0; i< sampleChoromosem.Length; i++)
            {
                var sc = sampleChoromosem[i];
                var ch = new Chromosome();
                ch.FromString($"{i};{sc}");

                pop.Chromosomes.Add(ch);
            }

            var pStr = pop.PopulationToString();
            pop.PopulationFromString(pStr);

            var pStr1 = pop.PopulationToString();

            int j = 0;
            foreach(var s in pStr1.Split(new string[] { Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                var strC = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                Assert.Equal(strC[0].Split(';')[1], sampleChoromosem[j]);
                j++;
            }

            Assert.True(true);

        }

    }
}
