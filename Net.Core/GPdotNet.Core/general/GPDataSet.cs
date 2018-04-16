using GPdotNet.Data;
using GPdotNet.Interfaces;
using GPdotNet.MathStuff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace GPdotNet.Core
{
    public class ExperimentData : IData
    {
        Experiment exp = null;
        //data row includes training data row and random constants,
        //the last element is output value
        double[][] m_trainingData = null;
        double[][] m_testingData = null;

        public ExperimentData(double[][] trainingData, double[][] testingData, double[] constants)
        {
            Data = trainingData;
            TData = testingData;
            RandConstants = constants;
            generateTerminalSet();
        }
        public void SetExperiment(Experiment e)
        {
            exp = e;
        }
        public double[][] Data { get; set; }
        public double[][] TData { get; set; }
        public double[] RandConstants { get; set; }


        public int RowCount
        {
            get
            { return Data.Length; }
        }
        public int ColCount
        {
            get
            {
                return m_trainingData[0].Length;
            }
        }
        public int ConstCount
        {
            get
            {
                if (RandConstants == null)
                    return 0;
                else
                    return RandConstants.Length;
            }
        }
        public int FeatureCount
        {
            get; set;
        }


        public double[] CalculateOutput(IChromosome chromosome, IParameters param, bool isTrainingData)
        {
            var ch = ((Chromosome)chromosome);
            var node = ch.expressionTree;

            if (TData == null && isTrainingData == false)//there is no test data
                return null;

            var rowCount = isTrainingData ? Data.Length : TData.Length;
            var y = new double[rowCount];
            //
            for (int i = 0; i < rowCount; i++)
            {
                var rowInputData = isTrainingData ? GetDataInputRow(i) : GetTestDataInputRow(i);

                // evaluate the function against each rowData
                if(param.IsMultipleOutput && ch.ExtraData != null)
                {
                    var oneHotVector = node.Evaluate2(rowInputData, param);

                    //calculate Mahanalobis distance for each training sample
                    double minMD = double.MaxValue;
                    int predictedCluster = 0;
                    foreach (var c in ch.ExtraData.Keys)
                    {
                        var md = AdvancedStatistics.MD(oneHotVector, ch.ExtraData[c].Item1, ch.ExtraData[c].Item2);

                        //map result with class
                        if (minMD > md)
                        {
                            predictedCluster = c;
                            minMD = md;
                        }

                    }
                    //map actual and predicted class value
                    y[i] = predictedCluster;
                    
                }
                else// evaluate the function against each rowData
                    y[i] = node.Evaluate(rowInputData, param);
            }

            return y;
        }


        public double[] GetDataOutputCol(bool isTrainingData)
        {
            if (TData == null && isTrainingData == false)//there is no test data
                return null;

            var rowCount = isTrainingData ? Data.Length : TData.Length;
            var y = new double[rowCount];
            //
            for (int i = 0; i < rowCount; i++)
            {
                var retVal = isTrainingData ? GetRowOutput(i) : GetTestRowOutput(i);

                y[i] = retVal;
            }

            return y;
        }
        public double[] GetRandConsts()
        {
            if (RandConstants == null || RandConstants.Length == 0)
                return new double[0];
            else
                return RandConstants;
        }
        public double[] GetDataInputRow(int index)
        {
            //
            return m_trainingData[index];
        }

        public double[] GetTestDataInputRow(int index)
        {
            //
            return m_testingData[index];
        }

        public double GetRowOutput(int index)
        {
            var outIndex = Data[0].Length - 1;
            return Data[index][outIndex];
        }
        public double[] GetRowOutputSoftmax(int index)
        {
            return exp.GetEncodedOutput(index, false, true);
        }
        public double GetTestRowOutput(int index)
        {
            var outIndex = TData[0].Length - 1;
            return TData[index][outIndex];
        }


        private void generateTerminalSet()
        {
            //
            int numCons = RandConstants == null ? 0 : RandConstants.Length;

            int numOfVariables = Data[0].Length + numCons;
            int inputParamCount = Data[0].Length - 1;
            int randConstCount = numCons;

            //
            FeatureCount = inputParamCount;
            //Generate training data
            var trainingData = new double[Data.Length][];

            for (int j = 0; j < Data.Length; j++)
            {
                trainingData[j] = new double[numOfVariables];

                for (int i = 0; i < numOfVariables; i++)
                {
                    if (i < inputParamCount)//input variable
                        trainingData[j][i] = Data[j][i];
                    else if (i >= inputParamCount && i < numOfVariables - 1)//constants
                        trainingData[j][i] = RandConstants[i - inputParamCount];
                    else
                        trainingData[j][i] = Data[j][i - randConstCount];//output variable
                }
            }


            m_trainingData = trainingData;
            //testing data
            if (TData != null)
            {
                //Generate testing data
                var testingData = new double[TData.Length][];

                for (int j = 0; j < TData.Length; j++)
                {
                    testingData[j] = new double[numOfVariables];

                    for (int i = 0; i < numOfVariables; i++)
                    {
                        if (i < inputParamCount)//input variable
                            testingData[j][i] = TData[j][i];
                        else if (i >= inputParamCount && i < numOfVariables - 1)//constants
                            testingData[j][i] = RandConstants[i - inputParamCount];
                        else
                            testingData[j][i] = TData[j][i - randConstCount];//output variable
                    }
                }

                m_testingData = testingData;
            }

        }


    }
}
