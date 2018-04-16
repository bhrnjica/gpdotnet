namespace GPdotNet.Wnd.GUI
{
    partial class ParametersPanel
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
            cmbSelectionMethods.SelectedIndexChanged -= this.cmbSelectionMethods_SelectedIndexChanged;
            //button3.Click -= button3_Click;
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
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPSize = new System.Windows.Forms.Label();
            this.txtPopSize = new System.Windows.Forms.TextBox();
            this.cmbFitnessFuncs = new System.Windows.Forms.ComboBox();
            this.cmbInitMethods = new System.Windows.Forms.ComboBox();
            this.lblFitness = new System.Windows.Forms.Label();
            this.lblInitialization = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.isProtectedOperation = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbSelParam1 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblElitism = new System.Windows.Forms.Label();
            this.txtSelParam1 = new System.Windows.Forms.TextBox();
            this.txtElitism = new System.Windows.Forms.TextBox();
            this.cmbSelectionMethods = new System.Windows.Forms.ComboBox();
            this.lblSelectionMethod = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtRandomConstNum = new System.Windows.Forms.TextBox();
            this.lblConstFrom = new System.Windows.Forms.Label();
            this.lblConsCount = new System.Windows.Forms.Label();
            this.txtRandomConsFrom = new System.Windows.Forms.TextBox();
            this.lblConstTo = new System.Windows.Forms.Label();
            this.txtRandomConsTo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioIsParallel = new System.Windows.Forms.RadioButton();
            this.radioSingleCore = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtProbMutation = new System.Windows.Forms.TextBox();
            this.txtProbCrossover = new System.Windows.Forms.TextBox();
            this.txtProbReproduction = new System.Windows.Forms.TextBox();
            this.lblPMutation = new System.Windows.Forms.Label();
            this.lblPCrossover = new System.Windows.Forms.Label();
            this.lblPReproduction = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOperationTreeDepth = new System.Windows.Forms.TextBox();
            this.txtInitTreeDepth = new System.Windows.Forms.TextBox();
            this.lblOMaximum = new System.Windows.Forms.Label();
            this.lblIMaximum = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblInitialTries = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBroodSize = new System.Windows.Forms.TextBox();
            this.lblBroodSize = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.eb_cutOff = new System.Windows.Forms.TextBox();
            this.lblTreshold = new System.Windows.Forms.Label();
            this.cb_rootNodeFunction = new System.Windows.Forms.ComboBox();
            this.lblRootNode = new System.Windows.Forms.Label();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.lblPSize);
            this.groupBox8.Controls.Add(this.txtPopSize);
            this.groupBox8.Controls.Add(this.cmbFitnessFuncs);
            this.groupBox8.Controls.Add(this.cmbInitMethods);
            this.groupBox8.Controls.Add(this.lblFitness);
            this.groupBox8.Controls.Add(this.lblInitialization);
            this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox8.Location = new System.Drawing.Point(21, 15);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox8.Size = new System.Drawing.Size(488, 154);
            this.groupBox8.TabIndex = 43;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Population";
            // 
            // label6
            // 
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(242, 26);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 25);
            this.label6.TabIndex = 30;
            this.label6.Text = "(50-5000)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPSize
            // 
            this.lblPSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPSize.Location = new System.Drawing.Point(8, 25);
            this.lblPSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPSize.Name = "lblPSize";
            this.lblPSize.Size = new System.Drawing.Size(132, 26);
            this.lblPSize.TabIndex = 0;
            this.lblPSize.Text = "Size:";
            this.lblPSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPopSize
            // 
            this.txtPopSize.Location = new System.Drawing.Point(158, 23);
            this.txtPopSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPopSize.Name = "txtPopSize";
            this.txtPopSize.Size = new System.Drawing.Size(74, 26);
            this.txtPopSize.TabIndex = 1;
            this.txtPopSize.Text = "500";
            // 
            // cmbFitnessFuncs
            // 
            this.cmbFitnessFuncs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFitnessFuncs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFitnessFuncs.Location = new System.Drawing.Point(156, 63);
            this.cmbFitnessFuncs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFitnessFuncs.Name = "cmbFitnessFuncs";
            this.cmbFitnessFuncs.Size = new System.Drawing.Size(320, 28);
            this.cmbFitnessFuncs.TabIndex = 31;
            // 
            // cmbInitMethods
            // 
            this.cmbInitMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInitMethods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbInitMethods.Location = new System.Drawing.Point(156, 103);
            this.cmbInitMethods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbInitMethods.Name = "cmbInitMethods";
            this.cmbInitMethods.Size = new System.Drawing.Size(320, 28);
            this.cmbInitMethods.TabIndex = 29;
            // 
            // lblFitness
            // 
            this.lblFitness.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFitness.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFitness.Location = new System.Drawing.Point(8, 65);
            this.lblFitness.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFitness.Name = "lblFitness";
            this.lblFitness.Size = new System.Drawing.Size(132, 32);
            this.lblFitness.TabIndex = 32;
            this.lblFitness.Tag = "";
            this.lblFitness.Text = "Fitness:";
            this.lblFitness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInitialization
            // 
            this.lblInitialization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInitialization.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInitialization.Location = new System.Drawing.Point(8, 106);
            this.lblInitialization.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInitialization.Name = "lblInitialization";
            this.lblInitialization.Size = new System.Drawing.Size(132, 28);
            this.lblInitialization.TabIndex = 16;
            this.lblInitialization.Tag = "";
            this.lblInitialization.Text = "Initialization:";
            this.lblInitialization.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.isProtectedOperation);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.lbSelParam1);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.lblElitism);
            this.groupBox6.Controls.Add(this.txtSelParam1);
            this.groupBox6.Controls.Add(this.txtElitism);
            this.groupBox6.Controls.Add(this.cmbSelectionMethods);
            this.groupBox6.Controls.Add(this.lblSelectionMethod);
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox6.Location = new System.Drawing.Point(21, 177);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox6.Size = new System.Drawing.Size(585, 157);
            this.groupBox6.TabIndex = 42;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Selection";
            // 
            // isProtectedOperation
            // 
            this.isProtectedOperation.AutoSize = true;
            this.isProtectedOperation.Location = new System.Drawing.Point(376, 25);
            this.isProtectedOperation.Name = "isProtectedOperation";
            this.isProtectedOperation.Size = new System.Drawing.Size(183, 24);
            this.isProtectedOperation.TabIndex = 46;
            this.isProtectedOperation.Text = "Protected operations";
            this.isProtectedOperation.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(236, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 43;
            this.label3.Text = "(-10 - 10)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSelParam1
            // 
            this.lbSelParam1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbSelParam1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbSelParam1.Location = new System.Drawing.Point(16, 98);
            this.lbSelParam1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSelParam1.Name = "lbSelParam1";
            this.lbSelParam1.Size = new System.Drawing.Size(124, 28);
            this.lbSelParam1.TabIndex = 39;
            this.lbSelParam1.Tag = "";
            this.lbSelParam1.Text = "Parameter 1:";
            this.lbSelParam1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbSelParam1.Visible = false;
            // 
            // label23
            // 
            this.label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(237, 23);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(123, 23);
            this.label23.TabIndex = 38;
            this.label23.Text = "(0-PopSize)";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblElitism
            // 
            this.lblElitism.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblElitism.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblElitism.Location = new System.Drawing.Point(16, 26);
            this.lblElitism.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElitism.Name = "lblElitism";
            this.lblElitism.Size = new System.Drawing.Size(124, 25);
            this.lblElitism.TabIndex = 18;
            this.lblElitism.Tag = "";
            this.lblElitism.Text = "Elitism:";
            this.lblElitism.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSelParam1
            // 
            this.txtSelParam1.Location = new System.Drawing.Point(156, 95);
            this.txtSelParam1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSelParam1.Name = "txtSelParam1";
            this.txtSelParam1.Size = new System.Drawing.Size(72, 26);
            this.txtSelParam1.TabIndex = 40;
            this.txtSelParam1.Text = "0";
            this.txtSelParam1.Visible = false;
            // 
            // txtElitism
            // 
            this.txtElitism.Location = new System.Drawing.Point(158, 20);
            this.txtElitism.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtElitism.Name = "txtElitism";
            this.txtElitism.Size = new System.Drawing.Size(72, 26);
            this.txtElitism.TabIndex = 37;
            this.txtElitism.Text = "1";
            // 
            // cmbSelectionMethods
            // 
            this.cmbSelectionMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectionMethods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSelectionMethods.Location = new System.Drawing.Point(158, 57);
            this.cmbSelectionMethods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbSelectionMethods.Name = "cmbSelectionMethods";
            this.cmbSelectionMethods.Size = new System.Drawing.Size(403, 28);
            this.cmbSelectionMethods.TabIndex = 3;
            this.cmbSelectionMethods.SelectedIndexChanged += new System.EventHandler(this.cmbSelectionMethods_SelectedIndexChanged);
            // 
            // lblSelectionMethod
            // 
            this.lblSelectionMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSelectionMethod.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSelectionMethod.Location = new System.Drawing.Point(16, 62);
            this.lblSelectionMethod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectionMethod.Name = "lblSelectionMethod";
            this.lblSelectionMethod.Size = new System.Drawing.Size(124, 28);
            this.lblSelectionMethod.TabIndex = 17;
            this.lblSelectionMethod.Tag = "";
            this.lblSelectionMethod.Text = "Method:";
            this.lblSelectionMethod.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.txtRandomConstNum);
            this.groupBox4.Controls.Add(this.lblConstFrom);
            this.groupBox4.Controls.Add(this.lblConsCount);
            this.groupBox4.Controls.Add(this.txtRandomConsFrom);
            this.groupBox4.Controls.Add(this.lblConstTo);
            this.groupBox4.Controls.Add(this.txtRandomConsTo);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Location = new System.Drawing.Point(644, 178);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(303, 155);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Random constants";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(192, 103);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 38);
            this.button3.TabIndex = 24;
            this.button3.Text = "Generate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // txtRandomConstNum
            // 
            this.txtRandomConstNum.Location = new System.Drawing.Point(130, 109);
            this.txtRandomConstNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRandomConstNum.Name = "txtRandomConstNum";
            this.txtRandomConstNum.Size = new System.Drawing.Size(49, 26);
            this.txtRandomConstNum.TabIndex = 23;
            this.txtRandomConstNum.Text = "2";
            // 
            // lblConstFrom
            // 
            this.lblConstFrom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblConstFrom.Location = new System.Drawing.Point(42, 35);
            this.lblConstFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConstFrom.Name = "lblConstFrom";
            this.lblConstFrom.Size = new System.Drawing.Size(80, 25);
            this.lblConstFrom.TabIndex = 18;
            this.lblConstFrom.Tag = "";
            this.lblConstFrom.Text = "From: ";
            this.lblConstFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblConsCount
            // 
            this.lblConsCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblConsCount.Location = new System.Drawing.Point(51, 109);
            this.lblConsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConsCount.Name = "lblConsCount";
            this.lblConsCount.Size = new System.Drawing.Size(70, 31);
            this.lblConsCount.TabIndex = 22;
            this.lblConsCount.Tag = "";
            this.lblConsCount.Text = "Count:";
            this.lblConsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRandomConsFrom
            // 
            this.txtRandomConsFrom.Location = new System.Drawing.Point(130, 32);
            this.txtRandomConsFrom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRandomConsFrom.Name = "txtRandomConsFrom";
            this.txtRandomConsFrom.Size = new System.Drawing.Size(49, 26);
            this.txtRandomConsFrom.TabIndex = 19;
            this.txtRandomConsFrom.Text = "0";
            // 
            // lblConstTo
            // 
            this.lblConstTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblConstTo.Location = new System.Drawing.Point(87, 72);
            this.lblConstTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConstTo.Name = "lblConstTo";
            this.lblConstTo.Size = new System.Drawing.Size(34, 25);
            this.lblConstTo.TabIndex = 21;
            this.lblConstTo.Tag = "";
            this.lblConstTo.Text = "To: ";
            this.lblConstTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRandomConsTo
            // 
            this.txtRandomConsTo.Location = new System.Drawing.Point(130, 69);
            this.txtRandomConsTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRandomConsTo.Name = "txtRandomConsTo";
            this.txtRandomConsTo.Size = new System.Drawing.Size(49, 26);
            this.txtRandomConsTo.TabIndex = 20;
            this.txtRandomConsTo.Text = "1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioIsParallel);
            this.groupBox3.Controls.Add(this.radioSingleCore);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(902, 15);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(225, 154);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Type of procesors";
            // 
            // radioIsParallel
            // 
            this.radioIsParallel.AutoSize = true;
            this.radioIsParallel.Checked = true;
            this.radioIsParallel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioIsParallel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioIsParallel.Location = new System.Drawing.Point(32, 98);
            this.radioIsParallel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioIsParallel.Name = "radioIsParallel";
            this.radioIsParallel.Size = new System.Drawing.Size(111, 24);
            this.radioIsParallel.TabIndex = 1;
            this.radioIsParallel.TabStop = true;
            this.radioIsParallel.Text = "Multy Core ";
            this.radioIsParallel.UseVisualStyleBackColor = true;
            // 
            // radioSingleCore
            // 
            this.radioSingleCore.AutoSize = true;
            this.radioSingleCore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioSingleCore.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioSingleCore.Location = new System.Drawing.Point(32, 49);
            this.radioSingleCore.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioSingleCore.Name = "radioSingleCore";
            this.radioSingleCore.Size = new System.Drawing.Size(114, 24);
            this.radioSingleCore.TabIndex = 0;
            this.radioSingleCore.Text = "Single Core";
            this.radioSingleCore.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtProbMutation);
            this.groupBox2.Controls.Add(this.txtProbCrossover);
            this.groupBox2.Controls.Add(this.txtProbReproduction);
            this.groupBox2.Controls.Add(this.lblPMutation);
            this.groupBox2.Controls.Add(this.lblPCrossover);
            this.groupBox2.Controls.Add(this.lblPReproduction);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(512, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(381, 154);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Probability of gp operations";
            // 
            // label10
            // 
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(285, 72);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 28);
            this.label10.TabIndex = 27;
            this.label10.Tag = "";
            this.label10.Text = " (0.0 -1.0)";
            // 
            // label11
            // 
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(285, 35);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 28);
            this.label11.TabIndex = 26;
            this.label11.Tag = "";
            this.label11.Text = " (0.0 -1.0)";
            // 
            // label15
            // 
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(285, 114);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 28);
            this.label15.TabIndex = 23;
            this.label15.Tag = "";
            this.label15.Text = " (0.0 -0.5)";
            // 
            // txtProbMutation
            // 
            this.txtProbMutation.Location = new System.Drawing.Point(156, 69);
            this.txtProbMutation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProbMutation.Name = "txtProbMutation";
            this.txtProbMutation.Size = new System.Drawing.Size(120, 26);
            this.txtProbMutation.TabIndex = 25;
            // 
            // txtProbCrossover
            // 
            this.txtProbCrossover.Location = new System.Drawing.Point(156, 31);
            this.txtProbCrossover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProbCrossover.Name = "txtProbCrossover";
            this.txtProbCrossover.Size = new System.Drawing.Size(120, 26);
            this.txtProbCrossover.TabIndex = 24;
            // 
            // txtProbReproduction
            // 
            this.txtProbReproduction.Location = new System.Drawing.Point(156, 111);
            this.txtProbReproduction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProbReproduction.Name = "txtProbReproduction";
            this.txtProbReproduction.Size = new System.Drawing.Size(120, 26);
            this.txtProbReproduction.TabIndex = 23;
            // 
            // lblPMutation
            // 
            this.lblPMutation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPMutation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPMutation.Location = new System.Drawing.Point(12, 72);
            this.lblPMutation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPMutation.Name = "lblPMutation";
            this.lblPMutation.Size = new System.Drawing.Size(117, 28);
            this.lblPMutation.TabIndex = 13;
            this.lblPMutation.Tag = "";
            this.lblPMutation.Text = "Mutation:";
            this.lblPMutation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPCrossover
            // 
            this.lblPCrossover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPCrossover.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPCrossover.Location = new System.Drawing.Point(12, 35);
            this.lblPCrossover.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPCrossover.Name = "lblPCrossover";
            this.lblPCrossover.Size = new System.Drawing.Size(117, 28);
            this.lblPCrossover.TabIndex = 12;
            this.lblPCrossover.Tag = "";
            this.lblPCrossover.Text = "Crossover:";
            this.lblPCrossover.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPReproduction
            // 
            this.lblPReproduction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPReproduction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPReproduction.Location = new System.Drawing.Point(12, 114);
            this.lblPReproduction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPReproduction.Name = "lblPReproduction";
            this.lblPReproduction.Size = new System.Drawing.Size(117, 28);
            this.lblPReproduction.TabIndex = 11;
            this.lblPReproduction.Tag = "";
            this.lblPReproduction.Text = "Reproduction:";
            this.lblPReproduction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOperationTreeDepth);
            this.groupBox1.Controls.Add(this.txtInitTreeDepth);
            this.groupBox1.Controls.Add(this.lblOMaximum);
            this.groupBox1.Controls.Add(this.lblIMaximum);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(21, 346);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(369, 145);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tree structure level";
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(285, 91);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 28);
            this.label7.TabIndex = 26;
            this.label7.Tag = "";
            this.label7.Text = " (3-17)";
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(285, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 28);
            this.label4.TabIndex = 23;
            this.label4.Tag = "";
            this.label4.Text = " (3-17)";
            // 
            // txtOperationTreeDepth
            // 
            this.txtOperationTreeDepth.Location = new System.Drawing.Point(219, 86);
            this.txtOperationTreeDepth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOperationTreeDepth.Name = "txtOperationTreeDepth";
            this.txtOperationTreeDepth.Size = new System.Drawing.Size(56, 26);
            this.txtOperationTreeDepth.TabIndex = 24;
            this.txtOperationTreeDepth.Text = "7";
            // 
            // txtInitTreeDepth
            // 
            this.txtInitTreeDepth.Location = new System.Drawing.Point(219, 34);
            this.txtInitTreeDepth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInitTreeDepth.Name = "txtInitTreeDepth";
            this.txtInitTreeDepth.Size = new System.Drawing.Size(56, 26);
            this.txtInitTreeDepth.TabIndex = 23;
            this.txtInitTreeDepth.Text = "4";
            // 
            // lblOMaximum
            // 
            this.lblOMaximum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOMaximum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOMaximum.Location = new System.Drawing.Point(4, 85);
            this.lblOMaximum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOMaximum.Name = "lblOMaximum";
            this.lblOMaximum.Size = new System.Drawing.Size(207, 28);
            this.lblOMaximum.TabIndex = 12;
            this.lblOMaximum.Tag = "";
            this.lblOMaximum.Text = "Maximum operation level:";
            this.lblOMaximum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIMaximum
            // 
            this.lblIMaximum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblIMaximum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblIMaximum.Location = new System.Drawing.Point(4, 32);
            this.lblIMaximum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIMaximum.Name = "lblIMaximum";
            this.lblIMaximum.Size = new System.Drawing.Size(207, 28);
            this.lblIMaximum.TabIndex = 11;
            this.lblIMaximum.Tag = "";
            this.lblIMaximum.Text = "Maximum initial level:";
            this.lblIMaximum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.lblInitialTries);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtBroodSize);
            this.groupBox5.Controls.Add(this.lblBroodSize);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox5.Location = new System.Drawing.Point(404, 346);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(314, 145);
            this.groupBox5.TabIndex = 45;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "GP improvements parameters";
            // 
            // label20
            // 
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(237, 92);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 28);
            this.label20.TabIndex = 25;
            this.label20.Tag = "";
            this.label20.Text = " (1-10)";
            this.label20.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(160, 89);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(68, 26);
            this.textBox1.TabIndex = 26;
            this.textBox1.Text = "5";
            this.textBox1.Visible = false;
            // 
            // lblInitialTries
            // 
            this.lblInitialTries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInitialTries.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInitialTries.Location = new System.Drawing.Point(12, 92);
            this.lblInitialTries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInitialTries.Name = "lblInitialTries";
            this.lblInitialTries.Size = new System.Drawing.Size(140, 28);
            this.lblInitialTries.TabIndex = 24;
            this.lblInitialTries.Tag = "";
            this.lblInitialTries.Text = "Initial tries:";
            this.lblInitialTries.Visible = false;
            // 
            // label9
            // 
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(237, 38);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 28);
            this.label9.TabIndex = 23;
            this.label9.Tag = "";
            this.label9.Text = " (2-10)";
            // 
            // txtBroodSize
            // 
            this.txtBroodSize.Location = new System.Drawing.Point(160, 34);
            this.txtBroodSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBroodSize.Name = "txtBroodSize";
            this.txtBroodSize.Size = new System.Drawing.Size(68, 26);
            this.txtBroodSize.TabIndex = 23;
            this.txtBroodSize.Text = "3";
            // 
            // lblBroodSize
            // 
            this.lblBroodSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBroodSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBroodSize.Location = new System.Drawing.Point(12, 38);
            this.lblBroodSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBroodSize.Name = "lblBroodSize";
            this.lblBroodSize.Size = new System.Drawing.Size(93, 28);
            this.lblBroodSize.TabIndex = 11;
            this.lblBroodSize.Tag = "";
            this.lblBroodSize.Text = "Brood size:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.eb_cutOff);
            this.groupBox7.Controls.Add(this.lblTreshold);
            this.groupBox7.Controls.Add(this.cb_rootNodeFunction);
            this.groupBox7.Controls.Add(this.lblRootNode);
            this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox7.Location = new System.Drawing.Point(724, 346);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox7.Size = new System.Drawing.Size(402, 145);
            this.groupBox7.TabIndex = 46;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Classification";
            // 
            // eb_cutOff
            // 
            this.eb_cutOff.Location = new System.Drawing.Point(108, 108);
            this.eb_cutOff.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_cutOff.Name = "eb_cutOff";
            this.eb_cutOff.Size = new System.Drawing.Size(80, 26);
            this.eb_cutOff.TabIndex = 35;
            this.eb_cutOff.Text = "0.5";
            // 
            // lblTreshold
            // 
            this.lblTreshold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTreshold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTreshold.Location = new System.Drawing.Point(8, 106);
            this.lblTreshold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTreshold.Name = "lblTreshold";
            this.lblTreshold.Size = new System.Drawing.Size(92, 28);
            this.lblTreshold.TabIndex = 34;
            this.lblTreshold.Tag = "";
            this.lblTreshold.Text = "Threshold:";
            this.lblTreshold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_rootNodeFunction
            // 
            this.cb_rootNodeFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_rootNodeFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_rootNodeFunction.Location = new System.Drawing.Point(9, 63);
            this.cb_rootNodeFunction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_rootNodeFunction.Name = "cb_rootNodeFunction";
            this.cb_rootNodeFunction.Size = new System.Drawing.Size(370, 28);
            this.cb_rootNodeFunction.TabIndex = 33;
            this.cb_rootNodeFunction.SelectedIndexChanged += new System.EventHandler(this.cb_rootNodeFunction_SelectedIndexChanged);
            // 
            // lblRootNode
            // 
            this.lblRootNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRootNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRootNode.Location = new System.Drawing.Point(12, 32);
            this.lblRootNode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRootNode.Name = "lblRootNode";
            this.lblRootNode.Size = new System.Drawing.Size(104, 28);
            this.lblRootNode.TabIndex = 11;
            this.lblRootNode.Tag = "";
            this.lblRootNode.Text = "Root node:";
            // 
            // ParametersPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ParametersPanel";
            this.Size = new System.Drawing.Size(1167, 505);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPSize;
        private System.Windows.Forms.TextBox txtPopSize;
        private System.Windows.Forms.ComboBox cmbFitnessFuncs;
        private System.Windows.Forms.ComboBox cmbInitMethods;
        private System.Windows.Forms.Label lblFitness;
        private System.Windows.Forms.Label lblInitialization;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lbSelParam1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblElitism;
        private System.Windows.Forms.TextBox txtSelParam1;
        private System.Windows.Forms.TextBox txtElitism;
        private System.Windows.Forms.ComboBox cmbSelectionMethods;
        private System.Windows.Forms.Label lblSelectionMethod;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtRandomConstNum;
        private System.Windows.Forms.Label lblConstFrom;
        private System.Windows.Forms.Label lblConsCount;
        private System.Windows.Forms.TextBox txtRandomConsFrom;
        private System.Windows.Forms.Label lblConstTo;
        private System.Windows.Forms.TextBox txtRandomConsTo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioIsParallel;
        private System.Windows.Forms.RadioButton radioSingleCore;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtProbMutation;
        private System.Windows.Forms.TextBox txtProbCrossover;
        private System.Windows.Forms.TextBox txtProbReproduction;
        private System.Windows.Forms.Label lblPMutation;
        private System.Windows.Forms.Label lblPCrossover;
        private System.Windows.Forms.Label lblPReproduction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOperationTreeDepth;
        private System.Windows.Forms.TextBox txtInitTreeDepth;
        private System.Windows.Forms.Label lblOMaximum;
        private System.Windows.Forms.Label lblIMaximum;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBroodSize;
        private System.Windows.Forms.Label lblBroodSize;
        private System.Windows.Forms.CheckBox isProtectedOperation;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cb_rootNodeFunction;
        private System.Windows.Forms.Label lblRootNode;
        private System.Windows.Forms.TextBox eb_cutOff;
        private System.Windows.Forms.Label lblTreshold;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblInitialTries;
    }
}
