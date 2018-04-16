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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using GPdotNET.Util;
namespace GPdotNet.Wnd.GUI
{
    public partial class ExportDialog : Form
    {
        public ExportDialog()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 1;
            this.Icon = Extensions.LoadIconFromName("GPdotNet.Wnd.Dll.Images.gpdotnet.ico");
            //this.pictureBox1.Image = GPModelGlobals.LoadImageFromName("GPdotNet.App.Resources.gpdotnet_ico48.png");
        }

        public bool isAnnModelExport { get; set; }

        public int SelectedOption
        {
            get 
            {
                return listBox1.SelectedIndex;
            }
        }

        public string SelectedItem
        {
            get
            {
                return listBox1.SelectedItem.ToString();
            }
        }

        private void ExportDialog_Load(object sender, EventArgs e)
        {
            if(isAnnModelExport)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Excel");
            }
            listBox1.SelectedItem = "Excel";
        }
    }
}
