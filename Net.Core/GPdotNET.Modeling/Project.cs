//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET - Genetic Programming Tool                                                  //
// Copyright 2006-2017 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the MIT License                                     //
// See license section of  http://github.com/bhrnjica/gpdotnet/blob/master/license.md  //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using GPdotNet.BasicTypes;
using GPdotNet.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Globalization;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json.Serialization;
using GPdotNet.Core;

namespace GPdotNet.Modeling
{

    /// <summary>
    /// Implements Project Class which contains experimental data, all defined models and project information
    /// </summary>
     [DataContract]
    public class Project
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Func<string> GetExperimentData { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Func<DataSet1> GetDataSet { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string Guid { get; set; }
        [DataMember]
        public string Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Data { get; set; }
        [DataMember]
        public DataSet1 DataSet { get; set; }
        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public string ProjectInfo { get; set; }
        [DataMember]
        public List<Model> Models { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public bool IsDirty { get; set; }

        public Project(string guid)
        {
            Guid = guid;
            Models = new List<Model>();
        }
        public void InitiNewProject(string name)
        {
            Name = name;
            FilePath = "[New GP Project]";
        }

        /// <summary>
        /// Create model from Console app
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="mName"></param>
        /// <param name="randomizeData"></param>
        public void CreateModel(string guid, string mName, bool randomizeData)
        {
            var dataset = DataSet.GetDataSet(randomizeData);
            var exp = new Data.Experiment(dataset);

            var name = Name;
            var classes = exp.GetColumnsFromOutput()[0].Statistics.Categories;
            var label = exp.GetColumnsFromOutput()[0].Name;
            var model = new Model(guid);

            //experiment
            model.ModelName = mName;
            model.Name = mName;
            //get data
            model.Project = this;
            model.DataSet = dataset;
            model.ExpData = exp;

            //init empty GPfactory based on default parameters and experiment
            model.InitNewModel();
            Models.Add(model);

        }

        public void Save(string strPath)
        {
             
            //JsonSerializerSettings sett = new JsonSerializerSettings();
            //sett.NullValueHandling = NullValueHandling.Ignore;
            var str = JsonSerializer.Serialize(this);
            
            System.IO.File.WriteAllText(strPath, str);
        }

        public static Project Open(string filePath)
        {
            try
            {
                //JsonSerializerSettings sett = new JsonSerializerSettings();
                //sett.NullValueHandling = NullValueHandling.Ignore;

                var strJson = System.IO.File.ReadAllText(filePath);
                var obj = JsonSerializer.Deserialize<IDictionary<string, object>>(strJson);
                var d = obj["DataSet"];
                var mods = obj["Models"];
                var name = obj["Name"].ToString();
                var projectInfo = obj["ProjectInfo"];


                //create NET Project object from Json
                var guid = System.Guid.NewGuid().ToString();
                var project = new Project(guid);
                project.Name = (string)name;

                project.DataSet = JsonSerializer.Deserialize<DataSet1>(d.ToString());
                project.FilePath = filePath;
                project.ProjectInfo = projectInfo.ToString();

                //
                //de-serialize models
                Model[] mod = JsonSerializer.Deserialize<Model[]>(mods.ToString());// mods.ToObject<Model[]>();
                foreach (var m in mod)
                {
                    var g = System.Guid.NewGuid().ToString();
                    var exp = new Data.Experiment(m.DataSet);

                    var classes = exp.GetColumnsFromOutput()[0].Statistics.Categories;
                    var label = exp.GetColumnsFromOutput()[0].Name;

                    var mm = new Model(g);
                    mm.Factory = m.Factory;

                    Chromosome ch = mm.Factory.Population.Chromosomes[0] as Chromosome;
                    if( ch != null )
                        mm.Factory.ProgresReport.BestSolution= ch;    

                    mm.Name = m.Name;
                    mm.DataSet = m.DataSet;
                    mm.ExpData = exp;
                    
                    //mm.SetParent(pController);
                    mm.InitPersistedModel();
                    //project.Models.Add(mm);
                    project.Models.Add(mm);
                }

                return project;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}