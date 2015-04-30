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
        private ClientCloud client_cloud = new ClientCloud();
        private const int MAX_SIZE = 30000; //bytes

        public MainFrm()
        {
            InitializeComponent();
            client_cloud.LoadKey("c:\\tmp\\aes_key\\key");

            //test count
            int c = client_cloud.GetCount();
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            //todo
            client_cloud.Initialize(@"c:\tmp\infiles", @"c:\tmp\outfiles");
            StdMsgBox.OK("Initialize Complete");

            //string[] files =  Directory.GetFiles("C:\\tmp\client");


            //InitializeFrm dlg = new InitializeFrm();
            //dlg.dirBrowser.TextBox.Text = Properties.Settings.Default.init_dir;
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    //todo
            //    client_cloud.Initialize(@"c:\tmp\infiles", @"c:\tmp\outfiles");

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
                client_cloud.LoadKey(dlg.dirBrowserKeyDir.TextBox.Text.TrimEnd('\\') + "\\key");
                Properties.Settings.Default.server_dir = dlg.dirBrowserServerDir.TextBox.Text;
                Properties.Settings.Default.local_dir = dlg.dirBrowserLocalDir.TextBox.Text;
                Properties.Settings.Default.key_path = dlg.dirBrowserKeyDir.TextBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void btnGetDirs_Click(object sender, EventArgs e)
        {
            GetDirectories();
        }

        public void GetDirectories()
        {
            string dir = Properties.Settings.Default.server_dir;
            XmlNodeList files = client_cloud.GetDirectories();

            serverfileList.Items.Clear();
            // add to fileList
            foreach (XmlNode n in files)
            {
                string name = n["name"].InnerText;  
                serverfileList.Items.Add(name);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int max = (int)Math.Pow(2, 16); 

            CreateFrm dlg = new CreateFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                
                byte[] data = File.ReadAllBytes(dlg.fileBrowser.TextBox.Text);
                if (data.Length < MAX_SIZE)
                {
                    client_cloud.Create(Path.GetFileName(dlg.fileBrowser.TextBox.Text), data);
                    GetDirectories();
                }
                else
                {
                    client_cloud.Create(Path.GetFileName(dlg.fileBrowser.TextBox.Text), data, true);
                    GetDirectories();

                }
            }
            
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string path = (string)serverfileList.SelectedItem;
            string name = Path.GetFileName(path);
            
            if (name != null)
            {
                byte[] data = null;
               // int len = (int)client_cloud.GetLength(name);
                data = client_cloud.Read(name, true);
                File.WriteAllBytes("c:\\tmp\\client\\" + name, data);
            }
            StdMsgBox.OK("Read Complete");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = (string)serverfileList.SelectedItem;
            if(name != null)
            {
                client_cloud.Delete(name);
                GetDirectories();
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

        }
   }
}
