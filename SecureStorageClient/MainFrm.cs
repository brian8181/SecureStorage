using System;
using System.IO;
using System.Text;
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
        private string current_dir = "/";
        private byte[] key = null;
        private byte[] iv = null;
        private readonly int FRAGMENT_SIZE;
        private readonly int MAX_SIZE;

        public MainFrm()
        {
            InitializeComponent();
            MAX_SIZE = Properties.Settings.Default.max_msg_size;
            FRAGMENT_SIZE = Properties.Settings.Default.fragment_size;
           
            string key_loc = Properties.Settings.Default.key_loc.TrimEnd('\\') + "\\key";
            SecureStorageUtility.LoadKey(key_loc, AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
            client_cloud = new SecureStorage(new WCFStorage(), new AES(key, iv), FRAGMENT_SIZE);
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
                if (Directory.Exists(output_dir))
                    Directory.Delete(output_dir, true);
                Directory.CreateDirectory(output_dir);

                // intitialize to server mirror dir
                string initial_input_dir = dlg.dirBrowser.TextBox.Text;
                SecureStorageUtility.InitializeLocalRoot(initial_input_dir, output_dir, key, iv);
                // save setting for next time
                Properties.Settings.Default.init_input_dir = initial_input_dir;
                Properties.Settings.Default.Save();
            }

            StdMsgBox.OK("InitializeLocalRoot Complete");
        }

        private void btnCreateKey_Click(object sender, EventArgs e)
        {
            string key_loc = Properties.Settings.Default.key_loc.TrimEnd('\\') + "\\key.tmp";
            SecureStorageUtility.CreateKey(key_loc, AES.KEY_SIZE, AES.IV_SIZE);
            StdMsgBox.OK("\"Key.tmp\" created.");
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
                client_cloud.CreateDirectory((CurrentDirectory + name).TrimStart('/'));
                RefreshFileList();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateFrm dlg = new CreateFrm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(dlg.fileBrowser.TextBox.Text);
                client_cloud.CreateFile((CurrentDirectory + Path.GetFileName(dlg.fileBrowser.TextBox.Text)).TrimStart('/'), data);
                RefreshFileList();
            }
        }

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
                if (name.EndsWith("/"))
                {
                    client_cloud.DeleteDirectory(name);
                }
                else
                {
                    client_cloud.DeleteFile(name);
                }
                RefreshFileList();
            }
        }

        private void btnCreateEmpty_Click(object sender, EventArgs e)
        {
            client_cloud.CreateEmptyFile("EMPTY", 1000, true);
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
    }
}
