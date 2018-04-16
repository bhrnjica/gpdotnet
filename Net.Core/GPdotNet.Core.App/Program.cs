using System;
using System.Linq;
using GPdotNet.Core;
using System.Threading.Tasks;
using System.Threading;
using GPdotNet.Data;
using GPdotNet.Interfaces;
using GPdotNet.BasicTypes;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using GPdotNet.Modeling;
using System.Collections.Generic;

namespace GPdotNETAppCore
{
    class Program
    {
       
        static async Task Main(string[] args)
        {
           // OpenRunAndSaveProject1();

            // await CreateNewIrisProject();

            //Irsi data set
            await OpenRunAndSaveProject1();

            //surface roughenss
            //await OpenRunAndSaveRegression();



            //stoping to program terminate
            Console.WriteLine("Press any key to continue....");
            Console.Read();
        }
        static string dataSetName = "Add DataSet Name";

        private static async Task OpenRunAndSaveRegression()
        {
            var irisPath = "Data/surface_roughness.gpa";
            dataSetName = "Surface Roughness";
            var project = Project.Open(irisPath);

            //select first model from project collection
            var irisModel = project.Models.FirstOrDefault();
            //get current active data
            var actvData = irisModel.GetModelSettings(reportProgress);
            //set predefined rootnode
            //actvData.Parameters.RootName = "Pol3";


            //if the solution is reseted constants from MODel must be update to activeData
            await irisModel.RunGPAsync(actvData, new CancellationToken(), true);

            //saved into bin folder
            var strPath = $@"Data\\surface_roughness_{DateTime.Now.Ticks}.gpa";

            project.Save(strPath);

        }

        private static async Task OpenRunAndSaveProject()
        {
            var irisPath = "Data/Iris.gpa";
            dataSetName = "Iris flower identification";
            var project = Project.Open(irisPath);

            //select first model from project collection
            var irisModel = project.Models.FirstOrDefault();
            //get current active data
            var actvData = irisModel.GetModelSettings(reportProgress);

            //if the solution is reseted constants from MODel must be update to activeData
            await irisModel.RunGPAsync(actvData, new CancellationToken(), true);

            //saved into bin folder
            var strPath = $@"Data\\iris_saved_{DateTime.Now.Ticks}.gpa";

            project.Save(strPath);

        }

        private static async Task OpenRunAndSaveProject1()
        {
            var irisPath = "Data/Iris.gpa";
            dataSetName = "Iris flower identification";
            var project = Project.Open(irisPath);

            //select first model from project collection
            var irisModel = project.Models[4];
            //get current active data
            var actvData = irisModel.GetModelSettings(reportProgress);

            //change Default PArams
            actvData.Parameters.ParallelProcessing = false;
            actvData.Parameters.FitnessName = "MAHD";
            actvData.TC.Value = 10;
            //if the solution is reseted constants from MODel must be update to activeData
            await irisModel.RunGPAsync(actvData, new CancellationToken(), true);

            //saved into bin folder
            var strPath = $@"Data\\iris_saved_{DateTime.Now.Ticks}.gpa";

            project.Save(strPath);

        }

        static async Task CreateNewIrisProject()
        {
            dataSetName = "Iris flower identification";
            Project proj = new Project("testProject");
            proj.InitiNewProject("IrisPrjTest");
            var metaData = getMetaData("iris");
            var strData = getIrisString(',',1);
            DataSet1 ds = new DataSet1();
            ds.MetaData = metaData;
            ds.Data = strData;
            var dataSet = ds.GetDataSet(true);

            //
            proj.DataSet = ds;
            //rft format of the simple text
            proj.ProjectInfo = "{\\rtf1\\ansi\\deff0\\nouicompat{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs17\\lang1033 Famous classification dataset\\par\r\n}\r\n";
            proj.CreateModel("iris-data-set", "Iris", true);
            var model = proj.Models[0];
            //

            ActiveDataBase actvData = getGPParams();
            //change Default PArams
            actvData.Parameters.ParallelProcessing = false;
            actvData.Parameters.FitnessName = "MAD";

            //if the solution is reseted constants from MODel must be update to activeData
            await model.RunGPAsync(actvData, new CancellationToken(), true);

            //saved into bin folder
            var strPath = $@"Data\\iris_{DateTime.Now.Ticks}.gpa";
            
            proj.Save(strPath);
        }

        static async Task CreateNewRegressionProject()
        {
            dataSetName = "Iris flower identification";
            Project proj = new Project("testProject");
            proj.InitiNewProject("IrisPrjTest");
            var metaData = getMetaData("iris");
            var strData = getIrisString(',', 1);
            DataSet1 ds = new DataSet1();
            ds.MetaData = metaData;
            ds.Data = strData;
            var dataSet = ds.GetDataSet(true);

            //
            proj.DataSet = ds;
            //rft format of the simple text
            proj.ProjectInfo = "{\\rtf1\\ansi\\deff0\\nouicompat{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs17\\lang1033 Famous classification dataset\\par\r\n}\r\n";
            proj.CreateModel("iris-data-set", "Iris", true);
            var model = proj.Models[0];
            //

            ActiveDataBase actvData = getGPParams();
            //change Default PArams
            actvData.Parameters.ParallelProcessing = true;
            actvData.Parameters.FitnessName = "RMSE";


            //if the solution is reseted constants from MODel must be update to activeData
            await model.RunGPAsync(actvData, new CancellationToken(), true);

            //saved into bin folder
            var strPath = $@"Data\\iris_{DateTime.Now.Ticks}.gpa";

            proj.Save(strPath);
        }

