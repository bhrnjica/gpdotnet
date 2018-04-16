//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET - Genetic Programming Tool                                                  //
// Copyright 2006-2017 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See license section of  https://github.com/bhrnjica/gpdotnet/blob/master/license.md  //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using GPdotNet.Interfaces;
using System.Collections.Generic;
using GPdotNet.MathStuff;
namespace GPdotNet.Core
{
    //https://jamesmccaffrey.wordpress.com/2016/08/25/machine-learning-scoring-rules/
    /// <summary>
    /// Logarithmic Scoring Rule for Softmax vector output
    /// </summary>
    public class MahanalobisDistance : IFitness
    {
         IData Data;
        public MahanalobisDistance(IData data)
        {
            Data = data;
        }
        public float Evaluate(IChromosome ch, IParameters param, int it)
        {
            var iData = GetData();

            var chr = (Chromosome)ch;
            var expTree = chr.expressionTree;
            double fitness = 0;
            double rowFitness = 0.0;
            double[] y;

            var evalData = new List<(int, double[])>();
            var dic = new Dictionary<int, List<double[]>>();
            var preCalcData = new Dictionary<int, (double[],double[][])>();
            var result = new List<(int, int)>();         

            //for each trainign sample
            for (int i = 0; i < iData.RowCount; i++)
            {
                var rowInputData = iData.GetDataInputRow(i);
                // evaluate the function against each rowData
                y = expTree.Evaluate2(rowInputData, param); 
                if(y==null)
                    return float.NaN;
                //get output Value
                var yo = (int)iData.GetRowOutput(i);

                //add result in to list
                evalData.Add((yo,y));

                //map result with class
                if (dic.ContainsKey(yo))
                    dic[yo].Add(y);
                else
                    dic.Add(yo, new List<double[]>() {y});
            }
            //calculate centroids and Covariance matrix for each cluster
            for(int i = 0; i< dic.Keys.Count; i++)
            {
                //class value
                var c = dic.Keys.ElementAt(i);
                var vals = dic.Values.ElementAt(i).ToArray();
                //predicted values for the class
                var cData = AdvancedStatistics.ToColumnVector<double>(vals);

                //calculated centroid for the cData 
                var cen = cData.Select(x=>x.MeanOf()).ToArray();

                //calculate CovarianceMatrix
                var cov = BasicStatistics.Covariance(cData);
                if (!isValid(cov))
                    return float.NaN;
                preCalcData.Add(c, (cen,cov));
                
            }

            //for each training sample
            //go through all samples
            foreach (var item in evalData)
            {
                //calculate Mahanalobis distance for each training sample
                double minMD = double.MaxValue;
                int predictedCluster = 0;
                foreach (var c in dic.Keys)
                {
                    var md = AdvancedStatistics.MD(item.Item2, preCalcData[c].Item1, preCalcData[c].Item2);
                   
                    //map result with class
                    if (minMD > md)
                    {
                        predictedCluster = c;
                        minMD = md;
                    }

                }
                //map actual and predicted class value
                result.Add((item.Item1, predictedCluster));
            }
            float corectedPredicted = result.Where(x => x.Item1 == x.Item2).Count();
            var rawFitness = corectedPredicted / (float)result.Count;



            if (double.IsNaN(rawFitness) || double.IsInfinity(rawFitness))
                fitness = float.NaN;
            else//calculate adjusted fitness
                fitness = (rawFitness * 1000.0);
            //store extra data into chromosome
            chr.ExtraData = preCalcData;
            
            return (float)System.Math.Round(fitness, 2);
        }

        private bool isValid(double[][] cov)
        {
            bool retVlaue = false;
            for(int i=0; i < cov.Length; i++)
            {
                for (int j = 0; j < cov.Length; j++)
                {
                    if(i==j)
                    {
                        if (cov[i][j] != 1)
                            retVlaue = true;
                    }
                    else
                    {
                        if (cov[i][j] != 0)
                            retVlaue = true;
                    }
                    
                  
                }
               
            }

            return retVlaue;
        }

        public IData GetData()
        {
            return Data;
        }

    }
}
