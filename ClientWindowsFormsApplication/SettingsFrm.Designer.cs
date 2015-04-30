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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRootName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(258, 149);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(177, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dirBrowserWD
            // 
            this.dirBrowserWD.Label = "Directory";
            this.dirBrowserWD.Location = new System.Drawing.Point(12, 68);
            this.dirBrowserWD.Name = "dirBrowserWD";
            this.dirBrowserWD.Size = new System.Drawing.Size(334, 34);
            this.dirBrowserWD.TabIndex = 2;
            // 
            // dirBrowserKD
            // 
            this.dirBrowserKD.Label = "Directory";
            this.dirBrowserKD.Location = new System.Drawing.Point(12, 108);
            this.dirBrowserKD.Name = "dirBrowserKD";
            this.dirBrowserKD.Size = new System.Drawing.Size(334, 34);
            this.dirBrowserKD.TabIndex = 3;
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
            // SettingsFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(394, 215);
            this.Controls.Add(this.txtRootName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dirBrowserKD);
            this.Controls.Add(this.dirBrowserWD);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "SettingsFrm";
            this.Text = "SettingsFrmcs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public Utility.GUI.DirBrowser dirBrowserWD;
        public Utility.GUI.DirBrowser dirBrowserKD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRootName;
    }
}