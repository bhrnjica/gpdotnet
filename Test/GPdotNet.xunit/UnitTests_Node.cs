using System;
using Xunit;
using System.Linq;
using GPdotNet.Core;
namespace gpdotnet.xunit
{
    public class NodeUnitTest : BaseTestClass
    {

        
        [Fact]
        public void Node_Level_Test01()
        {
            
            Node n1 = new Node();
            n1.Generate(5, Generation.strict, funSet, terSet);

            Assert.True(n1.Level == 0);
            Assert.True(n1.Children[0].Level == 1);
            Assert.True(n1.Children[0].Children[0].Level == 2);
            Assert.True(n1.Children[0].Children[0].Children[0].Level == 3);
            Assert.True(n1.Children[0].Children[0].Children[0].Children[0].Level == 4);
            Assert.True(n1.Children[0].Children[0].Children[0].Children[0].Children[0].Level == 5);
        }

        [Fact]
        public void Node_StringConversion_Test01()
        {
            var n1 = new Node();
            n1.FromString(sampleChoromosem[0]);

            Assert.True(n1.Children[0].Children[1].Children[1].Children[1].Children[1].Level == 5);
            Assert.True(n1.Children[1].Children[1].Children[1].Children[1].Value == 1001);

            var n2 = new Node();
            n2.FromString(sampleChoromosem[5]);
            Assert.True(n2.Children[0].Children[0].Children[1].Children[1].Children[0].Value == 2002);

            foreach(var strCh in sampleChoromosem)
            {
                var node = new Node();
                node.FromString(strCh);
                var str = node.ToString();
                Assert.True(str == strCh);
            }

        }


        [Fact]
        public void Node_Clone_Test01()
        {
            var n1 = new Node();
            n1.FromString(sampleChoromosem[0]);

            var n2 = n1.Clone();

            var rdnIndex = Globals.radn.Next(n1.NodeCount()-1);
            var rnode= n1.NodeAt(rdnIndex);
            var rnode2 = n2.NodeAt(rdnIndex);

            rnode.Children = null;
            Assert.True(rnode2.Value < 1999 ||  rnode2.Children != null);

            for(int i=0; i<10; i++)
            {
                n1.FromString(sampleChoromosem[0]);
                n2 = n1.Clone();
                rdnIndex = Globals.radn.Next(n1.NodeCount() - 1);
                rnode = n1.NodeAt(rdnIndex);
                rnode2 = n2.NodeAt(rdnIndex);

                rnode.Children = null;
                if(rnode2.Value > 1999)
                    Assert.True(rnode2.Children != null);
                else
                    Assert.True(rnode2.Value < 1999 || rnode2.Children != null);
            }
            

        }


