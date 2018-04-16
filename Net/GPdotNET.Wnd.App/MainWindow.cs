using GPdotNet.Data;
using GPdotNet.Interfaces;
using GPdotNet.Modeling;
using GPdotNet.Wnd.Dll;
using GPdotNet.Wnd.Dll.Dialogs;
using GPdotNet.Wnd.GUI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPdotNet.Wnd.App
{
    public partial class MainWindow : Form
    {
        private AppController Controller { get; set; }
        private StartPanel startPanel1;
        private ExperimentPanel expPanel1;
        private FunctionPanel funPanel1;
        private ParametersPanel parPanel1;
        private RunPanel runPanel1;
        private ResultPanel resPanel1;
        private TestPanel testPanel1;
        private InfoPanel infoPanel1;
        public MainWindow()
        {
            Controller = new AppController();
            Controller.ReportException = this.ReportException;
            InitializeComponent();
            this.Icon = Extensions.LoadIconFromName("GPdotNet.Wnd.Dll.Images.gpdotnet.ico");

            #region additional initialization
            this.expPanel1 = new ExperimentPanel();
            this.funPanel1 = new FunctionPanel();
            this.parPanel1 = new ParametersPanel();
            this.runPanel1 = new RunPanel();
            this.resPanel1 = new ResultPanel();
            this.testPanel1 = new TestPanel();
            this.infoPanel1 = new InfoPanel();
            // 
            // expPanel1
            // 
            this.expPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expPanel1.Location = new System.Drawing.Point(3, 3);
            this.expPanel1.Name = "expPanel1";
            this.expPanel1.Size = new System.Drawing.Size(463, 222);
            this.expPanel1.TabIndex = 0;
            // 
            // runPanel1
            // 
            this.infoPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel1.Location = new System.Drawing.Point(3, 3);
            this.infoPanel1.Name = "runPanel1";
            this.infoPanel1.Size = new System.Drawing.Size(463, 222);
            this.infoPanel1.TabIndex = 1;
            // 
            // funPanel1
            // 
            this.funPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.funPanel1.Location = new System.Drawing.Point(3, 3);
            this.funPanel1.Name = "funPanel1";
            this.funPanel1.Size = new System.Drawing.Size(463, 222);
            this.funPanel1.TabIndex = 0;
            // 
            // parPanel1
            // 
            this.parPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parPanel1.Location = new System.Drawing.Point(3, 3);
            this.parPanel1.Name = "parPanel1";
            this.parPanel1.Size = new System.Drawing.Size(463, 222);
            this.parPanel1.TabIndex = 0;
            // 
            // runPanel1
            // 
            this.runPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runPanel1.Location = new System.Drawing.Point(3, 3);
            this.runPanel1.Name = "runPanel1";
            this.runPanel1.Size = new System.Drawing.Size(463, 222);
            this.runPanel1.TabIndex = 0;
            // 
            // resPanel1
            // 
            this.resPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resPanel1.Location = new System.Drawing.Point(3, 3);
            this.resPanel1.Name = "resPanel1";
            this.resPanel1.Size = new System.Drawing.Size(463, 222);
            this.resPanel1.TabIndex = 0;
            // 
            // testPanel1
            // 
            this.testPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testPanel1.Location = new System.Drawing.Point(3, 3);
            this.testPanel1.Name = "testPanel1";
            this.testPanel1.Size = new System.Drawing.Size(463, 222);
            this.testPanel1.TabIndex = 0;
           
            //
            this.tabPage3.Controls.Add(funPanel1);
            this.tabPage4.Controls.Add(parPanel1);
            this.tabPage5.Controls.Add(runPanel1);
            this.tabPage6.Controls.Add(resPanel1);
            this.tabPage7.Controls.Add(testPanel1);
            this.tabPage1.Controls.Add(expPanel1);
            this.tabPage2.Controls.Add(infoPanel1);
            #endregion
            treeView1.LabelEdit = true;
            this.Load += MainWindow_Load;
            this.FormClosing += MainWindow_FormClosing;
            this.ribbonTab1.Text = AboutGPdotNET.AssemblyTitle;

            SetStopMode("");
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ribbonButton5.Checked)
            {
                if (MessageBox.Show("Model is running? By closing window you will lose all unsaved data.", "GPdotNET", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if (MessageBox.Show("Are you sure you want to Exit GPdotNET?", "GPdotNET", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

            treeView1.ExpandAll();
            treeView1.Select();
            startPanel1.Open = Open;
        }


        #region Handling View
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //get previously selected node to save state
            var n = treeView1.SelectedNode;
            if (n == null)
                return;
            //
            SaveView(n.Tag.ToString());
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //splitContainer1.Panel2
            if (e.Node.Text == "Start Page")
            {
                startPanel1.Visible = true;
                tabControl1.Visible = false;
                tabControl2.Visible = false;

            }
            else if (e.Node.Level == 0)
            {
                startPanel1.Visible = false;
                tabControl1.Visible = true;
                tabControl2.Visible = false;
            }
            else if (e.Node.Level == 1)
            {
                startPanel1.Visible = false;
                tabControl1.Visible = false;
                tabControl2.Visible = true;
            }
            else
            {
                startPanel1.Visible = true;
                tabControl1.Visible = false;
                tabControl2.Visible = false;
            }

            ShowView(e.Node.Tag.ToString());
        }

        private void ShowView(string guid)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var p = Controller.GetView(guid);
                this.Text = "Start Page";
                Controller.ActiveView = p;
                if (p == null)
                    return;
                else if (p is ProjectController)
                {
                    var pController = p as ProjectController;

                    //set new data on panel
                    if (pController.Project.DataSet == null)
                        expPanel1.ResetExperimentalPanel();
                    else
                        expPanel1.SetDataSet(pController.Project.DataSet);
                    
                    //project info
                    infoPanel1.InfoText = pController.Project.ProjectInfo;

                    //prepare callbacks for update model from GUI
                    expPanel1.CreateModel = CreateModel;
                    expPanel1.UpdateModel = UpdateModel;

                    //set windows title
                    this.Text = pController.Project.FilePath;
                }//show model on view
                else if (p is ModelController)
                {
                    var mController = p as ModelController;
                    mController.SetActiveData();
                    //function set
                    var funs = mController.GetFunctionset();
                    funPanel1.ResetFunctionSet();
                    funPanel1.FunctionSetFromString(funs);

                    //parameters
                    var gpType = mController.Model.ExpData.GetOutputColumnType();
                    var gpEncoding = mController.Model.ExpData.GetOutputColumnEncoding();
                    parPanel1.InitializeControls(gpType, gpEncoding);
                    //set parameters from active data
                    parPanel1.ParametersFromString(mController.GetParameters());

                    // GP run 
                    //set termination criteria is included
                    mController.SetRunPanel(mController.ActiveData.RunPanelData);
                    runPanel1.ActivatePanel(mController.ActiveData.RunPanelData);

                    //GP Solution
                    resPanel1.EvaluateResults = mController.Model.ModelEvaluation;
                    if (mController.Model.Factory.ProgresReport.BestSolution != null)
                    {
                        IChromosome sol = mController.Model.Factory.ProgresReport.BestSolution as IChromosome;
                        resPanel1.ReportResult(sol, mController.Model.Factory.Parameters);
                        
                    }
                    else
                    {
                        resPanel1.ReportResult(null,null);  
                    }

                    //GP Validation
                    mController.SetTestPanel(mController.ActiveData.TestPanelData);
                    testPanel1.ActivatePanel(mController.ActiveData.TestPanelData);

                    //set windows title
                    this.Text = mController.Parent.Project.FilePath;

                    mController.IsActive = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //back normal cursor
                Cursor.Current = Cursors.Default;
            }

        }

        private void SaveView(string guid)
        {
            try
            {
                var p = Controller.GetView(guid);

                if (p == null)
                    return;
                else if (p is ProjectController)
                {
                    Cursor.Current = Cursors.WaitCursor;


                    var pController = p as ProjectController;

                    //get latest data from gui  panel
                    pController.Project.DataSet = expPanel1.GetDataSet();

                    //get rich text project info
                    pController.Project.ProjectInfo = infoPanel1.InfoText;

                    //after the data is retrieved panel should be reset
                    expPanel1.ResetExperimentalPanel();

                }
                else if (p is ModelController)
                {
                    var mController = p as ModelController;
                    
                    //get functionSet
                    var funStr = funPanel1.FunctionSetToString();
                    mController.SetFunction(funStr);
                    //get parameters
                    var par = parPanel1.ParametersToString();
                    mController.SetParameters(par);

                    //save only termination criteria from run panel
                    runPanel1.ResetChart();
                    var tc = runPanel1.GetTerminationCriteria();
                    mController.SetTerminationCriteria(tc);

                    mController.IsActive = false;
                    //reset result panel
                    resPanel1.Reset();

                    //test panel
                    testPanel1.ResetChart();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //back normal cursor
                Cursor.Current = Cursors.Default;
            }

        }

        /// <summary>
        /// Central method for handling and showing all application exception
        /// </summary>
        /// <param name="ex"></param>
        void ReportException(Exception ex)
        {
            MessageBox.Show(ex.Message);

        }
        #endregion


        #region New, Open Save and Close Project
        public void Open(string filePath)
        {
            try
            {
                //this is coming from start panel
                if (string.IsNullOrEmpty(filePath))
                    filePath = PromptToOpenFile();

                if (string.IsNullOrEmpty(filePath))
                    return;
                var prj = Controller.IsOpen(filePath);
                if (prj != "")
                {
                    var node = getTreeItem(prj);
                    if(node != null)
                    {
                        treeView1.SelectedNode = node;
                        return;
                    }
                  
                }
                //create project in backed
                var p = Controller.OpenProject(filePath);
                
                //create tree structure
                treeView1.BeginUpdate();
                var n = createTreeNode(p.GetGuid(), p.Project.Name, 1);
                foreach(var m in p.Models)
                {
                    var nn = createTreeNode(m.Model.Guid, m.Model.Name, 2);
                    n.Nodes.Add(nn);
                }
                treeView1.Nodes.Add(n);
                n.ExpandAll();
                treeView1.SelectedNode = n;
                //
                return;
            }
            catch (Exception ex)
            {
                ReportException(ex);
                return;
            }
            finally
            {
                treeView1.EndUpdate();
            }
        }
        public void New()
        {

            TreeNode tn = createTreeNode(Guid.NewGuid().ToString(), $"Project{treeView1.Nodes.Count}", 1);
            treeView1.Nodes.Add(tn);

            //create project in backed
            Controller.NewProject(tn.Tag.ToString(), tn.Text);

            //then select project
            treeView1.SelectedNode = tn;
        }
        private void Save(ProjectController exp, string filePath)
        {
            try
            {
                //save experiment
                if (Controller.ActiveView is ProjectController)
                {
                    exp.Project.DataSet = expPanel1.GetDataSet();
                    exp.Project.ProjectInfo = infoPanel1.InfoText;
                    
                }
                else if (Controller.ActiveView is ModelController)
                    Controller.IsModified(exp, funPanel1, parPanel1, runPanel1, resPanel1, testPanel1);

                Controller.SaveProject(filePath, exp);

                //windows time should be changed when the path is changed
                this.Text = exp.Project.FilePath;
            }
            catch (Exception ex)
            {
               ReportException(ex);
            }
        }
        
        private void CloseProject()
        {
            try
            {
                //
                var exp = getActiveProject();
                if (exp == null)
                    return;
                if(exp.IsRunning())
                {
                    MessageBox.Show("Running model cannot be closed!");
                    return;
                }
                if (exp == null && treeView1.SelectedNode!=null && treeView1.SelectedNode.Text != "Start Page")
                {
                    treeView1.Nodes.Remove(treeView1.SelectedNode);
                    return;
                }


                DialogResult result = DialogResult.No;
                //
                if ((exp.Project.FilePath == "[New GP Project]"))
                {
                    result = MessageBox.Show("Would you like to save changes?", "GPdotNET", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Cancel)
                        return;
                    if(result == DialogResult.Yes)
                    {
                        string filePath = PromptToSaveFile();
                        if (string.IsNullOrEmpty(filePath))
                            return;
                        exp.Project.FilePath = filePath;
                        result = DialogResult.Yes;
                    }
                    
                }
                else if ( Controller.IsModified(exp, this.funPanel1, this.parPanel1, this.runPanel1, this.resPanel1, this.testPanel1))
                {
                    result = MessageBox.Show("Would you like to save changes?","GPdotNET", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Cancel)
                        return;
                }

                //
                if (result == DialogResult.Yes)
                    Controller.SaveProject(exp.Project.FilePath, exp);

                //close project
                if(Controller.CloseProject(exp))
                {
                    TreeNode fNode = null;
                    foreach (TreeNode nn1 in treeView1.Nodes)
                    {
                        if (nn1.Tag.Equals(exp.GetGuid()))
                        {
                            fNode = nn1;
                            break;
                        }
                        else
                            fNode = fromID(exp.GetGuid(), nn1);
                    }

                    //
                    treeView1.Nodes.Remove(fNode);

                    //expand tree item and select it
                }
            }
            catch (Exception ex)
            {

                ReportException(ex);
            }
        }

        private void Run()
        {
            try
            {
                if (modelRunnig() != null)
                {
                    return;
                }
                bool resetSolution = true;
                var m = getActiveModel();
                if (m != null && !m.IsRunnig)
                {
                    //new file not saved yet
                    if (m.Model.Factory.Population != null && !m.Model.Factory.Population.IsEmpty())
                    {
                        var retVal = handleRunAction(m);

                        if (!retVal.canRun)
                            return;

                        if (retVal.runOption == 1 || retVal.runOption == 3)
                            Save_Click(null, null);

                        resetSolution = (retVal.runOption == 1 || retVal.runOption == 3) ? true : false;

                    }

                    //
                    m.SetRunMode = SetRunMode;
                    m.SetStopMode = SetStopMode;
                    m.Run(resetSolution, this.funPanel1, this.parPanel1, this.runPanel1, this.resPanel1, this.testPanel1);

                }
                else if (m != null && m.IsRunnig)
                    m.Stop();
            }
            catch (Exception ex)
            {
                ReportException(ex);
                //throw;
            }
            
        }

        private ModelController modelRunnig()
        {
          foreach(var p in Controller.Projects)
            {
                var m = p.Models.Where(x => x.IsRunnig == true).FirstOrDefault();
                if ( m != null)
                    return m;
            }
            return null;
        }

        private (bool canRun,int runOption ) handleRunAction(ModelController mController)
        {
            Dll.Dialogs.PromptBeforeRun dlg;
            if (mController.IsModified())
            {
               dlg = new Dll.Dialogs.PromptBeforeRun(1);
            }
            else
            {
                dlg = new Dll.Dialogs.PromptBeforeRun(2);
            }
            //
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return (false, 0);
            else
                return (true, dlg.OptionResult);
 
        }

        private void Stop()
        {

            var mod = modelRunnig();
            if (mod == null)
                return;
            if (mod !=null && mod.IsRunnig)
               mod.Stop();
        }
        #endregion

        #region Model Creation and Updating
        void CreateModel(bool isRandomizedData)
        {
            if (Controller.ActiveView == null)
                return;
            ProjectController p = null;
            if (Controller.ActiveView is ProjectController)
            {
                p = (ProjectController)Controller.ActiveView;
            }
            else if (Controller.ActiveView is ModelController)
            {
                p = ((ModelController)Controller.ActiveView).Parent;
            }
            //
            CreateModel(p, isRandomizedData);
        }
        
        public void CreateModel(ProjectController project, bool randomizeData)
        {
            try
            {
                TreeNode fNode = null;
                foreach (TreeNode nn1 in treeView1.Nodes)
                {
                    if (nn1.Tag.Equals(project.GetGuid()))
                    {
                        fNode = nn1;
                        break;
                    }
                    else
                        fNode = fromID(project.GetGuid(), nn1);
                }

                //
                string guid = Guid.NewGuid().ToString();
                var modelName = $"Model{fNode.Nodes.Count}";
                //create model
                project.Project.DataSet = expPanel1.GetDataSet();
                project.CreateModel(guid, modelName, randomizeData);
                

                //expand tree item and select it
                var tn = createTreeNode(guid, modelName, 2);
                fNode.Nodes.Add(tn);
                tn.Expand();
                treeView1.SelectedNode = tn;
            }
            catch (Exception ex)
            {

                ReportException(ex);
            }
        }
        private void DeleteModel(TreeNode sn)
        {
            var m = getActiveModel();
            if(m!=null && m.Model.Guid == sn.Tag.ToString())
            {
                var result = MessageBox.Show($"Are you sure you want to delete {m.Model.Name} model?","GPdotNET" ,MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;

                var project = m.Parent;
                project.Models.Remove(m);
                //
                var p= project.Project.Models.Where(x=>x.Guid== m.Model.Guid).FirstOrDefault();
                if(p!=null)
                {
                    project.Project.Models.Remove(p);
                }
                project.Project.IsDirty = true;
                treeView1.Nodes.Remove(sn);
            }
        }
        private void RenameTreeItem(TreeNode sn, string newName)
        {
            if(sn.Level==0)
            {
                var p = getActiveProject();
                if (p != null && p.Project.Guid == sn.Tag.ToString())
                {
                    p.Project.Name = newName;
                    p.Project.IsDirty = true;
                   
                }
            }
            else if(sn.Level == 1)
            {
                var m = getActiveModel();
                if (m != null && m.Model.Guid == sn.Tag.ToString())
                {
                    m.Model.Name = newName;
                    m.Model.IsDiry = true;
                }
            }
            sn.Text = newName;

        }
        void UpdateModel(bool randomizeData)
        {
            if (MessageBox.Show("Once the model is updated, previous solution will be discarded.", "GPdotNET", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            DialogResult res = DialogResult.OK;
            var modelIndex = 0;
            var project = getActiveProject();
            if(project==null)
            {
                MessageBox.Show("Project is not selected.");
                return;
            }
            if(project.Models.Count==0)
            {
                MessageBox.Show("Select new project.");
                return;
            }
            else if (project.Models.Count > 1)
            {
                PromptForName dlg = new PromptForName();
                dlg.comboBox1.Items.AddRange(project.Models.Select(x=>x.Model.Name).ToArray());
                res = dlg.ShowDialog();
                modelIndex = dlg.comboBox1.SelectedIndex;
            }

            if(res== DialogResult.OK)
            {
                var model = project.Project.Models[modelIndex];

                var dataset = project.Project.DataSet.GetDataSet(randomizeData);
                var exp = new Data.Experiment(dataset);


                model.DataSet = dataset;
                model.ExpData = exp;
                
                model.ResetSolution();
                
            }
        }
        #endregion

        #region Helper
        //inline methods for 
        private TreeNode fromID(string itemId, TreeNode rootNode)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                if (node.Tag.Equals(itemId))
                    return node;
                TreeNode next = fromID(itemId, node);
                if (next != null)
                    return next;
            }
            return null;
        }
        private TreeNode getTreeItem(string projectName)
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Text.Equals(projectName))
                    return node;
            }
            return null;
        }
        private ProjectController getActiveProject()
        {
            var n = treeView1.SelectedNode;
            if (n == null)
            {
                MessageBox.Show("Select project before action.");
                return null;
            }

            ProjectController exp = null;

            var activeView = Controller.GetView(n.Tag.ToString());
            if (activeView is ModelController ee)
                exp = ee.Parent;
            else if (activeView is ProjectController)
                exp = (ProjectController)activeView;

            return exp;
        }
        private ModelController getActiveModel()
        {
            var n = treeView1.SelectedNode;
            if (n == null)
            {
                MessageBox.Show("Select model before action.");
                return null;
            }

            ModelController exp = null;

            var activeView = Controller.GetView(n.Tag.ToString());

            if (activeView is ModelController ee)
                return ee;
            else
            {
                MessageBox.Show("Select model before action.");
                return null;
            }
        }

        private TreeNode createTreeNode(string guid, string name, int image)
        {
            var n = new TreeNode();
            n.Text = name;
            n.Tag = guid;
            n.SelectedImageIndex = image;
            n.ImageIndex = image;
            return n;
        }
        private string PromptToOpenFile(string fileDescription = "GPdotNET standard file", string extension = "*.gpa")
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();

            if (string.IsNullOrEmpty(extension))
                dlg.Filter = "Plain text files (*.csv;*.txt;*.dat)|*.csv;*.txt;*.dat |All files (*.*)|*.*";
            else
                dlg.Filter = string.Format("{1} ({0})|{0}|All files (*.*)|*.*", extension, fileDescription);
            //
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return dlg.FileName;
            else
                return null;
        }
        private string PromptToSaveFile(string fileDescription = "GPdotNET standard file", string extension = "*.gpa")
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();

            if (string.IsNullOrEmpty(extension))
                dlg.Filter = "Plain text files (*.csv;*.txt;*.dat)|*.csv;*.txt;*.dat |All files (*.*)|*.*";
            else
                dlg.Filter = string.Format("{1} ({0})|{0}|All files (*.*)|*.*", extension, fileDescription);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return dlg.FileName;
            else
                return null;


        }
        public void SetStopMode(string guid)
        {
            setColorForGUI(false);
            var model = Controller.GetView(guid);
            if (model != null && model is ModelController)
            {
                ((ModelController)model).IsRunnig=false;
                if(((ModelController)model).CancelationSource!=null)
                    ((ModelController)model).CancelationSource.Cancel();
            }
               
            setImageIndexToRunningModel(guid, 2);
            ribbonButton5.Checked = false;

            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel2.Text = "No application message.";
            //AppStatus = "Ready";
            //StatusMessage = "No application message.";
        }

        private void setColorForGUI(bool isRunning)
        {
           
            var color1 = Color.BlueViolet;
            var color2 = Color.White;

            if (isRunning)
            {
                color1 = Color.Green;
                color2 = Color.White;
            }
               
            ribbon1.BackColor = color1;
            statusStrip1.BackColor = color1;
            startPanel1.ForeColor = Color.White;
            this.ForeColor = color2;
        }

        public void SetRunMode(string guid)
        {
            //AppStatus = "Running...";
            this.toolStripStatusLabel1.Text = "Running...";
            this.toolStripStatusLabel2.Text = "GPdotNET search process has been started! ";
            //StatusMessage = "GPdotNET search process has been started! ";

            ribbonButton5.Checked = !ribbonButton5.Checked;
            if (ribbonButton5.Checked == false)
                Stop();

            setImageIndexToRunningModel(guid, 3);

            setColorForGUI(true);
        }

        private void setImageIndexToRunningModel(string guid, int v)
        {
            TreeNode fNode = null;
            foreach (TreeNode nn1 in treeView1.Nodes)
            {
                if (nn1.Tag.Equals(guid))
                {
                    fNode = nn1;
                    break;
                }
                else
                    fNode = fromID(guid, nn1);
                if (fNode != null)
                    break;
            }
            if (fNode != null)
            {
                this.Invoke(
                   new Action(() =>
                   {
                       fNode.ImageIndex = v;
                       fNode.SelectedImageIndex = v;
                   }
                   ));
                
            }
        }
        #endregion

        #region command

        private void Open_Click(object sender, EventArgs e)
        {
            var str = PromptToOpenFile();
            if (!string.IsNullOrEmpty(str))
                Open(str);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var exp = getActiveProject();
            if (exp == null)
                return;

            string filePath = null;
            if (string.IsNullOrEmpty(exp.Project.FilePath) || exp.Project.FilePath == "[New GP Project]")
                filePath = PromptToSaveFile();
            else
                filePath = exp.Project.FilePath;

            if (string.IsNullOrEmpty(filePath))
                return;

            Save(exp, filePath);
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            var exp = getActiveProject();
            if (exp == null)
                return;

            string filePath = PromptToSaveFile();
            if (string.IsNullOrEmpty(filePath))
                return;

            Save(exp, filePath);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        private void New_Click(object sender, EventArgs e)
        {

            New();
        }
        private void Run_Click(object sender, EventArgs e)
        {

            Run();
        }

        private void Stop_Click(object sender, EventArgs e)
        {

            Stop();

        }
        private void ExpExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var model = getActiveModel();
                if (model == null)
                    return;

                var filepath = PromptToSaveFile("Microsoft Office Excel files", "*.xlsx");
                if (!string.IsNullOrEmpty(filepath))
                    model.ExportToExcel(filepath);
            }
            catch (Exception ex)
            {
                if (ex != null)
                 ReportException(ex);
            }
        }

        private void ExpMathem_Click(object sender, EventArgs e)
        {
            try
            {
                var model = getActiveModel();
                if (model == null)
                    return;

                var filepath = PromptToSaveFile("Wolfram Mathematica files", " *.nb");
                if (!string.IsNullOrEmpty(filepath))
                    model.ExportToWM(filepath);
            }
            catch (Exception ex)
            {
                if (ex != null)
                 ReportException(ex);
            }
        }

        private void ExpR_Click(object sender, EventArgs e)
        {
            try
            {
                var model = getActiveModel();
                if (model == null)
                    return;

                var filepath = PromptToSaveFile("R Language Script files", "*.r");
                if (!string.IsNullOrEmpty(filepath))
                    model.ExportToR(filepath);
            }
            catch (Exception ex)
            {
                if (ex != null)
                  ReportException(ex);
            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            var dlg = new AboutGPdotNET();
            dlg.ShowDialog();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                var sn = treeView1.SelectedNode;

                if (sn != null && sn.Level == 1 && !sn.IsEditing)
                {
                    DeleteModel(sn);
                }

            }

        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.CancelEdit || e.Node.Text == e.Label || e.Label==null)
                return;
            RenameTreeItem(e.Node, e.Label);
        }
        #endregion

    }
}
