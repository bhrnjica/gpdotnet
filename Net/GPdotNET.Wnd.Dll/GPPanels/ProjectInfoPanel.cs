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
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace GPdotNet.Wnd.GUI
{

    /// <summary>
    /// Main panel for loading and defining experimental dataA
    /// </summary>
    public partial class ProjectInfoPanel : UserControl
    {

        #region Ctor and Fields
       
        public ProjectInfoPanel()
        {
            InitializeComponent();
            richTextBox1.MouseUp += richTextBox1_MouseUp;
            button1.Click += Click_LoadRtfFile;
        }

        public string InfoText
        {
            get
            {
                return richTextBox1.Rtf;
            }
            set
            {

                if (this.InvokeRequired)
                {
                    // Execute the same method, but this time on the GUI thread
                    this.Invoke(
                        new Action(() =>
                        {
                            richTextBox1.Rtf = value;

                        }
                        ));
                }
                else
                {
                    richTextBox1.Rtf = value;

                }

                
            }
        }

        public string GetProjectInfo()
        {
            return richTextBox1.Rtf;
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   //click event
                //MessageBox.Show("you got it!");
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Cut");
                menuItem.Click += new EventHandler(CutAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste");
                menuItem.Click += new EventHandler(PasteAction);
                contextMenu.MenuItems.Add(menuItem);

                richTextBox1.ContextMenu = contextMenu;
            }
        }
        void CutAction(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        void CopyAction(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null && richTextBox1.SelectedText != "")
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                int selstart = richTextBox1.SelectionStart;
                if (richTextBox1.SelectedText != null && richTextBox1.SelectedText != "")
                {
                    richTextBox1.Text = richTextBox1.Text.Remove(selstart, richTextBox1.SelectionLength);
                }

                string clip = Clipboard.GetText(TextDataFormat.Rtf).ToString();
                richTextBox1.Rtf = richTextBox1.Text.Insert(selstart, clip);
                richTextBox1.SelectionStart = selstart + clip.Length;
            }
        }
        #endregion

        private void Click_LoadRtfFile(object sender, EventArgs e)
        {
            var strPath = "";

            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Filter = "RTF files (*.rtf)|*.rtf|Text files (*.txt)|*.txt";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                strPath= dlg.FileName;
            else
                return;


            if (strPath != null)
            {
                richTextBox1.LoadFile(strPath, RichTextBoxStreamType.RichText);
            }
        }
    }
}
