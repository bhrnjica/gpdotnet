namespace GPdotNet.Wnd.GUI.TestApp
{
    partial class DataLoaderWnd
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.experimentPanel1 = new GPdotNet.Wnd.TestApp.ExperimentPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // experimentPanel1
            // 
            this.experimentPanel1.AutoSize = true;
            this.experimentPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.experimentPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.experimentPanel1.CreateModel = null;
            this.experimentPanel1.GetDataSet = null;
            this.experimentPanel1.Location = new System.Drawing.Point(13, 33);
            this.experimentPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.experimentPanel1.Name = "experimentPanel1";
            this.experimentPanel1.Size = new System.Drawing.Size(1026, 711);
            this.experimentPanel1.TabIndex = 0;
            this.experimentPanel1.UpdateModel = null;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1061, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataLoaderWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 758);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.experimentPanel1);
            this.Name = "DataLoaderWnd";
            this.Text = "DataLoaderWnd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Wnd.TestApp.ExperimentPanel experimentPanel1;
        private System.Windows.Forms.Button button1;
    }
}