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

using System.Runtime.Serialization;
using System.Globalization;
using static System.Math;
using System.Text.Json.Serialization;

namespace GPdotNet.Core
{
    public enum IterationStatus
    {
        Initialize,
        Runing,
        Stopped,
        Compleated,
        Continue,
        Exception
    }
    public enum InitializationMethod
    {
        Full = 0,
        Grow = 1,
        HalfHalf = 2
    }
    public enum SelectionMethod
    {
        // 
        FitnessProportionateSelection = 0,
        Rankselection = 1,
        TournamentSelection = 2,

    }


    [DataContract]
    public class Parameters : IParameters
    {
        [DataMember]
        public InitializationMethod InitializationMethod { get; set; }
        [DataMember]
        public SelectionMethod SelectionMethod { get; set; }
        [DataMember]
        public int PopulationSize { get; set; }
        [DataMember]
        public int MaxLevel { get; set; }
        [DataMember]
        public int MaxInitLevel { get; set; }
        [DataMember]
        public int BroodSize { get; set; }
        [DataMember]
        public float CrossoverProbability { get; set; }
        [DataMember]
        public float MutationProbability { get; set; }
        [DataMember]
        public float SelectionProbability { get; set; }
        [DataMember]
        public ColumnType OutputType { get; set; }
        [DataMember]
        public Function RootFunctionNode { get; set; }
        [DataMember]
        public string RootName { get; set; }//first 4 character of function name of root node
        [DataMember]
        public float Threshold { get; set; }//cutoff value for binary classification problems
        [DataMember]
        public bool ParallelProcessing { get; set; }
        [DataMember]
        public int Elitism { get; set; }
        //[DataMember]
        public int TourSize { get; set; }
        [DataMember]
        public float ArgValue { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IFitness FitnessFunction { get; set; }
        [DataMember]
        public float ConstFrom { get; set; }
        [DataMember]
        public float ConstTo { get; set; }
        [DataMember]
        public int ConstNum { get; set; }
        [DataMember]
        public double[] Constants { get; set; }
        [DataMember]
        public bool IsProtectedOperation { get; set; }
        [DataMember]
        public string FitnessName { get; set; }

        public override string ToString()
        {
            var protect = this.IsProtectedOperation ? 1 : 0;
            if (string.IsNullOrEmpty(RootName) && RootFunctionNode != null)
                RootName = RootFunctionNode.Name.Substring(0, 4);
            else if (string.IsNullOrEmpty(RootName))
                RootName = "None";
            //
            string str = $"{this.PopulationSize};{this.FitnessName};{(int)this.InitializationMethod};{this.MaxInitLevel};{this.MaxLevel};{this.Elitism}" +
                $";{protect};{(int)this.SelectionMethod};{this.ArgValue.ToString(CultureInfo.InvariantCulture)};{this.ConstFrom.ToString(CultureInfo.InvariantCulture)};{this.ConstTo.ToString(CultureInfo.InvariantCulture)};{this.ConstNum}" +
                $";{this.CrossoverProbability.ToString(CultureInfo.InvariantCulture)};{this.MutationProbability.ToString(CultureInfo.InvariantCulture)};{this.SelectionProbability.ToString(CultureInfo.InvariantCulture)};{this.BroodSize};{this.RootName.Substring(0,4)};{this.Threshold.ToString(CultureInfo.InvariantCulture)}";

            //if constance are not initialized make them
            if (this.Constants == null && this.ConstNum > 0)
                this.Constants = Globals.GenerateConstants(this.ConstFrom, this.ConstTo, this.ConstNum);
            //
            if (this.Constants != null)
            {
                //first add semicolon to separate last value from constants
                str += ";";
                foreach (var c in this.Constants)
                    str += $"{Round(c, 5).ToString(CultureInfo.InvariantCulture)};";
            }

            return str;
        }
        public static Parameters FromString(string strParams)
        {
            if (string.IsNullOrEmpty(strParams))
                return new Parameters();

            var pstr = strParams.Split(new char[] { ';' });
            var p = new Parameters();
            try
            {
                int temp = 0;
                float ftemp = 0;
                //PopSize
                if (!int.TryParse(pstr[0], out temp))
                    temp = 500;
                p.PopulationSize = temp;

                //Fitness
                p.FitnessName = pstr[1];

                ///Init method
                temp = 0;
                if (!int.TryParse(pstr[2], out temp))
                    temp = 0;
                p.InitializationMethod = (InitializationMethod)temp;

                //init depth
                temp = 0;
                if (!int.TryParse(pstr[3], out temp))
                    temp = 5;
                p.MaxInitLevel = temp;


                //operation depth
                temp = 0;
                if (!int.TryParse(pstr[4], out temp))
                    temp = 6;
                p.MaxLevel = temp;
                //txtOperationTreeDepth.Text = pstr[4];


                //Selection Elitism
                temp = 0;
                if (!int.TryParse(pstr[5], out temp))
                    temp = 1;
                p.Elitism = temp;
                //txtElitism.Text = pstr[5];

                //protected operation
                if (!int.TryParse(pstr[6], out temp))
                    temp = 0;
                p.IsProtectedOperation = temp == 0 ? false : true;


                //Selection method
                temp = 0;
                if (!int.TryParse(pstr[7], out temp))
                    temp = 0;
                p.SelectionMethod = (SelectionMethod)temp;

                //Selection param1
                ftemp = 0;
                if (!float.TryParse(pstr[8], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out ftemp))
                    ftemp = 0;
                p.ArgValue = ftemp;

                //Random constant from
                ftemp = 0;
                if (!float.TryParse(pstr[9], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out ftemp))
                    ftemp = 0;
                p.ConstFrom = ftemp;
                //txtRandomConsFrom.Text = pstr[9];

                //Random constant to
                ftemp = 0;
                if (!float.TryParse(pstr[10], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out ftemp))
                    ftemp = 0;
                p.ConstTo = ftemp;

                //Random constant count
                temp = 0;
                if (!int.TryParse(pstr[11], out temp))
                    temp = 0;
                p.ConstNum = temp;
                //txtRandomConstNum.Text = pstr[11];

                //Crossover method
                ftemp = 0;
                if (!float.TryParse(pstr[12], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out ftemp))
                    ftemp = 0.9f;
                p.CrossoverProbability = ftemp;

                //mutation method
                ftemp = 0;
                if (!float.TryParse(pstr[13], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out ftemp))
                    ftemp = 0;
                p.MutationProbability = ftemp;

                //reproduction method
                ftemp = 0;
                if (!float.TryParse(pstr[14], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out ftemp))
                    ftemp = 0;
                p.SelectionProbability = ftemp;

                //Brood size
                temp = 0;
                if (!int.TryParse(pstr[15], out temp))
                    temp = 2;
                p.BroodSize = temp;
                //txtBroodSize.Text = pstr[15];

                //root node function
                p.RootName = pstr[16];

                //cut off value
                ftemp = 0;
                if (!float.TryParse(pstr[17], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out ftemp))
                    ftemp = 0.5f;
                p.Threshold = ftemp;

                if (pstr.Length <= 18 + p.ConstNum)
                    return p;


                //create constants
                p.Constants = new double[p.ConstNum];
                for (int i = 0; i < p.ConstNum; i++)
                {
                    ftemp = 0;
                    if (!float.TryParse(pstr[18 + i], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out ftemp))
                        ftemp = 0;
                    p.Constants[i] = Round(ftemp,5);
                }

                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string,string> ToDictionary()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("InitializationMethod",$"{InitializationMethod}");
            dic.Add("SelectionMethod", $"{SelectionMethod}");
            dic.Add("PopulationSize", $"{PopulationSize}");
            dic.Add("MaxLevel", $"{MaxLevel}");
            dic.Add("MaxInitLevel", $"{MaxInitLevel}");
            dic.Add("BroodSize", $"{BroodSize}");
            dic.Add("CrossoverProbability", $"{CrossoverProbability.ToString(CultureInfo.InvariantCulture)}");
            dic.Add("MutationProbability", $"{MutationProbability.ToString(CultureInfo.InvariantCulture)}");
            dic.Add("SelectionProbability", $"{SelectionProbability.ToString(CultureInfo.InvariantCulture)}");
            dic.Add("OutputType", $"{OutputType}");
            dic.Add("RootName", $"{RootName}");
            dic.Add("Threshold", $"{Threshold.ToString(CultureInfo.InvariantCulture)}");
            dic.Add("ParallelProcessing", $"{ParallelProcessing}");
            dic.Add("Elitism", $"{Elitism}");
          //  dic.Add("TourSize", $"{TourSize}");
            dic.Add("ArgValue", $"{ArgValue.ToString(CultureInfo.InvariantCulture)}");
            dic.Add("ConstFrom", $"{ConstFrom.ToString(CultureInfo.InvariantCulture)}");
            dic.Add("ConstTo", $"{ConstTo.ToString(CultureInfo.InvariantCulture)}");
            dic.Add("ConstNum", $"{ConstNum}");
            dic.Add("IsProtectedOperation", $"{IsProtectedOperation}");
            dic.Add("FitnessName", $"{FitnessName}");
            //
            if(Constants!=null && Constants.Length==ConstNum)
            {
                for(int i=0; i< ConstNum; i++)
                {
                    var key = $"C{i}";
                    var value = $"{Constants[i].ToString(CultureInfo.InvariantCulture)}";
                    dic.Add(key, value);
                }
            }
            return dic;
        }

        public static Parameters FromDictionary(Dictionary<string, string> dic)
        {
            var par = new Parameters();
            par.InitializationMethod = dic["InitializationMethod"].ToEnum<InitializationMethod>();
            par.SelectionMethod = dic["SelectionMethod"].ToEnum<SelectionMethod>();
            par.PopulationSize = int.Parse(dic["PopulationSize"]);
            par.MaxLevel = int.Parse(dic["MaxLevel"]);
            par.MaxInitLevel = int.Parse(dic["MaxInitLevel"]);
            par.BroodSize = int.Parse(dic["BroodSize"]);
            par.CrossoverProbability = float.Parse(dic["CrossoverProbability"], CultureInfo.InvariantCulture);
            par.MutationProbability = float.Parse(dic["MutationProbability"], CultureInfo.InvariantCulture);
            par.SelectionProbability = float.Parse(dic["SelectionProbability"], CultureInfo.InvariantCulture);
            par.OutputType = dic["OutputType"].ToEnum<ColumnType>();
            par.Threshold = float.Parse(dic["Threshold"], CultureInfo.InvariantCulture);
            par.ParallelProcessing = dic["ParallelProcessing"].ToBool();
            par.Elitism = int.Parse(dic["Elitism"]);
          //  par.TourSize = int.Parse(dic["TourSize"]);
            par.ArgValue = float.Parse(dic["ArgValue"], CultureInfo.InvariantCulture);
            par.ConstFrom = float.Parse(dic["ConstFrom"], CultureInfo.InvariantCulture);
            par.ConstTo = float.Parse(dic["ConstTo"], CultureInfo.InvariantCulture);
            par.ConstNum = int.Parse(dic["ConstNum"]);
            par.IsProtectedOperation = dic["IsProtectedOperation"].ToBool();
            par.FitnessName  = dic["FitnessName"];
            par.RootName = dic["RootName"];
            //par.RootFunctionNode = rootNodeFromName(par.RootName, par.OutputType, par.Threshold, nClasses);
            //
            if(par.ConstNum>0)
            {
                par.Constants = new double[par.ConstNum];
                //
                for (int i = 0; i < par.ConstNum; i++)
                {
                    var key = $"C{i}";
                    if(dic.ContainsKey(key))
                    {
                        var s = dic[key];
                        par.Constants[i] = double.Parse(s, CultureInfo.InvariantCulture);
                    }
                        
                }
                return par;
            }

            return par;
        }

        

        //Properties which are not persisted
        public int FeatureCount { get; set; }
        public bool IsMultipleOutput { get; set; }
        //public int MiniBatch { get ; set; }
    }
}