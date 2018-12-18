namespace MemCacheMonitor
{
    partial class frmServer
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lable5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtServerDescr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtport = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoYes = new System.Windows.Forms.RadioButton();
            this.rdoNo = new System.Windows.Forms.RadioButton();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.服务器ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.服务器名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否有效 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.更新时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.描述 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lable5);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtServerDescr);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtport);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 207);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "编辑";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(207, 154);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "删 除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(326, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "是否有效：";
            // 
            // lable5
            // 
            this.lable5.AutoSize = true;
            this.lable5.ForeColor = System.Drawing.Color.Red;
            this.lable5.Location = new System.Drawing.Point(93, 186);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(293, 12);
            this.lable5.TabIndex = 10;
            this.lable5.Text = "双击列表编辑和删除数据，点击重置可以重新添加数据";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(316, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "重置";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(97, 154);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtServerDescr
            // 
            this.txtServerDescr.Location = new System.Drawing.Point(97, 101);
            this.txtServerDescr.Multiline = true;
            this.txtServerDescr.Name = "txtServerDescr";
            this.txtServerDescr.Size = new System.Drawing.Size(422, 40);
            this.txtServerDescr.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "描述信息：";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(97, 58);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(223, 21);
            this.txtServerName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "服务器名称：";
            // 
            // txtport
            // 
            this.txtport.Location = new System.Drawing.Point(397, 18);
            this.txtport.Name = "txtport";
            this.txtport.Size = new System.Drawing.Size(72, 21);
            this.txtport.TabIndex = 3;
            this.txtport.Text = "11211";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口：";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(97, 18);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(223, 21);
            this.txtIp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoYes);
            this.panel1.Controls.Add(this.rdoNo);
            this.panel1.Location = new System.Drawing.Point(397, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(122, 34);
            this.panel1.TabIndex = 14;
            // 
            // rdoYes
            // 
            this.rdoYes.AutoSize = true;
            this.rdoYes.Checked = true;
            this.rdoYes.Location = new System.Drawing.Point(16, 14);
            this.rdoYes.Name = "rdoYes";
            this.rdoYes.Size = new System.Drawing.Size(35, 16);
            this.rdoYes.TabIndex = 12;
            this.rdoYes.TabStop = true;
            this.rdoYes.Text = "是";
            this.rdoYes.UseVisualStyleBackColor = true;
            // 
            // rdoNo
            // 
            this.rdoNo.AutoSize = true;
            this.rdoNo.Location = new System.Drawing.Point(69, 14);
            this.rdoNo.Name = "rdoNo";
            this.rdoNo.Size = new System.Drawing.Size(35, 16);
            this.rdoNo.TabIndex = 13;
            this.rdoNo.Text = "否";
            this.rdoNo.UseVisualStyleBackColor = true;
            // 
            // dgvServerList
            // 
            this.dgvServerList.AllowUserToAddRows = false;
            this.dgvServerList.AllowUserToDeleteRows = false;
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.服务器ID,
            this.服务器名称,
            this.是否有效,
            this.更新时间,
            this.描述});
            this.dgvServerList.Location = new System.Drawing.Point(12, 225);
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.ReadOnly = true;
            this.dgvServerList.RowHeadersVisible = false;
            this.dgvServerList.RowTemplate.Height = 23;
            this.dgvServerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServerList.Size = new System.Drawing.Size(556, 262);
            this.dgvServerList.TabIndex = 1;
            this.dgvServerList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvServerList_DataBindingComplete);
            this.dgvServerList.DoubleClick += new System.EventHandler(this.dgvServerList_DoubleClick);
            // 
            // 服务器ID
            // 
            this.服务器ID.DataPropertyName = "ServerID";
            this.服务器ID.HeaderText = "服务器ID";
            this.服务器ID.Name = "服务器ID";
            this.服务器ID.ReadOnly = true;
            this.服务器ID.Width = 172;
            // 
            // 服务器名称
            // 
            this.服务器名称.DataPropertyName = "ServerName";
            this.服务器名称.HeaderText = "服务器名称";
            this.服务器名称.Name = "服务器名称";
            this.服务器名称.ReadOnly = true;
            this.服务器名称.Width = 171;
            // 
            // 是否有效
            // 
            this.是否有效.DataPropertyName = "Valid";
            this.是否有效.HeaderText = "是否有效";
            this.是否有效.Name = "是否有效";
            this.是否有效.ReadOnly = true;
            this.是否有效.Width = 78;
            // 
            // 更新时间
            // 
            this.更新时间.DataPropertyName = "UpdateTime";
            this.更新时间.HeaderText = "更新时间";
            this.更新时间.Name = "更新时间";
            this.更新时间.ReadOnly = true;
            this.更新时间.Width = 78;
            // 
            // 描述
            // 
            this.描述.DataPropertyName = "ServerDescr";
            this.描述.HeaderText = "描述";
            this.描述.Name = "描述";
            this.描述.ReadOnly = true;
            this.描述.Width = 54;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 498);
            this.Controls.Add(this.dgvServerList);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "服务器信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtServerDescr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lable5;
        private System.Windows.Forms.RadioButton rdoNo;
        private System.Windows.Forms.RadioButton rdoYes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn 服务器ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 服务器名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否有效;
        private System.Windows.Forms.DataGridViewTextBoxColumn 更新时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 描述;
    }
}