        [Fact]
        public void Node_Evaluation_Test()
        {
            int[] funSet1 = new int[] { 2000, 2001,2002, 2003, 2034, 2037, 2035 };
            int[] terSet1 = new int[] { 1000, 1001 };

            var n1 = new Node();
            //FromString(n1, "200020012003100110002034100120022037100120351000");
            n1.FromString("2000&2:2001&2:2003&2:1001&-1:1000&-1:2034&1:1001&-1:2002&2:2037&1:1001&-1:2035&1:1000&-1:");

            var value2 = n1.Evaluate(new double[] { 3, 11 }, param);
            var value3 = n1.Evaluate(new double[] { 4, 12 }, param);
            var value1 = n1.Evaluate(new double[] { 2, 10 }, param);
            var value4 = n1.Evaluate(new double[] { 5, 13 }, param);
            var value5 = n1.Evaluate(new double[] { 6, 14 }, param);
            var value6 = n1.Evaluate(new double[] { 7, 15 }, param);
            var value7 = n1.Evaluate(new double[] { 8, 16 }, param);
            var value8 = n1.Evaluate(new double[] { 9, 17 }, param);

            Assert.True(Math.Round(value1,12) == 18.851652764526);
            Assert.True(Math.Round(value2, 12) == 48.513055916040);
            Assert.True(Math.Round(value3, 12) == 135.207204468345);
            Assert.True(Math.Round(value4, 12) == 379.666685803527);
            Assert.True(Math.Round(value5, 10) == 1063.2633903914);
            Assert.True(Math.Round(value6, 11) == 2968.00751901419);
            Assert.True(Math.Round(value7, 10) == 8262.9704963425);
            Assert.True(Math.Round(value8, 10) == 22955.531294877300);

        }
        [Fact]
        public void Node_Evaluation_Test1()
        {
            var n1 = new Node();
            //FromString(n1, "2030200020012004100010011002203710032002203510042025200210051006");
            n1.FromString("2030&1:2000&2:2001&2:2004&3:1000&-1:1001&-1:1002&-1:2037&1:1003&-1:2002&2:2035&1:1004&-1:2025&1:2002&2:1005&-1:1006&-1:");

            var value1 = n1.Evaluate( new double[] { 0.014895006, 0.213542552, 0.906429127, 0.103678641, 0.668730024, 0.381788437, 0.548912264}, param);
            var value2 = n1.Evaluate( new double[] { 0.22281443, 0.163950995, 0.141314889, 0.148642374, 0.157177393, 0.923610616, 0.675272623}, param);
            var value3 = n1.Evaluate( new double[] { 0.989445161,0.713569377, 0.165246695, 0.822129596, 0.505483625, 0.478724452, 0.240796222}, param);
            var value4 = n1.Evaluate( new double[] { 0.819759854,0.554792688, 0.151708153, 0.931165069, 0.588039038, 0.145382201, 0.739949441}, param);
            var value5 = n1.Evaluate( new double[] { 0.397621029,0.597007399, 0.804071716, 0.297994342, 0.464261336, 0.093325777, 0.625078189}, param);
            var value6 = n1.Evaluate( new double[] { 0.259813643,0.380585594, 0.913086336, 0.706473363, 0.7616939,   0.007204283, 0.176240186}, param);
            var value7 = n1.Evaluate( new double[] { 0.236898578,0.043109604, 0.306528715, 0.477659367, 0.4215936 ,  0.999975363, 0.075898418}, param);
            var value8 = n1.Evaluate( new double[] {0.893692539, 0.614737431, 0.902968819, 0.498673155, 0.489250572, 0.509733349, 0.43305714}, param);
            


            Assert.True(value1 == 1.3139487516731492/*1.313948751401*/);
            Assert.True(value2 == 1.2604166833898738/*1.260416683098*/);
            Assert.True(value3 == 1.1533618460689055/*1.153361846140*/);
            Assert.True(value4 == 1.06154108737273/*1.061541087046*/);
            Assert.True(value5 == 1.2589549580084063/*1.258954958225*/);
            Assert.True(value6 == 1.0871143484018455/*1.087114348363*/);
            Assert.True(value7 == 0.96412707855211854/*0.964127078366*/);
            Assert.True(value8 == 1.2897797409417957/*1.289779740964*/);

        }


        [Fact]
        public void Function_Evaluation_Test1()
        {

            //var n1 = new Node();
            ////FromString(n1, "2030200020012004100010011002203710032002203510042025200210051006");
            //n1.FromString("2030&1:2000&2:2001&2:2004&3:1000&-1:1001&-1:1002&-1:2037&1:1003&-1:2002&2:2035&1:1004&-1:2025&1:2002&2:1005&-1:1006&-1:");

            //var value1 = n1.Evaluate(new double[] { 0.014895006, 0.213542552, 0.906429127, 0.103678641, 0.668730024, 0.381788437, 0.548912264 }, param);
            //var value2 = n1.Evaluate(new double[] { 0.22281443, 0.163950995, 0.141314889, 0.148642374, 0.157177393, 0.923610616, 0.675272623 }, param);
            //var value3 = n1.Evaluate(new double[] { 0.989445161, 0.713569377, 0.165246695, 0.822129596, 0.505483625, 0.478724452, 0.240796222 }, param);
            //var value4 = n1.Evaluate(new double[] { 0.819759854, 0.554792688, 0.151708153, 0.931165069, 0.588039038, 0.145382201, 0.739949441 }, param);
            //var value5 = n1.Evaluate(new double[] { 0.397621029, 0.597007399, 0.804071716, 0.297994342, 0.464261336, 0.093325777, 0.625078189 }, param);
            //var value6 = n1.Evaluate(new double[] { 0.259813643, 0.380585594, 0.913086336, 0.706473363, 0.7616939, 0.007204283, 0.176240186 }, param);
            //var value7 = n1.Evaluate(new double[] { 0.236898578, 0.043109604, 0.306528715, 0.477659367, 0.4215936, 0.999975363, 0.075898418 }, param);
            //var value8 = n1.Evaluate(new double[] { 0.893692539, 0.614737431, 0.902968819, 0.498673155, 0.489250572, 0.509733349, 0.43305714 }, param);



            //Assert.True(value1 == 1.3139487516731492/*1.313948751401*/);
            //Assert.True(value2 == 1.2604166833898738/*1.260416683098*/);
            //Assert.True(value3 == 1.1533618460689055/*1.153361846140*/);
            //Assert.True(value4 == 1.06154108737273/*1.061541087046*/);
            //Assert.True(value5 == 1.2589549580084063/*1.258954958225*/);
            //Assert.True(value6 == 1.0871143484018455/*1.087114348363*/);
            //Assert.True(value7 == 0.96412707855211854/*0.964127078366*/);
            //Assert.True(value8 == 1.2897797409417957/*1.289779740964*/);

        }



    }
}
