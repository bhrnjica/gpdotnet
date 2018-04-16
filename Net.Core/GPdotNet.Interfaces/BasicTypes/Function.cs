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
using System.Collections;
using System.Collections.Generic;
using static System.Math;
namespace GPdotNet.BasicTypes
{
    public class Function
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string MathematicaDefinition { get; set; }
        public string ExcelDefinition { get; set; }
        public string RDefinition { get; set; }
        public string Definition { get; set; }

        public int Arity { get; set; }

        public bool HasParameter { get; set; }
        public double Parameter { get; set; }
        public int Weight { get; set; }

        public bool Selected { get; set; }
        //addition parameter, in case of categorical problem type represent number of classes
        public float Parameter2 { get; set; }

        public Function Clone()
        {
            return new Function()
            {
                Arity = this.Arity,
                Id = this.Id,
                Name = this.Name,
                Selected = this.Selected,
                Weight = this.Weight
            };
        }

        public static double [] Evaluate2(int functionId, bool isProtected, params double[] tt )
        {
            switch (functionId)
            {
                case 51: //softmax function
                    {
                        var result = Softmax(tt);
                        return result;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public static double Evaluate(int functionId,bool isProtected, params double[] tt)
        {
            switch (functionId)
            {
                case 0:
                    {
                        return tt[0] + tt[1];
                    }
                case 1:
                    {
                        return tt[0] - tt[1];
                    }
                case 2:
                    {
                        return tt[0] * tt[1];
                    }
                case 3://protected division
                    {
                        if (tt[1] == 0)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }

                        return tt[0] / tt[1];
                    }
                case 4:
                    {
                        return tt[0] + tt[1] + tt[2];
                    }
                case 5:
                    {
                        return tt[0] - tt[1] - tt[2];
                    }
                case 6:
                    {
                        return tt[0] * tt[1] * tt[2];
                    }
                case 7:
                    {
                        if (tt[1] == 0 || tt[2] == 0)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return tt[0] / tt[1] / tt[2];
                    }

                case 8:
                    {
                        return tt[0] + tt[1] + tt[2] + tt[3];
                    }
                case 9:
                    {
                        return tt[0] - tt[1] - tt[2] - tt[3];
                    }
                case 10:
                    {
                        return tt[0] * tt[1] * tt[2] * tt[3];
                    }
                case 11:
                    {
                        if (tt[1] == 0 || tt[2] == 0 || tt[3] == 0)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return tt[0] / tt[1] / tt[2] / tt[3];
                    }

                case 12:
                    {
                        return Pow(tt[0], 2);
                    }
                case 13:
                    {
                        return Pow(tt[0], 3);
                    }
                case 14:
                    {
                        return Pow(tt[0], 4);
                    }

                case 15:
                    {
                        return Pow(tt[0], 5);
                    }

                case 16:
                    {
                        return Pow(tt[0], 1 / 3.0);
                    }
                case 17:
                    {
                        return Pow(tt[0], 1 / 4.0);
                    }

                case 18:
                    {
                        return Pow(tt[0], 1 / 5.0);
                    }
                case 19:
                    {
                        if (tt[0] == 0)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return 1.0 / tt[0];
                    }
                case 20:
                    {
                        return Abs(tt[0]);
                    }
                case 21:
                    {
                        return Floor(tt[0]);
                    }
                case 22:
                    {
                        return Ceiling(tt[0]);
                    }
                case 23:
                    {
                        return Truncate(tt[0]);
                    }
                case 24:
                    {
                        return Round(tt[0]);
                    }
                case 25:
                    {
                        return Sin(tt[0]);
                    }
                case 26:
                    {
                        return Cos(tt[0]);
                    }
                case 27:
                    {
                        return Tan(tt[0]);
                    }

                case 28:
                    {
                        if (tt[0] > 1 && tt[0] < -1)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return Asin(tt[0]);
                    }
                case 29:
                    {
                        if (tt[0] > 1 && tt[0] < -1)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return Acos(tt[0]);
                    }
                case 30:
                    {
                        return Atan(tt[0]);
                    }
                case 31:
                    {
                        return Sinh(tt[0]);
                    }
                case 32:
                    {
                        return Cosh(tt[0]);
                    }
                case 33:
                    {
                        if (tt[0] == 0)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return Tanh(tt[0]);
                    }
                case 34:
                    {
                        if (tt[0] < 0)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return Sqrt(tt[0]);
                    }
                case 35:
                    {
                        return Pow(E, tt[0]);
                    }
                case 36:
                    {
                        if (tt[0] < 0)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return Log10(tt[0]);
                    }
                case 37:
                    {
                        if (tt[0] < 0)
                        {
                            if (isProtected)
                                return 0;
                            else
                                return double.NaN;
                        }
                        return Log(tt[0], E);
                    }
                case 38:
                    {
                        return tt[0] * tt[0] + tt[0] * tt[1] + tt[1] * tt[1];
                    }
                case 39:
                    {
                        return tt[0] * tt[0] * tt[0] + tt[1] * tt[1] * tt[1] + tt[2] * tt[2] * tt[2] + tt[0] * tt[1] * tt[2] + tt[0] * tt[1] + tt[1] * tt[2] + tt[0] * tt[2];
                    }
                case 40:
                    {
                        return tt[0] * tt[0] * tt[1];
                    }
                case 41:
                    {
                        return tt[0] * tt[0] * tt[1] * tt[1];
                    }
                case 42:
                    {
                        return tt[0] * tt[0] * tt[0] * tt[1];
                    }
                case 43:
                    {
                        return tt[0] * tt[0] * tt[0] * tt[1] * tt[1];
                    }
                case 44:
                    {
                        return tt[0] * tt[0] * tt[0] * tt[1] * tt[1] * tt[1];
                    }
                case 45:
                    {
                        return tt[0] * tt[0] * tt[1] * tt[1] * tt[1];
                    }
                case 46:
                    {
                        return tt[0] * tt[1] * tt[1] * tt[1];
                    }
                case 47:
                    {
                        return tt[0] * tt[0] * tt[0] * tt[0] * tt[1] * tt[1] * tt[1] * tt[1];
                    }
                case 48: //sigmoid function
                    {
                        return 1.0 / (1.0 + Exp(-tt[0]));
                    }
                case 49: //step function
                    {
                        return tt[0] < tt[1] ? 0 : 1;
                    }
                case 50: //scaled sigmoid on interval [o, NumClas-1]
                    {
                        if (tt.Length < 2)
                            return double.NaN;

                        var v = Truncate(tt[1] / (1.0 + Exp(-tt[0])));
                        //
                        if (v == tt[1])
                            v = tt[1] - 1;
                        return v;
                    }
                case 51: //softmax function
                    {
                        var result = Softmax(tt);
                        int maxIndex = 0;
                        double maxValue = float.MinValue;
                        for (int i = 0; i < result.Length; i++)
                        {
                            if (maxValue < result[i])
                            {
                                maxIndex = i;
                                maxValue = result[i];
                            }

                        }

                        return maxIndex;
                    }
                default:
                    {
                        return double.NaN;
                    }
            }
        }
        private static double[] Softmax(params double[] calculatedOutput)
        {
            //in case of invalid output return NAN
            if (calculatedOutput == null || calculatedOutput.Length == 0 )
                return new double[] { double.NaN };

            //first find maximum value
            double max = calculatedOutput[0];
            for (int i = 0; i < calculatedOutput.Length; ++i)
            {
                if (calculatedOutput[i] > max)
                    max = calculatedOutput[i];
            }

            double sum = 0.0;
            for (int i = 0; i < calculatedOutput.Length; ++i)
            {
                sum += Exp(calculatedOutput[i] - max);
            }

            double[] result = new double[calculatedOutput.Length];
            for (int i = 0; i < calculatedOutput.Length; ++i)
            {
                result[i] = Exp(calculatedOutput[i] - max) / sum;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preCalcData"></param>
        /// <param name="calculatedOutput"></param>
        /// <returns></returns>
        private static double[] SpecFun(Dictionary<int, (double[], double[][])> preCalcData, params double[] calculatedOutput)
        {
            return null;
        }
    }
}
