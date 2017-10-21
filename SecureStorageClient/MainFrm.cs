using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SecureStorageClient;
using SecureStorageLib;
using Utility;
using KeyStorage;
using System.Security.Cryptography;

namespace SecureStorageClient
{
    public partial class MainFrm : Form
    {
        private SecureStorage client_cloud = null;
        private string current_dir = "/";
        private readonly int FRAGMENT_SIZE;
        private readonly int MAX_SIZE;

        public MainFrm()
        {
            InitializeComponent();
            MAX_SIZE = Properties.Settings.Default.max_msg_size;
            FRAGMENT_SIZE = Properties.Settings.Default.fragment_size;
            
            
            string key_loc = Properties.Settings.Default.key_loc.TrimEnd('\\') + "\\keystore";
            //BKP SecureStorageUtility.LoadKey(key_loc, AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
            if (!File.Exists(key_loc))
            {
                //throw new Exception("No key @ " + "\"" + key_loc + "\"");
                MessageBox.Show("Key not found @ " + key_loc, "No key found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // load key store from path
            string pass = Properties.Settings.Default.keystore_pass;
            KeyStore store = new KeyStore(key_loc, pass);
            // get first key; just so happens to be an AES key
            byte[] key = store[0];


            //client_cloud = new SecureStorage(new WCFStorage(), new SecureStorageLib.AES( key ), FRAGMENT_SIZE);
            
            //TODO - use new abstract classes
            client_cloud = new SecureStorage(new WCFStorage(), new CryptographyLib.Cryptography<AesCryptoServiceProvider>(key, 256), FRAGMENT_SIZE);
            
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

        //private void btnInitialize_Click(object sender, EventArgs e)
        //{
        //    InitializeFrm dlg = new InitializeFrm();
        //    dlg.dirBrowser.TextBox.Text = Properties.Settings.Default.init_input_dir;
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        string output_dir = Properties.Settings.Default.server_mirror_dir;

        //        //delete all file server directory
        //        if (Directory.Exists(output_dir))
        //            Directory.Delete(output_dir, true);
        //        Directory.CreateDirectory(output_dir);

        //        // intitialize to server mirror dir
        //        string initial_input_dir = dlg.dirBrowser.TextBox.Text;
        //        SecureStorageUtility.InitializeLocalRoot(initial_input_dir, output_dir, key, iv);
        //        // save setting for next time
        //        Properties.Settings.Default.init_input_dir = initial_input_dir;
        //        Properties.Settings.Default.Save();
        //    }

        //    StdMsgBox.OK("InitializeLocalRoot Complete");
        //}

        private void btnCreateKey_Click(object sender, EventArgs e)
        {
            string keystore_loc = Properties.Settings.Default.key_loc.TrimEnd('\\') + "\\keystore.tmp";

            // old way, raw key, no keystore
            //SecureStorageUtility.GererateWriteKey(key_loc, 32);
            //StdMsgBox.OK("\"Key_new.tmp\" created.");

            byte[] key = SecureStorageUtility.GererateKey(32);
            KeyStore.CreateStore(keystore_loc, "abc", key);
            StdMsgBox.OK("\"keystore.tmp\" created.");
        }

        public void RefreshFileList()
        {
            //string seerver_dir = Properties.Settings.Default.output_dir;
            XmlNodeList files = client_cloud.GetNames(CurrentDirectory);
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
                bool is_directory = name.EndsWith("/");
                if (is_directory)
                    throw new SecureStorageException("Error, is a directory.");

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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string src_name = (string)serverfileList.SelectedItem;
            CopyFrm dlg = new CopyFrm(src_name);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                client_cloud.Copy(src_name, dlg.dstfileBrowser.TextBox.Text);
                RefreshFileList();
            }
          
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            string src_name = (string)serverfileList.SelectedItem;
            MoveFrm dlg = new MoveFrm(src_name);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                client_cloud.Move(src_name, dlg.dstfileBrowser.TextBox.Text);
                RefreshFileList();
            }
        }
    }
}
