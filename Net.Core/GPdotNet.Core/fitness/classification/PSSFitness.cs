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
using GPdotNet.MathStuff;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GPdotNet.Core
{
    /// <summary>
    /// Heidke Skill Score implementation as Fitness function for Classification GP. 
    /// 
    /// </summary>

    public class PSSFitness : IFitness
    {
        IData Data;
        public PSSFitness(IData data)
        {
            Data = data;
        }
        public float Evaluate(IChromosome ch, IParameters param, int it)
        {
            var iData = GetData();
            var expTree = ((Chromosome)ch).expressionTree;

            double y;
            int p;
            int[][] matrix = null;

            //root node is not defined
            if (param.RootFunctionNode==null)
                return float.NaN;

            //create confusion matrix based on number of classes
            int classes = (int) param.RootFunctionNode.Parameter2;
            if (classes > 0 )
            {
                matrix = new int[classes][];
                for (int i = 0; i < classes; i++)
                    matrix[i] = new int[classes];
            }
           

            for (int i = 0; i < iData.RowCount; i++)
            {
                var rowInputData = iData.GetDataInputRow(i);

                // evaluate the function against each rowData
                y = expTree.Evaluate(rowInputData, param);

                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    return float.NaN;

                //in case Threshold is defined for Sigmoid function
                //then it should be used in order to calculate the result
                if (param.RootFunctionNode.Id == 2048)//2048 is Sigmoid function
                {
                    //p = y < param.RootFunctionNode.Parameter ? 0 : 1;
                    p = y > param.RootFunctionNode.Parameter ? 1 : 0;
                    y = p;
                }
                else if (param.RootFunctionNode.Id == 2050)//scaled sigmoid
                {
                    p = (int)System.Math.Truncate(y);
                    y = p;
                }
                else
                    p = (int)y;

                //get observed value
                var o = (int)iData.GetRowOutput(i);

                //calculate confusion matrix
                ConfusionMatrix.ProcessRowValue(matrix, o, p);

            }

            //calculate PSS
            var pss = ConfusionMatrix.PSS(matrix, iData.RowCount);
            var fitn = pss * 1000;

            return (float)System.Math.Round(fitn, 2);
        }

        public IData GetData()
        {
            return Data;
        }

    }
}