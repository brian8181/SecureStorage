namespace SecureStorageSyncClient
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
            this.bnt_Sync = new System.Windows.Forms.Button();
            this.btn_Settings = new System.Windows.Forms.Button();
            this.bnt_Tools = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bnt_Sync
            // 
            this.bnt_Sync.Location = new System.Drawing.Point(88, 50);
            this.bnt_Sync.Name = "bnt_Sync";
            this.bnt_Sync.Size = new System.Drawing.Size(106, 81);
            this.bnt_Sync.TabIndex = 0;
            this.bnt_Sync.Text = "Sync";
            this.bnt_Sync.UseVisualStyleBackColor = true;
            this.bnt_Sync.Click += new System.EventHandler(this.bnt_Sync_Click);
            // 
            // btn_Settings
            // 
            this.btn_Settings.Location = new System.Drawing.Point(12, 12);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.Size = new System.Drawing.Size(75, 23);
            this.btn_Settings.TabIndex = 1;
            this.btn_Settings.Text = "Settings";
            this.btn_Settings.UseVisualStyleBackColor = true;
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            // 
            // bnt_Tools
            // 
            this.bnt_Tools.Location = new System.Drawing.Point(197, 12);
            this.bnt_Tools.Name = "bnt_Tools";
            this.bnt_Tools.Size = new System.Drawing.Size(75, 23);
            this.bnt_Tools.TabIndex = 2;
            this.bnt_Tools.Text = "Tools";
            this.bnt_Tools.UseVisualStyleBackColor = true;
            this.bnt_Tools.Click += new System.EventHandler(this.bnt_Tools_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 144);
            this.Controls.Add(this.bnt_Tools);
            this.Controls.Add(this.btn_Settings);
            this.Controls.Add(this.bnt_Sync);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bnt_Sync;
        private System.Windows.Forms.Button btn_Settings;
        private System.Windows.Forms.Button bnt_Tools;
    }
}

