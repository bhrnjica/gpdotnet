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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNet.Wnd.GUI
{ 
    public partial class ImportExperimentalData : Form
    {
        private string originData = "";
        public ImportExperimentalData()
        {
            InitializeComponent();
            this.Icon = Extensions.LoadIconFromName("GPdotNet.Wnd.Dll.Images.gpdotnet.ico");
        }

        //Import file
        private void button1_Click(object sender, EventArgs e)
        {
            var strFile = GetFileFromOpenDialog("","");
            if (strFile == null)
                return;

            textBox1.Text = strFile;

            var data = string.Join(Environment.NewLine, File.ReadAllLines(strFile).Where(l => !l.StartsWith("@") && !l.StartsWith("#") && !l.StartsWith("!")));
            originData = data;
            textBox3.Text = data;
            ProcesData();
            
            if (!string.IsNullOrEmpty(data))
                button2.Enabled = true;
        }

        public static string GetFileFromOpenDialog(string fileDescription = "Comma separated files ", string extension = "*.csv")
        {
#if WINDOWS_APP
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".*");
            //openPicker.FileTypeFilter.Add(".jpeg");
            //openPicker.FileTypeFilter.Add(".png");

            var tsk = openPicker.PickSingleFileAsync();
            return tsk.GetAwaiter().GetResult().Name;
#else
            OpenFileDialog dlg = new OpenFileDialog();
            if (string.IsNullOrEmpty(extension))
                dlg.Filter = "Plain text files (*.csv;*.txt;*.dat)|*.csv;*.txt;*.dat |All files (*.*)|*.*";
            else
                dlg.Filter = string.Format("{1} ({0})|{0}|All files (*.*)|*.*", extension, fileDescription);
            //
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.FileName;
            else
                return null;
#endif
        }

        private void ProcesData()
        {
            var data = originData;
            if (string.IsNullOrEmpty(data))
                return;

            if (checkBox2.Checked)
                data = data.Replace(";", "\t|\t");
            if (checkBox3.Checked)
                data = data.Replace(",", "\t|\t");
            if (checkBox4.Checked)
                data = data.Replace(" ", "\t|\t");
            if (checkBox6.Checked)
                data = data.Replace("\t", "\t|\t");
            if (checkBox5.Checked)
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                    data = data.Replace(textBox2.Text[0], '|');
            }

            //if header is present separate data with horizontal line
            if (checkBox1.Checked)
            {
                var index = data.IndexOf(Environment.NewLine);
                var index2 = data.IndexOf(Environment.NewLine, index + 1);
                int counter=0;
                while(counter<index2-index)
                {
                    data=data.Insert(index,"-");
                    counter++;
                }
                data = data.Insert(index, Environment.NewLine);
            }
            

            textBox3.Text = data;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("No file is selected!");
                    return;
                }


                var colDelimiter = GetColumDelimiter();

                LoadExperiment(originData, colDelimiter, checkBox1.Checked, radioButton1.Checked);
            }
            catch (Exception ex)
            {

                reportException(ex);
            }
            
        }

        private void reportException(Exception ex)
        {
            MessageBox.Show(ex.Message, "GPdotNET");
        }

        public string[] Header { get; set; }
        public string[][] Data { get; set; }
        /// <summary>
        /// Initialize Projects with string data, with specific formating 
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="columDelimiter"></param>
        /// <param name="isFirstRowHeader"></param>
        /// <param name="isFloatingPoint"></param>
        public void LoadExperiment(string strData, char[] columDelimiter, bool isFirstRowHeader, bool isFloatingPoint = true)
        {
            //define the row
            string[] rows = strData.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            //Define the columns
            var colCount = rows[0].Split(columDelimiter).Count();
            var rowCount = rows.Length;
            int headerCount = 0;
            ///
            Header = null;
            if (isFirstRowHeader)
                headerCount++;

            Data = new string[rowCount - headerCount][];

            //
            for (int i = 0; i < rowCount; i++)
            {
                var row = rows[i].Split(columDelimiter);

                //column creation for each row
                if (i == 0 && isFirstRowHeader)
                    Header = new string[colCount];
                else
                    Data[i - headerCount] = new string[colCount];

                if (row.Length != colCount)
                {

                    Data = null;
                    throw new Exception("Data is not consistent.");
                }
                //column enumeration
                for (int j = 0; j < colCount; j++)
                {
                    //value format
                    var value = "";
                    if (string.IsNullOrEmpty(row[j]))
                        value = "n/a";
                    else
                        value = row[j];

                    //
                    if (i == 0 && isFirstRowHeader)
                        Header[j] = value;
                    else
                        Data[i - headerCount][j] = value;


                }
            }


        }

        private char[] GetColumDelimiter()
        {
            var col = new List<char>();

            if (checkBox2.Checked)
                col.Add(';');
            if (checkBox3.Checked)
                col.Add(',');
            if (checkBox4.Checked)
                col.Add(' ');
            if (checkBox6.Checked)
                col.Add('\t');
            if (checkBox5.Checked)
                col.Add(textBox2.Text[0]);

            return col.ToArray();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var ch = sender as CheckBox;
            if (ch.Name == "checkBox5")
            {
                if (ch.Checked)
                    textBox2.Enabled = true;
                else
                {
                    textBox2.Text = "";
                    textBox2.Enabled = false;
                }
            }
            ProcesData();
        }
    }
}
