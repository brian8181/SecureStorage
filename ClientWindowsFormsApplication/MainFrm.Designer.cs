namespace ClientWindowsFormsApplication
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param path="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.fileList = new System.Windows.Forms.ListBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCreateKey = new System.Windows.Forms.Button();
            this.cbSync = new System.Windows.Forms.CheckBox();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnGetDirs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fileList
            // 
            this.fileList.FormattingEnabled = true;
            this.fileList.Location = new System.Drawing.Point(3, 2);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(191, 355);
            this.fileList.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(200, 222);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(102, 23);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Create/Upload";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(200, 251);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(102, 23);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Read/Download";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(200, 280);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreateKey
            // 
            this.btnCreateKey.Location = new System.Drawing.Point(200, 62);
            this.btnCreateKey.Name = "btnCreateKey";
            this.btnCreateKey.Size = new System.Drawing.Size(102, 23);
            this.btnCreateKey.TabIndex = 4;
            this.btnCreateKey.Text = "Create Key";
            this.btnCreateKey.UseVisualStyleBackColor = true;
            this.btnCreateKey.Click += new System.EventHandler(this.btnCreateKey_Click);
            // 
            // cbSync
            // 
            this.cbSync.AutoSize = true;
            this.cbSync.Location = new System.Drawing.Point(228, 329);
            this.cbSync.Name = "cbSync";
            this.cbSync.Size = new System.Drawing.Size(62, 17);
            this.cbSync.TabIndex = 6;
            this.cbSync.Text = "syncing";
            this.cbSync.UseVisualStyleBackColor = true;
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(200, 5);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(102, 23);
            this.btnInitialize.TabIndex = 7;
            this.btnInitialize.Text = "Initialize";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(200, 33);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(102, 23);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnGetDirs
            // 
            this.btnGetDirs.Location = new System.Drawing.Point(200, 193);
            this.btnGetDirs.Name = "btnGetDirs";
            this.btnGetDirs.Size = new System.Drawing.Size(102, 23);
            this.btnGetDirs.TabIndex = 5;
            this.btnGetDirs.Text = "Get Directories";
            this.btnGetDirs.UseVisualStyleBackColor = true;
            this.btnGetDirs.Click += new System.EventHandler(this.btnGetDirs_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 358);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnInitialize);
            this.Controls.Add(this.cbSync);
            this.Controls.Add(this.btnGetDirs);
            this.Controls.Add(this.btnCreateKey);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.fileList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainFrm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CryptoCloud";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox fileList;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCreateKey;
        private System.Windows.Forms.CheckBox cbSync;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnGetDirs;
    }
}

