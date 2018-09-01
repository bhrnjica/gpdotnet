namespace GPdotNet.Wnd.App
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Start Page");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.startPanel1 = new GPdotNet.Wnd.GUI.StartPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton2 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton3 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton4 = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonList1 = new System.Windows.Forms.RibbonButtonList();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.ribbonItemGroup1 = new System.Windows.Forms.RibbonItemGroup();
            this.ribbonDescriptionMenuItem1 = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton5 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton6 = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton7 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton8 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton9 = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton10 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton11 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.ribbonDescriptionMenuItem2 = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.ribbonDescriptionMenuItem3 = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.ribbonButton31 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton32 = new System.Windows.Forms.RibbonButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 196);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.startPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1870, 760);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 3;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageKey = "start.png";
            treeNode1.Name = "Node0";
            treeNode1.Tag = "startpage";
            treeNode1.Text = "Start Page";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(340, 760);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "start.png");
            this.imageList1.Images.SetKeyName(1, "experiment.png");
            this.imageList1.Images.SetKeyName(2, "model.png");
            this.imageList1.Images.SetKeyName(3, "runningmodel.png");
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1524, 760);
            this.tabControl2.TabIndex = 2;
            this.tabControl2.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage3.Size = new System.Drawing.Size(1508, 713);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Functions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage4.Size = new System.Drawing.Size(1508, 713);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Parameters";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage5.Location = new System.Drawing.Point(8, 39);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage5.Size = new System.Drawing.Size(1508, 713);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Run";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage6.Location = new System.Drawing.Point(8, 39);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage6.Size = new System.Drawing.Size(1508, 713);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Solution";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage7.Location = new System.Drawing.Point(8, 39);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage7.Size = new System.Drawing.Size(1508, 713);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "Validation";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1524, 760);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Size = new System.Drawing.Size(1508, 713);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Size = new System.Drawing.Size(1508, 713);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // startPanel1
            // 
            this.startPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.startPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPanel1.Location = new System.Drawing.Point(0, 0);
            this.startPanel1.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.startPanel1.Name = "startPanel1";
            this.startPanel1.New = null;
            this.startPanel1.Open = null;
            this.startPanel1.Size = new System.Drawing.Size(1524, 760);
            this.startPanel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 965);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1870, 37);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 32);
            this.toolStripStatusLabel1.Text = "Ready.";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1756, 32);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "No application message!";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ribbon1
            // 
            this.ribbon1.BackColor = System.Drawing.SystemColors.Control;
            this.ribbon1.CaptionBarVisible = false;
            this.ribbon1.CausesValidation = false;
            this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbon1.ForeColor = System.Drawing.Color.DarkViolet;
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 447);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon1.OrbVisible = false;
            // 
            // 
            // 
            this.ribbon1.QuickAccessToolbar.Text = "";
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Size = new System.Drawing.Size(1870, 196);
            this.ribbon1.TabIndex = 0;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(5, 2, 20, 0);
            this.ribbon1.TabSpacing = 4;
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add(this.ribbonPanel1);
            this.ribbonTab1.Panels.Add(this.ribbonPanel2);
            this.ribbonTab1.Panels.Add(this.ribbonPanel3);
            this.ribbonTab1.Panels.Add(this.ribbonPanel4);
            this.ribbonTab1.Text = "GPdotNET v5.1";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ButtonMoreEnabled = false;
            this.ribbonPanel1.ButtonMoreVisible = false;
            this.ribbonPanel1.Items.Add(this.ribbonButton2);
            this.ribbonPanel1.Items.Add(this.ribbonButton3);
            this.ribbonPanel1.Items.Add(this.ribbonButton31);
            this.ribbonPanel1.Items.Add(this.ribbonButton32);
            this.ribbonPanel1.Items.Add(this.ribbonButton4);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "Standard";
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.AltKey = "N";
            this.ribbonButton2.Image = global::GPdotNet.Wnd.App.Properties.Resources.newgp16;
            this.ribbonButton2.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.newgp16;
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.newgp24;
            this.ribbonButton2.Text = "New";
            this.ribbonButton2.ToolTip = "Creates new GPdotNET project.";
            this.ribbonButton2.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.newgp16;
            this.ribbonButton2.ToolTipTitle = "New GPdotNET project (Alt+N)";
            this.ribbonButton2.Click += new System.EventHandler(this.New_Click);
            // 
            // ribbonButton3
            // 
            this.ribbonButton3.AltKey = "O";
            this.ribbonButton3.Image = global::GPdotNet.Wnd.App.Properties.Resources.opengp16;
            this.ribbonButton3.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.opengp16;
            this.ribbonButton3.Name = "ribbonButton3";
            this.ribbonButton3.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.opengp24;
            this.ribbonButton3.Text = "Open";
            this.ribbonButton3.ToolTip = "Use this command to open already saved *.gpa file types.";
            this.ribbonButton3.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.opengp16;
            this.ribbonButton3.ToolTipTitle = "Open existing GPdotNET project (Alt+O)";
            this.ribbonButton3.Click += new System.EventHandler(this.Open_Click);
            // 
            // ribbonButton4
            // 
            this.ribbonButton4.AltKey = "C";
            this.ribbonButton4.DropDownItems.Add(this.ribbonButtonList1);
            this.ribbonButton4.DropDownItems.Add(this.ribbonSeparator1);
            this.ribbonButton4.DropDownItems.Add(this.ribbonItemGroup1);
            this.ribbonButton4.DropDownItems.Add(this.ribbonDescriptionMenuItem1);
            this.ribbonButton4.Image = global::GPdotNet.Wnd.App.Properties.Resources.closegp16;
            this.ribbonButton4.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.closegp16;
            this.ribbonButton4.Name = "ribbonButton4";
            this.ribbonButton4.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.closegp24;
            this.ribbonButton4.Text = "Close";
            this.ribbonButton4.ToolTip = "Use this command to close selected project.";
            this.ribbonButton4.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.closegp16;
            this.ribbonButton4.ToolTipTitle = "Close currently selected GPdotNET project (Alt+C)";
            this.ribbonButton4.Click += new System.EventHandler(this.Close_Click);
            // 
            // ribbonButtonList1
            // 
            this.ribbonButtonList1.ButtonsSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.ribbonButtonList1.FlowToBottom = false;
            this.ribbonButtonList1.ItemsSizeInDropwDownMode = new System.Drawing.Size(7, 5);
            this.ribbonButtonList1.Name = "ribbonButtonList1";
            this.ribbonButtonList1.Text = "ribbonButtonList1";
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            this.ribbonSeparator1.Text = "";
            // 
            // ribbonItemGroup1
            // 
            this.ribbonItemGroup1.Name = "ribbonItemGroup1";
            this.ribbonItemGroup1.Text = "ribbonItemGroup1";
            // 
            // ribbonDescriptionMenuItem1
            // 
            this.ribbonDescriptionMenuItem1.DescriptionBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ribbonDescriptionMenuItem1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonDescriptionMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.Image")));
            this.ribbonDescriptionMenuItem1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.LargeImage")));
            this.ribbonDescriptionMenuItem1.Name = "ribbonDescriptionMenuItem1";
            this.ribbonDescriptionMenuItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.SmallImage")));
            this.ribbonDescriptionMenuItem1.Text = "ribbonDescriptionMenuItem1";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ButtonMoreEnabled = false;
            this.ribbonPanel2.ButtonMoreVisible = false;
            this.ribbonPanel2.Items.Add(this.ribbonButton5);
            this.ribbonPanel2.Items.Add(this.ribbonButton6);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "Simulation";
            // 
            // ribbonButton5
            // 
            this.ribbonButton5.AltKey = "P";
            this.ribbonButton5.Image = global::GPdotNet.Wnd.App.Properties.Resources.runmodel16;
            this.ribbonButton5.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.runmodel16;
            this.ribbonButton5.Name = "ribbonButton5";
            this.ribbonButton5.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.runmodel24;
            this.ribbonButton5.Text = "Run";
            this.ribbonButton5.ToolTip = "After all GP parameters has been set, use this command to start GP search process" +
    ". In running mode, press this button one more time in order to stop running proc" +
    "ess";
            this.ribbonButton5.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.runmodel16;
            this.ribbonButton5.ToolTipTitle = "Start GP process of searching and building model (Alt+P)";
            this.ribbonButton5.Click += new System.EventHandler(this.Run_Click);
            // 
            // ribbonButton6
            // 
            this.ribbonButton6.AltKey = "T";
            this.ribbonButton6.Image = global::GPdotNet.Wnd.App.Properties.Resources.stopmodel16;
            this.ribbonButton6.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.stopmodel16;
            this.ribbonButton6.Name = "ribbonButton6";
            this.ribbonButton6.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.stopmodel24;
            this.ribbonButton6.Text = "Stop";
            this.ribbonButton6.ToolTip = "Use this command to stop GP running process.";
            this.ribbonButton6.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.closegp16;
            this.ribbonButton6.ToolTipTitle = "Stops the GP running process (Alt +T)";
            this.ribbonButton6.Click += new System.EventHandler(this.Stop_Click);
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ButtonMoreEnabled = false;
            this.ribbonPanel3.ButtonMoreVisible = false;
            this.ribbonPanel3.Items.Add(this.ribbonButton7);
            this.ribbonPanel3.Items.Add(this.ribbonButton8);
            this.ribbonPanel3.Items.Add(this.ribbonButton9);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "Export";
            // 
            // ribbonButton7
            // 
            this.ribbonButton7.AltKey = "E";
            this.ribbonButton7.Image = global::GPdotNet.Wnd.App.Properties.Resources.excel16;
            this.ribbonButton7.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.excel16;
            this.ribbonButton7.Name = "ribbonButton7";
            this.ribbonButton7.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.stopmodel24;
            this.ribbonButton7.Text = "Excel";
            this.ribbonButton7.ToolTip = "Use this command to export selected GP model to Excel for further analysis.";
            this.ribbonButton7.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.excel16;
            this.ribbonButton7.ToolTipTitle = "Export selected model to Excel (Alt+E)";
            this.ribbonButton7.Click += new System.EventHandler(this.ExpExcel_Click);
            // 
            // ribbonButton8
            // 
            this.ribbonButton8.AltKey = "W";
            this.ribbonButton8.Image = global::GPdotNet.Wnd.App.Properties.Resources.mathematica16;
            this.ribbonButton8.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.mathematica16;
            this.ribbonButton8.Name = "ribbonButton8";
            this.ribbonButton8.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.mathematica24;
            this.ribbonButton8.Text = "WM";
            this.ribbonButton8.ToolTip = "Use this command to export selected GP model to Wolfram Mathematica code for furt" +
    "her analysis.";
            this.ribbonButton8.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.mathematica16;
            this.ribbonButton8.ToolTipTitle = "Export selected GP model in to Mathematica code (Alt+W)";
            this.ribbonButton8.Click += new System.EventHandler(this.ExpMathem_Click);
            // 
            // ribbonButton9
            // 
            this.ribbonButton9.AltKey = "R";
            this.ribbonButton9.Image = global::GPdotNet.Wnd.App.Properties.Resources.rlanguage16;
            this.ribbonButton9.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.rlanguage16;
            this.ribbonButton9.Name = "ribbonButton9";
            this.ribbonButton9.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.rlanguage24;
            this.ribbonButton9.Text = "R";
            this.ribbonButton9.ToolTip = "Use this command to export selected GP model in R language for further analysis.";
            this.ribbonButton9.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.rlanguage16;
            this.ribbonButton9.ToolTipTitle = "Export selected GP model in to R language (Alt+R)";
            this.ribbonButton9.Click += new System.EventHandler(this.ExpR_Click);
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.ButtonMoreEnabled = false;
            this.ribbonPanel4.ButtonMoreVisible = false;
            this.ribbonPanel4.Items.Add(this.ribbonButton10);
            this.ribbonPanel4.Items.Add(this.ribbonButton11);
            this.ribbonPanel4.Name = "ribbonPanel4";
            this.ribbonPanel4.Text = "Help";
            // 
            // ribbonButton10
            // 
            this.ribbonButton10.AltKey = "A";
            this.ribbonButton10.Image = global::GPdotNet.Wnd.App.Properties.Resources.about16;
            this.ribbonButton10.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.about16;
            this.ribbonButton10.Name = "ribbonButton10";
            this.ribbonButton10.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.about24;
            this.ribbonButton10.Text = "About";
            this.ribbonButton10.ToolTip = "Use this command to see GPdotNET owner and license details.";
            this.ribbonButton10.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.about16;
            this.ribbonButton10.ToolTipTitle = "About GPdotNET (Alt+A)";
            this.ribbonButton10.Click += new System.EventHandler(this.About_Click);
            // 
            // ribbonButton11
            // 
            this.ribbonButton11.AltKey = "X";
            this.ribbonButton11.Image = global::GPdotNet.Wnd.App.Properties.Resources.exit16;
            this.ribbonButton11.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.exit16;
            this.ribbonButton11.Name = "ribbonButton11";
            this.ribbonButton11.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.exit24;
            this.ribbonButton11.Text = "Exit";
            this.ribbonButton11.ToolTip = "While GP process is running, it is not recommended to exit the application.";
            this.ribbonButton11.ToolTipImage = global::GPdotNet.Wnd.App.Properties.Resources.excel16;
            this.ribbonButton11.ToolTipTitle = "Exit GPdotNET (Alt+X)";
            this.ribbonButton11.Click += new System.EventHandler(this.Exit_Click);
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.LargeImage")));
            this.ribbonButton1.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "ribbonButton1";
            // 
            // ribbonDescriptionMenuItem2
            // 
            this.ribbonDescriptionMenuItem2.DescriptionBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ribbonDescriptionMenuItem2.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonDescriptionMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem2.Image")));
            this.ribbonDescriptionMenuItem2.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem2.LargeImage")));
            this.ribbonDescriptionMenuItem2.Name = "ribbonDescriptionMenuItem2";
            this.ribbonDescriptionMenuItem2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem2.SmallImage")));
            this.ribbonDescriptionMenuItem2.Text = "ribbonDescriptionMenuItem2";
            // 
            // ribbonDescriptionMenuItem3
            // 
            this.ribbonDescriptionMenuItem3.DescriptionBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ribbonDescriptionMenuItem3.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonDescriptionMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem3.Image")));
            this.ribbonDescriptionMenuItem3.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem3.LargeImage")));
            this.ribbonDescriptionMenuItem3.Name = "ribbonDescriptionMenuItem3";
            this.ribbonDescriptionMenuItem3.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem3.SmallImage")));
            this.ribbonDescriptionMenuItem3.Text = "ribbonDescriptionMenuItem3";
            // 
            // ribbonButton31
            // 
            this.ribbonButton31.AltKey = "S";
            this.ribbonButton31.Image = global::GPdotNet.Wnd.App.Properties.Resources.savegp16;
            this.ribbonButton31.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.savegp16;
            this.ribbonButton31.Name = "ribbonButton31";
            this.ribbonButton31.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.savegp24;
            this.ribbonButton31.Text = "Save";
            this.ribbonButton31.Click += new System.EventHandler(this.Save_Click);
            // 
            // ribbonButton32
            // 
            this.ribbonButton32.AltKey = "A";
            this.ribbonButton32.Image = global::GPdotNet.Wnd.App.Properties.Resources.savegp16;
            this.ribbonButton32.LargeImage = global::GPdotNet.Wnd.App.Properties.Resources.savegp16;
            this.ribbonButton32.Name = "ribbonButton32";
            this.ribbonButton32.SmallImage = global::GPdotNet.Wnd.App.Properties.Resources.savegp24;
            this.ribbonButton32.Text = "Save As";
            this.ribbonButton32.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1870, 1002);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ribbon1);
            this.ForeColor = System.Drawing.Color.DarkViolet;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1770, 1000);
            this.Name = "MainWindow";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonButton ribbonButton2;
        private System.Windows.Forms.RibbonButton ribbonButton3;
        private System.Windows.Forms.RibbonButton ribbonButton4;
        private System.Windows.Forms.RibbonButton ribbonButton5;
        private System.Windows.Forms.RibbonButton ribbonButton6;
        private System.Windows.Forms.RibbonButton ribbonButton7;
        private System.Windows.Forms.RibbonButton ribbonButton8;
        private System.Windows.Forms.RibbonButton ribbonButton9;
        private System.Windows.Forms.RibbonButton ribbonButton10;
        private System.Windows.Forms.RibbonButton ribbonButton11;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.RibbonButtonList ribbonButtonList1;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator1;
        private System.Windows.Forms.RibbonItemGroup ribbonItemGroup1;
        private System.Windows.Forms.RibbonDescriptionMenuItem ribbonDescriptionMenuItem1;
        private System.Windows.Forms.RibbonDescriptionMenuItem ribbonDescriptionMenuItem2;
        private System.Windows.Forms.RibbonDescriptionMenuItem ribbonDescriptionMenuItem3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;

        
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.RibbonButton ribbonButton31;
        private System.Windows.Forms.RibbonButton ribbonButton32;
    }
}

