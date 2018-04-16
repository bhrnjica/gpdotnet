//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET - Genetic Programming Tool                                                  //
// Copyright 2006-2017 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the MIT License                                     //
// See license section of  http://github.com/bhrnjica/gpdotnet/blob/master/license.md  //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;
using GPdotNet.Interfaces;
using System.Collections.Generic;
using static System.Math;

namespace GPdotNet.Core
{
    /// <summary>
    /// Root Mean Square Error
    /// </summary>
    public class RMSEFitnessSoftmax : IFitness
    {
         IData Data;
        public RMSEFitnessSoftmax(IData data)
        {
            Data = data;
        }
        public float Evaluate(IChromosome ch, IParameters param, int it)
        {
            var iData = GetData();//get experimental data

            var expTree = ((Chromosome)ch).expressionTree;
            double fitness = 0;
            double rawFitness = 0.0;
            double[] y;

            //index of output parameter

            for (int i = 0; i < iData.RowCount; i++)
            {
                var rowInputData = iData.GetDataInputRow(i);
                //get output Value
                var yo = iData.GetRowOutputSoftmax(i);
                
                // evaluate the function against each rowData
                y = expTree.Evaluate2(rowInputData, param);
                if(y==null)
                    return float.NaN;
                for (int j = 0; j <y.Length; ++j)
                {
                    // check for correct numeric value
                    if (double.IsNaN(y[j]) || double.IsInfinity(y[j]))
                        return float.NaN;

                    double err = y[j] - yo[j];
                    rawFitness += err * err;
                }
            }

            if (double.IsNaN(rawFitness) || double.IsInfinity(rawFitness))
                fitness = float.NaN;
            else//Root mean square error
                fitness = ((1.0 / (1.0 + Sqrt(rawFitness / iData.RowCount))) * 1000.0);

            return (float)Round(fitness, 2);
        }

        public IData GetData()
        {
            return Data;
        }

    }
}
