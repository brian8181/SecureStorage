namespace SecureStorageSyncClient
{
    partial class ToolsFrm
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
            this.btnFromCloud = new System.Windows.Forms.Button();
            this.btn_FromLocal = new System.Windows.Forms.Button();
            this.txtClloudPath = new System.Windows.Forms.TextBox();
            this.txtLocalPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFromCloud
            // 
            this.btnFromCloud.Location = new System.Drawing.Point(176, 51);
            this.btnFromCloud.Name = "btnFromCloud";
            this.btnFromCloud.Size = new System.Drawing.Size(159, 23);
            this.btnFromCloud.TabIndex = 0;
            this.btnFromCloud.Text = "From Cloud";
            this.btnFromCloud.UseVisualStyleBackColor = true;
            this.btnFromCloud.Click += new System.EventHandler(this.btnFromCloud_Click);
            // 
            // btn_FromLocal
            // 
            this.btn_FromLocal.Location = new System.Drawing.Point(176, 154);
            this.btn_FromLocal.Name = "btn_FromLocal";
            this.btn_FromLocal.Size = new System.Drawing.Size(159, 23);
            this.btn_FromLocal.TabIndex = 1;
            this.btn_FromLocal.Text = "From Local";
            this.btn_FromLocal.UseVisualStyleBackColor = true;
            this.btn_FromLocal.Click += new System.EventHandler(this.btn_FromLocal_Click);
            // 
            // txtClloudPath
            // 
            this.txtClloudPath.Location = new System.Drawing.Point(12, 25);
            this.txtClloudPath.Name = "txtClloudPath";
            this.txtClloudPath.Size = new System.Drawing.Size(323, 20);
            this.txtClloudPath.TabIndex = 2;
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Location = new System.Drawing.Point(12, 128);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.Size = new System.Drawing.Size(323, 20);
            this.txtLocalPath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Path";
            // 
            // ToolsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 191);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLocalPath);
            this.Controls.Add(this.txtClloudPath);
            this.Controls.Add(this.btn_FromLocal);
            this.Controls.Add(this.btnFromCloud);
            this.Name = "ToolsFrm";
            this.Text = "ToolsFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFromCloud;
        private System.Windows.Forms.Button btn_FromLocal;
        private System.Windows.Forms.TextBox txtClloudPath;
        private System.Windows.Forms.TextBox txtLocalPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}