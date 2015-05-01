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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRootName = new System.Windows.Forms.TextBox();
            this.dirBrowserLocalDir = new Utility.GUI.DirBrowser();
            this.dirBrowserKeyDir = new Utility.GUI.DirBrowser();
            this.dirBrowserServerDir = new Utility.GUI.DirBrowser();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(261, 191);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(180, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Root";
            // 
            // txtRootName
            // 
            this.txtRootName.Location = new System.Drawing.Point(71, 33);
            this.txtRootName.Name = "txtRootName";
            this.txtRootName.Size = new System.Drawing.Size(229, 20);
            this.txtRootName.TabIndex = 5;
            // 
            // dirBrowserLocalDir
            // 
            this.dirBrowserLocalDir.Label = "Directory";
            this.dirBrowserLocalDir.Location = new System.Drawing.Point(12, 151);
            this.dirBrowserLocalDir.Name = "dirBrowserLocalDir";
            this.dirBrowserLocalDir.Size = new System.Drawing.Size(334, 34);
            this.dirBrowserLocalDir.TabIndex = 6;
            this.dirBrowserLocalDir.Load += new System.EventHandler(this.dirBrowserLocalDir_Load);
            // 
            // dirBrowserKeyDir
            // 
            this.dirBrowserKeyDir.Label = "Directory";
            this.dirBrowserKeyDir.Location = new System.Drawing.Point(12, 71);
            this.dirBrowserKeyDir.Name = "dirBrowserKeyDir";
            this.dirBrowserKeyDir.Size = new System.Drawing.Size(334, 34);
            this.dirBrowserKeyDir.TabIndex = 3;
            // 
            // dirBrowserServerDir
            // 
            this.dirBrowserServerDir.Label = "Directory";
            this.dirBrowserServerDir.Location = new System.Drawing.Point(12, 111);
            this.dirBrowserServerDir.Name = "dirBrowserServerDir";
            this.dirBrowserServerDir.Size = new System.Drawing.Size(334, 34);
            this.dirBrowserServerDir.TabIndex = 2;
            // 
            // SettingsFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(361, 235);
            this.Controls.Add(this.dirBrowserLocalDir);
            this.Controls.Add(this.txtRootName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dirBrowserKeyDir);
            this.Controls.Add(this.dirBrowserServerDir);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "SettingsFrm";
            this.Text = "SettingsFrmcs";
            this.Load += new System.EventHandler(this.SettingsFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public Utility.GUI.DirBrowser dirBrowserServerDir;
        public Utility.GUI.DirBrowser dirBrowserKeyDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRootName;
        public Utility.GUI.DirBrowser dirBrowserLocalDir;
    }
}