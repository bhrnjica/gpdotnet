using GPdotNet.BasicTypes;
using GPdotNet.Modeling;
using GPdotNet.Wnd.GUI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPdotNet.Wnd.Dll
{
    public class AppController
    {
        public AppController()
        {
            Projects = new List<ProjectController>();
        }

        public Object ActiveView { get; set; }
        public List<ProjectController> Projects { get; set; }
        public Action<Exception> ReportException { get; set; }

        /// <summary>
        /// Status message appear on Status bar
        /// </summary>
        string m_StatusMessage = "No application message.";
        public string StatusMessage
        {
            get
            {
                return m_StatusMessage;
            }
            set
            {
                m_StatusMessage = value;
               // RaisePropertyChangedEvent("StatusMessage");
            }
        }

        /// <summary>
        /// Status message appear on Status bar
        /// </summary>
        string m_AppStatus = " Ready!";
        public string AppStatus
        {
            get
            {
                return m_AppStatus;
            }
            set
            {
                m_AppStatus = value;
                //("AppStatus");
            }
        }

        public object GetView(string guid)
        {
            if (guid == "startpage")
                return null;
            foreach (var p in Projects)
            {
                if (p.GetGuid().Equals(guid))
                    return p;
                else
                {
                    var m = p.FindModel(guid);
                    if (m != null)
                        return m;
                }

            }

            return null;
        }

        /// <summary>
        /// Check if the current state of project is different from the persisted version
        /// Also Update ActualData Vie with values from GUI
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="funPanel1"></param>
        /// <param name="parPanel1"></param>
        /// <param name="runPanel1"></param>
        /// <param name="resPanel1"></param>
        /// <param name="testPanel1"></param>
        /// <returns></returns>
        public bool IsModified(ProjectController exp, FunctionPanel funPanel1, ParametersPanel parPanel1, RunPanel runPanel1, ResultPanel resPanel1, TestPanel testPanel1)
        {
            var models = exp.Models;
            if(exp.Project.IsDirty)
                return true;
            //
            foreach(var m in models)
            {
                //active model can contains unsaved data
                if(ActiveView is ModelController)
                {
                    var mm = (ModelController)ActiveView;
                    if (mm.Model.Guid == m.Model.Guid)
                        m.getCurrentValues(funPanel1, parPanel1, runPanel1, resPanel1, testPanel1);
                }
                //check for modification
                if (m.IsModified())
                    return true;
            }
            return false;
        }
        public void NewProject(string tag, string name)
        {
            try
            {
                ProjectController p = new ProjectController(tag);
                p.InitiNewProject(name);
                Projects.Add(p);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public ProjectController OpenProject(string filePath)
        {
            try
            {

                //JsonSerializerSettings sett = new JsonSerializerSettings();
                //sett.NullValueHandling = NullValueHandling.Ignore;

                //var strJson = System.IO.File.ReadAllText(filePath);
                //var obj = JsonConvert.DeserializeObject(strJson, sett);
                //var d = ((dynamic)obj)["DataSet"] as Newtonsoft.Json.Linq.JObject;
                //var mods = ((dynamic)obj)["Models"] as Newtonsoft.Json.Linq.JArray;
                //var name = ((dynamic)obj)["Name"] as Newtonsoft.Json.Linq.JValue;
                //var projectInfo = ((dynamic)obj)["ProjectInfo"] as Newtonsoft.Json.Linq.JValue;


                ////create NET Project object from Json
                //var guid = Guid.NewGuid().ToString();
                //var pController = new ProjectController(guid);
                //pController.Project.Name = (string)name;

                //pController.Project.DataSet = d.ToObject<DataSet1>();
                //pController.Project.FilePath = filePath;
                //pController.Project.ProjectInfo = (string)projectInfo;

                ////
                ////de-serialize models
                //Model[] mod = mods.ToObject<Model[]>();
                //foreach (var m in mod)
                //{
                //    var g = Guid.NewGuid().ToString();
                //    var exp = new Data.Experiment(m.DataSet);

                //    var classes = exp.GetColumnsFromOutput()[0].Statistics.Categories;
                //    var label = exp.GetColumnsFromOutput()[0].Name;

                //    var mm = new ModelController(g, exp.GetOutputColumnType(), classes, label);
                //    mm.Model.Factory = m.Factory;
                //    mm.Model.Name = m.Name;
                //    mm.Model.DataSet = m.DataSet;
                //    mm.Model.ExpData = exp;
                //    mm.SetParent(pController);
                //    mm.InitPersistedModel();
                //    pController.Project.Models.Add(mm.Model);
                //    pController.Models.Add(mm);
                //}

                var guid = Guid.NewGuid().ToString();
                var pController = new ProjectController(guid);
                var project = Project.Open(filePath);
                foreach(var m in project.Models)
                {
                    var exp = m.ExpData;
                    var g = Guid.NewGuid().ToString();
                    var classes = exp.GetColumnsFromOutput()[0].Statistics.Categories;
                    var label = exp.GetColumnsFromOutput()[0].Name;
                    
                    var mm = new ModelController(g, exp.GetOutputColumnType(), classes, label);
                    mm.Model = m;
                    mm.SetParent(pController);
                    mm.InitPersistedModel();
                    pController.Project.Models.Add(mm.Model);
                    pController.Models.Add(mm);
                }

                //add project to app controller
                pController.Project = project;
                Projects.Add(pController);
                return pController;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool SaveProject(string filePath, ProjectController pController)
        {
            try
            {
                pController.PrepareForSave();
                //
                Project project = pController.Project;
                //JsonSerializerSettings sett = new JsonSerializerSettings();
                //sett.NullValueHandling = NullValueHandling.Ignore;

                //set experiment filename
                project.FilePath = filePath;
                //only for active project panel update data from GUI
                if (ActiveView is ProjectController)
                {
                    var p = ((ProjectController)ActiveView).Project;
                    //update changes from experimental model
                    if (project.GetExperimentData != null && p.Guid == project.Guid)
                        project.Data = project.GetExperimentData();
                    //update changes from experimental model
                    if (project.GetDataSet != null && p.Guid == project.Guid)
                        project.DataSet = project.GetDataSet();
                }

                //projectInfo rich-text is already set to property 
                //
                for (int i= 0; i < project.Models.Count; i++)
                {
                    var mContr = pController.Models[i];
                    var m = project.Models[i];
                    m.PrepareForSave(mContr.ActiveData);
                }
                //
                var str = JsonSerializer.Serialize(project);
                System.IO.File.WriteAllText(filePath, str);
                project.IsDirty = false;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
           
        }


        public bool CloseProject(ProjectController exp)
        {
            Projects.Remove(exp);
            return true;
        }

        public string IsOpen(string filePath)
        {
            if (Projects == null)
                return "";

            if (Projects.Count == 0)
                return "";
            foreach(var p in Projects)
            {
                if (p.Project.FilePath == filePath)
                {
                   

                    return p.Project.Name;
                }

            }

            return "";
        }
    }
}
