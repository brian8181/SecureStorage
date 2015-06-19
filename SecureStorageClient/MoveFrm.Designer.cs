namespace SecureStorageClient
{
    partial class MoveFrm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.srcfileBrowser = new utility.extra.FileBrowser();
            this.dstfileBrowser = new utility.extra.FileBrowser();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(188, 114);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(269, 114);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // srcfileBrowser
            // 
            this.srcfileBrowser.Label = "Src";
            this.srcfileBrowser.Location = new System.Drawing.Point(10, 12);
            this.srcfileBrowser.Name = "srcfileBrowser";
            this.srcfileBrowser.Size = new System.Drawing.Size(334, 34);
            this.srcfileBrowser.TabIndex = 8;
            // 
            // dstfileBrowser
            // 
            this.dstfileBrowser.Label = "Dst";
            this.dstfileBrowser.Location = new System.Drawing.Point(10, 52);
            this.dstfileBrowser.Name = "dstfileBrowser";
            this.dstfileBrowser.Size = new System.Drawing.Size(334, 34);
            this.dstfileBrowser.TabIndex = 9;
            // 
            // CopyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 149);
            this.Controls.Add(this.dstfileBrowser);
            this.Controls.Add(this.srcfileBrowser);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "CopyFrm";
            this.Text = "CopyFrm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public utility.extra.FileBrowser srcfileBrowser;
        public utility.extra.FileBrowser dstfileBrowser;
    }
}