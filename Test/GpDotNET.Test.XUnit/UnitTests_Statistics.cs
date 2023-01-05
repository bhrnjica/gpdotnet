using System;
using Xunit;
using System.Linq;
using GPdotNet.Core;
using System.Collections.Generic;
using GPdotNet.MathStuff;
using static System.Math;
namespace gpdotnet.xunit
{
    public class UnitTests_Statistics : BaseTestClass
    {

        [Fact]
        public void calcCovariantMatrix_Test()
        {
            var X1 = new double[]{ 64,66,68,69,73};
            var X2 = new double[]{ 580, 570, 590, 660, 600};
            var X3 = new double[]{ 29, 33, 37, 46, 55};
            var X4 = new double[]{ };

           var matrix =  GPdotNet.MathStuff.BasicStatistics.Covariance( new List<double[]>() {X1,X2,X3 });
            Assert.True(Round(matrix[0][0], 5) == 3.68852);
            Assert.True(Round(matrix[0][1], 5) == 0.06273);
            Assert.True(Round(matrix[0][2], 5) == -1.28214);
            Assert.True(Round(matrix[1][0], 5) == 0.06273);
            Assert.True(Round(matrix[1][1], 5) == 0.00222);
            Assert.True(Round(matrix[1][2], 5) == -0.02395);
            Assert.True(Round(matrix[2][0], 5) == -1.28214);
            Assert.True(Round(matrix[2][1], 5) == -0.02395);
            Assert.True(Round(matrix[2][2], 5) == 0.45877);

            //Assert.True(matrix[0][0]==11.5 && matrix[0][1] == 50 && matrix[0][2] == 34.75);
            //Assert.True(matrix[1][0] == 50 && matrix[1][1] == 1250 && matrix[1][2] == 205);
            //Assert.True(matrix[2][0] == 34.75 && matrix[2][1] == 205 && matrix[2][2] == 110);

            ////inverse matrix
            //Matrix mat = new Matrix(matrix.Length, matrix.Length);
            //for(int i=0; i< matrix.Length; i++)
            //{
            //    for (int j = 0; j < matrix.Length; j++)
            //    {
            //        mat[i,j] = matrix[i][j];
            //    }
            //}

            //var covMat = mat.Invert();




        }

        [Fact]
        public void calcMahanalobiusDistance_Test()
        {
            var X1 = new double[] { 64, 66, 68, 69, 73 };
            var X2 = new double[] { 580, 570, 590, 660, 600 };
            var X3 = new double[] { 29, 33, 37, 46, 55 };
            var X4 = new double[] { };

            var matrix = GPdotNet.MathStuff.BasicStatistics.CovMatrix(new List<double[]>() { X1, X2, X3 });
            Assert.True(Round(matrix[0,0], 5) == 3.68852);
            Assert.True(Round(matrix[0,1], 5) == 0.06273);
            Assert.True(Round(matrix[0,2], 5) == -1.28214);
            Assert.True(Round(matrix[1,0], 5) == 0.06273);
            Assert.True(Round(matrix[1,1], 5) == 0.00222);
            Assert.True(Round(matrix[1,2], 5) == -0.02395);
            Assert.True(Round(matrix[2,0], 5) == -1.28214);
            Assert.True(Round(matrix[2,1], 5) == -0.02395);
            Assert.True(Round(matrix[2,2], 5) == 0.45877);

            var vector = new Matrix(1,3);
            vector[0, 0] = 66;
            vector[0, 1] = 570;
            vector[0, 2] = 33;

            var mean = new Matrix(1, 3);
            mean[0, 0] = X1.MeanOf();
            mean[0, 1] = X2.MeanOf();
            mean[0, 2] = X3.MeanOf();

            var m = vector - mean;
            var trm = Matrix.Transpose(m);
            var tmp = (m * matrix);
            Assert.True(Round(tmp[0, 0], 5) == -0.28396);
            Assert.True(Round(tmp[0,1], 5) == -0.02436);
            Assert.True(Round(tmp[0,2], 5) == 0.07147);
           
            //
            var mdist2 = Sqrt( (tmp * trm)[0,0]);

            Assert.True(Round(mdist2, 5) == 0.89359);
            
        }

    }
}
