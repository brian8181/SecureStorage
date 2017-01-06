using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SecureStorageLib;
using System.IO;
using KeyStorage;
using System.Xml;
using System.Security.Cryptography;

namespace SecureStorageSyncClient
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            SettingsFrm dlg = new SettingsFrm();
            dlg.ShowDialog();
        }

        private void bnt_Tools_Click(object sender, EventArgs e)
        {
            ToolsFrm dlg = new ToolsFrm();
            dlg.ShowDialog();
        }

        private void bnt_Sync_Click(object sender, EventArgs e)
        {
            // if sync data does not exist then create else sync
            ToolsFrm frm = new ToolsFrm();
        }
    }
}
