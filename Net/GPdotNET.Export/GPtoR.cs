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
    public class GPtoR
    {

        public static void Export(Node ch, Parameters param, IData data, Experiment experiment, string strFilePath)
        {
            try
            {
                // open selected file and retrieve the content
                using (TextWriter tw = new StreamWriter(strFilePath))
                {
                    tw.Flush();

                    //GP Model formula
                    //
                    string formula = NodeDecoding.Decode(ch, param, EncodeType.RLanguage);
                    List<string> inputArgs = new List<string>();
                    AlphaCharEnum alphaEnum = new AlphaCharEnum();
                    //make a formula to de normalize value
                    var cols = experiment.GetColumnsFromInput();
                    var totCols = experiment.GetEncodedColumnInputCount();
                    var diff = totCols - cols.Count;//difference between column count and normalized column count due to Category column clusterization
                                        
                    ///
                    for (int i = totCols - 1; i >= 0; i--)
                    {
                        string var = "x" + (i+1).ToString() + " ";
                        string cell = var;

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
                            replCell = GPToExcel.createNormalizationFormulaForColumn(col, cell);
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
                        formula = formula.Replace(var, constValue);
                    }

                    //in case of category output
                    //category output is pre-calculated with sigmoid multiply with Class count.
                    var outCol = experiment.GetColumnsFromOutput().FirstOrDefault();
                    //de normalize output column if available
                    if (outCol.ColumnDataType == ColumnType.Numeric)
                    {
                        var normFormula = GPToExcel.createDeNormalizationFormulaForOutput(outCol, formula);
                        formula = normFormula;
                    }

                    //in case of softMax we must defined separate function in R
                    var customFun = "";
                    if(param.RootFunctionNode!=null && param.RootFunctionNode.Name =="Softmax")
                    {
                        customFun =
                        "Softmax <- function(x) {" +Environment.NewLine+
                        "        max = max(x)" + Environment.NewLine +
                        "        sum = sum(exp(x - max))" + Environment.NewLine +
                        "       which.max(exp(x - max) / sum)-1;" + Environment.NewLine +
                        "    };" + Environment.NewLine;
                    }

                    //create training and testing data frame
                    var trainingDataset = createDataFrame(experiment,false);
                    var testingDataset = createDataFrame(experiment, true);


                    //add arguments to the model
                    string arguments = "";
                    string calcArguments = "";
                    
                    for (int i = 0; i < inputArgs.Count; i++)
                    {
                        int k = inputArgs.Count - i - 1;
                        var a = inputArgs[k];
                        if (formula.Contains(a))
                        {
                            a = a.Replace(" ", ",");
                            //
                            arguments = arguments+a;
                            calcArguments += $"x[{i + 1}],";
                        }
                    }
                    if (!string.IsNullOrEmpty(arguments) && arguments.EndsWith(","))
                        arguments = arguments.Substring(0, arguments.Length - 1);
                    //calculate output column for training and testing data set
                    var calcTrain = "training_set$Ygp <- apply(training_set, 1, function(x) gpModel(" + calcArguments.Substring(0,calcArguments.Length-1) + "));";// x[1], x[2], x[3], x[4]));";
                    var calcTest = "testing_set$Ygp <- apply(testing_set, 1, function(x) gpModel(" + calcArguments.Substring(0, calcArguments.Length - 1)+ "));";// x[1], x[2], x[3], x[4]));"

                    //construct function
                    formula = @"gpModel<- function("+arguments+") {"+Environment.NewLine + formula+ Environment.NewLine +"};";
                    
                    //add custom defined function
                    formula = trainingDataset+Environment.NewLine+testingDataset+Environment.NewLine + customFun +  formula +
                                            Environment.NewLine + calcTrain + Environment.NewLine+ calcTest;

                   

                    tw.WriteLine(formula);
                    tw.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string createDataFrame(Experiment data, bool isTest)
        {
            //get input parameter column
            var cols = data.GetColumnsFromInput(isTest);
            var colNames = new List<(string,string)>();

            //Input variable names
            int counter = 0;
            for (int i = 0; i < cols.Count; i++)
            {
                if (cols[i].ColumnDataType == ColumnType.Category)
                {
  
                    for (int j = 0; j < cols[i].GetEncodedColumCount(); j++)
                    {

                        var varName = $"x{counter+1}";
                        if (isTest)
                            varName += "t";
                        var varComment =$"#{cols[i].EncodedHeader(j)}";
                        colNames.Add((varName,varComment));
                        counter++;
                    }
                }
                else
                {
                    var varName = $"x{counter+1}";
                    if (isTest)
                        varName += "t";
                    var varComment = $"#{cols[i].EncodedHeader(0)}";
                    colNames.Add((varName, varComment));
                    counter++;
                }

            }


            //Output names
            var outCol = data.GetColumns().Where(x => x.IsOutput).FirstOrDefault();
            string nameCol = "y";
            if (isTest)
                nameCol += "t";
            string nameComment = $"#{outCol.EncodedHeader(0)}";

            //output columns
            colNames.Add((nameCol, nameComment));

            ///
            string rcode = "";
            if (isTest)
                rcode = $"#create testing set from the defined variable {Environment.NewLine} testing_set <- data.frame(";
            else
                rcode = $"#create training set from the defined variable {Environment.NewLine} training_set <- data.frame(";

            for (int i = 0; i < colNames.Count; i++)
            {

                var c = colNames[i];
                rcode += c.Item1 + ",";
                //
                c.Item1 += "= c(";
                colNames[i] = c;
            }

            //Add Data. 
             counter = 0;
            for (int i = 0; i < data.GetRowCount(isTest); i++)
            {
                //get normalized and numeric row
                var row = data.GetRowFromInput(i, isTest);
                var row_norm = data.GetEncodedInput(i, isTest, false);

                //input columns
                for (int j = 0; j < row_norm.Length; j++)
                {
                    var b = colNames[j];
                    b.Item1 += row_norm[j].ToString(CultureInfo.InvariantCulture) + ",";
                    colNames[j] = b;
                }

            }
            //add output values
            var ocols = data.GetColumnsFromOutput();

            for (int i = 0; i < data.GetRowCount(isTest); i++)
            {
                //get normalized and numeric row
                var row = data.GetRowFromOutput(i, isTest);
                var row_norm = data.GetEncodedOutput(i,  isTest);

                //
                var c = colNames[colNames.Count - 1];
                if (outCol.ColumnDataType == ColumnType.Category)
                    c.Item1 += outCol.GetNumericValue(i) + ","; 
                else
                    c.Item1 += row_norm[0].ToString(CultureInfo.InvariantCulture) + ",";

               colNames[colNames.Count - 1] = c;
            }

            //create data frame from vectors
            string vecs = "";
            for (int i = 0; i < colNames.Count; i++)
            {
                var c = colNames[i];
                c.Item1 = c.Item1.Substring(0, c.Item1.Length - 1) + ")";
                colNames[i] = c;
                vecs += c.Item2+Environment.NewLine+c.Item1+Environment.NewLine;
                
            }

            rcode = rcode.Substring(0, rcode.Length - 1) + ");";
            rcode = vecs + rcode;
            
            return rcode;
        }

    }
}
