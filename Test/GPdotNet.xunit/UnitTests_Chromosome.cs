using System;
using Xunit;
using System.Linq;
using GPdotNet.Core;
using System.Collections.Generic;

namespace gpdotnet.xunit
{
    public class UnitTests_Chromosome : BaseTestClass
    {
       
        
       


        //[Fact]
        //public void prepareCHromosome()
        //{
        //    string[] sampleChoromosem2 = new string[] {
        //    "",
        //    "2000200220041000100110002037100020031001203720252000100120121000",
        //    };
        //    var list = new List<String>();
        //    foreach (var n in sampleChoromosem2)
        //    {
        //        Node node = new Node();
        //        FromString(node,n);
        //        list.Add(node.ToString());
        //    }

        //    Assert.True(true);
        //}

        [Fact]
        public void ChromsomeMutate_Test()
        {
            var strch1 = "0;2000&2:2037&1:2000&2:1000&-1:2002&2:1001&-1:1002&-1:2002&2:2012&1:2035&1:1000&-1:2001&2:2002&2:1001&-1:1002&-1:2037&1:1000&-1:";
            var strch2 = "0;2000&2:2002&2:2004&3:1000&-1:1001&-1:1000&-1:2037&1:1000&-1:2003&2:1001&-1:2037&1:2025&1:2000&2:1001&-1:2012&1:1000&-1:";
            Node n1 = new Node();
            Node n2 = new Node();
             
            //FromString(n1,"20002037200010002002100110022002201220351000200120021001100220371000");
            //FromString(n2, "2000200220041000100110002037100020031001203720252000100120121000");

            Chromosome ch1 = new Chromosome();
            Chromosome ch2 = new Chromosome();

            ch1.FromString(strch1);
            ch2.FromString(strch2);

            Chromosome.ApplyMutate(ch1.expressionTree, 16, 8 ,funSet, terSet);
            Chromosome.ApplyMutate(ch2.expressionTree, 12, 8, funSet, terSet);
            var o1 = ch1.ToString();
            var o2 = ch2.ToString();
            Assert.True(o1.Contains("0;2000&2:2037&1:2000&2:1000&-1:2002&2"));
                                    
                                    

            Assert.True(o2.Contains("0;2000&2:2002&2:2004&3:1000&-1:1001&-1:1000&-1:2037&1:1000&-1:2003&2:1001&-1:2037&1:2025&1:"));

                                
        }                       

        [Fact]
        public void ChromosomeCrosOver_Test()
        {
            
            var strch1 = "0;2000&2:2037&1:2000&2:1000&-1:2002&2:1001&-1:1002&-1:2002&2:2012&1:2035&1:1000&-1:2001&2:2002&2:1001&-1:1002&-1:2037&1:1000&-1:";
                           
            var strch2 = "0;2000&2:2002&2:2004&3:1000&-1:1001&-1:1000&-1:2037&1:1000&-1:2003&2:1001&-1:2037&1:2025&1:2000&2:1001&-1:2012&1:1000&-1:";
           

            Chromosome ch1 = new Chromosome();
            Chromosome ch2 = new Chromosome();

            ch1.FromString(strch1);
            ch2.FromString(strch2);

            Chromosome.ApplyCrossover(ch1.expressionTree, ch2.expressionTree, 2, 2, 20, funSet, terSet);


            var o1 = ch1.ToString();
            var o2 = ch2.ToString();
            Assert.Equal("0;2000&2:2002&2:2004&3:1000&-1:1001&-1:1000&-1:2037&1:1000&-1:2002&2:2012&1:2035&1:1000&-1:2001&2:2002&2:1001&-1:1002&-1:2037&1:1000&-1:", o1);
            Assert.Equal("0;2000&2:2037&1:2000&2:1000&-1:2002&2:1001&-1:1002&-1:2003&2:1001&-1:2037&1:2025&1:2000&2:1001&-1:2012&1:1000&-1:", o2);
                        


            ch1.FromString(strch1);
            ch2.FromString(strch2);

            Chromosome.ApplyCrossover(ch1.expressionTree, ch2.expressionTree, 4, 6, 20, funSet, terSet);
            o1 = ch1.ToString();
            o2 = ch2.ToString();
            Assert.Equal("0;2000&2:2037&1:2000&2:1000&-1:2002&2:1001&-1:1002&-1:2002&2:2004&3:1000&-1:1001&-1:1000&-1:2001&2:2002&2:1001&-1:1002&-1:2037&1:1000&-1:", o1);
            Assert.Equal("0;2000&2:2002&2:2012&1:2035&1:1000&-1:2037&1:1000&-1:2003&2:1001&-1:2037&1:2025&1:2000&2:1001&-1:2012&1:1000&-1:", o2);


            ch1.FromString(strch1);
            ch2.FromString(strch2);

            Chromosome.ApplyCrossover(ch1.expressionTree, ch2.expressionTree, 2, 15, 20, funSet, terSet);
            o1 = ch1.ToString();
            o2 = ch2.ToString();
            Assert.Equal("0;2000&2:1000&-1:2002&2:2012&1:2035&1:1000&-1:2001&2:2002&2:1001&-1:1002&-1:2037&1:1000&-1:", o1);
            Assert.Equal("0;2000&2:2002&2:2004&3:1000&-1:1001&-1:1000&-1:2037&1:1000&-1:2003&2:1001&-1:2037&1:2025&1:2000&2:1001&-1:2012&1:2037&1:2000&2:1000&-1:2002&2:1001&-1:1002&-1:", o2);

            //if some tree exceed the levels trim it
            //ch2.Trim(6,terSet);
            //var str = ch2.ToString();
            //Assert.True(str.Contains("0;20002002200410001001100020371000200310012037202520001001"));

        }
    }
}
