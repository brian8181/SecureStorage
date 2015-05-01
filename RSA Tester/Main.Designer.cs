namespace RAS_Tester
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnCreateKeys = new System.Windows.Forms.Button();
            this.btnLoadKey = new System.Windows.Forms.Button();
            this.cbDecrypt = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.fileBrowser1 = new utility.extra.FileBrowser();
            this.dirBrowser1 = new Utility.GUI.DirBrowser();
            this.outfileBrowser = new utility.extra.FileBrowser();
            this.inputfileBrowser = new utility.extra.FileBrowser();
            this.keyfileBrowser = new utility.extra.FileBrowser();
            this.txtBits = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(246, 315);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(110, 23);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnCreateKeys
            // 
            this.btnCreateKeys.Location = new System.Drawing.Point(281, 54);
            this.btnCreateKeys.Name = "btnCreateKeys";
            this.btnCreateKeys.Size = new System.Drawing.Size(75, 23);
            this.btnCreateKeys.TabIndex = 1;
            this.btnCreateKeys.Text = "CreateKeys";
            this.btnCreateKeys.UseVisualStyleBackColor = true;
            this.btnCreateKeys.Click += new System.EventHandler(this.btnCreateKeys_Click);
            // 
            // btnLoadKey
            // 
            this.btnLoadKey.Location = new System.Drawing.Point(281, 204);
            this.btnLoadKey.Name = "btnLoadKey";
            this.btnLoadKey.Size = new System.Drawing.Size(75, 23);
            this.btnLoadKey.TabIndex = 9;
            this.btnLoadKey.Text = "LoadKey";
            this.btnLoadKey.UseVisualStyleBackColor = true;
            this.btnLoadKey.Click += new System.EventHandler(this.btnLoadKey_Click);
            // 
            // cbDecrypt
            // 
            this.cbDecrypt.AutoSize = true;
            this.cbDecrypt.Location = new System.Drawing.Point(179, 319);
            this.cbDecrypt.Name = "cbDecrypt";
            this.cbDecrypt.Size = new System.Drawing.Size(61, 17);
            this.cbDecrypt.TabIndex = 15;
            this.cbDecrypt.Text = "decrypt";
            this.cbDecrypt.UseVisualStyleBackColor = true;
            this.cbDecrypt.CheckedChanged += new System.EventHandler(this.cbDecrypt_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(281, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "SaveKey";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(195, 127);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "public only";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // fileBrowser1
            // 
            this.fileBrowser1.Label = "File";
            this.fileBrowser1.Location = new System.Drawing.Point(22, 83);
            this.fileBrowser1.Name = "fileBrowser1";
            this.fileBrowser1.Size = new System.Drawing.Size(334, 34);
            this.fileBrowser1.TabIndex = 17;
            // 
            // dirBrowser1
            // 
            this.dirBrowser1.Label = "Directory";
            this.dirBrowser1.Location = new System.Drawing.Point(22, 14);
            this.dirBrowser1.Name = "dirBrowser1";
            this.dirBrowser1.Size = new System.Drawing.Size(334, 34);
            this.dirBrowser1.TabIndex = 13;
            // 
            // outfileBrowser
            // 
            this.outfileBrowser.Label = "File";
            this.outfileBrowser.Location = new System.Drawing.Point(22, 273);
            this.outfileBrowser.Name = "outfileBrowser";
            this.outfileBrowser.Size = new System.Drawing.Size(334, 34);
            this.outfileBrowser.TabIndex = 12;
            // 
            // inputfileBrowser
            // 
            this.inputfileBrowser.Label = "File";
            this.inputfileBrowser.Location = new System.Drawing.Point(22, 233);
            this.inputfileBrowser.Name = "inputfileBrowser";
            this.inputfileBrowser.Size = new System.Drawing.Size(334, 34);
            this.inputfileBrowser.TabIndex = 11;
            // 
            // keyfileBrowser
            // 
            this.keyfileBrowser.Label = "File";
            this.keyfileBrowser.Location = new System.Drawing.Point(22, 164);
            this.keyfileBrowser.Name = "keyfileBrowser";
            this.keyfileBrowser.Size = new System.Drawing.Size(334, 34);
            this.keyfileBrowser.TabIndex = 10;
            // 
            // txtBits
            // 
            this.txtBits.Location = new System.Drawing.Point(171, 54);
            this.txtBits.Name = "txtBits";
            this.txtBits.Size = new System.Drawing.Size(100, 20);
            this.txtBits.TabIndex = 19;
            this.txtBits.Text = "2048";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 378);
            this.Controls.Add(this.txtBits);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.fileBrowser1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbDecrypt);
            this.Controls.Add(this.dirBrowser1);
            this.Controls.Add(this.outfileBrowser);
            this.Controls.Add(this.inputfileBrowser);
            this.Controls.Add(this.keyfileBrowser);
            this.Controls.Add(this.btnLoadKey);
            this.Controls.Add(this.btnCreateKeys);
            this.Controls.Add(this.btnEncrypt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Text = " ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnCreateKeys;
        private System.Windows.Forms.Button btnLoadKey;
        private utility.extra.FileBrowser keyfileBrowser;
        private utility.extra.FileBrowser inputfileBrowser;
        private utility.extra.FileBrowser outfileBrowser;
        private System.Windows.Forms.CheckBox cbDecrypt;
        private Utility.GUI.DirBrowser dirBrowser1;
        private utility.extra.FileBrowser fileBrowser1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtBits;
    }
}

