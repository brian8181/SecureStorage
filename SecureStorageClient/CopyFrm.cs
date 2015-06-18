using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureStorageClient
{
    public partial class CopyFrm : Form
    {
        public CopyFrm(string src_name)
        {
            InitializeComponent();
            srcfileBrowser.TextBox.Text = src_name;
        }

      
    }
}
