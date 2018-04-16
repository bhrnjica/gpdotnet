using GPdotNet.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPdotNet.Wnd.GUI.TestApp
{
    public partial class DataLoaderWnd : Form
    {
        public DataLoaderWnd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var dataset = this.experimentPanel1.GetData();
                var exp = new Experiment(dataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                
            }
           
        }
    }
}
