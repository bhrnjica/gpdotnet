namespace GPdotNet.Wnd.GUI
{
    partial class RunPanel
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.eb_durationInMin = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.eb_timeleft = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.eb_timeToCompleate = new System.Windows.Forms.TextBox();
            this.eb_timePerRun = new System.Windows.Forms.TextBox();
            this.eb_timeStart = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.eb_bestSolutionFound = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.eb_maximumFitness = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.m_cbIterationType = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.eb_currentFitness = new System.Windows.Forms.TextBox();
            this.m_eb_iterations = new System.Windows.Forms.TextBox();
            this.eb_currentIteration = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.zedFitness = new ZedGraph.ZedGraphControl();
            this.zedModel = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox7.Controls.Add(this.eb_durationInMin);
            this.groupBox7.Controls.Add(this.label44);
            this.groupBox7.Controls.Add(this.eb_timeleft);
            this.groupBox7.Controls.Add(this.label43);
            this.groupBox7.Controls.Add(this.eb_timeToCompleate);
            this.groupBox7.Controls.Add(this.eb_timePerRun);
            this.groupBox7.Controls.Add(this.eb_timeStart);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.eb_bestSolutionFound);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.eb_maximumFitness);
            this.groupBox7.Controls.Add(this.label31);
            this.groupBox7.Controls.Add(this.m_cbIterationType);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.eb_currentFitness);
            this.groupBox7.Controls.Add(this.m_eb_iterations);
            this.groupBox7.Controls.Add(this.eb_currentIteration);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(247, 321);
            this.groupBox7.TabIndex = 16;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Evolution";
            // 
            // eb_durationInMin
            // 
            this.eb_durationInMin.Location = new System.Drawing.Point(122, 250);
            this.eb_durationInMin.Name = "eb_durationInMin";
            this.eb_durationInMin.ReadOnly = true;
            this.eb_durationInMin.Size = new System.Drawing.Size(119, 20);
            this.eb_durationInMin.TabIndex = 35;
            this.eb_durationInMin.Visible = false;
            // 
            // label44
            // 
            this.label44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label44.Location = new System.Drawing.Point(15, 250);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(100, 18);
            this.label44.TabIndex = 34;
            this.label44.Text = "Duration(min):";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label44.Visible = false;
            // 
            // eb_timeleft
            // 
            this.eb_timeleft.Location = new System.Drawing.Point(122, 226);
            this.eb_timeleft.Name = "eb_timeleft";
            this.eb_timeleft.ReadOnly = true;
            this.eb_timeleft.Size = new System.Drawing.Size(119, 20);
            this.eb_timeleft.TabIndex = 33;
            this.eb_timeleft.Visible = false;
            // 
            // label43
            // 
            this.label43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label43.Location = new System.Drawing.Point(15, 226);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(100, 18);
            this.label43.TabIndex = 32;
            this.label43.Text = "Avg. time left (min):";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label43.Visible = false;
            // 
            // eb_timeToCompleate
            // 
            this.eb_timeToCompleate.Location = new System.Drawing.Point(122, 204);
            this.eb_timeToCompleate.Name = "eb_timeToCompleate";
            this.eb_timeToCompleate.ReadOnly = true;
            this.eb_timeToCompleate.Size = new System.Drawing.Size(119, 20);
            this.eb_timeToCompleate.TabIndex = 31;
            this.eb_timeToCompleate.Visible = false;
            // 
            // eb_timePerRun
            // 
            this.eb_timePerRun.Location = new System.Drawing.Point(122, 182);
            this.eb_timePerRun.Name = "eb_timePerRun";
            this.eb_timePerRun.ReadOnly = true;
            this.eb_timePerRun.Size = new System.Drawing.Size(119, 20);
            this.eb_timePerRun.TabIndex = 30;
            this.eb_timePerRun.Visible = false;
            // 
            // eb_timeStart
            // 
            this.eb_timeStart.Location = new System.Drawing.Point(122, 161);
            this.eb_timeStart.Name = "eb_timeStart";
            this.eb_timeStart.ReadOnly = true;
            this.eb_timeStart.Size = new System.Drawing.Size(119, 20);
            this.eb_timeStart.TabIndex = 29;
            this.eb_timeStart.Visible = false;
            // 
            // label18
            // 
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(0, 204);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(115, 18);
            this.label18.TabIndex = 28;
            this.label18.Text = "Avg. finish time:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label18.Visible = false;
            // 
            // label14
            // 
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(7, 185);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 18);
            this.label14.TabIndex = 27;
            this.label14.Text = "Cur. iteration (sec):";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label14.Visible = false;
            // 
            // label9
            // 
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(27, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 18);
            this.label9.TabIndex = 26;
            this.label9.Text = "Run started at:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Visible = false;
            // 
            // eb_bestSolutionFound
            // 
            this.eb_bestSolutionFound.Location = new System.Drawing.Point(150, 138);
            this.eb_bestSolutionFound.Name = "eb_bestSolutionFound";
            this.eb_bestSolutionFound.ReadOnly = true;
            this.eb_bestSolutionFound.Size = new System.Drawing.Size(91, 20);
            this.eb_bestSolutionFound.TabIndex = 25;
            // 
            // label33
            // 
            this.label33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label33.Location = new System.Drawing.Point(14, 140);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(130, 18);
            this.label33.TabIndex = 24;
            this.label33.Text = "Changed at generation:";
            // 
            // label32
            // 
            this.label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label32.Location = new System.Drawing.Point(14, 94);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(101, 18);
            this.label32.TabIndex = 22;
            this.label32.Text = "Best fitness:";
            // 
            // eb_maximumFitness
            // 
            this.eb_maximumFitness.Location = new System.Drawing.Point(150, 113);
            this.eb_maximumFitness.Name = "eb_maximumFitness";
            this.eb_maximumFitness.ReadOnly = true;
            this.eb_maximumFitness.Size = new System.Drawing.Size(91, 20);
            this.eb_maximumFitness.TabIndex = 21;
            this.eb_maximumFitness.Text = "1000,00";
            // 
            // label31
            // 
            this.label31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label31.Location = new System.Drawing.Point(14, 116);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(101, 18);
            this.label31.TabIndex = 20;
            this.label31.Text = "Max fitness:";
            // 
            // m_cbIterationType
            // 
            this.m_cbIterationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbIterationType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cbIterationType.FormattingEnabled = true;
            this.m_cbIterationType.Items.AddRange(new object[] {
            "Generation number",
            "Fitness >="});
            this.m_cbIterationType.Location = new System.Drawing.Point(13, 33);
            this.m_cbIterationType.Name = "m_cbIterationType";
            this.m_cbIterationType.Size = new System.Drawing.Size(135, 21);
            this.m_cbIterationType.TabIndex = 19;
            // 
            // label21
            // 
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(14, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(76, 18);
            this.label21.TabIndex = 18;
            this.label21.Text = "Envolve until:";
            // 
            // eb_currentFitness
            // 
            this.eb_currentFitness.Location = new System.Drawing.Point(150, 90);
            this.eb_currentFitness.Name = "eb_currentFitness";
            this.eb_currentFitness.ReadOnly = true;
            this.eb_currentFitness.Size = new System.Drawing.Size(91, 20);
            this.eb_currentFitness.TabIndex = 3;
            // 
            // m_eb_iterations
            // 
            this.m_eb_iterations.Location = new System.Drawing.Point(150, 34);
            this.m_eb_iterations.Name = "m_eb_iterations";
            this.m_eb_iterations.Size = new System.Drawing.Size(91, 20);
            this.m_eb_iterations.TabIndex = 17;
            this.m_eb_iterations.Text = "100";
            // 
            // eb_currentIteration
            // 
            this.eb_currentIteration.Location = new System.Drawing.Point(150, 68);
            this.eb_currentIteration.Name = "eb_currentIteration";
            this.eb_currentIteration.ReadOnly = true;
            this.eb_currentIteration.Size = new System.Drawing.Size(91, 20);
            this.eb_currentIteration.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(14, 71);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(101, 18);
            this.label20.TabIndex = 0;
            this.label20.Text = "Generation:";
            // 
            // zedFitness
            // 
            this.zedFitness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedFitness.Location = new System.Drawing.Point(4, 4);
            this.zedFitness.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedFitness.Name = "zedFitness";
            this.zedFitness.ScrollGrace = 0D;
            this.zedFitness.ScrollMaxX = 0D;
            this.zedFitness.ScrollMaxY = 0D;
            this.zedFitness.ScrollMaxY2 = 0D;
            this.zedFitness.ScrollMinX = 0D;
            this.zedFitness.ScrollMinY = 0D;
            this.zedFitness.ScrollMinY2 = 0D;
            this.zedFitness.Size = new System.Drawing.Size(123, 153);
            this.zedFitness.TabIndex = 0;
            // 
            // zedModel
            // 
            this.zedModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedModel.Location = new System.Drawing.Point(4, 165);
            this.zedModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedModel.Name = "zedModel";
            this.zedModel.ScrollGrace = 0D;
            this.zedModel.ScrollMaxX = 0D;
            this.zedModel.ScrollMaxY = 0D;
            this.zedModel.ScrollMaxY2 = 0D;
            this.zedModel.ScrollMinX = 0D;
            this.zedModel.ScrollMinY = 0D;
            this.zedModel.ScrollMinY2 = 0D;
            this.zedModel.Size = new System.Drawing.Size(123, 154);
            this.zedModel.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 725F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(406, 331);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox7, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(402, 327);
            this.tableLayoutPanel2.TabIndex = 20;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.zedFitness, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.zedModel, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(269, 2);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.13021F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.86979F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(131, 323);
            this.tableLayoutPanel3.TabIndex = 21;
            // 
            // RunPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RunPanel";
            this.Size = new System.Drawing.Size(406, 331);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

       
        protected System.Windows.Forms.GroupBox groupBox7;
        protected System.Windows.Forms.TextBox eb_durationInMin;
        protected System.Windows.Forms.Label label44;
        protected System.Windows.Forms.TextBox eb_timeleft;
        protected System.Windows.Forms.Label label43;
        protected System.Windows.Forms.TextBox eb_timeToCompleate;
        protected System.Windows.Forms.TextBox eb_timePerRun;
        protected System.Windows.Forms.TextBox eb_timeStart;
        protected System.Windows.Forms.Label label18;
        protected System.Windows.Forms.Label label14;
        protected System.Windows.Forms.Label label9;
        protected System.Windows.Forms.TextBox eb_bestSolutionFound;
        protected System.Windows.Forms.Label label33;
        protected System.Windows.Forms.Label label32;
        protected System.Windows.Forms.TextBox eb_maximumFitness;
        protected System.Windows.Forms.Label label31;
        protected System.Windows.Forms.ComboBox m_cbIterationType;
        protected System.Windows.Forms.Label label21;
        protected System.Windows.Forms.TextBox eb_currentFitness;
        protected System.Windows.Forms.TextBox m_eb_iterations;
        protected System.Windows.Forms.TextBox eb_currentIteration;
        protected System.Windows.Forms.Label label20;
        protected ZedGraph.ZedGraphControl zedFitness;
        private ZedGraph.ZedGraphControl zedModel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}
