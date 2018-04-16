using GPdotNet.BasicTypes;
using GPdotNet.Core;
using GPdotNet.Export;
using GPdotNet.Interfaces;
using GPdotNet.Modeling;
using GPdotNet.Wnd.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZedGraph;

namespace GPdotNet.Wnd.Dll
{
    public class ActiveData: ActiveDataBase
    {
        public ActiveData()
        {
            TC = new TerminationCriteria();
            TC.IsIteration = true;
            TC.Value = 500;
            RunPanelData = new RunPanelData();
            TestPanelData = new TestPanelData();
        }
        public RunPanelData RunPanelData { get; set; }
        public TestPanelData TestPanelData { get; set; }
    }
    public class ModelController
    {
        public ActiveData ActiveData { get; set; }
        //callbacks for interaction with WInForms GUI Panels 
        public Action<RunPanelData> ReportProgress { get; set; }
        public Action<TestPanelData> ReportTest { get; set; }
        public Action<IChromosome, Parameters> ReportResults { get; set; }
        public Action<string> SetRunMode { get; set; }
        public Action<string> SetStopMode { get; set; }

        //callback functions for sending information to GUI Panels
        public Action<LineItem> UpdateChartData { get; set; }
        public Action<LineItem> UpdateTestChartData { get; set; }
        public Action<string[]> UpdateTestOutputData { get; set; }
        public Action<bool> UpdateChartFitness { get; set; }
        public Action<Exception> ReportException { get; set; }
        public ColumnType ModelType { get; set; }
        public List<string> Classes { get; set; }
        public string Label { get; set; }
        //cancellation to cancel GP run
        public CancellationTokenSource CancelationSource;

        public ModelController(string guid, ColumnType modelType, List<string> classes, string label="y")
        {
            ActiveData = new ActiveData();
            Label = label;
            ModelType = modelType;
            Classes = classes;
            ActiveData.RunPanelData = prepareRunPanel();
            ActiveData.TestPanelData = prepareTestPanel();

            Model = new Model(guid);

        }
        public ProjectController Parent { get; set; }
        
        public bool IsModified()
        {
            //after RUN all models contains unsaved data
            if (Model.IsDiry)
                return true;

            //compare parameters
            var p1 = ActiveData.Parameters.ToString();
            var p2 = Model.Factory.Parameters.ToString();

            if (!p1.Equals(p2))
                return true;
            //compare function set
            var f1 = string.Join(";", ActiveData.FunctionSet.Select(x => $"1,{x.Id},{x.Weight}").ToArray());
            var f2 = string.Join(";", Model.Factory.FunctionSet.Select(x => $"1,{x.Id},{x.Weight}").ToArray());

            if (!f1.Equals(f2))
                return true;

            //compare termination criteria
            if (Model.Factory.TC==null || ActiveData.TC.IsIteration != Model.Factory.TC.IsIteration || ActiveData.TC.Value != Model.Factory.TC.Value)
                return true;
            
            return false;
        }

        public Model Model { get; set; }
        public bool IsRunnig { get; set; }
        public bool IsActive { get; set; }
        public void InitNewModel()
        {
            //init model
            Model.InitNewModel();

            initActiveData();
        }

        private void initActiveData()
        {
            //init additional stuff related to GUI
            ActiveData.Parameters = Parameters.FromString(Model.Factory.Parameters.ToString());
            ActiveData.FunctionSet = Model.FunctionSetFromString(Model.FunctionToString(Model.Factory.FunctionSet));
            if (Model.Factory.TC == null)
                Model.Factory.TC = new TerminationCriteria() { IsIteration = true, Value=500 };
            ActiveData.TC = new TerminationCriteria() { IsIteration = Model.Factory.TC.IsIteration, Value = Model.Factory.TC.Value };
            
        }

        public string GetFunctionset()
        {
           return Model.FunctionToString(ActiveData.FunctionSet);
        }

        public string GetParameters()
        {
            return ActiveData.Parameters.ToString();
        }

