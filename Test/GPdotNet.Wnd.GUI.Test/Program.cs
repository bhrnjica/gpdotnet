using GPdotNet.TestProject;
using GPdotNet.Wnd.GUI.TestApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizationTestsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new VisualisationTests());
            Application.Run(new DataLoaderWnd());
        }
    }
}
