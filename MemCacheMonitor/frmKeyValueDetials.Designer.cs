namespace MemCacheMonitor
{
    partial class frmKeyValueDetials
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKeyValueDetials));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbNext = new System.Windows.Forms.LinkLabel();
            this.lbPrev = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReplace = new System.Windows.Forms.Button();
            this.txtNewValue = new System.Windows.Forms.TextBox();
            this.txtOldValue = new System.Windows.Forms.TextBox();
            this.lbFormat = new System.Windows.Forms.LinkLabel();
            this.lbCancel = new System.Windows.Forms.LinkLabel();
            this.lbModify = new System.Windows.Forms.LinkLabel();
            this.lbDelete = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtKey
            // 
            resources.ApplyResources(this.txtKey, "txtKey");
            this.txtKey.Name = "txtKey";
            // 
            // txtValue
            // 
            resources.ApplyResources(this.txtValue, "txtValue");
            this.txtValue.Name = "txtValue";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbNext);
            this.groupBox1.Controls.Add(this.lbPrev);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnReplace);
            this.groupBox1.Controls.Add(this.txtNewValue);
            this.groupBox1.Controls.Add(this.txtOldValue);
            this.groupBox1.Controls.Add(this.lbFormat);
            this.groupBox1.Controls.Add(this.lbCancel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbModify);
            this.groupBox1.Controls.Add(this.lbDelete);
            this.groupBox1.Controls.Add(this.txtKey);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Name = "label5";
            // 
            // lbNext
            // 
            resources.ApplyResources(this.lbNext, "lbNext");
            this.lbNext.Name = "lbNext";
            this.lbNext.TabStop = true;
            this.lbNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbNext_LinkClicked);
            // 
            // lbPrev
            // 
            resources.ApplyResources(this.lbPrev, "lbPrev");
            this.lbPrev.Name = "lbPrev";
            this.lbPrev.TabStop = true;
            this.lbPrev.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbPrev_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnReplace
            // 
            resources.ApplyResources(this.btnReplace, "btnReplace");
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // txtNewValue
            // 
            resources.ApplyResources(this.txtNewValue, "txtNewValue");
            this.txtNewValue.Name = "txtNewValue";
            // 
            // txtOldValue
            // 
            resources.ApplyResources(this.txtOldValue, "txtOldValue");
            this.txtOldValue.Name = "txtOldValue";
            // 
            // lbFormat
            // 
            resources.ApplyResources(this.lbFormat, "lbFormat");
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.TabStop = true;
            this.lbFormat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbFormat_LinkClicked_1);
            // 
            // lbCancel
            // 
            resources.ApplyResources(this.lbCancel, "lbCancel");
            this.lbCancel.Name = "lbCancel";
            this.lbCancel.TabStop = true;
            this.lbCancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbCancel_LinkClicked_1);
            // 
            // lbModify
            // 
            resources.ApplyResources(this.lbModify, "lbModify");
            this.lbModify.Name = "lbModify";
            this.lbModify.TabStop = true;
            this.lbModify.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbModify_LinkClicked_1);
            // 
            // lbDelete
            // 
            resources.ApplyResources(this.lbDelete, "lbDelete");
            this.lbDelete.Name = "lbDelete";
            this.lbDelete.TabStop = true;
            this.lbDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbDelete_LinkClicked_1);
            // 
            // frmKeyValueDetials
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKeyValueDetials";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmKeyValueDetials_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lbCancel;
        private System.Windows.Forms.LinkLabel lbDelete;
        private System.Windows.Forms.LinkLabel lbModify;
        private System.Windows.Forms.LinkLabel lbFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.TextBox txtNewValue;
        private System.Windows.Forms.TextBox txtOldValue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel lbNext;
        private System.Windows.Forms.LinkLabel lbPrev;
        private System.Windows.Forms.Label label5;
    }
}