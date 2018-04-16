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
using GPdotNet.Interfaces;
using System.Windows.Forms;
using System;
using GPdotNet.Core;
using GPdotNet.Wnd.GUI.Dialogs;
using System.Collections.Generic;
using System.Linq;
namespace GPdotNet.Wnd.GUI
{
   /// <summary>
   /// Class implements several controls for showing best result GP found during GP program running
   /// </summary>
    public partial class ResultPanel : UserControl
    {
        #region Ctor and Fields
        public Func<Dictionary<string, List<object>>> EvaluateResults { get;  set; }
        public Parameters Parameters { get; set; }
        IChromosome m_bestSolution = null;
        Factory m_factory;
        public ResultPanel()
        {
            InitializeComponent();
           // initTreeDrawer();
          
        }

        private void initTreeDrawer()
        {
            this.Controls.Remove(this.treeCtrlDrawer1);
            this.treeCtrlDrawer1 = new TreeCtrlDrawer();
            // 
            // treeCtrlDrawer1
            // 
            this.treeCtrlDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCtrlDrawer1.AutoScroll = true;
            this.treeCtrlDrawer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.treeCtrlDrawer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeCtrlDrawer1.Location = new System.Drawing.Point(4, 32);
            this.treeCtrlDrawer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeCtrlDrawer1.Name = "treeCtrlDrawer1";
            this.treeCtrlDrawer1.Size = new System.Drawing.Size(595, 173);
            this.treeCtrlDrawer1.TabIndex = 29;
            this.Controls.Add(this.treeCtrlDrawer1);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
         
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PNG Image|*.png|All Files|*.*";
            dlg.Title = "Save GP Model As Image File";

            //dlg.Filter = string.Format("{1} ({0})|{0}|All files (*.*)|*.*", "PNG File Format", "*.png");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                treeCtrlDrawer1.SaveAsImage(dlg.FileName, Parameters);

            }
            
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            
        }
        
        /// <summary>
        /// Reports about current evolution. 
        /// </summary>
        /// <param name="currentEvolution"></param>
        /// <param name="averageFitness"></param>
        /// <param name="ch"></param>
        /// <param name="repType"></param>
        public void ReportResult(IChromosome ch, Parameters param)
        {
            
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.Invoke(
                    new Action(() =>
                    {
                        showTree(ch, param);
                        
                    }
                    ));
            }
            else
            {
                showTree(ch,param);

            }
            
        }

        private void showTree(IChromosome ch,Parameters param)
        {
            if (ch == null)
                return;
            var model = (GPdotNet.Core.Chromosome)ch;
            if (model == null)
                return;
            m_bestSolution = ch;
            Parameters = param;

            //
            solutionExpression.Text = NodeDecoding.Decode(model.expressionTree, param, BasicTypes.EncodeType.Default);

            if (treeCtrlDrawer1 == null)
                initTreeDrawer();

            treeCtrlDrawer1.DrawTreeExpression(model.expressionTree, Globals.FunctionFromId,param);
            
        }

        public void Reset()
        {
            m_factory = null;
            treeCtrlDrawer1.DrawTreeExpression(new Node(), Globals.FunctionFromId, null);
            treeCtrlDrawer1.Clear();
            solutionExpression.Text = "";
            m_bestSolution = null;
        }

      
        public string ResultPanelTostring()
        {
            if (m_bestSolution == null)
                return string.Empty;
            return m_bestSolution.ToString();

        }

        public void ResultPanelFromString( string str, Parameters param)
        {
            var ch = new Chromosome();
            if (!string.IsNullOrEmpty(str))
            {
                ch.FromString(str);
                m_bestSolution = ch;

                showTree(ch,param);
            }
             

        }
        #endregion

        private void btnmodelEvaluation_Click(object sender, System.EventArgs e)
        {
            var ret = EvaluateResults();

            if (EvaluateResults != null && ret != null)
            {
                
                //
                if (Parameters.OutputType == BasicTypes.ColumnType.Numeric)
                {
                    RModelEvaluation dlg = new RModelEvaluation();
                    dlg.Evaluate(ret["obs_train"].Select(x => (double)x).ToArray(), ret["prd_train"].Select(x => (double)x).ToArray(),
                            ret.ContainsKey("obs_test") ? ret["obs_test"].Select(x => (double)x).ToArray() : null,
                            ret.ContainsKey("prd_test") ? ret["prd_test"].Select(x => (double)x).ToArray() : null);

                    dlg.ShowDialog();

                }
                else if (Parameters.OutputType == BasicTypes.ColumnType.Binary || (ret.ContainsKey("Classes") && ret["Classes"].Count()==2))
                {
                    BModelEvaluation dlg = new BModelEvaluation();
                    var cl = ret["Classes"].Select(x => x.ToString()).ToArray();
                    dlg.loadClasses(cl);
                    dlg.loadData(ret["obs_train"].Select(x => (double)x).ToArray(), ret["prd_train"].Select(x => (double)x).ToArray(),
                        ret.ContainsKey("obs_test") ? ret["obs_test"].Select(x => (double)x).ToArray() : null,
                        ret.ContainsKey("prd_test") ? ret["prd_test"].Select(x => (double)x).ToArray() : null);

                    dlg.ShowDialog();

                }
                else
                {
                    MModelEvaluation dlg = new MModelEvaluation();
                    var cl = ret["Classes"].Select(x => x.ToString()).ToArray();
                    dlg.loadClasses(cl);
                    dlg.loadData(ret["obs_train"].Select(x => (double)x).ToArray(), ret["prd_train"].Select(x => (double)x).ToArray(),
                        ret.ContainsKey("obs_test") ? ret["obs_test"].Select(x => (double)x).ToArray() : null,
                        ret.ContainsKey("prd_test") ? ret["prd_test"].Select(x => (double)x).ToArray() : null);

                    dlg.ShowDialog();
                }
            }
            else
                MessageBox.Show("Evaluation process is not initialized.");
            

        }

       
    }
}
