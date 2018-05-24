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
using System.Globalization;
using System.Windows.Forms;
using GPdotNet.Core;
using GPdotNet.BasicTypes;
using System.Collections.Generic;

namespace GPdotNet.Wnd.GUI
{
    /// <summary>
    /// Class implements parameters for GP
    /// </summary>
    public partial class ParametersPanel : UserControl
    {
        #region CTor and Fields
        public ParametersPanel()
        {
            InitializeComponent();

            loadInitMethodsinCombo();
            loadSelectionMethodsInCombo();
            //Set initial value
            Reset();

            InitToolTipDescription();
        }

        private void InitToolTipDescription()
        {
            //population size
            new ToolTip().SetToolTip(lblPSize, "The number of chromosomes in the population.");
            //
            new ToolTip().SetToolTip(lblFitness, "The Fitness method used in the GP.");
            new ToolTip().SetToolTip(lblInitialization, "The method for initialize the population.");
            new ToolTip().SetToolTip(lblElitism, "The number of the best chromosomes copied\n in the new population directly.");
            new ToolTip().SetToolTip(lblSelectionMethod, "The selection method.");
            new ToolTip().SetToolTip(lblIMaximum, "Maximum tree level during the generation of the initial population.");
            new ToolTip().SetToolTip(lblOMaximum, "Maximum tree level during the genetic operations.");
            new ToolTip().SetToolTip(lblBroodSize, "Number of repetition of each selected chromosome(s) for genetic operations.");
            new ToolTip().SetToolTip(lblInitialTries, "Maximum number of repetition during generation\n of initial population to remove as much as possible\n duplicate chromosomes.");
            new ToolTip().SetToolTip(lblRootNode, "Specific function for root node for all chromosomes.");
            new ToolTip().SetToolTip(lblTreshold, "Threshold value for specific root node function.");
            new ToolTip().SetToolTip(lblPCrossover, "Probability value for  crossover operation.");
            new ToolTip().SetToolTip(lblPMutation, "Probability value for  mutation operation.");
            new ToolTip().SetToolTip(lblPReproduction, "Probability value for  reproduction operation.");
            new ToolTip().SetToolTip(lblConstFrom, "Lower value for random constant generation.");
            new ToolTip().SetToolTip(lblConstTo, "Upper value for random constant generation.");
            new ToolTip().SetToolTip(lblConsCount, "Number of randomly generated constant.");
        }

        public void InitializeControls(ColumnType problemType, CategoryEncoding encoding)
        {

            loadFitnessFunsInCombo(problemType, encoding);

            loadRootNodeFunction(problemType, encoding);

        }
       
        #endregion


        #region Properties

        double[] Constances { get; set; }
        public Action ResetModel { get; set; }
        #endregion

        #region private Methods