        #region Helpers
        private static ActiveDataBase getGPParams()
        {
            var adb = new ActiveDataBase();

            //
            //first 4 algebraical operations +,-,*,/
            adb.FunctionSet = Globals.functionSet.Where(x=>x.Value.Selected).Select(x=>x.Value).ToArray();

            //prepare params
            adb.Parameters = prepareGP();

            //termination criteria
            adb.TC = new TerminationCriteria() { IsIteration = true, Value = 50 };

            adb.reportRun = reportProgress;

            return adb;
        }

        private static Parameters prepareGP()
        {
            var param = new Parameters();
            //probabilities
            param.InitializationMethod = InitializationMethod.HalfHalf;
            param.SelectionMethod = SelectionMethod.FitnessProportionateSelection;
            param.PopulationSize = 100;
            param.MaxLevel = 7;
            param.MaxInitLevel = 7;
            param.BroodSize = 1;
            //probability
            param.CrossoverProbability = 1f;// 0.95f;
            param.MutationProbability = 1f;//0.05f;
            param.SelectionProbability = 0.15f;
            //output related parameters
            param.OutputType = ColumnType.Category;
            param.RootFunctionNode = Globals.GetFunction(2051);//SoftMAx
            param.RootName = "Softmax";
            param.IsMultipleOutput = true;
            param.Threshold = 0.5f;
            //
            param.ParallelProcessing = true;
            param.Elitism = 3;
            param.ArgValue = 0.3f;//selection
            param.IsProtectedOperation = true;
            //fitness
            param.FitnessName = "ACC";
            //param.FitnessFunction = new RMSEFitnessSoftmax();
            //random constants
            param.ConstFrom = 0;
            param.ConstTo = 1f;
            param.ConstNum = 2;
            param.Constants = new double[] { 0.2, 0.4 };

            return param;

        }


        private static string[][] getIrisString(char delimiter, int skipRows)
        {
           var strRows = File.ReadAllLines("Data/iris.csv");
            List<string[]> strData = new List<string[]>();
           foreach(var r in strRows.Skip(skipRows))
            {
                var row = r.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                strData.Add(row);
            }

            return strData.ToArray();
        }

        private static MetaColumn[] getMetaData(string dataName)
        {
            if(dataName=="iris")
            {
                MetaColumn[] cols = new MetaColumn[5];
            cols[0] = new MetaColumn(){Id = 0,Encoding = "none",Index = 0,MissingValue = "none",
                                     Name = "sepal_length",Scale = "none",Type = "numeric",Param = "input",};
            cols[1] = new MetaColumn(){Id = 1,Encoding = "none",Index = 1,MissingValue = "none",
                                     Name = "sepal_width",Scale = "none",Type = "numeric",Param = "input",};
            cols[2] = new MetaColumn(){Id = 2,Encoding = "none",Index = 2,MissingValue = "none",
                                     Name = "petal_length",Scale = "none",Type = "numeric",Param = "input",};
            cols[3] = new MetaColumn(){Id = 3,Encoding = "none",Index = 3,MissingValue = "none",
                                     Name = "petal_width",Scale = "none",Type = "numeric",Param = "input",};
            cols[4] = new MetaColumn(){Id = 4,Encoding = /*"1 out of N"*/"Level",Index = 4,MissingValue = "none",
                                     Name = "species",Scale = "none",Type = "categorical",Param = "output",};

                return cols;
            }

            throw new Exception($"Meatadata for data set name '{dataName}' is not defined!");
        }
        #endregion

        static private void reportProgress(ProgressReport pr, Parameters par)
        {
            if (pr.IterationStatus == IterationStatus.Initialize)
                startProgress(par,dataSetName);
            else
                Console.WriteLine($"It={pr.Iteration}; Fitness= {pr.BestSolution.Fitness.ToString("F")}, Total={pr.IterationStatistics.IterationSeconds} sec; " /*+ pr.Message*/);
        }

        static private void startProgress(Parameters par, string dataseName)
        {
            Console.WriteLine("GPdotNET v5 - tree based GP");
            Console.WriteLine("****************************");
            Console.WriteLine($"Dataset Name: {dataSetName}");
            Console.WriteLine($"ML type: {par.OutputType}");
            Console.WriteLine($"Number of Features: {par.FeatureCount}");
            Console.WriteLine($"Random Constance: {string.Join(';',par.Constants)}");
            Console.WriteLine($"Fitness function: {par.FitnessName}");
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("");
        }

    }
}