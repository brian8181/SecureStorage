namespace ClientWindowsFormsApplication
{
    partial class SettingsFrm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dirBrowserWD = new Utility.GUI.DirBrowser();
            this.dirBrowserKD = new Utility.GUI.DirBrowser();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(258, 93);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(177, 92);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dirBrowserWD
            // 
            this.dirBrowserWD.Label = "Directory";
            this.dirBrowserWD.Location = new System.Drawing.Point(12, 12);
            this.dirBrowserWD.Name = "dirBrowserWD";
            this.dirBrowserWD.Size = new System.Drawing.Size(334, 34);
            this.dirBrowserWD.TabIndex = 2;
            // 
            // dirBrowserKD
            // 
            this.dirBrowserKD.Label = "Directory";
            this.dirBrowserKD.Location = new System.Drawing.Point(12, 52);
            this.dirBrowserKD.Name = "dirBrowserKD";
            this.dirBrowserKD.Size = new System.Drawing.Size(334, 34);
            this.dirBrowserKD.TabIndex = 3;
            // 
            // SettingsFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(373, 128);
            this.Controls.Add(this.dirBrowserKD);
            this.Controls.Add(this.dirBrowserWD);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "SettingsFrm";
            this.Text = "SettingsFrmcs";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public Utility.GUI.DirBrowser dirBrowserWD;
        public Utility.GUI.DirBrowser dirBrowserKD;
    }
}