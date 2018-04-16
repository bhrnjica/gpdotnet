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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using GPdotNet.Data;
using GPdotNet.Interfaces;
using GPdotNet.BasicTypes;

namespace GPdotNet.Wnd.GUI
{
   /// <summary>
   /// Class implements calculation of best solution against Test data
   /// </summary>
    public partial class TestPanel : UserControl
    {
        #region CTor and Fields
        //private List<LineItem> gpDataLine;
        //private List<LineItem> gpModelLine;
       
        /// <summary>
        /// CTOR
        /// </summary>
        public TestPanel()
        {
            InitializeComponent();
            //gpDataLine = new List<LineItem>();
            //gpModelLine = new List<LineItem>();
            zedModel.GraphPane.Title.Text = "Prediction";
            zedModel.GraphPane.XAxis.Title.Text = "Samples";
            zedModel.GraphPane.YAxis.Title.Text = "Output";
            zedModel.GraphPane.YAxis.MajorGrid.IsZeroLine = false;
            //var dl = zedModel.GraphPane.AddCurve("Projects 1", null, null, Color.Red, ZedGraph.SymbolType.Plus);
            //dl.Symbol.Border = new Border(Color.Green, 0.1f);
            //gpDataLine.Add(dl);
            //this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

            //var ml = zedModel.GraphPane.AddCurve("Prediction 1", null, null, Color.Blue, ZedGraph.SymbolType.Plus);
            //ml.Symbol.Border = new Border(Color.Cyan, 0.1f);
            //gpModelLine.Add(ml);
            //this.zedModel.GraphPane.AxisChange(this.CreateGraphics());


        }
        double max = 0;
        /// <summary>
        /// Deserilization of run condition
        /// </summary>
        /// <param name="p"></param>
        public void ActivatePanel(TestPanelData data)
        {
            //clear previous data if exist
            this.zedModel.GraphPane.CurveList.Clear();
            //
            this.zedModel.GraphPane.CurveList.Add(data.gpCalculateOutput);
            this.zedModel.GraphPane.CurveList.Add(data.experimentalData);
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

           

            if (data.OutputType != BasicTypes.ColumnType.Numeric)
            {
                for (int i = 0; i < data.experimentalData.Points.Count; i++)
                {
                    if (data.experimentalData.Points[i].Y > max)
                        max = data.experimentalData.Points[i].Y;
                }
                Classes = data.Classes;
                zedModel.GraphPane.YAxis.ScaleFormatEvent += YAxis_ScaleFormatEvent;
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
            zedModel.GraphPane.YAxis.Scale.MajorStep = 1;
            zedModel.GraphPane.YAxis.Scale.MinorStep = 1;
            zedModel.GraphPane.XAxis.Scale.MajorStep = 1;
            zedModel.GraphPane.XAxis.Scale.MinorStepAuto = false;

            if (Classes == null || val >= Classes.Count|| val < 0)
                return String.Format("{0}", (int)val);
            else
                return String.Format("{0}", Classes[(int)val]);
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
        private void updateChartData(LineItem points)
        {
            //firs chech if curvelist already exist
            var list = this.zedModel.GraphPane.CurveList.Find((x) => x.Label.Text == points.Label.Text);

            if (list == null)
            {
                this.zedModel.GraphPane.CurveList.Add(points);
                this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
            }

            zedModel.RestoreScale(zedModel.GraphPane);
        }

        public void UpdateOutputColumnAsync(string [] output)
        {
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.Invoke(
                    new Action(() =>
                    {
                        updateOutputColumn(output);
                    }
                    ));
            }
            else
            {
                updateOutputColumn(output);

            }
        }

        private void updateOutputColumn(string[] output)
        {
           
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// SHow 2D data in grid
        /// </summary>
        /// <param name="data"></param>
        private void FillGridWithData(Experiment exp)
        {

            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;

            int numCol = exp.GetColumnInputCount() + exp.GetColumnOutputCount();
            int numRow = exp.GetRowCount(true);

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Pos";
            colHeader.Width = 100;
            listView1.Columns.Add(colHeader);

            //add experiment column
            var colss = exp.GetColumns(true);
            for (int i = 0; i < numCol; i++)
            {
                colHeader = new ColumnHeader();
                colHeader.Text = colss[i].Name;
                
                //if (i + 1 == numCol)
                //    colHeader.Text = exp.get;
                //else if (i + 2 == numCol)
                //    colHeader.Text = "Ymodel";
                //else if (i + 3 == numCol)
                //    colHeader.Text = "Y";
                //else
                //    colHeader.Text = "X" + (i + 1).ToString();

                //colHeader.Width = 100;
                listView1.Columns.Add(colHeader);
            }

            //add predicted output columns
            var outCols = exp.GetColumnsFromOutput(true);
            for (int i = 0; i < exp.GetColumnOutputCount(); i++)
            {
                colHeader = new ColumnHeader();
                colHeader.Text = outCols[i].Name+"_pred";
                colHeader.Width = 100;
                //if (i + 1 == numCol)
                //    colHeader.Text = exp.get;
                //else if (i + 2 == numCol)
                //    colHeader.Text = "Ymodel";
                //else if (i + 3 == numCol)
                //    colHeader.Text = "Y";
                //else
                //    colHeader.Text = "X" + (i + 1).ToString();

                //colHeader.Width = 100;
                listView1.Columns.Add(colHeader);
            }

            for (int j = 0; j < numRow; j++)
            {
                ListViewItem LVI = listView1.Items.Add((j + 1).ToString());
                LVI.UseItemStyleForSubItems = false;
                int i = 0;
                //add experimental value cells
                for (; i < numCol; i++)
                {
                    System.Windows.Forms.ListViewItem.ListViewSubItem itm = new ListViewItem.ListViewSubItem();
                    itm.Text=colss[i].GetData(j);
                   
                    if (colss[i].IsOutput)
                        itm.ForeColor = Color.Red;
                    LVI.SubItems.Add(itm);                 
                }
                //add predicted cells
                for (; i < numCol+outCols.Count; i++)
                {
                    System.Windows.Forms.ListViewItem.ListViewSubItem itm = new ListViewItem.ListViewSubItem();
                    itm.Text = "-";
                    itm.ForeColor = Color.Blue;
                    LVI.SubItems.Add(itm);
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update chartz and grid with prediction data
        /// </summary>
        /// <param name="data"></param>
        public void FillPredictionData(Experiment expData)
        {
            double[][] y= expData.GetColumnOutputValues(true);
            UpdateChartDataPoint(y, false);

            double[][] data = expData.GetColumnAllValues(true);

            FillGridWithData(expData);
        }

         

         
        
        /// <summary>
        /// Update grid with new data
        /// </summary>
        /// <param name="y"></param>
        public void FillGPPredictionResult(double[] y)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var row = listView1.Items[i];

                if (y == null)
                    return;
                //
                double Ygp=y[i];
                row.SubItems[row.SubItems.Count - 2].Text = Math.Round(Ygp, 5).ToString();
                float Ydata = 0;
                if (float.TryParse(row.SubItems[row.SubItems.Count - 3].Text, out Ydata))
                {

                    row.SubItems[row.SubItems.Count - 1].Text = Math.Round(Ydata - Ygp,5).ToString();
                }  
                else
                    row.SubItems[row.SubItems.Count - 1].Text = "-";
            }
        }

        /// <summary>
        /// Proces of updating chart with new data
        /// </summary>
        /// <param name="y"></param>
        /// <param name="isGPData"></param>
        public void UpdateChartDataPoint(double[][] y, bool isGPData)
        {
            if (this.zedModel.GraphPane == null)
                return;

            //Add aditional lines
            //if(!isGPData)
            //{
            //    InitChart(y);
            //    if(gpModelLine!=null && gpModelLine.Count>0)
            //    {
            //        foreach (var l in gpModelLine)
            //            l.Clear();
            //    }
            //}
               
            //for (int j = 0; y!=null && j < y.Length; j++)
            //{
            //    LineItem li = null;
            //    if (isGPData)
            //        li = gpModelLine[j];
            //    else
            //        li = gpDataLine[j];

            //    li.Clear();
            //    for (int i = 0; i < y[j].Length; i++)
            //        li.AddPoint(i + 1, y[j][i]);
            //}
            //this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
            //this.zedModel.Refresh();
        }

        public void ResetChart()
        {
            max = 0;
            Classes = null;
            zedModel.GraphPane.YAxis.ScaleFormatEvent -= YAxis_ScaleFormatEvent;
        }

        private void InitChart(double[][] y)
        {
            //if(y!=null && y.Length>1)
            //{
            //    for(int i=1; i<y.Length; i++)
            //    {
            //        var dl = zedModel.GraphPane.AddCurve("Projects "+(i+1).ToString(), null, null, Color.Red, ZedGraph.SymbolType.Plus);
            //        dl.Symbol.Border = new Border(Color.Green, 0.1f);
            //        gpDataLine.Add(dl);
            //        this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

            //        var ml = zedModel.GraphPane.AddCurve("Prediction " + (i + 1).ToString(), null, null, Color.Blue, ZedGraph.SymbolType.Plus);
            //        ml.Symbol.Border = new Border(Color.Cyan, 0.1f);
            //        gpModelLine.Add(ml);
            //        this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
            //    }
                
            //}
           
        }
        ///// <summary>
        ///// Update grid with new data
        ///// </summary>
        ///// <param name="y"></param>
        //public void FillGPPredictionResult(double[][] y)
        //{
        //    // FillGPPredictionResult(y[0]);
        //    if(Projects==null && y!=null && y.Length>0)
        //    {
        //        FillGPPredictionResult(y[0]);
        //        return;
        //    }

        //    var colOut=Projects.GetColumnOutputCount();
        //     var cols= Projects.GetColumnsFromOutput();
        //     for (int i = 0; i < listView1.Items.Count; i++)
        //     {
        //         var row = listView1.Items[i];
        //         for (int j = 0; j < colOut; j++)
        //         {
        //             var c = cols[j];
        //             double Ymodel = y[j][i];
        //             if (c.ColumnDataType== ColumnDataType.Categorical)
        //             {
        //               var str=  c.GetCategoryFromNumeric(Ymodel, null);
        //               row.SubItems[row.SubItems.Count - colOut + j].Text = str;
        //             }
        //             else if(c.ColumnDataType== ColumnDataType.Binary)
        //             {
        //                 var str = c.GetBinaryClassFromNumeric(Ymodel, null);
        //                 row.SubItems[row.SubItems.Count - colOut + j].Text = str;
        //             }
        //             else
        //                row.SubItems[row.SubItems.Count - colOut+j].Text = Math.Round(Ymodel, 5).ToString();
        //         }
        //     }
        //}


         
        public void ReportProgress(TestPanelData data)
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

        public void ReportProgressSync(TestPanelData data)
        {
            //eb_currentIteration.Text = data.CurrentIteration.ToString(CultureInfo.InvariantCulture);
            //eb_currentFitness.Text = data.BestFitness.ToString(CultureInfo.InvariantCulture);
            //eb_bestSolutionFound.Text = data.ChangedAtGeneration.ToString(CultureInfo.InvariantCulture);
        }

        #endregion


    }
}
