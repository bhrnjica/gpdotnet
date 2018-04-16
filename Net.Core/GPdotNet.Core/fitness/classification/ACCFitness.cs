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
using GPdotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNet.Core
{
    /// <summary>
    /// GPdotNET 4.0 implements the Fitness for Classification problems.
    /// It calculates division between number of correctly predicted and total rows.
    /// </summary>

    public class ACCFitness : IFitness
    {
        IData Data;
        public ACCFitness(IData data)
        {
            Data = data;
        }
        public float Evaluate(IChromosome ch, IParameters param, int it)
        {
            var iData = GetData();
            var expTree = ((Chromosome)ch).expressionTree;
            double rawFitness = 0.0;
            double fitness = 0.0;
            double y;

            for (int i = 0; i < Data.RowCount; i++)
            {
                var rowInputData = iData.GetDataInputRow(i);

                // evaluate the function with row data and truncate to class in order create proper
                y = expTree.Evaluate(rowInputData, param);

                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    return float.NaN;

                //in case Threshold is defined for Sigmoid function
                //then it should be used in order to calculate the result
                if (param.RootFunctionNode.Id == 2048)//2048 is Sigmoid function
                {
                    y = y > param.RootFunctionNode.Parameter ? 1 : 0;
                }
                else if(param.RootFunctionNode.Id == 2050)//scaled sigmoid
                {
                    y = System.Math.Truncate(y);
                }
               
                //
                double yo = iData.GetRowOutput(i);

                //Calculate accuracy
                var result = (y - yo) == 0 ? 1 : 0;
                rawFitness += result;
            }

            if (double.IsNaN(rawFitness) || double.IsInfinity(rawFitness))
                fitness = float.NaN;
            else 
                fitness = (rawFitness/Data.RowCount) * 1000.0;

            return (float)System.Math.Round(fitness, 2);
        }

        public IData GetData()
        {
            return Data;
        }

    }
}
