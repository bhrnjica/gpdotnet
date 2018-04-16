using GPdotNet.BasicTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace GPdotNet.Wnd.GUI
{
    public class RunPanelData
    {
        public int TerminationType { get; set; }
        public float TerminationValue { get; set; }
        public int CurrentIteration { get; set; }
        public float BestFitness { get; set; }
        public int ChangedAtGeneration { get; set; }
        public ColumnType OutputType { get; set; }
        public List<string> Classes { get; set; }
        public string Label { get; set; }

        //Zed Graph chart components
        public LineItem experimentalData { get; set; }
        public LineItem gpCalculateOutput { get; set; }
        public LineItem maxFitness { get; set; }
        public LineItem avgFitness { get; set; }
    }

}
