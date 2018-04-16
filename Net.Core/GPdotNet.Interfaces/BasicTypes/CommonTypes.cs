using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GPdotNet.BasicTypes
{
    //Type of the column data
    public enum ColumnType
    {
        Unknown = 0,
        Numeric,//continuous column values
        Binary,//binary column values (0,1)
        Category,//categorical column values    
    }
    //Types of categorical column  encoding 
    public enum CategoryEncoding
    {
        None,
        Level,
        [Description("1 out of N")]
        OnevsAll,
        [Description("1 out of N-1")]
        OnevsAll_1,
    }

    //Scaling of the numerical column
    public enum Scaling
    {
        MinMax = 0,
        Gauss,
        None
    }

    //Parameter type
    public enum ParameterType
    {
        Input, //-treat column as input parameter or feature
        Output, // - treat column as output value or label
        Ignored, // ignore columns in modeling
    }

    public enum MissingValue
    {
        Ignore,//remove the row from the experiment
        Average,//recalculate the column and put average value in all missing rows
        Mode,//recalculate the column and put most frequent value in all missing rows
        Random,//recalculate the column and put most random value in all missing rows
        Max,//recalculate the column and put Max value in all missing rows
        Min //recalculate the column and put Min value in all missing rows
    }

    public enum EncodeType
    {
        Default,
        Excel,
        Mathematica,
        RLanguage,

    }
    //Class for defining Column metadata
    public class MetaColumn
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Encoding { get; set; }//encoding category column only
        public string Param { get; set; }
        public string Scale { get; set; }
        public string MissingValue { get; set; }

        public bool IsIngored
        {
            get
            {
                return (Type.IndexOf("string",System.StringComparison.OrdinalIgnoreCase)>=0 || Param.Contains(ParameterType.Ignored.ToString()));
            }
        }

        public override string ToString()
        {
            string retVal = "";
            retVal += Id.ToString() + ";";
            retVal += Index.ToString() + ";";
            retVal += Name + ";";
            retVal += Type + ";";
            retVal += Param + ";";
            retVal += Scale + ";";
            retVal += MissingValue;

            return retVal;
        }

    }

    //Statistic for Column
    public class Statistics
    {
        public double Mean;
        public double Median;
        public double Mode;
        public double Random;
        public double Range;
        public double Min;
        public double Max;
        public double StdDev;
        public List<string> Categories;
    }

    /// <summary>
    /// Class holding full information about data set including 
    /// metadata and values in original format
    /// </summary>
    public class DataSet1
    {
        public MetaColumn[] MetaData { get; set; }
        public string[][] Data { get; set; }
        public int TestRows { get; set; }
        public bool IsPrecentige { get; set; }   
        
        public DataSet1 GetDataSet(bool randomizeData)
        {
            var ds = new DataSet1();
            var ls = new List<MetaColumn>();
            for(int i=0; i< MetaData.Length; i++)
            {
                if (MetaData[i].IsIngored)
                    continue;
                else
                {
                    var c = new MetaColumn();
                    c.Id = MetaData[i].Id;
                    c.Index = MetaData[i].Index;
                    c.MissingValue = MetaData[i].MissingValue;
                    c.Name = MetaData[i].Name;
                    c.Param = MetaData[i].Param;
                    c.Scale = MetaData[i].Scale;
                    c.Type = MetaData[i].Type;
                    c.Encoding = MetaData[i].Encoding;
                    ls.Add(c);
                }
               
            }

            //
            ds.MetaData = ls.ToArray();
            ds.IsPrecentige = IsPrecentige;
            ds.TestRows = TestRows;

            //
            string[][] data = new string[this.Data.Length][];
            for (int i = 0; i < this.Data.Length; i++)
            {
                data[i] = new string[ds.MetaData.Length];
                for (int j = 0; j < ds.MetaData.Length; j++)
                {
                    data[i][j] = this.Data[i][ds.MetaData[j].Index];
                }
            }

            if (randomizeData)
            {
                var rnd = new Random((int)DateTime.UtcNow.Ticks);
                var data1 = data.ToList<string[]>();
                var res1 = data1.OrderBy(row => rnd.Next());
                var res2 = res1.OrderBy(row => rnd.Next());
                data = res2.ToArray();
            }

            ds.Data = data;
            return ds;

        }
    }

}
