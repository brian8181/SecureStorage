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
            this.serverfileList = new System.Windows.Forms.ListBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCreateKey = new System.Windows.Forms.Button();
            this.cbSync = new System.Windows.Forms.CheckBox();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnGetDirs = new System.Windows.Forms.Button();
            this.localfileList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateEmpty = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverfileList
            // 
            this.serverfileList.FormattingEnabled = true;
            this.serverfileList.Location = new System.Drawing.Point(3, 35);
            this.serverfileList.Name = "serverfileList";
            this.serverfileList.Size = new System.Drawing.Size(191, 355);
            this.serverfileList.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(397, 252);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(102, 23);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Create/Upload";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(397, 281);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(102, 23);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Read/Download";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(397, 310);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreateKey
            // 
            this.btnCreateKey.Location = new System.Drawing.Point(397, 92);
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
            this.cbSync.Location = new System.Drawing.Point(397, 339);
            this.cbSync.Name = "cbSync";
            this.cbSync.Size = new System.Drawing.Size(62, 17);
            this.cbSync.TabIndex = 6;
            this.cbSync.Text = "syncing";
            this.cbSync.UseVisualStyleBackColor = true;
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(397, 63);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(102, 23);
            this.btnInitialize.TabIndex = 7;
            this.btnInitialize.Text = "(Re)Initialize";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(397, 34);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(102, 23);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnGetDirs
            // 
            this.btnGetDirs.Location = new System.Drawing.Point(397, 223);
            this.btnGetDirs.Name = "btnGetDirs";
            this.btnGetDirs.Size = new System.Drawing.Size(102, 23);
            this.btnGetDirs.TabIndex = 5;
            this.btnGetDirs.Text = "Get Files List";
            this.btnGetDirs.UseVisualStyleBackColor = true;
            this.btnGetDirs.Click += new System.EventHandler(this.btnGetDirs_Click);
            // 
            // localfileList
            // 
            this.localfileList.FormattingEnabled = true;
            this.localfileList.Location = new System.Drawing.Point(200, 35);
            this.localfileList.Name = "localfileList";
            this.localfileList.Size = new System.Drawing.Size(191, 355);
            this.localfileList.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(197, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Local";
            // 
            // btnCreateEmpty
            // 
            this.btnCreateEmpty.Location = new System.Drawing.Point(397, 121);
            this.btnCreateEmpty.Name = "btnCreateEmpty";
            this.btnCreateEmpty.Size = new System.Drawing.Size(102, 23);
            this.btnCreateEmpty.TabIndex = 12;
            this.btnCreateEmpty.Text = "Create Empty";
            this.btnCreateEmpty.UseVisualStyleBackColor = true;
            this.btnCreateEmpty.Click += new System.EventHandler(this.btnCreateEmpty_Click);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(397, 194);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(102, 23);
            this.btnSync.TabIndex = 13;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 393);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnCreateEmpty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.localfileList);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnInitialize);
            this.Controls.Add(this.cbSync);
            this.Controls.Add(this.btnGetDirs);
            this.Controls.Add(this.btnCreateKey);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.serverfileList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainFrm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CryptoCloud";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox serverfileList;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCreateKey;
        private System.Windows.Forms.CheckBox cbSync;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnGetDirs;
        private System.Windows.Forms.ListBox localfileList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateEmpty;
        private System.Windows.Forms.Button btnSync;
    }
}