        public void SetFunction(string funStr)
        {
            ActiveData.FunctionSet = Model.FunctionSetFromString(funStr);
        }
        public void SetParameters(string parStr)
        {
            ActiveData.Parameters= Parameters.FromString(parStr);
        }
        public void SetTerminationCriteria((int, float) tc)
        {
            ActiveData.TC = new Core.TerminationCriteria() { IsIteration = tc.Item1 == 0, Value = tc.Item2};
        }

        public void SetRunPanel(RunPanelData runPanelData)
        {
            if (runPanelData == null)
                runPanelData = new RunPanelData();
            //load experimental data on Run panel
            var y = Model.ExpData.GetColumnOutputValues(false);
            if (y != null)
            {
                runPanelData.experimentalData.Clear();
                for (int i = 0; i < y[0].Length; i++)
                    runPanelData.experimentalData.AddPoint(new PointPair( i + 1, y[0][i]));
            }

            //calculate output 
            if (Model.Factory.Parameters.RootFunctionNode == null)
                Model.setLearningType(Model.Factory.Parameters);
            var ygp = Model.calculateOutput(true);
            if(ygp != null)//model is not yet calculated
            {
                runPanelData.gpCalculateOutput.Clear();

                for (int i = 0; i < ygp.Length; i++)
                {
                    PointPair pt = new PointPair(i + 1, ygp[i]);
                    runPanelData.gpCalculateOutput.AddPoint(pt);
                }
                   
            }

            //set termination criteria to run panel
            runPanelData.TerminationType = ActiveData.TC.IsIteration ? 0 : 1;
            runPanelData.TerminationValue = ActiveData.TC.Value;
        }



        public void SetTestPanel(TestPanelData testPanelData)
        {
            if (testPanelData == null)
                testPanelData = new TestPanelData();

            //load testing data on Test panel
            testPanelData.Header = Model.ExpData.HeaderAsString;
            var yt = Model.ExpData.GetColumnOutputValues(true);
            if (yt != null)
            {
                testPanelData.experimentalData.Clear();
                for (int i = 0; i < yt[0].Length; i++)
                    testPanelData.experimentalData.AddPoint(i + 1, yt[0][i]);
            }
            //calculate output 
            var ygp = Model.calculateOutput(false);
            testPanelData.gpCalculateOutput.Clear();
            if (ygp != null)
            {
                for (int i = 0; i < ygp.Length; i++)
                    testPanelData.gpCalculateOutput.AddPoint(i + 1, ygp[i]);
            }
        }

        public void SetParent(ProjectController pController)
        {
            Parent = pController;
            Model.Project = pController.Project;
        }

