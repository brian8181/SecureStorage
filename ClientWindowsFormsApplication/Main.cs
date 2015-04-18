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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

        }

        private void btnRead_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteFrm dlg = new DeleteFrm();
            dlg.ShowDialog();
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            
            InitializeFrm dlg = new InitializeFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Cloud.InitializeFileSystem(dlg.dirBrowser.TextBox.Text);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsFrm dlg = new SettingsFrm();
            dlg.ShowDialog();
        }


       
    }
}
