namespace SecureStorageClient
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
            this.btnInitializeLocal = new System.Windows.Forms.Button();
            this.btnGetDirs = new System.Windows.Forms.Button();
            this.lblSever = new System.Windows.Forms.Label();
            this.btnCreateEmpty = new System.Windows.Forms.Button();
            this.btnUpDirectory = new System.Windows.Forms.Button();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnCreateDir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpLocal = new System.Windows.Forms.GroupBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverfileList
            // 
            this.serverfileList.FormattingEnabled = true;
            this.serverfileList.Location = new System.Drawing.Point(3, 35);
            this.serverfileList.Name = "serverfileList";
            this.serverfileList.Size = new System.Drawing.Size(191, 433);
            this.serverfileList.TabIndex = 0;
            this.serverfileList.DoubleClick += new System.EventHandler(this.serverfileList_DoubleClick);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(6, 106);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(102, 23);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "CreateFile/Upload";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(6, 164);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(102, 23);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Read/Download";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(6, 193);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreateKey
            // 
            this.btnCreateKey.Location = new System.Drawing.Point(6, 48);
            this.btnCreateKey.Name = "btnCreateKey";
            this.btnCreateKey.Size = new System.Drawing.Size(102, 23);
            this.btnCreateKey.TabIndex = 4;
            this.btnCreateKey.Text = "CreateFile Key";
            this.btnCreateKey.UseVisualStyleBackColor = true;
            this.btnCreateKey.Click += new System.EventHandler(this.btnCreateKey_Click);
            // 
            // btnInitializeLocal
            // 
            this.btnInitializeLocal.Location = new System.Drawing.Point(6, 19);
            this.btnInitializeLocal.Name = "btnInitializeLocal";
            this.btnInitializeLocal.Size = new System.Drawing.Size(102, 23);
            this.btnInitializeLocal.TabIndex = 7;
            this.btnInitializeLocal.Text = "(Re)InitializeLocalRoot";
            this.btnInitializeLocal.UseVisualStyleBackColor = true;
            this.btnInitializeLocal.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnGetDirs
            // 
            this.btnGetDirs.Location = new System.Drawing.Point(6, 48);
            this.btnGetDirs.Name = "btnGetDirs";
            this.btnGetDirs.Size = new System.Drawing.Size(102, 23);
            this.btnGetDirs.TabIndex = 5;
            this.btnGetDirs.Text = "Get List";
            this.btnGetDirs.UseVisualStyleBackColor = true;
            this.btnGetDirs.Click += new System.EventHandler(this.btnGetDirs_Click);
            // 
            // lblSever
            // 
            this.lblSever.AutoSize = true;
            this.lblSever.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSever.Location = new System.Drawing.Point(0, 8);
            this.lblSever.Name = "lblSever";
            this.lblSever.Size = new System.Drawing.Size(44, 13);
            this.lblSever.TabIndex = 10;
            this.lblSever.Text = "Server";
            // 
            // btnCreateEmpty
            // 
            this.btnCreateEmpty.Location = new System.Drawing.Point(6, 135);
            this.btnCreateEmpty.Name = "btnCreateEmpty";
            this.btnCreateEmpty.Size = new System.Drawing.Size(102, 23);
            this.btnCreateEmpty.TabIndex = 12;
            this.btnCreateEmpty.Text = "CreateFile Empty";
            this.btnCreateEmpty.UseVisualStyleBackColor = true;
            this.btnCreateEmpty.Click += new System.EventHandler(this.btnCreateEmpty_Click);
            // 
            // btnUpDirectory
            // 
            this.btnUpDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpDirectory.Location = new System.Drawing.Point(153, 4);
            this.btnUpDirectory.Name = "btnUpDirectory";
            this.btnUpDirectory.Size = new System.Drawing.Size(41, 23);
            this.btnUpDirectory.TabIndex = 14;
            this.btnUpDirectory.Text = "^";
            this.btnUpDirectory.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpDirectory.UseVisualStyleBackColor = true;
            this.btnUpDirectory.Click += new System.EventHandler(this.btnUpDirectory_Click);
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(6, 19);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(102, 23);
            this.btnInitialize.TabIndex = 15;
            this.btnInitialize.Text = "(Re)Initialize";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click_1);
            // 
            // btnCreateDir
            // 
            this.btnCreateDir.Location = new System.Drawing.Point(6, 77);
            this.btnCreateDir.Name = "btnCreateDir";
            this.btnCreateDir.Size = new System.Drawing.Size(102, 23);
            this.btnCreateDir.TabIndex = 16;
            this.btnCreateDir.Text = "CreateDir";
            this.btnCreateDir.UseVisualStyleBackColor = true;
            this.btnCreateDir.Click += new System.EventHandler(this.btnCreateDir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Controls.Add(this.btnCreateEmpty);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.btnCreateDir);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Controls.Add(this.btnInitialize);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnGetDirs);
            this.groupBox1.Location = new System.Drawing.Point(200, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 276);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Remote";
            // 
            // grpLocal
            // 
            this.grpLocal.Controls.Add(this.btnCreateKey);
            this.grpLocal.Controls.Add(this.btnInitializeLocal);
            this.grpLocal.Location = new System.Drawing.Point(200, 34);
            this.grpLocal.Name = "grpLocal";
            this.grpLocal.Size = new System.Drawing.Size(118, 86);
            this.grpLocal.TabIndex = 18;
            this.grpLocal.TabStop = false;
            this.grpLocal.Text = "Local";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(6, 222);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(102, 23);
            this.btnCopy.TabIndex = 17;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 470);
            this.Controls.Add(this.grpLocal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUpDirectory);
            this.Controls.Add(this.lblSever);
            this.Controls.Add(this.serverfileList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainFrm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SecureStorage Tester";
            this.groupBox1.ResumeLayout(false);
            this.grpLocal.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox serverfileList;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCreateKey;
        private System.Windows.Forms.Button btnInitializeLocal;
        private System.Windows.Forms.Button btnGetDirs;
        private System.Windows.Forms.Label lblSever;
        private System.Windows.Forms.Button btnCreateEmpty;
        private System.Windows.Forms.Button btnUpDirectory;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnCreateDir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpLocal;
        private System.Windows.Forms.Button btnCopy;
    }
}