        private RunPanelData prepareRunPanel()
        {
            var runPanelData = new RunPanelData();
            runPanelData.Label = Label;
            runPanelData.OutputType = ModelType;
            runPanelData.Classes = Classes;
            runPanelData.TerminationType = 0;
            runPanelData.TerminationValue = 500;
            runPanelData.BestFitness = 0;

            if(ModelType != ColumnType.Numeric)
            {
                runPanelData.experimentalData = new LineItem("Data Points", null, null, Color.Red, ZedGraph.SymbolType.Circle, 0);
                runPanelData.experimentalData.Symbol.Fill = new Fill(Color.Red);
                runPanelData.experimentalData.Symbol.Size = 5;


                runPanelData.gpCalculateOutput = new LineItem("GP Model", null, null, Color.Blue, ZedGraph.SymbolType.Diamond, 0);
                runPanelData.gpCalculateOutput.Symbol.Border = new Border(Color.Blue, 0.5f);
                runPanelData.gpCalculateOutput.Symbol.Fill = new Fill(Color.Blue);
                runPanelData.gpCalculateOutput.Symbol.Size = 4;
            }
            else
            {
                runPanelData.experimentalData = new LineItem("Data Points", null, null, Color.Red, ZedGraph.SymbolType.None, 0.5f);
                runPanelData.gpCalculateOutput = new LineItem("GP Model", null, null, Color.Blue, ZedGraph.SymbolType.None, 0.5f);
                
            }

            

            runPanelData.maxFitness = new LineItem("Maximum", null, null, Color.Red, ZedGraph.SymbolType.None);
            runPanelData.maxFitness.Symbol.Border = new Border(Color.Red, 0.1f);


            runPanelData.avgFitness = new LineItem("Average", null, null, Color.Blue, ZedGraph.SymbolType.None);
            runPanelData.avgFitness.Symbol.Border = new Border(Color.Blue, 0.1f);
            return runPanelData;
        }
        private TestPanelData prepareTestPanel()
        {
            var testPanelData = new TestPanelData();
            testPanelData.Label = Label;
            testPanelData.OutputType = ModelType;
            testPanelData.Classes = Classes;
            if (ModelType != ColumnType.Numeric)
            {
                testPanelData.experimentalData = new LineItem("Data Points", null, null, Color.Red, ZedGraph.SymbolType.Circle, 0);
                testPanelData.experimentalData.Symbol.Fill = new Fill(Color.Red);
                testPanelData.experimentalData.Symbol.Size = 5;


                testPanelData.gpCalculateOutput = new LineItem("GP Model", null, null, Color.Blue, ZedGraph.SymbolType.Diamond, 0);
                testPanelData.gpCalculateOutput.Symbol.Border = new Border(Color.Blue, 0.5f);
                testPanelData.gpCalculateOutput.Symbol.Fill = new Fill(Color.Blue);
                testPanelData.gpCalculateOutput.Symbol.Size = 4;
            }
            else
            {
                testPanelData.experimentalData = new LineItem("Data Points", null, null, Color.Red, ZedGraph.SymbolType.None, 0.5f);
                testPanelData.gpCalculateOutput = new LineItem("GP Model", null, null, Color.Blue, ZedGraph.SymbolType.None, 0.5f);

            }

            //
            return testPanelData;
        }

        public void InitPersistedModel()
        {
            Model.InitPersistedModel();
            initActiveData();
            //if fitness graphs are empty fill them
            if (Model.Factory != null && ActiveData.RunPanelData.avgFitness.Points.Count == 0 && Model.Factory.History.Count > 0)
            {
                foreach (var h in Model.Factory.History)
                {
                    ActiveData.RunPanelData.maxFitness.AddPoint(h.Iteration, h.MaxFitness);
                    ActiveData.RunPanelData.avgFitness.AddPoint(h.Iteration, h.AvgFitness);
                }

            }
            //
            ActiveData.TC.IsIteration = Model.Factory.TC.IsIteration;
            ActiveData.TC.Value= Model.Factory.TC.Value;
            ActiveData.RunPanelData.BestFitness = Model.Factory.ProgresReport.IterationStatistics.MaximumFitness;
            ActiveData.RunPanelData.ChangedAtGeneration = Model.Factory.ProgresReport.BestIteration;
            ActiveData.RunPanelData.CurrentIteration = Model.Factory.ProgresReport.Iteration;
        }

