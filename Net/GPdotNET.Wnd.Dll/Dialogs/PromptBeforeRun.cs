using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPdotNet.Wnd.Dll.Dialogs
{
    public partial class PromptBeforeRun : Form
    {
        int Option { get; set; }
        public int OptionResult { get; set; }
        public PromptBeforeRun(int option)
        {
            InitializeComponent();
            this.Icon = Extensions.LoadIconFromName("GPdotNet.Wnd.Dll.Images.gpdotnet.ico");
            this.FormClosing += PromptBeforeRun_FormClosing;
            if(option== 2)
            {
                radioButton1.Visible = false;
                radioButton2.Visible = false;
            }
            
         
        }

        private void PromptBeforeRun_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (radioButton1.Checked)
                OptionResult = 1;
            else if (radioButton2.Checked)
                OptionResult = 2;
            else if (radioButton3.Checked)
                OptionResult = 3;
            else if (radioButton1.Checked)
                OptionResult = 4;
        }
    }
}
