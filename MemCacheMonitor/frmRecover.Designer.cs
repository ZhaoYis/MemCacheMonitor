namespace MemCacheMonitor
{
    partial class frmRecover
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
            this.btnChoose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRecoverPath = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRecover = new System.Windows.Forms.Button();
            this.cbMemServer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(271, 44);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(74, 23);
            this.btnChoose.TabIndex = 13;
            this.btnChoose.Text = "选择";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "还原路径：";
            // 
            // txtRecoverPath
            // 
            this.txtRecoverPath.Location = new System.Drawing.Point(89, 46);
            this.txtRecoverPath.Name = "txtRecoverPath";
            this.txtRecoverPath.ReadOnly = true;
            this.txtRecoverPath.Size = new System.Drawing.Size(176, 21);
            this.txtRecoverPath.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(190, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRecover
            // 
            this.btnRecover.Location = new System.Drawing.Point(57, 80);
            this.btnRecover.Name = "btnRecover";
            this.btnRecover.Size = new System.Drawing.Size(75, 23);
            this.btnRecover.TabIndex = 9;
            this.btnRecover.Text = "开始还原";
            this.btnRecover.UseVisualStyleBackColor = true;
            this.btnRecover.Click += new System.EventHandler(this.btnRecover_Click);
            // 
            // cbMemServer
            // 
            this.cbMemServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMemServer.FormattingEnabled = true;
            this.cbMemServer.Location = new System.Drawing.Point(89, 13);
            this.cbMemServer.Name = "cbMemServer";
            this.cbMemServer.Size = new System.Drawing.Size(256, 20);
            this.cbMemServer.TabIndex = 8;
            this.cbMemServer.SelectedIndexChanged += new System.EventHandler(this.cbMemServer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "服务器：";
            // 
            // frmRecover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(360, 117);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRecoverPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRecover);
            this.Controls.Add(this.cbMemServer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecover";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "还原";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRecoverPath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRecover;
        private System.Windows.Forms.ComboBox cbMemServer;
        private System.Windows.Forms.Label label1;
    }
}