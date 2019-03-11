namespace GPdotNet.Wnd.GUI
{
    partial class ResultPanel
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
            button1.Click -= button1_Click;
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
            this.label42 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.solutionExpression = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnROC = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.treeCtrlDrawer1 = new GPdotNet.Wnd.GUI.TreeCtrlDrawer();
            this.SuspendLayout();
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label42.Location = new System.Drawing.Point(24, 9);
            this.label42.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(257, 25);
            this.label42.TabIndex = 28;
            this.label42.Text = "Best Solution  (Tree form)";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(276, 265);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 30;
            this.label1.Text = "Best Solution";
            // 
            // solutionExpression
            // 
            this.solutionExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.solutionExpression.Location = new System.Drawing.Point(285, 312);
            this.solutionExpression.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.solutionExpression.Multiline = true;
            this.solutionExpression.Name = "solutionExpression";
            this.solutionExpression.ReadOnly = true;
            this.solutionExpression.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.solutionExpression.Size = new System.Drawing.Size(511, 200);
            this.solutionExpression.TabIndex = 31;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(637, 253);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 46);
            this.button1.TabIndex = 32;
            this.button1.Text = "Save to png ...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnROC
            // 
            this.btnROC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnROC.Location = new System.Drawing.Point(425, 254);
            this.btnROC.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnROC.Name = "btnROC";
            this.btnROC.Size = new System.Drawing.Size(201, 46);
            this.btnROC.TabIndex = 33;
            this.btnROC.Text = "Model Evaluation";
            this.btnROC.UseVisualStyleBackColor = true;
            this.btnROC.Click += new System.EventHandler(this.btnmodelEvaluation_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(5, 312);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(263, 200);
            this.listView1.TabIndex = 34;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // treeCtrlDrawer1
            // 
            this.treeCtrlDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCtrlDrawer1.AutoScroll = true;
            this.treeCtrlDrawer1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.treeCtrlDrawer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeCtrlDrawer1.Location = new System.Drawing.Point(5, 40);
            this.treeCtrlDrawer1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.treeCtrlDrawer1.Name = "treeCtrlDrawer1";
            this.treeCtrlDrawer1.Size = new System.Drawing.Size(793, 202);
            this.treeCtrlDrawer1.TabIndex = 29;
            // 
            // ResultPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnROC);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.solutionExpression);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeCtrlDrawer1);
            this.Controls.Add(this.label42);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "ResultPanel";
            this.Size = new System.Drawing.Size(805, 520);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label42;
        private TreeCtrlDrawer treeCtrlDrawer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox solutionExpression;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnROC;
        private System.Windows.Forms.ListView listView1;
    }
}
