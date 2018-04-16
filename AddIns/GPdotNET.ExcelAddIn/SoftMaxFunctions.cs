using ExcelDna.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPdotNET.ExcelAddIn
{
    public class SoftMaxFunctions
    {
        //Example of usage in Excel: =Softmax({x1,2,x3})
        //Example of usage in Excel: =Softmax(A1:A34)
        //Example of usage in Excel: =Softmax(A1:C1)
        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax(object arg)
        {
            try
            {
                //First convert object in to array
                object[,] obj = (object[,])arg;
                //create list to convert values
                List<double> calculatedOutput = new List<double>();
                //
                foreach (var s in obj)
                {
                    var ss = double.Parse(s.ToString());
                    calculatedOutput.Add(ss);
                }

                return Softmax(calculatedOutput).ToString();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax2(double x1, double x2)
        {
           
            try
            {
                return Softmax(new double[] { x1, x2 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax3(double x1, double x2, double x3)
        {
            
            try
            {
                return Softmax(new double[] { x1, x2, x3 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax4(double x1, double x2, double x3, double x4)
        {
           
            try
            {
                return Softmax(new double[] { x1, x2, x3, x4 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax5(double x1, double x2, double x3, double x4, double x5)
        {
           
            try
            {
                return Softmax(new double[] { x1, x2, x3, x4, x5 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax6(double x1, double x2, double x3, double x4, double x5, double x6)
        {
           

            try
            {
                return Softmax(new double[] { x1, x2, x3, x4, x5, x6 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax7(double x1, double x2, double x3, double x4, double x5, double x6, double x7)
        {
            
            try
            {
                return Softmax(new double[] { x1, x2, x3, x4, x5, x6, x7 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax8(double x1, double x2, double x3, double x4, double x5, double x6, double x7, double x8)
        {
            
            try
            {
                return Softmax(new double[] { x1, x2, x3, x4, x5, x6, x7, x8 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax9(double x1, double x2, double x3, double x4, double x5, double x6, double x7, double x8, double x9)
        {
           
            try
            {
                return Softmax(new double[] { x1, x2, x3, x4, x5, x6, x7, x8, x9 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax10(double x1, double x2, double x3, double x4, double x5, double x6, double x7, double x8, double x9, double x10)
        {
            try
            {
                return Softmax(new double[] { x1, x2, x3, x4, x5, x6, x7, x8, x9, x10 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            
        }

        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax11(double x1, double x2, double x3, double x4, double x5, double x6, double x7, double x8, double x9, double x10, double x11)
        {
            try
            {
                return Softmax(new double[] { x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
        [ExcelFunction(Description = "GPdotNET - SoftMax function")]
        public static string Softmax12(double x1, double x2, double x3, double x4, double x5, double x6, double x7, double x8, double x9, double x10, double x11, double x12)
        {
            try
            {
                return Softmax(new double[] { x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12 }).ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
        
        private static double Softmax(double[] vector)
        {
            try
            {
                //first find maximum value
                double max = vector[0];
                for (int i = 0; i < vector.Length; ++i)
                {
                    if (vector[i] > max)
                        max = vector[i];
                }

                double sum = 0.0;
                for (int i = 0; i < vector.Length; ++i)
                {
                    sum += Math.Exp(vector[i] - max);
                }

                double[] result = new double[vector.Length];
                for (int i = 0; i < vector.Length; ++i)
                {
                    result[i] = Math.Exp(vector[i] - max) / sum;
                }

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
            catch (Exception)
            {
                throw;
                //return ex.Message;
            }
        }
    }
}
