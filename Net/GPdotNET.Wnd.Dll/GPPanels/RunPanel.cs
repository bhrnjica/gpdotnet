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
using GPdotNet.Core;
using GPdotNet.Interfaces;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ZedGraph;
using System.Linq;
using System.Collections.Generic;

namespace GPdotNet.Wnd.GUI
{
    /// <summary>
    /// Class implements simulation of Running GP
    /// </summary>
    public partial class RunPanel : UserControl//, IGPPanel
    {
        #region Ctor and Fields
        //zedChart lines
        //protected LineItem gpDataLine;
        //protected LineItem gpModelLine;
        //protected LineItem gpAvgFitnLine;
        //protected LineItem gpMaxFitnLine;


        protected DateTime m_startTime;

        protected float prevFitness = float.MinValue;
        protected DateTime prevIterationTime;

      
        

        public RunPanel()
        {
            InitializeComponent();
            PrepareGraphs();
        }

        public void Reset()
        {

            prevFitness = float.MinValue;
            //if (gpMaxFitnLine != null)
            //    gpMaxFitnLine.Clear();
            //if (gpAvgFitnLine != null)
            //    gpAvgFitnLine.Clear();
            //if (gpModelLine != null)
            //{
            //    gpModelLine.Clear();
            //    gpModelLine = null;
            //}
               

            m_cbIterationType.SelectedIndex = 0;
            eb_currentIteration.Text = "0";
            eb_currentFitness.Text = "";
            eb_bestSolutionFound.Text = 0.ToString();
            eb_timeStart.Text = "";
            eb_timePerRun.Text = "";
            eb_timeToCompleate.Text = "";
            eb_durationInMin.Text = "";
            eb_timeleft.Text = "";

        }
        #endregion

        #region Protected and Private methods

        /// <summary>
        /// Initi props for charts
        /// </summary>
        protected void PrepareGraphs()
        {
            // base.PrepareGraphs();

            ///Fitness simulation chart
            zedFitness.GraphPane.Title.Text = "GP Fitness Simulation";
            zedFitness.GraphPane.XAxis.Title.Text = "Evolution";
            zedFitness.GraphPane.YAxis.Title.Text = "Value";

            //gpMaxFitnLine = zedFitness.GraphPane.AddCurve("Maximum", null, null, Color.Red, ZedGraph.SymbolType.None);
            //gpMaxFitnLine.Symbol.Border = new Border(Color.Green, 0.1f);
            //this.zedFitness.GraphPane.AxisChange(this.CreateGraphics());

            //gpAvgFitnLine = zedFitness.GraphPane.AddCurve("Average", null, null, Color.Blue, ZedGraph.SymbolType.None);
            //gpAvgFitnLine.Symbol.Border = new Border(Color.Cyan, 0.1f);
            //this.zedFitness.GraphPane.AxisChange(this.CreateGraphics());

            zedModel.GraphPane.Title.Text = "GP Model Simulation";
            zedModel.GraphPane.XAxis.Title.Text = "Samples";
            zedModel.GraphPane.YAxis.Title.Text = "Output";
            zedModel.GraphPane.YAxis.MajorGrid.IsZeroLine = false;
            //gpDataLine = zedModel.GraphPane.AddCurve("Data Points", null, null, Color.Red, ZedGraph.SymbolType.Circle);
            //gpDataLine.Symbol.Border = new Border(Color.Green, 0.1f);
            //this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

            //gpModelLine = zedModel.GraphPane.AddCurve("GP Model", null, null, Color.Blue, ZedGraph.SymbolType.Plus);
            //gpModelLine.Symbol.Border = new Border(Color.Cyan, 0.1f);
            //this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
        }
        #endregion

        #region Public Methods

        public void ReportProgress(RunPanelData data)
        {
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.Invoke(
                    new Action(() =>
                    {
                        ReportProgressSync(data);
                        
                    }
                    ));
            }
            else
            {
                ReportProgressSync(data);
               
            }
        }

        public void ReportProgressSync(RunPanelData data)
        {
            eb_currentIteration.Text = data.CurrentIteration.ToString(CultureInfo.InvariantCulture);
            eb_currentFitness.Text = data.BestFitness.ToString(CultureInfo.InvariantCulture);
            eb_bestSolutionFound.Text = data.ChangedAtGeneration.ToString(CultureInfo.InvariantCulture);
        }

