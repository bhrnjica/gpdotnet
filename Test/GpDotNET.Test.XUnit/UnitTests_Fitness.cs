using System;

using System.Linq;
using GPdotNet.Core;
using GPdotNet.MathStuff;
using Xunit;

namespace gpdotnet.xunit
{
    public class FitnessUnitTest : BaseTestClass
    {
        //regression
        string ch1 = "0;2000&2:2002&2:1002&-1:1000&-1:2002&2:1003&-1:1001&-1:";
        string ch2 = "0;2000&2:2002&2:1002&-1:1000&-1:2002&2:2002&2:2025&1:1003&-1:2003&2:1003&-1:2003&2:1003&-1:1001&-1:1001&-1:";
       // string ch3 = "0;2000&2:2002&2:1002&-1:1000&-1:2002&2:2002&2:2025&1:1001&-1:2003&2:1001&-1:2003&2:1001&-1:1002&-1:1002&-1:";
        //binary problem
        string cb1 = "0;2049&1:2000&2:2002&2:1002&-1:1000&-1:2002&2:1003&-1:1001&-1:";
        string cb2 = "0;2049&1:2000&2:2002&2:1002&-1:1000&-1:2002&2:2002&2:2025&1:1003&-1:2003&2:1003&-1:2003&2:1003&-1:1001&-1:1001&-1:";

        double[][] gpOutputCh1Ch2 = new double[][] {
            new double[]{1.66564, 1.13409 } ,
            new double[]{1.17200, 0.60665},
            new double[]{1.28006, 0.80182},
            new double[]{2.23614, 1.68091},
            new double[]{1.34174, 0.83781},
            new double[]{0.77112, 0.43777},
            new double[]{1.94320, 1.35360},
            new double[]{1.17212, 0.74655},
            new double[]{1.69656, 1.25494},
            new double[]{1.23368, 0.65623},
            new double[]{1.43414, 0.83945},
            new double[]{0.86376, 0.74680},
            new double[]{1.81992, 1.32211},
            new double[]{1.57312, 1.07531},
            new double[]{1.54236, 1.18897},
            new double[]{1.31106, 1.12749},
            new double[]{1.15682, 0.99929},
            new double[]{1.54208, 0.95211},
            new double[]{1.91236, 1.32601},
            new double[]{0.61684, 0.22576},
            new double[]{1.49610, 1.17307},
            new double[]{1.94332, 1.44551},
            new double[]{1.11044, 0.71936},
            new double[]{0.87902, 0.41483},
            new double[]{1.15682, 0.99929},
            new double[]{0.53982, 0.38229},
            new double[]{2.48298, 1.90237},
            new double[]{2.03564, 1.46305},
            new double[]{2.06648, 1.50023},
            new double[]{1.97404, 1.38199},
            new double[]{1.54220, 0.97685},
            new double[]{1.80446, 1.26798},
            new double[]{0.81742, 0.53768},
            new double[]{1.51140, 0.99030},
            new double[]{1.51128, 0.91757},
            new double[]{0.41638, 0.15949},
            new double[]{2.03560, 1.49319},
            new double[]{0.89456, 0.64939},
            new double[]{0.18508, 0.12500},
            new double[]{1.58854, 1.08461},
            new double[]{0.64772, 0.31437},
            new double[]{2.28248, 1.69251},
            new double[]{2.54470, 1.95620},
    };

        [Fact]
        public void Fitness_Node_Evaluation_Test()
        {
            var c1 = new Chromosome();
            c1.FromString(ch1);

            var c2 = new Chromosome();
            c2.FromString(ch2);
            var data = Create_Numeric_TrainingDataSet();
            //
            for (int i = 0; i < gpOutputCh1Ch2.Length; i++)
            {
                double ych1 = c1.expressionTree.Evaluate(data.GetDataInputRow(i), param);
                double y1 = gpOutputCh1Ch2[i][0];

                Assert.Equal(Math.Round(ych1, 5), Math.Round(y1, 5));

                double ych2 = c2.expressionTree.Evaluate(data.GetDataInputRow(i), param);
                double y2 = gpOutputCh1Ch2[i][1];

                Assert.Equal(Math.Round(ych2, 5), Math.Round(y2, 5));
            }
        }

        [Fact]
        public void Confusion_Matrix_Test01()
        {
            var data = new int[][] {
            new int[]{0  , 0},
            new int[]{2  , 0},
            new int[]{1  , 1},
            new int[]{1  , 2},
            new int[]{2  , 2},
            new int[]{1  , 1},
            new int[]{0  , 2},
            new int[]{1  , 2},
            new int[]{2  , 1},
            new int[]{0  , 0},
            new int[]{0  , 2},
            new int[]{1  , 1},
            new int[]{1  , 0},
            new int[]{2  , 0},
            new int[]{1  , 1},
            new int[]{0  , 2},
            new int[]{0  , 2},
            new int[]{0  , 2},
            new int[]{0  , 1},
            new int[]{0  , 2},
            new int[]{2  , 2},
            new int[]{0  , 0},
            new int[]{1  , 1},
            new int[]{0  , 0},
            new int[]{0  , 1},
            new int[]{0  , 0},
            new int[]{0  , 0},
            new int[]{0  , 0},
            new int[]{2  , 2},
            new int[]{0  , 0},
            new int[]{2  , 2}
            };

            ConfusionMatrix matrix = new ConfusionMatrix(data.Select(x => x[0]).ToArray(), data.Select(x => x[1]).ToArray(), 3);

            var acc = ConfusionMatrix.OAC(matrix.Matrix);
            var hss = ConfusionMatrix.HSS(matrix.Matrix,data.Length);
            var pss = ConfusionMatrix.PSS(matrix.Matrix, data.Length);
            Assert.True(Math.Round(acc, 6) == 0.548387);
            Assert.True(Math.Round(hss, 6) == 0.318681);
            Assert.True(Math.Round(pss, 6) == 0.342905);

        }


