using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindowsFormsApplication
{
    public partial class SettingsFrm : Form
    {
        public SettingsFrm()
        {
           InitializeComponent();

           dirBrowserKD.Label = "Key";
           dirBrowserWD.Label = "Work";


           txtRootName.Text = Properties.Settings.Default.root_name;     
           dirBrowserWD.TextBox.Text = Properties.Settings.Default.working_dir;
           dirBrowserKD.TextBox.Text = Properties.Settings.Default.key_path;
        }

       
    }
}