        public void UpdateChartFitnessAsync(bool refreshChart)
        {
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.Invoke(
                    new Action(() =>
                    {
                        updateChartFitness(refreshChart);

                    }
                    ));
            }
            else
            {
                updateChartFitness(refreshChart);

            }
        }
        private void updateChartFitness(bool refreshChart)
        {
            if (refreshChart)
                zedFitness.RestoreScale(zedFitness.GraphPane);       
        }

       
        /// <summary>
        /// Uodate GP Model chart when data or GP model is changed
        /// </summary>
        /// <param name="y">output value</param>
        /// <param name="gpModel"> indicator is it about GPMOdel or Data Point</param>
        public void UpdateChartDataAsync(LineItem points)
        {
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.Invoke(
                    new Action(() =>
                    {
                        updateChartData(points);
                    }
                    ));
            }
            else
            {
                updateChartData(points);

            }
        }
        public void updateChartData(LineItem points)
        {
            //firs chech if curvelist already exist
            var list = this.zedModel.GraphPane.CurveList.Find((x) => x.Label.Text == points.Label.Text);

            if(list==null)
            {
                this.zedModel.GraphPane.CurveList.Add(points);
                this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
            }

            zedModel.RestoreScale(zedModel.GraphPane);
        }
        double max = 0;
        /// <summary>
        /// Deserilization of run condition
        /// </summary>
        /// <param name="p"></param>
        public void ActivatePanel(RunPanelData data)
        {
            m_cbIterationType.SelectedIndex = data.TerminationType;
            m_eb_iterations.Text = data.TerminationValue.ToString();
            eb_currentIteration.Text = data.CurrentIteration.ToString();
            eb_currentFitness.Text = data.BestFitness.ToString();
            eb_bestSolutionFound.Text = data.ChangedAtGeneration.ToString();

            //clear previous data if exist
            this.zedFitness.GraphPane.CurveList.Clear();
            this.zedModel.GraphPane.CurveList.Clear();

            this.zedFitness.GraphPane.CurveList.Add(data.maxFitness);
            this.zedFitness.GraphPane.CurveList.Add(data.avgFitness);
            this.zedFitness.GraphPane.AxisChange(this.CreateGraphics());

            this.zedModel.GraphPane.CurveList.Add(data.gpCalculateOutput);
            this.zedModel.GraphPane.CurveList.Add(data.experimentalData);         
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

            //
            this.zedFitness.RestoreScale(zedFitness.GraphPane);
            
            

            if(data.OutputType != BasicTypes.ColumnType.Numeric)
            {
                for (int i = 0; i < data.experimentalData.Points.Count; i++)
                {
                    if (data.experimentalData.Points[i].Y > max)
                        max = data.experimentalData.Points[i].Y;
                }
               
                Classes = data.Classes;
                zedModel.GraphPane.YAxis.ScaleFormatEvent += YAxis_ScaleFormatEvent;

                //zedModel.GraphPane.XAxis.Scale.Min = 0;
                zedModel.GraphPane.YAxis.Scale.Min = -0.1;
                zedModel.GraphPane.YAxis.Scale.Max = max + 0.2;
                zedModel.GraphPane.YAxis.MajorGrid.DashOff = 0;
                zedModel.GraphPane.YAxis.MajorGrid.IsVisible = true;
                zedModel.GraphPane.XAxis.MajorGrid.DashOff = 0;
                zedModel.GraphPane.XAxis.MajorGrid.IsVisible = false;
                zedModel.GraphPane.YAxis.Scale.MajorStep = 1;
                zedModel.GraphPane.YAxis.Scale.MinorStep = 1;
                zedModel.GraphPane.XAxis.Scale.MajorStep = 1;
                zedModel.GraphPane.XAxis.Scale.MinorStep = 1;
            }
            else
            {
                zedModel.GraphPane.YAxis.Scale.MaxAuto = true;
                zedModel.GraphPane.YAxis.Scale.MinAuto = true;
                
                zedModel.GraphPane.YAxis.MajorGrid.IsVisible = false;
                zedModel.GraphPane.XAxis.MajorGrid.IsVisible = false;

                zedModel.GraphPane.YAxis.Scale.MajorStepAuto = true;
                zedModel.GraphPane.YAxis.Scale.MinorStepAuto = true;

                zedModel.GraphPane.XAxis.Scale.MajorStep = 1;
                zedModel.GraphPane.XAxis.Scale.MinorStep = 0;
            }
            zedModel.GraphPane.YAxis.Title.Text = data.Label;
            this.zedModel.RestoreScale(zedModel.GraphPane);

        }
        List<string> Classes;
        private string YAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            //zedModel.GraphPane.XAxis.Scale.Min = 0;
            zedModel.GraphPane.YAxis.Scale.Min = -0.1;
            zedModel.GraphPane.YAxis.Scale.Max = max + 0.2;
            zedModel.GraphPane.YAxis.MajorGrid.DashOff = 0;
            zedModel.GraphPane.YAxis.MajorGrid.IsVisible = true;
            zedModel.GraphPane.XAxis.MajorGrid.DashOff = 0;
            zedModel.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zedModel.GraphPane.YAxis.Scale.MajorStep = 1;
            zedModel.GraphPane.YAxis.Scale.MinorStep = 0;

            if(Classes==null || val>= Classes.Count)
                return String.Format("{0}", (int)val);
            else
                return String.Format("{0}", Classes[(int)val]);
        }


        public (int, float) GetTerminationCriteria()
        {
            if (!float.TryParse(m_eb_iterations.Text, out float val))
                val = 0;
            return (m_cbIterationType.SelectedIndex, val);
        }

        public void ResetChart()
        {
            max = 0;
            Classes = null;
            zedModel.GraphPane.YAxis.ScaleFormatEvent -= YAxis_ScaleFormatEvent;
        }
        #endregion



    }

}
