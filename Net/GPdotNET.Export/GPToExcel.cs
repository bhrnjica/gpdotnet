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
using ClosedXML.Excel;
using GPdotNet.BasicTypes;
using GPdotNet.Core;
using GPdotNet.Data;
using GPdotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPdotNet.Export
{
    public class GPToExcel
    {
        /// <summary>
        /// Exports data and the model to excel 
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="ch"></param>
        /// <param name="strFilePath"></param>
        public static void Export(Node ch, Parameters param, IData data, Experiment experiment,  string strFilePath)
        {
            try
            {
                //
                var wb = new XLWorkbook();
                var ws1 = wb.Worksheets.Add("TRAINING DATA");
                var ws2 = ws1;
                if (experiment.IsTestDataExist())
                    ws2 = wb.Worksheets.Add("TESTING DATA");
                else
                    ws2 = null;


                ws1.Cell(1, 1).Value = "Training Data";
                if (experiment.IsTestDataExist())
                    ws2.Cell(1, 1).Value = "Testing Data";

                writeDataToExcel(experiment, ws1, false);
                if (experiment.IsTestDataExist())
                    writeDataToExcel(experiment, ws2, true);


                //GP Model formula
                string formula = NodeDecoding.Decode(ch,param, EncodeType.Excel);
                AlphaCharEnum alphaEnum = new AlphaCharEnum();

                //make a formula to de normalize value
                var cols = experiment.GetColumnsFromInput();
                var totCols = experiment.GetEncodedColumnInputCount();
                var diff = totCols - cols.Count;//difference between column count and normalized column count due to Category column clusterization

                //Replace Input variable with excel column references
                for (int i = totCols-1; i >= 0; i--)
                {
                    string var = "x" + (i+1).ToString() + " ";
                    string cell = alphaEnum.AlphabetFromIndex(2 + i) + "3";

                    //make a formula to de normalize value
                    var col = cols[i - diff];
                    string replCell = cell;
                    if (col.ColumnDataType == ColumnType.Category)
                    {
                        formula = formula.Replace(var, replCell);
                        if (diff > 0)
                            diff -= 1;
                    }
                    else if (col.ColumnDataType == ColumnType.Binary)
                    {
                        formula = formula.Replace(var, replCell);
                    }
                    else
                    {
                        replCell = createNormalizationFormulaForColumn(col, cell);
                        formula = formula.Replace(var, replCell);
                    }

                }

                //Replace random constants with real values
                int constCount = 1;
                var consts = data.GetRandConsts();
                for (int i = 0; i < consts.Length; i++)
                {
                    string var = "r" + (i+1).ToString() + " ";
                    string constValue = consts[i].ToString(); 
                    formula = formula.Replace(var, constValue);
                    constCount++;
                }

                //output column
                var outCol = experiment.GetColumnsFromOutput().FirstOrDefault();

                //de normalize output column if available
                if(outCol.ColumnDataType == ColumnType.Numeric)
                {
                    var normFormula = createDeNormalizationFormulaForOutput(outCol, formula);
                    formula = normFormula;
                }
              
                //in case of decimal point, semicolon of Excel formula must be replaced with comma
                if ("." == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                    formula = formula.Replace(";", ",");

                ws1.Cell(3, totCols + 3).Value = formula;
                if (experiment.IsTestDataExist())
                    ws2.Cell(3, totCols + 3).Value = formula;
                //
                wb.SaveAs(strFilePath, false);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Write data set to excel worksheet
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="ws"></param>
        /// <param name="isTest"></param>
        private static void writeDataToExcel(Experiment experiment, IXLWorksheet ws, bool isTest = false)
        {
            //TITLE
            ws.Range("A1", "D1").Style.Font.Bold = true;
            ws.Range("A1", "D1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            //COLUMNS NAMES
            //RowNumber
            ws.Cell(2, 1).Value = "Nr";

            //get input parameter column
            var cols = experiment.GetColumnsFromInput();
            int cellIndex = 2;//starting with offset of 2 cells  
            //Input variable names
            for (int i = 0; i < cols.Count; i++)
            {

                if (cols[i].ColumnDataType == ColumnType.Category)
                {

                    for(int j=0; j < cols[i].GetEncodedColumCount(); j++)
                    {
                        ws.Cell(2, cellIndex).Value = cols[i].EncodedHeader(j);
                        cellIndex++;
                    }
                }
                else
                {
                    ws.Cell(2, cellIndex).Value =  cols[i].EncodedHeader(0);
                    cellIndex++;
                }

            }

            //Output names
            var outCol = experiment.GetColumnsFromOutput().FirstOrDefault();

            if (outCol.ColumnDataType == ColumnType.Category)
            {
                var tempValu = outCol.Encoding;
                outCol.Encoding = CategoryEncoding.Level;
                ws.Cell(2, cellIndex).Value = outCol.EncodedHeader(0);
                ws.Cell(2, cellIndex + 1).Value = outCol.EncodedHeader(0) + "gp";
                outCol.Encoding = tempValu;
            }
            else
            {
                ws.Cell(2, cellIndex).Value = outCol.EncodedHeader(0);
                ws.Cell(2, cellIndex + 1).Value = outCol.EncodedHeader(0) + "gp";
            }

            //Add Data.         
            for (int i = 0; i < experiment.GetRowCount(isTest); i++)
            {
                ws.Cell(i + 3, 1).Value = i + 1;
                //get normalized and numeric row
                var row = experiment.GetRowFromInput(i, isTest);
                var row_norm = experiment.GetEncodedInput(i, isTest, false);
                cellIndex = 2;//starting with offset of 2 cells  

                //input columns
                for (int k = 0; k < row_norm.Length; k++)
                {
                    ws.Cell(i + 3, cellIndex).Value = row_norm[k];
                    cellIndex++;
                }
                //output columns
                if(outCol.ColumnDataType== ColumnType.Category)
                    ws.Cell(i + 3, cellIndex).Value = outCol.GetNumericValue(i);
                else
                    ws.Cell(i + 3, cellIndex).Value = experiment.GetEncodedOutput(i, isTest, false);
                cellIndex++;
                

            }
        }

        /// <summary>
        /// returning the excel formula of normalization
        /// </summary>
        /// <param name="col"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static string createNormalizationFormulaForColumn(ColumnData col, string varName)
        {
            //
            if (col.Scaling == Scaling.Gauss)
            {
                //
                var str = string.Format("(({0}-{1})/{2})", varName, col.Statistics.Mean, col.Statistics.StdDev);
                return str;
            }
            else if (col.Scaling == Scaling.MinMax)
            {
                //
                var str = string.Format("(({0}-{1})/({2}-{1}))", varName, col.Statistics.Min, col.Statistics.Max);
                return str;
            }
            else if (col.Scaling == Scaling.None)
            {
                return varName;
            }
            else
                throw new Exception("Unknown normalization data type.");

        }

        /// <summary>
        /// returning the excel formula of de normalization
        /// </summary>
        /// <param name="col"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static string createDeNormalizationFormulaForOutput(ColumnData col, string varName)
        {
            //
            if (col.Scaling == Scaling.Gauss)
            {
                //
                var str = string.Format("(({2}*{0}+{1}))", varName, col.Statistics.Mean, col.Statistics.StdDev);
                return str;
            }
            else if (col.Scaling == Scaling.MinMax)
            {
                //
                var str = string.Format("({0}*{1}+{2})", varName, col.Statistics.Max - col.Statistics.Min, col.Statistics.Min);
                return str;
            }
            else if (col.Scaling == Scaling.None)
            {
                return varName;
            }
            else
                throw new Exception("Unknown normalization data type.");

        }


    }
}
