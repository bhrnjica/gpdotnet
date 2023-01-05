using System;

using System.Linq;
using GPdotNet.Core;
using GPdotNet.MathStuff;
using Xunit;

namespace gpdotnet.xunit
{
    public class ConfusionMatrix_UnitTest 
    {

        
        [Fact]
        public void Binary_Classifier_Test01()
        {

            int[] y1 = new int[6] { 0, 1, 1, 0, 1, 1 };
            double[] y2 = new double[6] { 0.103345, 0.816285, 0.36523, 0.164645, 0.988035, 0.963756 };
            var y22 = calculateOutput(y2,0.5);

            int[] y11 = new int[] { 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1, 1 };
            double[] y21 = new double[] { 0.583333, 0, 1, 0.1026, 0.058482, 0.222538, 0, 0.306647, 0.05, 0.345238, 0, 0.45, 0.137926, 0.291667, 0, 1, 0.137926, 0.055871, 0, 0, 0.46875, 0.479167, 0, 0.125, 0, 0, 0.240183, 0.270833, 0.125, 1, 0.5625, 0.1026, 1, 0.125, 0.375, 1, 0.137926, 0.240183, 0.275984, 0.5, 0.137926, 0.625, 0.5, 0.5, 0.875, 0, 0.875, 0.765625, 0.137926, 0.875, 1, 0.375, 1, 0.1026, 0.45, 0.583333, 0.351637, 1, 0.1026, 0, 0.6875, 0.25, 0, 0.625, 0, 0.1026, 0.649777, 1, 0.202232, 0, 0, 0, 0.479167, 0.375, 0.25, 1, 0.17748, 0.125, 0.5625, 0.1026, 0.75463, 0, 0, 0, 1, 0.1026, 1, 0.458333, 0.1026, 1, 0.875, 1 };
            var y222 = calculateOutput(y21, 0.5);

            //construct binary Confusion matrix
            var cm = new ConfusionMatrix(y1, y22, 2);

            //
            var val1 = cm.Matrix[1][1];
            var val2 = ConfusionMatrix.TPi(cm.Matrix, 1);
            Assert.True(val1==val2);

            //
            val1 = cm.Matrix[1][0]; 
            val2 = ConfusionMatrix.FNi(cm.Matrix, 1);
            Assert.True(val1 == val2);

            //
            val1 = cm.Matrix[0][1]; 
            val2 = ConfusionMatrix.FPi(cm.Matrix, 1);
            Assert.True(val1 == val2);
            //
            val1 = cm.Matrix[0][0]; 
            val2 = ConfusionMatrix.TNi(cm.Matrix, 1);
            Assert.True(val1 == val2);

            //Accuracy
           double dval1 = (cm.Matrix[0][0]+ cm.Matrix[1][1])/ (double)y1.Length;
           double dval2 = ConfusionMatrix.OAC(cm.Matrix);
           double dval3 = ConfusionMatrix.AAC(cm.Matrix);
           Assert.True(dval1 == dval2);
           Assert.True(dval2 == dval3);

            //Error

            //Precision a/a+b
            var p1 = (double)cm.Matrix[1][1] / (double)(cm.Matrix[0][1] + cm.Matrix[1][1]);
            dval2 = ConfusionMatrix.Precision(cm.Matrix, 1);
            
            Assert.True(p1 == dval2);



            //Recall - a/a+c
            var r1 = (double)cm.Matrix[1][1]/(double)(cm.Matrix[1][0]+ cm.Matrix[1][1]);
            dval2 = ConfusionMatrix.Recall(cm.Matrix, 1);

            Assert.True(r1 == dval2);
            //FScore
            dval1 = 2 * (p1 * r1) / (p1 + r1);
            dval2 = ConfusionMatrix.Fscore(cm.Matrix, 1);
            Assert.True(dval1 == dval2);

            //Specificity
            //dval1 = ConfusionMatrix.TNR(cm.Matrix);
            //dval2 = ConfusionMatrix.Specificity(cm.Matrix, 1);


        }
        [Fact]
        public void MCC_Classifier_Test01()
        {
            int[] y1 = new int[13] { 0, 1, 2, 1, 2, 0, 1, 0, 2, 2, 2, 0, 2 };
            int[] y2 = new int[13] { 1, 2, 0, 1, 2, 0, 1, 0, 2, 1, 2, 0, 2 };

            //construct binary Confusion matrix
            var cm = new ConfusionMatrix(y1, y2, 3);

            //
            double v1 = ConfusionMatrix.TPi(cm.Matrix,0);
            double v2 = ConfusionMatrix.FPi(cm.Matrix, 0);
            double v3 = ConfusionMatrix.FNi(cm.Matrix, 0);
            double v4 = ConfusionMatrix.TNi(cm.Matrix, 0);
            Assert.True(v1 == 3);
            Assert.True(v2 == 1);
            Assert.True(v3 == 1);
            Assert.True(v4 == 8);

            v1 = ConfusionMatrix.TPi(cm.Matrix, 1);
            v2 = ConfusionMatrix.FPi(cm.Matrix, 1);
            v3 = ConfusionMatrix.FNi(cm.Matrix, 1);
            v4 = ConfusionMatrix.TNi(cm.Matrix, 1);
            Assert.True(v1 == 2);
            Assert.True(v2 == 2);
            Assert.True(v3 == 1);
            Assert.True(v4 == 8);

            v1 = ConfusionMatrix.TPi(cm.Matrix, 2);
            v2 = ConfusionMatrix.FPi(cm.Matrix, 2);
            v3 = ConfusionMatrix.FNi(cm.Matrix, 2);
            v4 = ConfusionMatrix.TNi(cm.Matrix, 2);
            Assert.True(v1 == 4);
            Assert.True(v2 == 1);
            Assert.True(v3 == 2);
            Assert.True(v4 == 6);



            //Accuracy
            v1 = ((3.0 + 8.0) / 13.0 + (2.0 + 8.0) / 13.0 + (4.0 + 6.0) / 13.0) / 3;
            v2 = (3.0 + 2.0+ 4.0) / 13.0;
            double dval1 = ConfusionMatrix.OAC(cm.Matrix);
            double dval2 = ConfusionMatrix.AAC(cm.Matrix);
            Assert.True(dval1 == v2);
            Assert.True(dval2 == v1);



            //Error

            //Precision
            var pv1 = (3.0+2.0+4.0)/(3.0 + 2.0 + 4.0+1.0+2.0+1.0);
            var pv2 = (3.0/(3.0+1.0) +2.0/(2.0 + 2.0) + 4.0/(4.0 + 1.0)) / (3);
            dval1 = ConfusionMatrix.MicroPrecision(cm.Matrix);
            dval2 = ConfusionMatrix.MacroPrecision(cm.Matrix);

            Assert.True(dval1 == pv1);
            Assert.True(dval2 == pv2);

            //Recall
            var rv1 = (3.0+2.0+4.0)/(3.0 + 2.0 + 4.0+1.0+1.0+2.0);
            var rv2 = (3.0/(3.0+1.0) +2.0/(2.0 + 1.0) + 4.0/(4.0 + 2.0)) / (3);
            dval1 = ConfusionMatrix.MicroRecall(cm.Matrix);
            dval2 = ConfusionMatrix.MacroRecall(cm.Matrix);

            Assert.True(dval1 == rv1);
            Assert.True(dval2 == rv2);

            //FScore
            dval1 = ConfusionMatrix.MicroFscore(cm.Matrix);
            dval2 = ConfusionMatrix.MacroFscore(cm.Matrix);

            Assert.True(dval1 == 2*(pv1 * rv1)/(pv1+rv1));
            Assert.True(dval2 == 2*(pv2 * rv2) / (pv2 + rv2));

        }
        private int[] calculateOutput(double[] y, double thresholdValue)
        {
            int[] retVal = new int[y.Length];
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] > thresholdValue)
                    retVal[i] = 1;
                else
                    retVal[i] = 0;
            }
            return retVal;
        }



    }
}