        public void ExportToExcel(string filePath)
        {
            try
            {
                if (Model==null || Model.Factory == null || Model.Factory.ProgresReport.BestSolution == null)
                {
                    throw new Exception("No solution to export!");
                }

                //
                GPToExcel.Export(Model.Factory.ProgresReport.BestSolution.expressionTree.Clone(), Model.Factory.Parameters, Model.Inputs, Model.ExpData, filePath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ExportToR(string filePath)
        {
            try
            {
                if (Model == null || Model.Factory == null || Model.Factory.ProgresReport.BestSolution == null)
                {
                    throw new Exception("No solution to export!");
                }

                //
                GPtoR.Export(Model.Factory.ProgresReport.BestSolution.expressionTree.Clone(), Model.Factory.Parameters, Model.Inputs, Model.ExpData, filePath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ExportToWM(string filePath)
        {
            try
            {
                if (Model == null || Model.Factory == null || Model.Factory.ProgresReport.BestSolution == null)
                {
                    throw new Exception("No solution to export!");
                }

                //
                GPtoMathematica.Export(Model.Factory.ProgresReport.BestSolution.expressionTree.Clone(), Model.Factory.Parameters, Model.Inputs, Model.ExpData, filePath);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Stop()
        {
            
            //change GUI to stop mode
            if (SetStopMode != null)
                SetStopMode(Model.Guid);

            return true;
            
        }

       

        public void Run(bool resetSolution, FunctionPanel funPanel1, ParametersPanel parPanel1, RunPanel runPanel1, ResultPanel resPanel1, TestPanel testPanel1)
        {
            try
            {
                IsRunnig = true;
                if (SetRunMode != null)
                    SetRunMode(Model.Guid);

                CancelationSource = new CancellationTokenSource();
                var actvData = getCurrentValues(funPanel1, parPanel1, runPanel1, resPanel1, testPanel1);
                //if the solution is reseted constants from MODel must be update to activeData
                if (resetSolution)
                    resetActiveDataValues();
                Model.RunGP(actvData, CancelationSource.Token, resetSolution);
                actvData.Parameters = Parameters.FromDictionary(Model.Factory.Parameters.ToDictionary());
                parPanel1.ParametersFromString(actvData.Parameters.ToString());
                //IconUri = "Images/runningmodel.png";
            }
            catch (Exception)
            {
                Stop();
                throw;
            }
           
        }

        private void resetActiveDataValues()
        {
            if(ActiveData!=null && ActiveData.RunPanelData != null)
            {
                ActiveData.RunPanelData.avgFitness.Clear();
                ActiveData.RunPanelData.maxFitness.Clear();
                ActiveData.RunPanelData.gpCalculateOutput.Clear();
            }
            if (ActiveData != null && ActiveData.TestPanelData != null)
            {
                ActiveData.TestPanelData.gpCalculateOutput.Clear();
            }
           

        }

        internal ActiveData getCurrentValues(FunctionPanel funPanel1, ParametersPanel parPanel1, RunPanel runPanel1, ResultPanel resPanel1, TestPanel testPanel1)
        {
            //var actData = new ActiveData();
            ActiveData.reportRun = reportProgress;
            ActiveData.FunctionSet = funPanel1.GetSelectedFunctions();
            ActiveData.Parameters = Parameters.FromDictionary(parPanel1.ParametersToDictionary());
            
            var tc= runPanel1.GetTerminationCriteria();
            ActiveData.TC = new TerminationCriteria() { IsIteration = tc.Item1==0?true:false, Value = tc.Item2};

            //settings callback during GP run
            ReportProgress = runPanel1.ReportProgress;
            ReportResults = resPanel1.ReportResult;
            UpdateChartData = runPanel1.UpdateChartDataAsync;
            UpdateChartFitness = runPanel1.UpdateChartFitnessAsync;
            UpdateTestChartData = testPanel1.UpdateChartDataAsync;
            UpdateTestOutputData = testPanel1.UpdateOutputColumnAsync;


            return ActiveData;
        }

        /// <summary>
        /// Fill active data from model
        /// </summary>
        public void SetActiveData()
        {
            //var actData = new ActiveData();
            //ActiveData.reportRun = Model.Factory.ProgresReport;
            ActiveData.FunctionSet = Model.FunctionSetFromString(Model.FunctionToString(Model.Factory.FunctionSet));
            ActiveData.Parameters = Parameters.FromDictionary(Model.Factory.Parameters.ToDictionary());

            var tc = Model.Factory.TC;
            ActiveData.TC = new TerminationCriteria() { IsIteration = tc.IsIteration, Value = tc.Value };

        }

        /// <summary>
        /// This method is called after one generation is completed. 
        /// It is iteration reporter which is used to send progress to GUI panels.
        /// </summary>
        /// <param name="pr"></param>
        private void reportProgress(ProgressReport pr, Parameters par)
        {

            //when the GP factory finish iterations
            if (pr.IterationStatus == IterationStatus.Stopped ||
                pr.IterationStatus == IterationStatus.Compleated ||
                pr.IterationStatus == IterationStatus.Exception)
            {
                //unchecked run button on main Ribbon control
                if (SetStopMode != null)
                    SetStopMode(Model.Guid);

                if (pr.IterationStatus == IterationStatus.Exception && ReportException != null)
                    ReportException(new Exception(pr.Message));//report problem to presenter
            }
            //when the GP factory starts i-th iteration
            if (pr.IterationStatus == IterationStatus.Initialize)
            {
                prepareForGPRun();

            }

            //add fitness related point to the chart lines
            ActiveData.RunPanelData.maxFitness.AddPoint(pr.Iteration, pr.IterationStatistics.MaximumFitness);
            ActiveData.RunPanelData.avgFitness.AddPoint(pr.Iteration, pr.IterationStatistics.AverageFitness);
            ActiveData.RunPanelData.CurrentIteration = pr.Iteration;

            //if better solution is found change the modelData and add fitness point
            if (pr.BestSolution.Fitness > ActiveData.RunPanelData.BestFitness)
            {
                //BestSolution = pr.BestSolution.Clone() as Chromosome;
                //update change data gen
                ActiveData.RunPanelData.ChangedAtGeneration = pr.Iteration;
                //back information to progress report
                pr.BestIteration = pr.Iteration;

                //show best solution 
                runAndReport(pr, par);

                //testing current solution 
                testAndReport(pr, par);
            }

            //this line must be after if statement
            ActiveData.RunPanelData.BestFitness = pr.BestSolution.Fitness;

            //update fitness
            if (UpdateChartFitness != null && IsActive)
                UpdateChartFitness(pr.Iteration % 5 == 0);

            //update current state of run panel
            if (ReportProgress != null && IsActive)
                ReportProgress(ActiveData.RunPanelData);

            System.Diagnostics.Debug.WriteLine(pr.Message);
        }

        /// <summary>
        /// Call methods on Presentation layer to be prepared for running
        /// </summary>
        private void prepareForGPRun()
        {
            //clean previous data if exist
            ActiveData.RunPanelData.gpCalculateOutput.Clear();
            ActiveData.RunPanelData.maxFitness.Clear();
            ActiveData.RunPanelData.avgFitness.Clear();
            ActiveData.RunPanelData.BestFitness = float.MinValue;
            ActiveData.RunPanelData.ChangedAtGeneration = 0;
            //clean previous test data
            ActiveData.TestPanelData.gpCalculateOutput.Clear();


        }

        /// <summary>
        /// Callback method for calculating model and sends to Run GUI panel
        /// </summary>
        /// <param name="pr"></param>
        private void runAndReport(ProgressReport pr, Parameters par)
        {
            double[] y = null;
            if (pr.BestSolution != null)
                y = Model.calculateOutput(true, false);

            if (y != null)
            {
                //clear previous calculated model
                ActiveData.RunPanelData.gpCalculateOutput.Clear();

                for (int i = 0; i < y.Length; i++)
                    ActiveData.RunPanelData.gpCalculateOutput.AddPoint(i + 1, y[i]);

                if (UpdateChartData != null && IsActive)
                  UpdateChartData(ActiveData.RunPanelData.gpCalculateOutput);

                //show current best solution to the result page
                if (ReportResults != null && IsActive)
                    ReportResults(pr.BestSolution, Model.Factory.Parameters);                
            }
        }

        /// <summary>
        /// Callback method for calculating test data and sends to Report GUI Panel
        /// </summary>
        /// <param name="pr"></param>
        private void testAndReport(ProgressReport pr, Parameters par)
        {
            double[] y = null;
            if (pr.BestSolution != null)
                y = Model.calculateOutput(false, false);
            //log training and testing data
            if (y != null)
            {
                //calculate output for test data set and show it on the Test page
                //clear previous calculated model
                ActiveData.TestPanelData.gpCalculateOutput.Clear();

                for (int i = 0; i < y.Length; i++)
                    ActiveData.TestPanelData.gpCalculateOutput.AddPoint(i + 1, y[i]);

                if (UpdateTestChartData != null && IsActive)
                    UpdateTestChartData(ActiveData.TestPanelData.gpCalculateOutput);

            }
        }
    }
}
