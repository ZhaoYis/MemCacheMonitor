namespace MemCacheMonitor
{
    partial class frmBackUp
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbMemServer = new System.Windows.Forms.ComboBox();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBackPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChoose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器：";
            // 
            // cbMemServer
            // 
            this.cbMemServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMemServer.FormattingEnabled = true;
            this.cbMemServer.Location = new System.Drawing.Point(86, 15);
            this.cbMemServer.Name = "cbMemServer";
            this.cbMemServer.Size = new System.Drawing.Size(256, 20);
            this.cbMemServer.TabIndex = 1;
            this.cbMemServer.SelectedIndexChanged += new System.EventHandler(this.cbMemServer_SelectedIndexChanged);
            // 
            // btnBackUp
            // 
            this.btnBackUp.Location = new System.Drawing.Point(54, 82);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(75, 23);
            this.btnBackUp.TabIndex = 2;
            this.btnBackUp.Text = "开始备份";
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(187, 82);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBackPath
            // 
            this.txtBackPath.Location = new System.Drawing.Point(86, 48);
            this.txtBackPath.Name = "txtBackPath";
            this.txtBackPath.ReadOnly = true;
            this.txtBackPath.Size = new System.Drawing.Size(176, 21);
            this.txtBackPath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "备份路径：";
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(268, 46);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(74, 23);
            this.btnChoose.TabIndex = 6;
            this.btnChoose.Text = "选择";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // frmBackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(360, 117);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBackPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBackUp);
            this.Controls.Add(this.cbMemServer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBackUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "备份";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMemServer;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtBackPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChoose;
    }
}