        private void cb_rootNodeFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_rootNodeFunction.SelectedItem!=null && (cb_rootNodeFunction.SelectedItem.ToString().StartsWith("Step") || cb_rootNodeFunction.SelectedItem.ToString().StartsWith("Sigmoid")))
            {
                lblTreshold.Visible = true;
                eb_cutOff.Visible = true;
                if (cb_rootNodeFunction.SelectedItem.ToString().StartsWith("Step"))
                   eb_cutOff.Text = "0";
                else if (cb_rootNodeFunction.SelectedItem.ToString().StartsWith("Sigmoid"))
                   eb_cutOff.Text =  0.5.ToString();
            }
            else
            {
                lblTreshold.Visible = false;
                eb_cutOff.Visible = false;
            }
        }
        /// <summary>
        ///  //we need here to provide loading all class which are derived from IFitness interface
        /// on that way we hav complete customization of th fitness functions
        /// </summary>
        private void loadFitnessFunsInCombo(ColumnType problemType, CategoryEncoding encoding)
        {
            cmbFitnessFuncs.Items.Clear();
            //
            if (problemType== ColumnType.Numeric)
            {
                cmbFitnessFuncs.Items.Add("AE	-Absolute error (regression) ");
                cmbFitnessFuncs.Items.Add("MAE	-Mean absolute error (regression) ");

                cmbFitnessFuncs.Items.Add("RMSE	-Root mean square error (regression) ");
                cmbFitnessFuncs.Items.Add("RSE	-Root square error  (regression) ");
                cmbFitnessFuncs.Items.Add("SE	-Square error (regression) ");
                //cmbFitnessFuncs.Items.Add("MSE	-Mean square error (regression) ");
                //cmbFitnessFuncs.Items.Add("RRSE	-Relative root square error (regression) ");
                //cmbFitnessFuncs.Items.Add("RAE	-Root absolute error (regression) ");
            }
            else
            {
                if(encoding== CategoryEncoding.OnevsAll || encoding == CategoryEncoding.OnevsAll_1)
                {
                    cmbFitnessFuncs.Items.Add("SRMS	- Softmax root mean square error (classification) ");
                    cmbFitnessFuncs.Items.Add("LSRF	- Logarithmic scoring rule (classification) ");
                    cmbFitnessFuncs.Items.Add("MAHD	- Mahanalobis Distance (classification) ");
                }
                else
                {
                    cmbFitnessFuncs.Items.Add("ACC -Total accuracy (classification) ");
                    cmbFitnessFuncs.Items.Add("HSS -Heidke skill score (classification) ");
                    cmbFitnessFuncs.Items.Add("PSS -Peirce skill score (classification) ");
                }
               
            }


        }

        

        private void loadRootNodeFunction(ColumnType problemType, CategoryEncoding encoding)
        {
            //
            cb_rootNodeFunction.Items.Clear();

            if (problemType == ColumnType.Numeric)
            {
                cb_rootNodeFunction.Items.Add("None ");
            }
            else if (problemType == ColumnType.Binary)
            {
                cb_rootNodeFunction.Items.Add("Sigmoid(two class ) ");
                cb_rootNodeFunction.Items.Add("Step(two class) ");
                cb_rootNodeFunction.Items.Add("Scaled Sigmoid[0, numClasses] (multi class) ");
                cb_rootNodeFunction.Items.Add("Softmax function(multi class) ");
            }
            else
            {
                if (encoding == CategoryEncoding.OnevsAll || encoding == CategoryEncoding.OnevsAll_1)
                {
                    cb_rootNodeFunction.Items.Add("Softmax function(multi class) ");
                }
                else
                {
                    cb_rootNodeFunction.Items.Add("Scaled Sigmoid[0, numClasses] (multi class) ");
                    cb_rootNodeFunction.Items.Add("Softmax function(multi class) ");
                }
                
            }


        }

        private void selectRootNode(string rootNodeName)
        {
            cb_rootNodeFunction.SelectedItem = null;
            for (int i = 0; i < cb_rootNodeFunction.Items.Count; i++)
            {
                var itm = cb_rootNodeFunction.Items[i].ToString();
                if (itm.StartsWith(rootNodeName) || rootNodeName == i.ToString())
                {
                    cb_rootNodeFunction.SelectedItem = cb_rootNodeFunction.Items[i];
                    return;
                }
            }
        }

        private void selectFitness(string ftnName)
        {
            cmbFitnessFuncs.SelectedIndex = -1;
            for(int i =0;i<cmbFitnessFuncs.Items.Count; i++)
            {
                var itm = cmbFitnessFuncs.Items[i].ToString();
                if (itm.StartsWith(ftnName) || ftnName == i.ToString())
                {
                    cmbFitnessFuncs.SelectedItem = cmbFitnessFuncs.Items[i];
                    return;
                }

            }
        }

        /// <summary>
        /// Enumerate ENUM of selection methods and insert as ComboBox items
        /// so the user can easily select.
        /// this is also handy when you extend selection methods and populate them automatically
        /// </summary>
        private void loadInitMethodsinCombo()
        {
            //fill combo with initialization methods
            foreach (var initM in Enum.GetValues(typeof(InitializationMethod)))
            {
                cmbInitMethods.Items.Add(initM.ToString());
            }
        }

        /// <summary>
        /// Enumerate ENUM of selection methods and insert as ComboBox items
        /// so the user can easily select.
        /// this is also handy when you extend selection methods and populate them automatically
        /// </summary>
        private void loadSelectionMethodsInCombo()
        {
            //fill combo with initialization methods
            foreach (var initM in Enum.GetValues(typeof(SelectionMethod)))
               cmbSelectionMethods.Items.Add(initM.ToString());
        }


        /// <summary>
        /// When the selection method is changed, default prams must be set properly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectionMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSelectionMethods.SelectedItem==null)
                return;


            switch (cmbSelectionMethods.SelectedIndex)
            {
                //Fitness proportionate selection
                case 0:
                    lbSelParam1.Visible = false;
                    txtSelParam1.Visible = false;
                    label3.Visible = false;
                    break;
                //Rank Selection
                case 1:
                    lbSelParam1.Visible = true;
                    lbSelParam1.Text = "Rank value:";
                    txtSelParam1.Text = (1.6f).ToString();
                    txtSelParam1.Visible = true;
                    label3.Text = "(0-2)";
                    label3.Visible = true;
                    break;
                //Tournament Selection
                case 2:
                    lbSelParam1.Visible = true;
                    lbSelParam1.Text = "Tour Size:";
                    txtSelParam1.Text = 2.ToString();
                    txtSelParam1.Visible = true;
                    label3.Text = "(0-10)";
                    label3.Visible = true;
                    break;
                //Skrgic Selection
                case 3:
                    lbSelParam1.Visible = true;
                    lbSelParam1.Text = "Nonlinear Coef:";
                    txtSelParam1.Text = (1.0 / 5.0).ToString();
                    txtSelParam1.Visible = true;
                    label3.Text = "(-1 - 10)";
                    label3.Visible = true;
                    break;
                //Default Selection
                default:
                    break;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Reset GP parameters value
        /// </summary>
        public void Reset()
        {
            txtPopSize.Text = "500";
            //Set initial value
            cmbInitMethods.SelectedIndex = 2;
            cmbSelectionMethods.SelectedIndex = 1;
            cmbFitnessFuncs.SelectedIndex = -1;

            txtProbMutation.Text = (5.0 / 100.0).ToString();
             
            txtProbReproduction.Text = (20.0 / 100.0).ToString();
            txtProbCrossover.Text = (90.0 / 100.0).ToString();
             
            txtElitism.Text = 1.ToString();
            cb_rootNodeFunction.SelectedIndex =-1;
            //
            eb_cutOff.Text = 0.5.ToString();
            txtBroodSize.Text = "2";
        }

        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            groupBox1.Enabled = p;
            groupBox2.Enabled = p;
            groupBox3.Enabled = p;
            groupBox4.Enabled = p;
            groupBox6.Enabled = p;
            groupBox8.Enabled = p;

        }


        

       
        /// <summary>
        /// Return current value of GP params
        /// </summary>
        /// <returns></returns>
        public string GetParameters()
        {

            var str = ParametersToString();
            return str;
            
        }

        /// <summary>
        /// De serialization of parameters
        /// </summary>
        /// <param name="p"></param>
        public void ParametersFromString(string p)
        {
            var pstr = p.Split(new char[] { ';' });
            
            try
            {

                //PopSize
                txtPopSize.Text=pstr[0];
                //Fitness
                int temp=0;
                if (!int.TryParse(pstr[1], out temp))
                    temp = 0;

                selectFitness(pstr[1]);
                //cmbFitnessFuncs.SelectedIndex = temp;

                ///Init method
                temp = 0;
                if (!int.TryParse(pstr[2], out temp))
                    temp = 0;
                if(temp >= cmbInitMethods.Items.Count)
                    cmbInitMethods.SelectedIndex = 0;
                else
                    cmbInitMethods.SelectedIndex = temp;

                //init depth
                txtInitTreeDepth.Text = pstr[3];

                //operation depth
                txtOperationTreeDepth.Text = pstr[4];


                //Selection Elitism
                txtElitism.Text = pstr[5];

                //protected operation
                if (!int.TryParse(pstr[6], out temp))
                    temp = 0;
                isProtectedOperation.Checked = temp == 0 ? false : true;


                //Selection method
                temp = 0;
                if (!int.TryParse(pstr[7], out temp))
                    temp = 0;
                cmbSelectionMethods.SelectedIndex = temp;

                //Selection param1
                txtSelParam1.Text = (double.Parse(pstr[8], CultureInfo.InvariantCulture)).ToString(); 

                //Random constant from
                txtRandomConsFrom.Text = (double.Parse(pstr[9], CultureInfo.InvariantCulture)).ToString();

                //Random constant to
                txtRandomConsTo.Text = (double.Parse(pstr[10], CultureInfo.InvariantCulture)).ToString();

                //Random constant count
                txtRandomConstNum.Text = pstr[11];

                //Crossover method
                txtProbCrossover.Text = (double.Parse(pstr[12], CultureInfo.InvariantCulture)).ToString(); 

                //mutation method
                txtProbMutation.Text = (double.Parse(pstr[13], CultureInfo.InvariantCulture)).ToString();

                //reproduction method
                txtProbReproduction.Text = (double.Parse(pstr[14], CultureInfo.InvariantCulture)).ToString();

                //Brood size
                txtBroodSize.Text = pstr[15];
               
                //
                selectRootNode(pstr[16]);
                //

                //parsing cutOff value
                double v = 0;
                if (double.TryParse(pstr[17], NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                    eb_cutOff.Text = v.ToString();
                else
                    eb_cutOff.Text = "0";

                int numCount = int.Parse(pstr[11]);

                if (pstr.Length <= 18 + numCount)
                    return;

                //cons
                Constances = new double[numCount];
                for(int i=0; i<numCount; i++)
                {
                    double cv = 0;
                    if (!double.TryParse(pstr[18 + i], NumberStyles.Any, CultureInfo.InvariantCulture, out cv))
                        cv = 0;

                    Constances[i] = cv;
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// Serialization of parameters
        /// </summary>
        /// <returns></returns>
        public string ParametersToString()
        {
            string retVal = "";
            try
            {

                //PopSize
                int intValue = 0;
                if (!int.TryParse(txtPopSize.Text, out intValue))
                {
                    throw new Exception("Invalid value for Population size!");
                }
                retVal += intValue.ToString() + ";";
                //Fitness
                retVal += cmbFitnessFuncs.SelectedItem==null? cmbFitnessFuncs.Items[0].ToString().Substring(0, 4) + ";" : cmbFitnessFuncs.SelectedItem.ToString().Substring(0, 4) + ";";

                ///Init method
                if (cmbInitMethods.SelectedIndex == -1)
                {
                    throw new Exception("Invalid value for Initialize method!");
                }
                retVal += cmbInitMethods.SelectedIndex.ToString() + ";";

                //init depth
                if (!int.TryParse(txtInitTreeDepth.Text, out intValue))
                {
                    throw new Exception("Invalid value for init  dept tree!");
                }
                retVal += intValue.ToString() + ";";

                //operation depth
                if (!int.TryParse(txtOperationTreeDepth.Text, out intValue))
                {
                    throw new Exception("Invalid value for operation dept tree!");
                }
                retVal += intValue.ToString() + ";";


                //Selection Elitism
                if (!int.TryParse(txtElitism.Text, out intValue))
                {
                    throw new Exception("Invalid value for operation dept tree!");
                }
                retVal += intValue.ToString() + ";";

                //Protected operation
                retVal += isProtectedOperation.Checked ? "1" : "0";
                retVal += ";";


                //Selection method
                if (cmbSelectionMethods.SelectedIndex == -1)
                {
                    throw new Exception("Invalid value for Initialize method!");
                }
                retVal += cmbSelectionMethods.SelectedIndex.ToString() + ";";

                //Selection param1
                float floatValue;
                if (!float.TryParse(txtSelParam1.Text, out floatValue))
                {
                    throw new Exception("Invalid value for Sel param 1!");
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";


                //Random constant from
                if (!float.TryParse(txtRandomConsFrom.Text, out floatValue))
                {
                    throw new Exception("Invalid value for Random constant From!");
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Random constant to
                if (!float.TryParse(txtRandomConsTo.Text, out floatValue))
                {
                    throw new Exception("Invalid value for Random constant To!");
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Random constant count
                
                if (!int.TryParse(txtRandomConstNum.Text, out intValue))
                {
                    throw new Exception("Invalid value for Random constant NumCount!");
                }
                int numConst = intValue;
                retVal += intValue.ToString() + ";";

                //Crossover method
                if (!float.TryParse(txtProbCrossover.Text, out floatValue))
                {
                    throw new Exception("Invalid value for probability of Crossover.!");
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

                //mutation method
                if (!float.TryParse(txtProbMutation.Text, out floatValue))
                {
                    throw new Exception("Invalid value for probability of Mutation.!");
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";


                //reproduction method
                if (!float.TryParse(txtProbReproduction.Text, out floatValue))
                {
                    throw new Exception("Invalid value for probability of Reproduction.!");
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";


                //brood size
                if (!int.TryParse(txtBroodSize.Text, out intValue))
                {
                    throw new Exception("Invalid Brood SIze value!");
                }
                retVal += intValue.ToString(CultureInfo.InvariantCulture) + ";";

                if (!int.TryParse(txtRandomConstNum.Text, out int constNum))
                {
                    throw new Exception("Invalid value for operation dept tree!");
                }

                retVal += cb_rootNodeFunction.SelectedItem==null? cb_rootNodeFunction.Items[0]+";" : cb_rootNodeFunction.SelectedItem.ToString().Substring(0, 4) + ";";

                //parsing 
                //parsing cutOff value
                float v = 0;
                if (!float.TryParse(eb_cutOff.Text, out v))
                      eb_cutOff.Text = "0";

                retVal += v.ToString(CultureInfo.InvariantCulture) + ";";


                //cons
                if(Constances!=null && Constances.Length== numConst)
                {
                    for (int i = 0; i < numConst; i++)
                    {
                        retVal += Constances[i].ToString(CultureInfo.InvariantCulture) + ";";
                    }
                }
                return retVal;
            }
            catch (Exception ex)
            {
                //ex;
                throw;

            }
        }

        public Dictionary<string, string> ParametersToDictionary()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("InitializationMethod", $"{cmbInitMethods.SelectedIndex}");
            dic.Add("SelectionMethod", $"{cmbSelectionMethods.SelectedIndex}");
            dic.Add("PopulationSize", $"{int.Parse(txtPopSize.Text)}");
            dic.Add("MaxLevel", $"{int.Parse(txtOperationTreeDepth.Text)}");
            dic.Add("MaxInitLevel", $"{int.Parse(txtInitTreeDepth.Text)}");
            dic.Add("BroodSize", $"{int.Parse(txtBroodSize.Text)}");
            dic.Add("CrossoverProbability", $"{float.Parse(txtProbCrossover.Text).ToString(CultureInfo.InvariantCulture)}");
            dic.Add("MutationProbability", $"{float.Parse(txtProbMutation.Text).ToString(CultureInfo.InvariantCulture)}");
            dic.Add("SelectionProbability", $"{float.Parse(txtProbReproduction.Text).ToString(CultureInfo.InvariantCulture)}");
            dic.Add("OutputType", $"{getOutputType()}");
            dic.Add("RootName", $"{cb_rootNodeFunction.SelectedItem.ToString().Substring(0,4)}");
            dic.Add("Threshold", $"{float.Parse(eb_cutOff.Text).ToString(CultureInfo.InvariantCulture)}");
            dic.Add("ParallelProcessing", $"{radioIsParallel.Checked}");
            dic.Add("Elitism", $"{txtElitism.Text}");
            //dic.Add("TourSize", $"{txtSelParam1.Text}");
            dic.Add("ArgValue", $"{float.Parse(txtSelParam1.Text).ToString(CultureInfo.InvariantCulture)}");// $"{txtSelParam1.Text}");
            dic.Add("ConstFrom", $"{float.Parse(txtRandomConsFrom.Text).ToString(CultureInfo.InvariantCulture)}");
            dic.Add("ConstTo", $"{float.Parse(txtRandomConsTo.Text).ToString(CultureInfo.InvariantCulture)}");
            dic.Add("ConstNum", $"{int.Parse(txtRandomConstNum.Text)}");
            dic.Add("IsProtectedOperation", $"{isProtectedOperation.Checked}");
            dic.Add("FitnessName", $"{cmbFitnessFuncs.SelectedItem.ToString().Substring(0,4)}");
            //
            if (this.Constances != null && Constances.Length > 0)
            {
                for (int i = 0; i < Constances.Length; i++)
                {
                    var key = $"C{i}";
                    var value = $"{Constances[i].ToString(CultureInfo.InvariantCulture)}";
                    dic.Add(key, value);
                }
            }
            return dic;
        }

        private ColumnType getOutputType()
        {
            if (cmbFitnessFuncs.Items[0].ToString().Contains("AE"))
                return ColumnType.Numeric;
            if (cb_rootNodeFunction.Items.Count > 3)
                return ColumnType.Binary;
            else
                return ColumnType.Category;
        }
        #endregion


    }
}
