using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ClientWindowsFormsApplication
{
    public partial class MainFrm : Form
    {
        Cloud cloud = new Cloud();
        public MainFrm()
        {
            InitializeComponent();
            cloud.LoadKey(Properties.Settings.Default.key_path.TrimEnd('\\') + "\\key");
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            //InitializeFrm dlg = new InitializeFrm();
            //dlg.dirBrowser.TextBox.Text = Properties.Settings.Default.init_dir;
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    cloud.InitializeFileSystem(dlg.dirBrowser.TextBox.Text);
            //    Properties.Settings.Default.init_dir = dlg.dirBrowser.TextBox.Text;
            //    Properties.Settings.Default.Save();
            //}
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsFrm dlg = new SettingsFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // load the key
                cloud.LoadKey(dlg.dirBrowserKD.TextBox.Text.TrimEnd('\\') + "\\key");
                Properties.Settings.Default.working_dir = dlg.dirBrowserWD.TextBox.Text;
                Properties.Settings.Default.key_path = dlg.dirBrowserKD.TextBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void btnGetDirs_Click(object sender, EventArgs e)
        {
            GetDirs();
            
        }

        public void GetDirs()
        {
            string dir = Properties.Settings.Default.working_dir;
            XmlNodeList files = cloud.GetDirectories();

            fileList.Items.Clear();
            // add to fileList
            foreach (XmlNode n in files)
            {
                string name = n["name"].InnerText;
                fileList.Items.Add(name);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateFrm dlg = new CreateFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(dlg.fileBrowser.TextBox.Text);
                cloud.Create(Path.GetFileName(dlg.fileBrowser.TextBox.Text), data);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = (string)fileList.SelectedItem;
            if(name != null)
            {
                cloud.Delete(name);
                GetDirs();
            }
        }
        

        private void btnCreateKey_Click(object sender, EventArgs e)
        {

        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

        }
      
    }
}
