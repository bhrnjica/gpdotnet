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
            this.treeCtrlDrawer1 = new GPdotNet.Wnd.GUI.TreeCtrlDrawer();
            this.SuspendLayout();
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label42.Location = new System.Drawing.Point(18, 7);
            this.label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(190, 20);
            this.label42.TabIndex = 28;
            this.label42.Text = "Best Solution  (Tree form)";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(18, 229);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "Best Solution (Simple text)";
            // 
            // solutionExpression
            // 
            this.solutionExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.solutionExpression.Location = new System.Drawing.Point(4, 254);
            this.solutionExpression.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.solutionExpression.Multiline = true;
            this.solutionExpression.Name = "solutionExpression";
            this.solutionExpression.ReadOnly = true;
            this.solutionExpression.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.solutionExpression.Size = new System.Drawing.Size(594, 98);
            this.solutionExpression.TabIndex = 31;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(478, 211);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 37);
            this.button1.TabIndex = 32;
            this.button1.Text = "Save to png ...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnROC
            // 
            this.btnROC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnROC.Location = new System.Drawing.Point(295, 212);
            this.btnROC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnROC.Name = "btnROC";
            this.btnROC.Size = new System.Drawing.Size(175, 37);
            this.btnROC.TabIndex = 33;
            this.btnROC.Text = "Model Evaluation";
            this.btnROC.UseVisualStyleBackColor = true;
            this.btnROC.Click += new System.EventHandler(this.btnmodelEvaluation_Click);
            // 
            // treeCtrlDrawer1
            // 
            this.treeCtrlDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCtrlDrawer1.AutoScroll = true;
            this.treeCtrlDrawer1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.treeCtrlDrawer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeCtrlDrawer1.Location = new System.Drawing.Point(4, 32);
            this.treeCtrlDrawer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeCtrlDrawer1.Name = "treeCtrlDrawer1";
            this.treeCtrlDrawer1.Size = new System.Drawing.Size(595, 173);
            this.treeCtrlDrawer1.TabIndex = 29;
            // 
            // ResultPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.btnROC);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.solutionExpression);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeCtrlDrawer1);
            this.Controls.Add(this.label42);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ResultPanel";
            this.Size = new System.Drawing.Size(604, 358);
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
    }
}
