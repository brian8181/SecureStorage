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
using CryptographyLib;
using KeyStorage;
using SecureStorageLib;

namespace SecureStorageSyncClient
{
    public partial class ToolsFrm : Form
    {
        private SecureStorage client_cloud = null;
        private string current_dir = "/";
        private readonly int FRAGMENT_SIZE;
        private readonly int MAX_SIZE;
               

        public ToolsFrm()
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

            // obsolete
            //client_cloud = new SecureStorage(new WCFStorage(), new SecureStorageLib.AES( key ), FRAGMENT_SIZE);
            
            client_cloud = new SecureStorage(new WCFStorage(), new Cryptography<AesCryptoServiceProvider>(key, 256), FRAGMENT_SIZE);
            
            //lblSever.Text = current_dir;
        }

        private void btnFromCloud_Click(object sender, EventArgs e)
        {
            BuildFromCloud("/");
        }

        private void BuildFromCloud(string dir)
        {
             XmlNodeList files2 = client_cloud.GetFiles(dir);

            // add to fileList
            foreach (XmlNode n in files2)
            {
                string name = n["name"].InnerText;
                Read(name);
            }

            // interate directories
            XmlNodeList dirs = client_cloud.GetDirectories(dir);
            // add to dirList
            foreach (XmlNode n in dirs)
            {
                string name = n["name"].InnerText;
                BuildFromCloud(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        private void Read(string name)
        {
            if (name != null)
            {
                bool is_directory = name.EndsWith("/");
                if (is_directory)
                    return;

                byte[] data = null;
                // int len = (int)client_cloud.GetLength(name);
                data = client_cloud.Read(name);
                string dir_name = Path.GetDirectoryName(name);

                DirectoryInfo di = new DirectoryInfo(Properties.Settings.Default.local_folder + "\\" + dir_name);
                if (di.Exists != true)
                    di.Create();
                File.WriteAllBytes(di.FullName + "\\" + Path.GetFileName(name), data);
            }
        }

        private void btn_FromLocal_Click(object sender, EventArgs e)
        {

        }
    }
}
