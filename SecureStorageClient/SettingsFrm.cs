using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureStorageClient
{
    public partial class SettingsFrm : Form
    {
        public SettingsFrm()
        {
            InitializeComponent();

            dirBrowserKeyDir.Label = "Key";
            dirBrowserServerDir.Label = "Server";
            dirBrowserLocalDir.Label = "Local";


            txtRootName.Text = Properties.Settings.Default.root_name;
            dirBrowserServerDir.TextBox.Text = Properties.Settings.Default.server_mirror_dir;
            dirBrowserLocalDir.TextBox.Text = Properties.Settings.Default.local_dir;
            dirBrowserKeyDir.TextBox.Text = Properties.Settings.Default.key_path;
        }

        private void SettingsFrm_Load(object sender, EventArgs e)
        {

        }

        private void dirBrowserLocalDir_Load(object sender, EventArgs e)
        {

        }


    }
}
