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
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using GPdotNet.Core;
using GPdotNet.BasicTypes;
using System.Collections;

namespace GPdotNet.Wnd.GUI
{
    /// <summary>
    /// Panel for selecting which primitive programs will be included in to GP
    /// </summary>
    public partial class FunctionPanel : UserControl
    {
        #region Constructor and Fields
        private Dictionary<int, Function> _gpFunctions;
        private int sortColumn = -1;

        public FunctionPanel()
        {
            InitializeComponent();
            listView1.CheckBoxes = true;
            listView1.GridLines = true;
            listView1.HideSelection = false;

            if (this.DesignMode)
                return;

            //Create local copy of global available function set
            _gpFunctions = new Dictionary<int, Function>(Globals.functionSet.Count,
                                                            Globals.functionSet.Comparer);

            listView1.ColumnClick += ListView1_ColumnClick;

        }
        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listView1.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              listView1.Sorting);

        }

        /// <summary>
        /// load listView table with FunctionSet
        /// </summary>
        private void loadFunctions()
        {
            

            if (_gpFunctions.Count==0)
            {
                foreach (KeyValuePair<int, Function> entry in Globals.functionSet)
                {
                    int k = entry.Key;
                    var f = new Function()
                    {
                        Arity = entry.Value.Arity,
                        Id = entry.Value.Id,
                        Name = entry.Value.Name,
                        Selected = entry.Value.Selected,
                        Weight = entry.Value.Weight
                    };
                    _gpFunctions.Add(k, f);
                }
            }

            //fill ListView ctrl
            fillListView(_gpFunctions.Where(x=>x.Value.Weight > 0).Select(x => x.Value).ToList());

        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Proces of filling listView with functions data
        /// </summary>
        /// <param name="funs"></param>
        private void fillListView(List<Function> funs)
        {

            //clear the list
            listView1.Clear();

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Selected";
            colHeader.Width = 100;
            listView1.Columns.Add(colHeader);
            

            colHeader = new ColumnHeader();
            colHeader.Text = "ID";
            colHeader.Width = 80;
            listView1.Columns.Add(colHeader);


            colHeader = new ColumnHeader();
            colHeader.Text = "Name";
            colHeader.Width = 100;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Weight";
            colHeader.Width = 70;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Arity";
            colHeader.Width = 70;
            listView1.Columns.Add(colHeader);

            //colHeader = new ColumnHeader();
            //colHeader.Text = "Parameters";
            //colHeader.Width = 80;
            //listView1.Columns.Add(colHeader);


            //colHeader = new ColumnHeader();
            //colHeader.Text = "Description";
            //colHeader.Width = 200;
            //listView1.Columns.Add(colHeader);

            

            //colHeader = new ColumnHeader();
            //colHeader.Text = "ExcelDefinition";
            //colHeader.Width = 100;
            //listView1.Columns.Add(colHeader);

            //colHeader = new ColumnHeader();
            //colHeader.Text = "ReadOnly";
            //colHeader.Width = 0;
            //listView1.Columns.Add(colHeader);

            //colHeader = new ColumnHeader();
            //colHeader.Text = "IsDistribution";
            //colHeader.Width = 0;
            //listView1.Columns.Add(colHeader);

         

            for (int i = 0; i <funs.Count; i++)
            {
                var fun = funs[i];
                ListViewItem LVI = listView1.Items.Add(" ");
                LVI.Checked = fun.Selected;
                LVI.SubItems.Add(fun.Id.ToString());
                LVI.SubItems.Add(fun.Name.ToString());
                LVI.SubItems.Add(fun.Weight.ToString());
                LVI.SubItems.Add(fun.Arity.ToString());
                LVI.SubItems.Add(fun.Parameter.ToString());
               

                

               // LVI.SubItems.Add(fun.Definition.ToString());
               // LVI.SubItems.Add(fun.Description.ToString());
               // LVI.SubItems.Add(fun.ExcelDefinition.ToString());
               // LVI.SubItems.Add(fun.IsReadOnly.ToString());
               // LVI.SubItems.Add(fun.IsDistribution.ToString());
               // LVI.SubItems.Add(fun.Id.ToString());
 
            }
        }

        /// <summary>
        /// Selection changed listView event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var ind= int.Parse(listView1.SelectedIndices[0].ToString());
                textBox1.Text= _gpFunctions[ind+2000].Weight.ToString();
            }
        }

        /// <summary>
        /// Fire event of save new value for Weight of selected function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                int newWeight = int.Parse(textBox1.Text);

                //find selected row from listVeiew
                if (listView1.SelectedIndices.Count > 0)
                {
                    var ind = int.Parse(listView1.SelectedIndices[0].ToString());

                    _gpFunctions[ind+2000].Weight = newWeight;

                    //If the user change selection state of function
                    SaveModification();

                    loadFunctions();
                    //fillListView(_gpFunctions.Values.ToList());

                    listView1.SelectedIndices.Add(ind);


                    
                }
                else
                {
                    MessageBox.Show("First select listView row then modify value.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Save modification user made in listView in to backed list of GPFunctions.
        /// </summary>
        private void SaveModification()
        {
            for (int i=0;i<listView1.Items.Count; i++)
            {
                 ListViewItem LVI = listView1.Items[i];
                 var gpFUn=_gpFunctions[i+2000];
                 if (gpFUn.Selected != LVI.Checked)
                 {
                     gpFUn.Selected = LVI.Checked;
                 }
            }
        }
        #endregion
        
        #region Public Methods

        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            button1.Enabled = p;
            listView1.Enabled = p;
        }
        /// <summary>
        /// Deserilization of function
        /// </summary>
        /// <param name="p"></param>
        public void FunctionSetFromString(string p)
        {
            var funs = p.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (listView1.Items.Count == 0)
               loadFunctions();

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem LVI = listView1.Items[i];
                LVI.Checked = false;
                if (funs.Length > 0)
                {
                    foreach(var f in funs)
                    {
                        var st = f.Split(',');

                        if (LVI.SubItems[1].Text == st[1])
                        {
                            LVI.Checked = st[0] == "1" ? true : false;
                            LVI.SubItems[1].Text = st[1];
                            LVI.SubItems[3].Text = st[2];
                            break;
                        }
                    }
                    
                }
                else
                    LVI.Checked = false;

            }
        }

        /// <summary>
        /// Serilization of functions
        /// Serialization of Checked, and Weight columns.
        /// </summary>
        /// <returns></returns>
        public string FunctionSetToString()
        {
            var funs ="";

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem LVI = listView1.Items[i];
                string str = LVI.Checked == true ? "1" : "0";
                str+=","+LVI.SubItems[1].Text+",";
                str += LVI.SubItems[3].Text + ";";
                funs += str;

            }

            return funs;
        }
        /// <summary>
        /// Reset the panle from previous state
        /// </summary>
        public void ResetFunctionSet()
        {
            listView1.Clear();

            _gpFunctions = new Dictionary<int, Function>(Globals.functionSet.Count,
                                                            Globals.functionSet.Comparer);

            //loadFunctions();

        }

        /// <summary>
        /// This method is called when GPFactory is going to be created. We need last state of the functionSet
        /// </summary>
        /// <returns></returns>
        public Function[] GetSelectedFunctions()
        {
            var funSet = new List<Function>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem LVI = listView1.Items[i];
                if(LVI.Checked)
                {
                    var fun = _gpFunctions.Where(x => x.Value.Name == LVI.SubItems[2].Text).Select(x => x.Value).FirstOrDefault();
                    funSet.Add(fun);
                }

            }
            return funSet.ToArray();
        }
        #endregion
    }

    // Implements the manual sorting of items by columns.
    class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal;
            // Determine whether the type being compared is a date type.
            try
            {
                // Parse the two objects passed as a parameter as a DateTime.
                var xLvi = ((ListViewItem)x);//.SubItems[col].Text;
                var yLvi = ((ListViewItem)y);//.SubItems[col].Text;

                if(DateTime.TryParse(xLvi.SubItems[col].Text, out DateTime dtx) && DateTime.TryParse(yLvi.SubItems[col].Text, out DateTime dty))
                {

                    // Compare the two dates.
                    returnVal = DateTime.Compare(dtx, dty);
                }
                else if(xLvi.SubItems[col].Text == " " || xLvi.SubItems[col].Text == " ")
                {
                    // Compare the two items as a string.
                    returnVal = String.Compare(((ListViewItem)x).Checked.ToString(),
                                ((ListViewItem)y).Checked.ToString());
                }
                // If neither compared object has a valid date format, compare
                // as a string.
                else
                {
                    // Compare the two items as a string.
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                ((ListViewItem)y).SubItems[col].Text);
                }

                // Determine whether the sort order is descending.
                if (order == SortOrder.Descending)
                    // Invert the value returned by String.Compare.
                    returnVal *= -1;
                return returnVal;

            }
           
            catch
            {
                throw;
            }
            

        }
    }
}
