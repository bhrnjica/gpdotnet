namespace GPdotNet.Wnd.GUI
{
    partial class ExperimentPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //this.btnLoadTesting.Click -= btnLoadTrainig_Click;
            //this.btnLoadTesting.Click -= btnLoadTesting_Click;
            //this.btnSetToGP.Click -= btnLoadTesting_Click;

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numberRadio = new System.Windows.Forms.RadioButton();
            this.presentigeRadio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numCtrlNumForTest = new System.Windows.Forms.NumericUpDown();
            this.btnUpdateModle = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnCreateModel = new System.Windows.Forms.Button();
            this.randomoizeDataSet = new System.Windows.Forms.CheckBox();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCtrlNumForTest)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.numberRadio);
            this.groupBox5.Controls.Add(this.presentigeRadio);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.numCtrlNumForTest);
            this.groupBox5.Location = new System.Drawing.Point(4, 269);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(506, 95);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Testing data set";
            // 
            // numberRadio
            // 
            this.numberRadio.AutoSize = true;
            this.numberRadio.Checked = true;
            this.numberRadio.Location = new System.Drawing.Point(234, 26);
            this.numberRadio.Name = "numberRadio";
            this.numberRadio.Size = new System.Drawing.Size(209, 24);
            this.numberRadio.TabIndex = 17;
            this.numberRadio.TabStop = true;
            this.numberRadio.Text = "# for testing. (0-n/2 rows)";
            this.numberRadio.UseVisualStyleBackColor = true;
            // 
            // presentigeRadio
            // 
            this.presentigeRadio.AutoSize = true;
            this.presentigeRadio.Location = new System.Drawing.Point(234, 58);
            this.presentigeRadio.Name = "presentigeRadio";
            this.presentigeRadio.Size = new System.Drawing.Size(187, 24);
            this.presentigeRadio.TabIndex = 16;
            this.presentigeRadio.Text = "% for testing. (0-50%)";
            this.presentigeRadio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Select last ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numCtrlNumForTest
            // 
            this.numCtrlNumForTest.Location = new System.Drawing.Point(111, 29);
            this.numCtrlNumForTest.Name = "numCtrlNumForTest";
            this.numCtrlNumForTest.Size = new System.Drawing.Size(117, 26);
            this.numCtrlNumForTest.TabIndex = 14;
            this.numCtrlNumForTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnUpdateModle
            // 
            this.btnUpdateModle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateModle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUpdateModle.Location = new System.Drawing.Point(532, 358);
            this.btnUpdateModle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUpdateModle.Name = "btnUpdateModle";
            this.btnUpdateModle.Size = new System.Drawing.Size(170, 38);
            this.btnUpdateModle.TabIndex = 3;
            this.btnUpdateModle.Text = "Update Model";
            this.btnUpdateModle.UseVisualStyleBackColor = true;
            this.btnUpdateModle.Click += new System.EventHandler(this.btnUpdateModle_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(4, 5);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(698, 261);
            this.listView1.TabIndex = 25;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadData.Location = new System.Drawing.Point(532, 274);
            this.btnLoadData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(171, 38);
            this.btnLoadData.TabIndex = 27;
            this.btnLoadData.Text = "Load data...";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnCreateModel
            // 
            this.btnCreateModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCreateModel.Location = new System.Drawing.Point(532, 315);
            this.btnCreateModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCreateModel.Name = "btnCreateModel";
            this.btnCreateModel.Size = new System.Drawing.Size(170, 38);
            this.btnCreateModel.TabIndex = 28;
            this.btnCreateModel.Text = "New Model";
            this.btnCreateModel.UseVisualStyleBackColor = true;
            this.btnCreateModel.Click += new System.EventHandler(this.btnCreateModel_Click);
            // 
            // randomoizeDataSet
            // 
            this.randomoizeDataSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.randomoizeDataSet.AutoSize = true;
            this.randomoizeDataSet.Location = new System.Drawing.Point(4, 374);
            this.randomoizeDataSet.Name = "randomoizeDataSet";
            this.randomoizeDataSet.Size = new System.Drawing.Size(178, 24);
            this.randomoizeDataSet.TabIndex = 48;
            this.randomoizeDataSet.Text = "Randomize data set";
            this.randomoizeDataSet.UseVisualStyleBackColor = true;
            // 
            // ExperimentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.randomoizeDataSet);
            this.Controls.Add(this.btnCreateModel);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.btnUpdateModle);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox5);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ExperimentPanel";
            this.Size = new System.Drawing.Size(714, 415);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCtrlNumForTest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnUpdateModle;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton numberRadio;
        public System.Windows.Forms.NumericUpDown numCtrlNumForTest;
        private System.Windows.Forms.RadioButton presentigeRadio;
        private System.Windows.Forms.Button btnCreateModel;
        private System.Windows.Forms.CheckBox randomoizeDataSet;
    }
}
