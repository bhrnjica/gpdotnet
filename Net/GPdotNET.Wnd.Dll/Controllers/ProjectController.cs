using GPdotNet.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPdotNet.Wnd.Dll
{
    public class ProjectController
    {
        public ProjectController(string gui)
        {
            Project = new Project(gui);
            Models = new List<ModelController>();
        }

        public Project Project { get; set; }
        public List<ModelController> Models { get; set; }

        public void InitiNewProject(string name)
        {
            Project.InitiNewProject(name);
        }

        public string GetGuid()
        {
            return Project.Guid;
        }

        public object FindModel(string guid)
        {
           foreach(var m in Models)
            {
                if (m.Model.Guid == guid)
                    return m;
            }
            return null;
        }

        public void CreateModel(string guid, string mName, bool randomizeData)
        {
            var dataset = Project.DataSet.GetDataSet(randomizeData);
            var exp = new Data.Experiment(dataset); 
            
            var name = Project.Name;
            var classes = exp.GetColumnsFromOutput()[0].Statistics.Categories;
            var label = exp.GetColumnsFromOutput()[0].Name;
            var model = new ModelController(guid, exp.GetOutputColumnType(), classes);

            //experiment
            model.Parent = this;
            model.Model.Name= mName;
            //get data
            model.Model.DataSet = dataset;
            model.Model.ExpData = exp;
            //init empty GPfactory based on default parameters and experiment
            model.InitNewModel();
            Project.Models.Add(model.Model);
            //add model-controller to parent experiment            
            Models.Add(model);


        }

        internal void PrepareForSave()
        {
            
        }

        public bool IsRunning()
        {
            foreach(var m in Models)
            {
                if (m.IsRunnig)
                    return true;

            }
            return false;
        }
    }
}
