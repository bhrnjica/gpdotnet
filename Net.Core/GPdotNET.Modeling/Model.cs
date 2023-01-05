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
using GPdotNet.BasicTypes;
using GPdotNet.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Globalization;
using GPdotNet.Core;
using GPdotNet.Data;
using System.Text.Json.Serialization;

namespace GPdotNet.Modeling
{
    public class ActiveDataBase
    {
        public ActiveDataBase()
        {
            TC = new TerminationCriteria();
            TC.IsIteration = true;
            TC.Value = 500;
        }
        public Parameters Parameters { get; set; }
        public Function[] FunctionSet { get; set; }

        public TerminationCriteria TC { get; set; }

        public Action<ProgressReport, Parameters> reportRun { get; set; }
    }
    /// <summary>
    /// Implements Model class, which holds one GP solution
    /// </summary>
    [DataContract]
    public class Model
    {
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string ModelString
        {
            get
            {
                if (Factory != null)
                    return Factory.GPFactoryToString();
                else
                    return null;
            }
            set
            {
                Factory = new Factory();
                Factory.GPFactoryFromString(value);

            }
        }
        [DataMember]
        public DataSet1 DataSet
        {
            get;
            //{
            //    ;// return ExpData.GetDataSet();
            //}
            set;
            //{
            //    ;// ExpData = new Experiment(value);
            //}
        }
        
