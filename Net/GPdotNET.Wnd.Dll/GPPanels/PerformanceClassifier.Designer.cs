namespace GPdotNet.Wnd.GUI
{
    partial class PerformanceClassfier
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.zedModel = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labThresholdValue = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txScore = new System.Windows.Forms.TextBox();
            this.txNegativeLable = new System.Windows.Forms.TextBox();
            this.trThreshold = new System.Windows.Forms.TrackBar();
            this.txRecall = new System.Windows.Forms.TextBox();
            this.txPrecision = new System.Windows.Forms.TextBox();
            this.txPositiveLabel = new System.Windows.Forms.TextBox();
            this.txAccuracy = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txTN = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txFN = new System.Windows.Forms.TextBox();
            this.txFP = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txTP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.TextBox();
            this.txtAUC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(654, 429);
            this.tabControl1.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(395, 207);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Confusion matrix";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(646, 403);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ROC Curve";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.zedModel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(640, 397);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // zedModel
            // 
            this.zedModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedModel.Location = new System.Drawing.Point(3, 3);
            this.zedModel.Name = "zedModel";
            this.zedModel.ScrollGrace = 0D;
            this.zedModel.ScrollMaxX = 0D;
            this.zedModel.ScrollMaxY = 1D;
            this.zedModel.ScrollMaxY2 = 1D;
            this.zedModel.ScrollMinX = 0D;
            this.zedModel.ScrollMinY = 0D;
            this.zedModel.ScrollMinY2 = 0D;
            this.zedModel.Size = new System.Drawing.Size(634, 275);
            this.zedModel.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labThresholdValue);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txScore);
            this.panel1.Controls.Add(this.txNegativeLable);
            this.panel1.Controls.Add(this.trThreshold);
            this.panel1.Controls.Add(this.txRecall);
            this.panel1.Controls.Add(this.txPrecision);
            this.panel1.Controls.Add(this.txPositiveLabel);
            this.panel1.Controls.Add(this.txAccuracy);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txTN);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txFN);
            this.panel1.Controls.Add(this.txFP);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txTP);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtError);
            this.panel1.Controls.Add(this.txtAUC);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(2, 283);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 110);
            this.panel1.TabIndex = 5;
            // 
            // labThresholdValue
            // 
            this.labThresholdValue.AutoSize = true;
            this.labThresholdValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labThresholdValue.Location = new System.Drawing.Point(271, 72);
            this.labThresholdValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labThresholdValue.Name = "labThresholdValue";
            this.labThresholdValue.Size = new System.Drawing.Size(0, 13);
            this.labThresholdValue.TabIndex = 32;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(165, 58);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 31;
            this.label14.Text = "Trashhold:";
            // 
            // txScore
            // 
            this.txScore.Location = new System.Drawing.Point(563, 27);
            this.txScore.Margin = new System.Windows.Forms.Padding(2);
            this.txScore.Name = "txScore";
            this.txScore.ReadOnly = true;
            this.txScore.Size = new System.Drawing.Size(70, 20);
            this.txScore.TabIndex = 29;
            // 
            // txNegativeLable
            // 
            this.txNegativeLable.Location = new System.Drawing.Point(88, 88);
            this.txNegativeLable.Margin = new System.Windows.Forms.Padding(2);
            this.txNegativeLable.Name = "txNegativeLable";
            this.txNegativeLable.ReadOnly = true;
            this.txNegativeLable.Size = new System.Drawing.Size(70, 20);
            this.txNegativeLable.TabIndex = 21;
            // 
            // trThreshold
            // 
            this.trThreshold.Location = new System.Drawing.Point(234, 55);
            this.trThreshold.Margin = new System.Windows.Forms.Padding(2);
            this.trThreshold.Name = "trThreshold";
            this.trThreshold.Size = new System.Drawing.Size(84, 45);
            this.trThreshold.TabIndex = 30;
            this.trThreshold.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // txRecall
            // 
            this.txRecall.Location = new System.Drawing.Point(563, 72);
            this.txRecall.Margin = new System.Windows.Forms.Padding(2);
            this.txRecall.Name = "txRecall";
            this.txRecall.ReadOnly = true;
            this.txRecall.Size = new System.Drawing.Size(70, 20);
            this.txRecall.TabIndex = 27;
            // 
            // txPrecision
            // 
            this.txPrecision.Location = new System.Drawing.Point(563, 2);
            this.txPrecision.Margin = new System.Windows.Forms.Padding(2);
            this.txPrecision.Name = "txPrecision";
            this.txPrecision.ReadOnly = true;
            this.txPrecision.Size = new System.Drawing.Size(70, 20);
            this.txPrecision.TabIndex = 28;
            // 
            // txPositiveLabel
            // 
            this.txPositiveLabel.Location = new System.Drawing.Point(88, 67);
            this.txPositiveLabel.Margin = new System.Windows.Forms.Padding(2);
            this.txPositiveLabel.Name = "txPositiveLabel";
            this.txPositiveLabel.ReadOnly = true;
            this.txPositiveLabel.Size = new System.Drawing.Size(70, 20);
            this.txPositiveLabel.TabIndex = 20;
            // 
            // txAccuracy
            // 
            this.txAccuracy.Location = new System.Drawing.Point(563, 51);
            this.txAccuracy.Margin = new System.Windows.Forms.Padding(2);
            this.txAccuracy.Name = "txAccuracy";
            this.txAccuracy.ReadOnly = true;
            this.txAccuracy.Size = new System.Drawing.Size(70, 20);
            this.txAccuracy.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 90);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Negative Label:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 69);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Positive Label:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(521, 74);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Recall:";
            // 
            // txTN
            // 
            this.txTN.Location = new System.Drawing.Point(248, 31);
            this.txTN.Margin = new System.Windows.Forms.Padding(2);
            this.txTN.Name = "txTN";
            this.txTN.ReadOnly = true;
            this.txTN.Size = new System.Drawing.Size(70, 20);
            this.txTN.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(507, 53);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Accuracy:";
            // 
            // txFN
            // 
            this.txFN.Location = new System.Drawing.Point(248, 8);
            this.txFN.Margin = new System.Windows.Forms.Padding(2);
            this.txFN.Name = "txFN";
            this.txFN.ReadOnly = true;
            this.txFN.Size = new System.Drawing.Size(70, 20);
            this.txFN.TabIndex = 16;
            // 
            // txFP
            // 
            this.txFP.Location = new System.Drawing.Point(88, 31);
            this.txFP.Margin = new System.Windows.Forms.Padding(2);
            this.txFP.Name = "txFP";
            this.txFP.ReadOnly = true;
            this.txFP.Size = new System.Drawing.Size(70, 20);
            this.txFP.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(507, 27);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "F1 Score:";
            // 
            // txTP
            // 
            this.txTP.Location = new System.Drawing.Point(88, 6);
            this.txTP.Margin = new System.Windows.Forms.Padding(2);
            this.txTP.Name = "txTP";
            this.txTP.ReadOnly = true;
            this.txTP.Size = new System.Drawing.Size(70, 20);
            this.txTP.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "True Negative:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(507, 4);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Precision:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "False Negative:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 33);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "False Positive:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "True Positive:";
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(373, 28);
            this.txtError.Margin = new System.Windows.Forms.Padding(2);
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(61, 20);
            this.txtError.TabIndex = 9;
            // 
            // txtAUC
            // 
            this.txtAUC.Location = new System.Drawing.Point(373, 4);
            this.txtAUC.Margin = new System.Windows.Forms.Padding(2);
            this.txtAUC.Name = "txtAUC";
            this.txtAUC.ReadOnly = true;
            this.txtAUC.Size = new System.Drawing.Size(61, 20);
            this.txtAUC.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Error:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "AUC:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(436, 64);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(61, 20);
            this.textBox1.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(337, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Heidke skill score:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(436, 87);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(61, 20);
            this.textBox2.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(337, 90);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "Peirce skill score:";
            // 
            // PerformanceClassfier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.tabControl1);
            this.Name = "PerformanceClassfier";
            this.Size = new System.Drawing.Size(654, 429);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trThreshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labThresholdValue;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txScore;
        private System.Windows.Forms.TextBox txNegativeLable;
        private System.Windows.Forms.TrackBar trThreshold;
        private System.Windows.Forms.TextBox txRecall;
        private System.Windows.Forms.TextBox txPrecision;
        private System.Windows.Forms.TextBox txPositiveLabel;
        private System.Windows.Forms.TextBox txAccuracy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txTN;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txFN;
        private System.Windows.Forms.TextBox txFP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txTP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.TextBox txtAUC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ZedGraph.ZedGraphControl zedModel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}