        [Fact]
        public void AccFitness_Binary_Test()
        {
            var data = Create_Binary_TrainingDataSet();

            var c1 = new Chromosome();
            c1.FromString(cb1);

            var c2 = new Chromosome();
            c2.FromString(cb2);

            var fit = new ACCFitness(data);
            var fitness = fit.Evaluate(c1, initBinaryClassParameters(),0);
            //
            Assert.Equal(Math.Round(fitness, 2), Math.Round(511.627907, 2));


            var p = initBinaryClassParameters();
            p.RootFunctionNode.Parameter = 0.8;

            fitness = fit.Evaluate(c2, p,0);
            Assert.Equal(Math.Round(fitness, 2), Math.Round(883.72093, 2));
        }

        [Fact]
        public void HSSFitness_Binary_Test()
        {
            var data = Create_Binary_TrainingDataSet();

            var c1 = new Chromosome();
            c1.FromString(cb1);

            var c2 = new Chromosome();
            c2.FromString(cb2);

            var fit = new HSSFitness(data);
            var fitness = fit.Evaluate(c1, initBinaryClassParameters(),0);
            //
            Assert.Equal(Math.Round(fitness, 2), Math.Round(188.679245, 2));


            var p = initBinaryClassParameters();
            p.RootFunctionNode.Parameter = 0.8;

            fitness = fit.Evaluate(c2, p,0);
            Assert.Equal(Math.Round(fitness, 2), Math.Round(740.024184, 2));
        }

        [Fact]
        public void PSSFitness_Binary_Test()
        {
            var data = Create_Binary_TrainingDataSet();

            var c1 = new Chromosome();
            c1.FromString(cb1);

            var c2 = new Chromosome();
            c2.FromString(cb2);

            var fit = new PSSFitness(data);
            var fitness = fit.Evaluate(c1, initBinaryClassParameters(),0);
            //
            Assert.Equal(Math.Round(fitness, 2), Math.Round(250.0, 2));


            var p = initBinaryClassParameters();
            p.RootFunctionNode.Parameter = 0.8;

            fitness = fit.Evaluate(c2, p,0);
            Assert.Equal(Math.Round(fitness, 2), Math.Round(/*2.73258*/ 728.571429, 2));
        }


        [Fact]
       
        public void AEFitness_Test()
        {
            var data = Create_Numeric_TrainingDataSet();

            var c1 = new Chromosome();
            c1.FromString(ch1);

            var c2 = new Chromosome();
            c2.FromString(ch2);

            var fit = new AEFitness(data);
            var fitness = fit.Evaluate(c1, initRegressionParameters(),0);
            //
            Assert.Equal(Math.Round(fitness, 2), Math.Round(/*2.88131*/2.88131, 2));

            fitness = fit.Evaluate(c2, initRegressionParameters(),0);
            Assert.Equal(Math.Round(fitness, 2), Math.Round(/*2.73258*/ 2.73258, 2));
        }

        [Fact]
        public void MAEFitness_Test()
        {
            var data = Create_Numeric_TrainingDataSet();

            var c1 = new Chromosome();
            c1.FromString(ch1);

            var c2 = new Chromosome();
            c2.FromString(ch2);

            var fit = new MAEFitness(data);
            var fitness = fit.Evaluate(c1, initRegressionParameters(),0);
            //
            Assert.Equal(Math.Round(fitness, 2), Math.Round(/*110.52155*/110.53, 2));

            fitness = fit.Evaluate(c2, initRegressionParameters(),0);
            Assert.Equal(Math.Round(fitness, 2), Math.Round(/*105.40384*/ 105.41, 2));
        }

        [Fact]
        public void RMSEFitness_Test()
        {
            var data = Create_Numeric_TrainingDataSet();

            var c1 = new Chromosome();
            c1.FromString(ch1);

            var c2 = new Chromosome();
            c2.FromString(ch2);

            var fit = new RMSEFitness(data);
            var fitness = fit.Evaluate(c1, initRegressionParameters(),0);
            //
             Assert.Equal(Math.Round(fitness, 2), Math.Round(100.846, 2));

            fitness = fit.Evaluate(c2, initRegressionParameters(),0);
            Assert.Equal(Math.Round(fitness, 2), Math.Round(96.69799, 2));

        }

        [Fact]
        public void RSEFitness_Test()
        {
            var data = Create_Numeric_TrainingDataSet();

            var c1 = new Chromosome();
            c1.FromString(ch1);

            var c2 = new Chromosome();
            c2.FromString(ch2);

            var fit = new RSEFitness(data);
            var fitness = fit.Evaluate(c1, initRegressionParameters(),0);
            //
            Assert.Equal(Math.Round(fitness, 2), Math.Round(16.81609, 2));

            fitness = fit.Evaluate(c2, initRegressionParameters(),0);
            Assert.Equal(Math.Round(fitness, 2), Math.Round(16.06267, 2));
        }

        [Fact]
        public void SEFitness_Test()
        {
            var data = Create_Numeric_TrainingDataSet();

            var c1 = new Chromosome();
            c1.FromString(ch1);

            var c2 = new Chromosome();
            c2.FromString(ch2);

            var fit = new SEFitness(data);
            var fitness = fit.Evaluate(c1, initRegressionParameters(),0);
            //
            Assert.Equal(Math.Round(fitness, 2), Math.Round(0.29245, 2));

            fitness = fit.Evaluate(c2, initRegressionParameters(), 0);
            Assert.Equal(Math.Round(fitness, 2), Math.Round(0.26643, 2));
        }
    }
}
