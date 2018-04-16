using GPdotNet.BasicTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace GPdotNet.Wnd.GUI
{
    public class TestPanelData
    {
        //prediction table
        public string[] Header { get; set; }
        public string[][] TestResult { get; set; }
        public string[] TestOutput { get; set; }
        public ColumnType OutputType { get; set; }
        public List<string> Classes { get; set; }
        public string Label { get; set; }

        //Zed Graph chart components
        public LineItem experimentalData { get; set; }
        public LineItem gpCalculateOutput { get; set; }
    }
}
