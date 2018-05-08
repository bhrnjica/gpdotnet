using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
namespace GPdotNET.ExcelAddIn
{
    /// <summary>
    /// This class implements several functions to Excel in order to support all Function set from GPdotNET
    /// </summary>
    public class GPdotNETFunctions
    {

        /// <summary>
        /// Example of usage in Excel: =Sigmoid(x1, 4)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [ExcelFunction(Description = "GPdotNET Sigmoid function")]
        public static double Sigmoid(double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }

        /// <summary>
        /// Example of usage in Excel: =SSigmoid(x1, 4)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        [ExcelFunction(Description = "GPdotNET Scaled Sigmoid function")]
        public static double SSigmoid(double value, double scale)
        {
            return scale / (1.0 + Math.Exp(-value));
        }
       
        
       
    }
}
