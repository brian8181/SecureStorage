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
        private ClientCloud client_cloud = null;
        private const int MAX_SIZE = 30000; //bytes
        private const string LOCAL_PATH = "c:\\tmp\\client\\";
        private const string KEY_PATH = "c:\\tmp\\aes_key\\key";

        public MainFrm()
        {
            InitializeComponent();

            client_cloud = new ClientCloud();
            client_cloud.LoadKey(KEY_PATH);

            //test count
            int c = client_cloud.GetCount();
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            InitializeFrm dlg = new InitializeFrm();
            dlg.dirBrowser.TextBox.Text = Properties.Settings.Default.init_input_dir;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string server_dir = Properties.Settings.Default.server_dir;

                //delete all file server directory
                Directory.Delete(server_dir, true);
                Directory.CreateDirectory(server_dir);
                
                // intitialize to server mirror dir
                string initial_input_dir = dlg.dirBrowser.TextBox.Text;
                //client_cloud.Initialize(initial_input_dir, server_dir);
                client_cloud.InitRoot(initial_input_dir, server_dir);
                
                // save setting for next time
                Properties.Settings.Default.init_input_dir = initial_input_dir;
                Properties.Settings.Default.Save();
            }

            StdMsgBox.OK("Initialize Complete");
        }
        
        private void btnCreateKey_Click(object sender, EventArgs e)
        {
            StdMsgBox.OK("not implemented");
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

        public void RefreshFileList()
        {
            //string seerver_dir = Properties.Settings.Default.server_dir;
            XmlNodeList files = client_cloud.GetFiles();

            serverfileList.Items.Clear();
            // add to fileList
            foreach (XmlNode n in files)
            {
                string name = n["name"].InnerText;
                serverfileList.Items.Add(name);
            }

            // get local files
            localfileList.Items.Clear();
            string[] local_dirs = Directory.GetDirectories(Properties.Settings.Default.local_dir);
            foreach (string dir in local_dirs)
            {
                this.localfileList.Items.Add(Path.GetFileName(dir) + "/");
            }
            string[] local_files = Directory.GetFiles(Properties.Settings.Default.local_dir);
            foreach (string file in local_files)
            {
                this.localfileList.Items.Add(Path.GetFileName(file));
            }
        }

        private void btnGetDirs_Click(object sender, EventArgs e)
        {
            RefreshFileList();

           
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
             CreateFrm dlg = new CreateFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(dlg.fileBrowser.TextBox.Text);
                client_cloud.Create(Path.GetFileName(dlg.fileBrowser.TextBox.Text), data);
                RefreshFileList();
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
                File.WriteAllBytes(LOCAL_PATH + name, data);
            }

            RefreshFileList();
            StdMsgBox.OK("Read Complete");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = (string)serverfileList.SelectedItem;
            if(name != null)
            {
                client_cloud.Delete(name);
                RefreshFileList();
            }
        }

        private void btnCreateEmpty_Click(object sender, EventArgs e)
        {

            client_cloud.CreateEmptyFile("EMPTY", 1000, true);
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            StdMsgBox.OK("not implemented");
        }
   }
}
