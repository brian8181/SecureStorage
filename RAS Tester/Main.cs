using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace RAS_Tester
{
    public partial class Main : Form
    {
        QuickRSA rsa = new QuickRSA();
        public Main()
        {
            InitializeComponent();
            keyfileBrowser.Label = "Path";
            inputfileBrowser.Label = "In";
            outfileBrowser.Label = "Out";
        }

        private void btnCreateKeys_Click(object sender, EventArgs e)
        {
            try
            {
                QuickRSA.CreateKeyPair(dirBrowser1.TextBox.Text, int.Parse( txtBits.Text) );
            }
            catch(Exception ex)
            {
                StdMsgBox.Error(ex.Message);
                return;
            }

            StdMsgBox.OK("Keys created.");
        }

        private void btnLoadKey_Click(object sender, EventArgs e)
        {
            rsa.LoadKey(keyfileBrowser.TextBox.Text);
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            byte[] data = File.ReadAllBytes(inputfileBrowser.TextBox.Text);
            rsa.Encrypt_RSA_AES(data);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            byte[] data = File.ReadAllBytes(outfileBrowser.TextBox.Text);
            rsa.Decrypt(data);
        }

        private void cbDecrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDecrypt.Checked)
            {
                btnEncrypt.Text = "Decrypt";
            }
            else
            {
                btnEncrypt.Text = "Encrypt";
            }
        }

     
    }
}
