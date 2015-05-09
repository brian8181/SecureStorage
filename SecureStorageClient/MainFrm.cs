using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using SecureStorageClient;
using SecureStorageLib;
using Utility;

namespace SecureStorageClient
{
    public partial class MainFrm : Form
    {
        private SecureStorage client_cloud = null;
        private CloudUtility client_utility = new CloudUtility(32, 16);
        private const int MAX_SIZE = 30000; //bytes
        //private const string LOCAL_PATH = "c:\\tmp\\client\\";
        private const string KEY_PATH = "c:\\tmp\\aes_key\\key";
        string current_dir = "/";
        private const int FRAGMENT_SIZE = 20000;

        public MainFrm()
        {
            InitializeComponent();
            
            client_utility.LoadKey(KEY_PATH);
            //bkp 
            client_cloud = new SecureStorage( new WCFStorage(), new CryptoAES(client_utility.Key, client_utility.IV), FRAGMENT_SIZE );
       
            lblSever.Text = current_dir;

         }

        private string CurrentDirectory
        {
            get
            {
                return current_dir;
            }
            set
            {
                current_dir = value;
                lblSever.Text = value;
            }
        }


        private void btnInitialize_Click(object sender, EventArgs e)
        {
            InitializeFrm dlg = new InitializeFrm();
            dlg.dirBrowser.TextBox.Text = Properties.Settings.Default.init_input_dir;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string output_dir = Properties.Settings.Default.server_mirror_dir;

                //delete all file server directory
                Directory.Delete(output_dir, true);
                Directory.CreateDirectory(output_dir);

                // intitialize to server mirror dir
                string initial_input_dir = dlg.dirBrowser.TextBox.Text;

                client_utility.InitializeLocalRoot(initial_input_dir, output_dir);

                // save setting for next time
                Properties.Settings.Default.init_input_dir = initial_input_dir;
                Properties.Settings.Default.Save();
            }

            StdMsgBox.OK("InitializeLocalRoot Complete");
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
                Properties.Settings.Default.server_mirror_dir = dlg.dirBrowserServerDir.TextBox.Text;
                Properties.Settings.Default.local_dir = dlg.dirBrowserLocalDir.TextBox.Text;
                Properties.Settings.Default.key_path = dlg.dirBrowserKeyDir.TextBox.Text;
                Properties.Settings.Default.Save();
            }
        }
        
        public void RefreshFileList()
        {
            //string seerver_dir = Properties.Settings.Default.output_dir;
            XmlNodeList files = client_cloud.GetFiles(CurrentDirectory);

            serverfileList.Items.Clear();
            // add to fileList
            foreach (XmlNode n in files)
            {
                string name = n["name"].InnerText;
                serverfileList.Items.Add(name);
            }
       }

        private void btnGetDirs_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void btnCreateDir_Click(object sender, EventArgs e)
        {
            CreateDirectoryFrm dlg = new CreateDirectoryFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string name = dlg.txtName.Text;
                name = name.EndsWith("/") ? name : name + "/";
                client_cloud.CreateName(CurrentDirectory + name, null);
                RefreshFileList();
            }
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateFrm dlg = new CreateFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(dlg.fileBrowser.TextBox.Text);
                client_cloud.CreateName(CurrentDirectory + Path.GetFileName(dlg.fileBrowser.TextBox.Text), data);
                RefreshFileList();
            }

        }

   
        //private void OnClick()
        //{
        //    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(CreateThread));
        //    t.Start();
        //}

        //private void CreateThread()
        //{
        //    CreateFrm dlg = new CreateFrm();
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        byte[] data = File.ReadAllBytes(dlg.fileBrowser.TextBox.Text);
        //        client_cloud.CreateName(CurrentDirectory + Path.GetFileName(dlg.fileBrowser.TextBox.Text), data);
        //        RefreshFileList();
        //    }

        //    UpdateStatus("");
        //}

        //string status;
        //private void UpdateStatus(string status)
        //{
        //    if (this.InvokeRequired) // invoke on gui thread
        //    {
        //        this.Invoke(new StringDelegate(UpdateStatus), new object[] { status });
        //        return;
        //    }
        //    this.status = status;
        //}


        private void btnRead_Click(object sender, EventArgs e)
        {
            string name = (string)serverfileList.SelectedItem;
            if (name != null)
            {
                byte[] data = null;
                // int len = (int)client_cloud.GetLength(name);
                data = client_cloud.Read(name);
                string dir_name = Path.GetDirectoryName(name);

                DirectoryInfo di = new DirectoryInfo(Properties.Settings.Default.local_dir + "\\" + dir_name);
                if (di.Exists != true)
                    di.Create();
                File.WriteAllBytes(di.FullName + "\\" + Path.GetFileName(name), data);
            }

            RefreshFileList();
            StdMsgBox.OK("Read Complete");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = (string)serverfileList.SelectedItem;
            if (name != null)
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

        private void btnUpDirectory_Click(object sender, EventArgs e)
        {
            string dir = current_dir.Trim('/');
            StringBuilder sb = new StringBuilder();

            if (current_dir != "/")
            {
                string[] dirs = dir.Split('/');
                for (int i = 0; i < (dirs.Length - 1); ++i)
                {
                    sb.Append(dirs[i] + "/");
                }

                CurrentDirectory = sb.ToString();
                if (string.IsNullOrWhiteSpace(current_dir))
                    CurrentDirectory = "/"; // nothing equals root

                RefreshFileList();
            }
        }

        private void btnInitialize_Click_1(object sender, EventArgs e)
        {
            client_cloud.Initialize();
            StdMsgBox.OK("Initialized.");

            CurrentDirectory = "/";

            RefreshFileList();
        }

        private void serverfileList_DoubleClick(object sender, EventArgs e)
        {
            if (serverfileList.SelectedItem != null)
            {
                string s = serverfileList.SelectedItem.ToString();

                if (s != "/")
                    s.TrimStart('/');

                if (s.EndsWith("/"))
                {
                    CurrentDirectory = s;
                    RefreshFileList();
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            StdMsgBox.OK("not implemented");
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            StdMsgBox.OK("not implemented");
        }
    }
}
