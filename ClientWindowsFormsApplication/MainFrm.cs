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
using Utility;

namespace ClientWindowsFormsApplication
{
    public partial class MainFrm : Form
    {
        private ClientCloud cleint_cloud = new ClientCloud();


        public MainFrm()
        {
            InitializeComponent();
            cleint_cloud.LoadKey("c:\\tmp\\aes_key\\key");

            //test count
            int c = cleint_cloud.GetCount();
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            //todo
            cleint_cloud.Initialize(@"c:\tmp\infiles", @"c:\tmp\outfiles");
            StdMsgBox.OK("Initialize Complete");

            //InitializeFrm dlg = new InitializeFrm();
            //dlg.dirBrowser.TextBox.Text = Properties.Settings.Default.init_dir;
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    //todo
            //    cleint_cloud.Initialize(@"c:\tmp\infiles", @"c:\tmp\outfiles");

            //    Properties.Settings.Default.init_dir = dlg.dirBrowser.TextBox.Text;
            //    Properties.Settings.Default.Save();
            //}
        }
        
        private void btnCreateKey_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsFrm dlg = new SettingsFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // load the key
                cleint_cloud.LoadKey(dlg.dirBrowserKD.TextBox.Text.TrimEnd('\\') + "\\key");
                Properties.Settings.Default.working_dir = dlg.dirBrowserWD.TextBox.Text;
                Properties.Settings.Default.key_path = dlg.dirBrowserKD.TextBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void btnGetDirs_Click(object sender, EventArgs e)
        {
            GetDirectories();
        }

        public void GetDirectories()
        {
            string dir = Properties.Settings.Default.working_dir;
            XmlNodeList files = cleint_cloud.GetDirectories();

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
            int max = (int)Math.Pow(2, 16); 

            CreateFrm dlg = new CreateFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                
                byte[] data = File.ReadAllBytes(dlg.fileBrowser.TextBox.Text);
                if (data.Length < max)
                {
                    cleint_cloud.Create(Path.GetFileName(dlg.fileBrowser.TextBox.Text), data);
                    GetDirectories();
                }
                else
                {
                    cleint_cloud.Create(Path.GetFileName(dlg.fileBrowser.TextBox.Text), data, true);
                    GetDirectories();

                }
            }
            
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string name = (string)fileList.SelectedItem;
            
            if (name != null)
            {
                byte[] data = cleint_cloud.Read(name);
                File.WriteAllBytes("c:\\tmp\\client\\" + name, data);
            }
            StdMsgBox.OK("Read Complete");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = (string)fileList.SelectedItem;
            if(name != null)
            {
                cleint_cloud.Delete(name);
                GetDirectories();
            }
        }
   }
}