        public Model(string guid)
        {
            Guid = guid;
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string Guid { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Factory Factory { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public IData Inputs { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public String ModelName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Experiment ExpData { get; set; }
        //public string ExpString { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Project Project { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public bool IsDiry { get; set; }

        public void InitNewModel()
        {

            if (ExpData == null)
                throw new Exception("Projects is not initialized in the model.");

            string par = "";
            //first time setting parameter
            if (ExpData.GetOutputColumnType() == ColumnType.Binary)
                par = initBinaryClassParameters();
            else if (ExpData.GetOutputColumnType() == ColumnType.Category)
                par = initMultiClassParameters();
            else 
                par = initRegressionParameters();
            //
            var p = Parameters.FromString(par);
            //
            var fs = new Function[4] { new Function() { Selected=true, Id=2000, Weight=1 },
                                        new Function() { Selected=true, Id=2001, Weight=1 },
                                        new Function() { Selected=true, Id=2002, Weight=1 },
                                        new Function() { Selected=true, Id=2003, Weight=1 },
                                       };
            var t = terminalSet(p).ToArray();

            //
            Factory = new Factory();
            Factory.FunctionSet = fs;
            Factory.TerminalSet = t;
            Factory.Parameters = p;
        }

        public void InitPersistedModel()
        {
           
            //gp panel
            if (Factory != null && Factory.Parameters != null)
            {
                var p = Factory.Parameters;

                //Input data
                //Inputs = ExpData.GetInputData(p.Constants);
                var rv = ExpData.GetInputData();
                var dataSet = new ExperimentData(rv.train, rv.test, p.Constants);
                dataSet.SetExperiment(ExpData);
                Inputs = dataSet;

                //Calculate nonPersisted parameter properties
                Factory.Parameters.FeatureCount = ExpData.GetEncodedColumnInputCount();
                Factory.Parameters.IsMultipleOutput = ExpData.GetEncodedColumnOutputCount()>1;
            }
        }

        public void PrepareForSave(ActiveDataBase activeData)
        {
            //transfer activadata in to Model
            Factory.FunctionSet = copyFunctionSet(activeData.FunctionSet);
            var dic = activeData.Parameters.ToDictionary();
            Factory.Parameters = Parameters.FromDictionary(dic);
            //termination criteria
            Factory.TC.IsIteration = activeData.TC.IsIteration;
            Factory.TC.Value = activeData.TC.Value;

            //mark the file as saved
            IsDiry = false;

        }

        private string initBinaryClassParameters()
        {
           return "500;ACC;2;5;6;1;1;1;0.80;0;1;5;0.9;0.05;0.2;2;Sigm;0.5";
        }
        private string initMultiClassParameters()
        {
            return "500;ACC;2;5;6;1;1;1;0.80;0;1;5;0.9;0.05;0.2;2;Scal;0";

            //return parStr;
        }
        private string initRegressionParameters()
        {
            return  "500;RMSE;2;5;6;1;1;1;0.80;0;1;5;0.9;0.05;0.2;2;None;0";
        }

        private List<int> terminalSet(Parameters p)
        {
            var colCount = ExpData.GetColumnInputCount() + p.ConstNum;
            if (colCount > 1000)
                throw new Exception("GPdotNET supports up to 1000 features.");

            p.FeatureCount = ExpData.GetEncodedColumnInputCount();
            p.IsMultipleOutput = ExpData.GetEncodedColumnOutputCount() > 1;

            var term = new List<int>();
            for (int i = 0; i < colCount; i++)
            {
                term.Add(1000 + i);

            }

            return term;
        }

        private IFitness selectFitnessFunction(string fitn, IData inputs)
        {
            if (fitn.StartsWith("AE"))
                return new AEFitness(inputs);
            else if (fitn.StartsWith("MAE"))
                return new MAEFitness(inputs);
            else if (fitn.StartsWith("RMSE"))
                return new RMSEFitness(inputs);
            else if (fitn.StartsWith("RSE"))
                return new RSEFitness(inputs);
            else if (fitn.StartsWith("SE"))
                return new SEFitness(inputs);
            else if (fitn.StartsWith("ACC"))
                return new ACCFitness(inputs);
            else if (fitn.StartsWith("HSS"))
                return new HSSFitness(inputs);
            else if (fitn.StartsWith("PSS"))
                return new PSSFitness(inputs);
            else if (fitn.StartsWith("SRMS"))
                return new RMSEFitnessSoftmax(inputs);
            else if (fitn.StartsWith("LSRF"))
                return new LSRFitnessSoftmax(inputs);
            else if (fitn.StartsWith("MAHD"))
                return new MahanalobisDistance(inputs);
            else
                return new RMSEFitness(inputs);
        }

        public bool setLearningType(Parameters param)
        {
            //determine the type of ML
            param.OutputType = ExpData.GetOutputColumnType();
            if (param.OutputType == ColumnType.Binary)
            {
                if (param.RootName.StartsWith("Sigm"))
                    param.RootFunctionNode = new Function() { Id = 2048, Name = "Sigmoid", Arity = 1, HasParameter = true, Parameter = param.Threshold, Parameter2 = 2 };
                else if (param.RootName.StartsWith("Step"))
                    param.RootFunctionNode = new Function() { Id = 2049, Name = "Step", Arity = 1, HasParameter = true, Parameter = param.Threshold, Parameter2 = 2 };
                else if (param.RootName.StartsWith("Scal"))
                    param.RootFunctionNode = new Function() { Id = 2050, Name = "SSigmoid", Arity = 1, HasParameter = true, Parameter = param.Threshold, Parameter2 = 2 };
                else if (param.RootName.StartsWith("Soft"))
                    param.RootFunctionNode = new Function() { Id = 2051, Name = "Softmax", Arity = 2, Parameter2 = 2 };
            }
            else if (param.OutputType == ColumnType.Category)
            {
                var clss = ExpData.GetColumnsFromOutput().FirstOrDefault().Statistics.Categories.Count;

                if (param.RootName.StartsWith("Scal"))
                    param.RootFunctionNode = new Function() { Id = 2050, Name = "SSigmoid", Arity = 1, HasParameter = true, Parameter = clss, Parameter2 = clss };
                else if (param.RootName.StartsWith("Soft"))
                    param.RootFunctionNode = new Function() { Id = 2051, Name = "Softmax", Arity = clss, Parameter2 = clss };
                else
                    throw new Exception("Predefined Root Node is not compatible with multi class classification modeling!");
            }
            else if (param.OutputType == ColumnType.Numeric)
            {
                if (param.RootName.StartsWith("Pol3"))
                    param.RootFunctionNode = Globals.GetFunction(2039);// new Function() { Id = 2050, Name = "P3", Arity = 1, HasParameter = true, Parameter = clss, Parameter2 = clss };
               
            }
            return true;
        }

        private void setRandomConstants(Parameters param, bool resetPrevSolution)
        {
            if (resetPrevSolution)
            {
                //when reset prev solution reset random constants too
                param.Constants = Globals.GenerateConstants(param.ConstFrom, param.ConstTo, param.ConstNum);
            }
            else//we have to check if constance parameters changed
            {
                if (Factory != null && (Factory.Parameters.ConstFrom != param.ConstFrom || Factory.Parameters.ConstTo != param.ConstTo || Factory.Parameters.ConstNum != param.ConstNum))
                    param.Constants = Globals.GenerateConstants(param.ConstFrom, param.ConstTo, param.ConstNum);
            }
            //raise exception if constants is not initialized
            if ((param.Constants == null || param.Constants.Length == 0) && param.ConstNum > 0)
                param.Constants = Globals.GenerateConstants(param.ConstFrom, param.ConstTo, param.ConstNum);
        }

        //info to panels
        public string FunctionToString(Function[] fun)
        {
            if (fun != null)
                return string.Join(";", fun.Select(x => $"1,{x.Id},{x.Weight}").ToArray());
            else
                return "";
        }
        //public bool FunctionFromGUI(Function[] funs)
        //{
        //    Factory.FunctionSet = funs;
        //    return true;
        //}
        //public string ParametersToGUI()
        //{
        //    if (Factory != null)
        //        return Factory.Parameters.ToString();
        //    else
        //        return "";
        //}

        public Function[] FunctionSetFromString(string funStr)
        {
            try
            {
                var fns = funStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                List<Function> fun = new List<Function>();
                foreach (var frow in fns)
                {
                    var f = frow.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (f != null && f.Length == 3 && f[0] == "1")
                    {
                        var id = int.Parse(f[1]);
                        var ff = GPdotNet.Core.Globals.GetFunction(id);
                        ff.Weight = int.Parse(f[2]);
                        fun.Add(ff);
                    }
                }
                return fun.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void ParametersFromString(string parStr)
        {
            try
            {
                Parameters p = new Parameters();
                Factory.Parameters = Parameters.FromString(parStr);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Run GP Program
        /// </summary>
        public void RunGP(ActiveDataBase currentData, CancellationToken cancellationToken, bool resetPrevSolution)
        {
            try
            {
                if (resetPrevSolution)
                {
                    ResetModel();
                }
                //Get parameters from Settings panel
                var param = Parameters.FromDictionary(currentData.Parameters.ToDictionary());

                //set random constants
                setRandomConstants(param, resetPrevSolution);


                //use this only when developing this module in order to make debug simple
                //param.ParallelProcessing = false;

                //create fitness function
                //Inputs = ExpData.GetInputData(param.Constants);
                var rv = ExpData.GetInputData();
                var dataSet = new ExperimentData(rv.train, rv.test, param.Constants);
                dataSet.SetExperiment(ExpData);
                Inputs = dataSet;
                param.FitnessFunction = selectFitnessFunction(param.FitnessName, Inputs);

                if (param.FitnessFunction == null)
                    throw new Exception("Fitness type is not defined!");
                
                //define Learning type
                setLearningType(param);

                //creating function and terminal set
                Function[] functionSet = copyFunctionSet(currentData.FunctionSet);


                var term = terminalSet(param);
               
                //create termination criteria
                var terrCriteria = new TerminationCriteria() { IsIteration = currentData.TC.IsIteration, Value = currentData.TC.Value };
                //param.MiniBatch = terrCriteria.MinibatchSize;
                //create GPFactory
                if (Factory != null && !resetPrevSolution)
                {
                    Factory.Population.Token = cancellationToken;
                    Factory.Continue(param, functionSet, term.ToArray());
                }
                else
                    Factory = new Core.Factory(param, functionSet, term.ToArray(), cancellationToken);


                //start GP
                Factory.Run(currentData.reportRun, terrCriteria, cancellationToken);

                IsDiry = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Run GP Program
        /// </summary>
        public async Task RunGPAsync(ActiveDataBase currentData, CancellationToken cancellationToken, bool resetPrevSolution)
        {
            try
            {
                if (resetPrevSolution)
                {
                    ResetModel();
                }
                //Get parameters from Settings panel
                var param = Parameters.FromDictionary(currentData.Parameters.ToDictionary());

                //set random constants
                setRandomConstants(param, resetPrevSolution);


                //use this only when developing this module in order to make debug simple
                //param.ParallelProcessing = false;

                //create fitness function

                //Inputs = ExpData.GetInputData(param.Constants);
                var rv = ExpData.GetInputData();
                var dataSet = new ExperimentData(rv.train, rv.test, param.Constants);
                dataSet.SetExperiment(ExpData);
                Inputs = dataSet;
                //
                param.FitnessFunction = selectFitnessFunction(param.FitnessName, Inputs);

                if (param.FitnessFunction == null)
                    throw new Exception("Fitness type is not defined!");

                //define Learning type
                setLearningType(param);

                //creating function and terminal set
                Function[] functionSet = copyFunctionSet(currentData.FunctionSet);


                var term = terminalSet(param);
                //create termination criteria
                var terrCriteria = new TerminationCriteria() { IsIteration = currentData.TC.IsIteration, Value = currentData.TC.Value};
                //create GPFactory
                if (Factory != null && !resetPrevSolution)
                {
                    Factory.Population.Token = cancellationToken;
                    Factory.Continue(param, functionSet, term.ToArray());
                }
                else
                    Factory = new Core.Factory(param, functionSet, term.ToArray(), cancellationToken);

                IsDiry = true;

                //start GP
                await Factory.RunAsync(currentData.reportRun, terrCriteria, cancellationToken);

                
            }
            catch (Exception)
            {
                throw;
            }
        }

       
        private Function[] copyFunctionSet(Function[] functionSet)
        {
            if(functionSet!=null && functionSet.Length>0)
            {
                var fnc = new Function[functionSet.Length];
                for(int i=0; i<functionSet.Length; i++)
                {
                    var f = new Function();
                    f.Arity = functionSet[i].Arity;
                    f.Definition = functionSet[i].Definition;
                    f.ExcelDefinition = functionSet[i].ExcelDefinition;
                    f.HasParameter = functionSet[i].HasParameter;
                    f.Id = functionSet[i].Id;
                    f.MathematicaDefinition = functionSet[i].MathematicaDefinition;
                    f.Name = functionSet[i].MathematicaDefinition;
                    f.Parameter = functionSet[i].Parameter;
                    f.Parameter2 = functionSet[i].Parameter2;
                    f.RDefinition = functionSet[i].RDefinition;
                    f.Selected = functionSet[i].Selected;
                    f.Weight = functionSet[i].Weight;
                    //
                    fnc[i] = f;
                }
                return fnc;
            }

            return null;
        }

        private void ResetModel()
        {
            
        }

        public double[] calculateOutput(bool isTraining, bool probValue=false)
        {
            if (Factory.ProgresReport.BestSolution == null)
                return null;
            //
            var ch = Factory.ProgresReport.BestSolution;//.expressionTree;
            var par = Factory.Parameters;
            if (par.RootFunctionNode == null)
                setLearningType(par);
            double[] yc = null;
            if (ch != null)
            {
                yc = Inputs.CalculateOutput(ch, par, isTraining);//gp

                //no testing data set
                if (yc == null)
                    return null;

                //de normalize output
                double[] output = yc;
                for (int i = 0; i < yc.Length; i++)
                {
                    //calculate de normalization
                    if (ExpData.GetOutputColumnType() == ColumnType.Numeric)
                    {
                        double[] normRow = new double[1] { yc[i] };
                        output[i] = ExpData.GetDecodedOutputRow(normRow)[0];
                    }
                    else
                    {
                        if(par.RootFunctionNode.Id==2048)//sigmoid
                        {
                            if (probValue)//when model is evaluate we need probability f event in order to optimize threshold value
                                output[i] = yc[i];
                            else
                                output[i] = yc[i] > par.RootFunctionNode.Parameter ? 1 : 0;
                        }
                        else if (par.RootFunctionNode.Id == 2050)//step
                        {
                            output[i] = Math.Truncate(yc[i]);
                        }
                        else
                          output[i] = Math.Truncate(yc[i]);
                    }
                }
                return output;
            }
            return null;//no solution yet

        }

        public Dictionary<string, List<object>> ModelEvaluation()
        {
            var bs = Factory.ProgresReport.BestSolution;
            var par = Factory.Parameters;
            var dic = new Dictionary<string, List<object>>();

            //
            if (bs != null)
            {
                //get output for training data set
                var yy = ExpData.GetColumnOutputValues(false);
                var y1 = yy[0];//Inputs.GetDataOutputCol(true);//experiment
                var output = calculateOutput(true, true);

                //get output for testing data set if available
                var yy1 = ExpData.GetColumnOutputValues(true);
                var y2 = yy1 != null ? yy1[0] : null;//Inputs.GetDataOutputCol(false);//experiment
                var outputt = calculateOutput(false, true);

                //
                var col = ExpData.GetColumnsFromOutput().FirstOrDefault();
                if (col.ColumnDataType == ColumnType.Binary || col.ColumnDataType == ColumnType.Category)
                    dic.Add("Classes", col.Statistics.Categories.ToList<object>());


                //add data sets
                dic.Add("obs_train", y1.Select(x => (object)x).ToList<object>());
                dic.Add("prd_train", output.Select(x => (object)x).ToList<object>());
                //add test dataset
                if (y2 != null)
                {
                    dic.Add("obs_test", y2.Select(x => (object)x).ToList<object>());
                    dic.Add("prd_test", outputt.Select(x => (object)x).ToList<object>());
                }

                return dic;

            }

            return null;

        }

        public void ResetSolution()
        {
            Factory.History.Clear();
            Factory.Population.Clear();
            Factory.ProgresReport.BestIteration = 0;
            Factory.ProgresReport.BestSolution = null;
            Factory.ProgresReport.Iteration = 0;
            Factory.ProgresReport.IterationStatistics = null;
            Factory.ProgresReport.IterationStatus = IterationStatus.Initialize;
            Factory.ProgresReport.SolutionStarted = DateTime.MinValue;
        }

        /// <summary>
        /// Returns the curent settings of the model
        /// </summary>
        /// <returns></returns>
        public ActiveDataBase GetModelSettings(Action<ProgressReport, Parameters> reportRun)
        {
            var adb = new ActiveDataBase();
            //
            //first 4 algebraical operations +,-,*,/
            adb.FunctionSet = copyFunctionSet(Factory.FunctionSet);

            //terminal set
            // adb.t= new int[] { 1000, 1001, 1002, 1003 };

            //prepare params
            adb.Parameters = Parameters.FromDictionary(Factory.Parameters.ToDictionary()); ;

            //termination criteria
            adb.TC = new TerminationCriteria() { IsIteration = Factory.TC.IsIteration, Value = Factory.TC.Value};

            adb.reportRun = reportRun;

            return adb;
        }
    }

}