using GPdotNet.BasicTypes;
using GPdotNet.Core;
using GPdotNet.Data;
using GPdotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPdotNet.Export
{
    public class GPtoMathematica
    {

        public static void Export(Node ch, Parameters param, IData data, Experiment experiment, string strFilePath)
        {
            try
            {
                // open selected file and retrieve the content
                using (TextWriter tw = new StreamWriter(strFilePath))
                {

                    tw.Flush();
                    //Add Data.
                    var cols = experiment.GetColumnsFromInput();
                    var outCol = experiment.GetColumnsFromOutput(false).FirstOrDefault();
                    string cmd = "trainingDataSet={";
                    for (int i = 0; i < experiment.GetRowCount(false); i++)
                    {
                        string line = "{";

                        //get normalized and numeric row
                        var row = experiment.GetRowFromInput(i, false);
                        var row_norm = experiment.GetEncodedInput(i, false, false);

                        //input columns
                        for (int j = 0; j < row_norm.Length; j++)
                        {
                            line += row_norm[j].ToString(CultureInfo.InvariantCulture);

                            if (j + 1 != row_norm.Length)
                                line += ",";
                            else
                            {
                                if (outCol.ColumnDataType == ColumnType.Category)
                                    line += "," + outCol.GetNumericValue(i).Value.ToString(CultureInfo.InvariantCulture);
                                else
                                    line += "," + experiment.GetRowFromOutput(i, false)[0].ToString(CultureInfo.InvariantCulture);

                                line += "}";
                            }
                            //
                            
                        }

                        cmd += line;
                        if (i + 1 < experiment.GetRowCount(false))
                            cmd += ",";
                        else
                            cmd += "};";

                    }
                    tw.WriteLine(cmd);

                    //GP Model formula
                    string formula = NodeDecoding.Decode(ch, param, EncodeType.Mathematica);
                    List<string> inputArgs = new List<string>();
                    AlphaCharEnum alphaEnum = new AlphaCharEnum();

                    var totCols = experiment.GetEncodedColumnInputCount();
                    var diff = totCols - cols.Count;//difference between column count and normalized column count due to Category column clusterization

                    for (int i = totCols - 1; i >= 0; i--)
                    {
                        string var = "x" + (i+1).ToString() + " ";
                       
                        //make a formula to de normalize value
                        var col = cols[i - diff];
                        if (col.ColumnDataType == ColumnType.Category)
                        {
                            //formula = formula.Replace(var, replCell);
                            if (diff > 0)
                                diff -= 1;
                        }
                        else if (col.ColumnDataType == ColumnType.Binary)
                        {
                            //formula = formula.Replace(var, replCell);
                        }
                        else
                        {
                            var replCell = GPToExcel.createNormalizationFormulaForColumn(col, var);
                            formula = formula.Replace(var, replCell);
                        }
                        // 
                        inputArgs.Add(var);

                    }

                    //Replace random constants with real values
                    var consts = data.GetRandConsts();
                    for (int i = 0; i < consts.Length; i++)
                    {
                        string var = "r" + (i + 1).ToString() + " ";
                        string constValue = consts[i].ToString(CultureInfo.InvariantCulture);
                        if (constValue[0] == '-')
                            constValue = "(" + constValue + ")";
                        formula = formula.Replace(var, constValue);
                    }


                    //in case of category output
                    //category output is pre calculated with sigmoid multiply with Class count.
                    if (outCol.ColumnDataType == ColumnType.Numeric)//for numeric output we need to de normalize formula
                    {
                        var normFormula = GPToExcel.createDeNormalizationFormulaForOutput(outCol, formula);
                        formula = normFormula;
                    }


                    //in case of softMax we must defined separate function in R
                    var customFun = "";
                    if (param.RootFunctionNode != null && param.RootFunctionNode.Name == "Softmax")
                    {
                        customFun =
                        "Softmax[x_List] :=  Ordering[Exp[x - Max[x]] / Total[Exp[x - Max[x]]], 1][[1]] -1; " + Environment.NewLine;
                    }

                    //add model name and arguments
                    formula = "gpModel[{0}]:=" + formula;

                    //add arguments to the model
                    string arguments = "";
                    for (int i = 0; i < inputArgs.Count; i++)
                    {
                        var a = inputArgs[i];
                        if (formula.Contains(a))
                        {
                            if (i == 0)
                                a = a.Replace(" ", "_");
                            else
                                a = a.Replace(" ", "_,"); ;
                            //
                            arguments = a + arguments;
                        }
                    }
                    if (arguments.EndsWith(","))
                        arguments = arguments.Substring(0, arguments.Length - 1);
                    formula = string.Format(formula, arguments);
                    tw.WriteLine(formula + ";");
                    tw.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
