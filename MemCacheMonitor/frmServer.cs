using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MemCacheMoniter;
using Commons;

namespace MemCacheMonitor
{
    public partial class frmServer : Form
    {
        public frmMain frm;
        public frmServer(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            this.btnDelete.Enabled = false;
            BindServer();
        }
        string serverid = string.Empty;
        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList serverList = new ArrayList();
            string ip = this.txtIp.Text;
            string port = this.txtport.Text;
            string servername = this.txtServerName.Text;
            string serverDescr = this.txtServerDescr.Text;
            string serverId = ip + ":" + port;
            string valid = this.rdoYes.Checked ? "1" : "0";
            int rs = 0;
            string sql = string.Empty;
            serverList.Add(serverId);
            string xmlpath = Application.StartupPath + @"\MemServerInfo.xml";
            if (MemServer.GetServerList().Count > 0)
            {
                try
                {
                    ArrayList arrlist = MemCachedManager.GetStats(serverList, MemCachedManager.Stats.Detail, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("MemCache服务器验证失败！" + ex.Message.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            MemServer mem = new MemServer(xmlpath);
            if (string.IsNullOrEmpty(serverid))
            {
                if (IsExistsServerId(serverId))
                {
                    MessageBox.Show("已存在该服务器，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                mem.Add(serverId, servername, "1", serverDescr);
                //sql = "Insert Into tb_MemServer('ServerID','ServerName','ServerDescr','UpdateTime','Valid') values('" + serverId + "','" + servername + "','" + serverDescr + "','" + DateTime.Now + "','1')";
                //rs = SQLiteHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                mem.Modify(serverId, servername, "1", serverDescr);
                // sql = "Update tb_MemServer set ServerID='" + serverId + "',ServerName='" + servername + "',ServerDescr='" + serverDescr + "',UpdateTime='" + DateTime.Now + "',valid='" + valid + "' where ServerID='" + serverid + "'";mem.Add(serverId, servername, serverDescr,"1");
                //rs = SQLiteHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            //if (rs >= 0)
            Clear();
            BindServer();
            frm.BindMemServer();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            this.serverid = string.Empty;
            this.txtIp.Text = this.txtport.Text = this.txtServerDescr.Text = this.txtServerName.Text = "";
            this.rdoYes.Checked = true;
            this.btnSave.Text = "添加";
            this.btnDelete.Enabled = false;
        }

        public void BindServer()
        {
            //string sql = "select * from tb_MemServer";
            //DataTable dt = SQLiteHelper.ExecuteQuery(CommandType.Text, sql, null).Tables[0];
            //List<MemServer> list = ConvertHelper<MemServer>.ConvertToList(dt);
            List<MemServer> list = MemServer.GetAll();
            this.dgvServerList.DataSource = list;
        }

        bool IsExistsServerId(string serverId)
        {
            //string sql = "select count(1) from tb_MemServer where ServerID='" + serverId + "'";
            //object obj = SQLiteHelper.ExecuteScalar(CommandType.Text, sql, null);
            //return Convert.ToInt32(obj) >= 1 ? true : false;
            List<MemServer> list = MemServer.GetAll();
            return list.FindAll(s => s.ServerID.Equals(serverid)).Count > 1 ? true : false;
        }

        private void dgvServerList_DoubleClick(object sender, EventArgs e)
        {
            int RowIndex = this.dgvServerList.CurrentRow.Index;//获取行的索引
            MemServer ms = dgvServerList.Rows[RowIndex].DataBoundItem as MemServer;
            if (ms != null)
            {
                serverid = ms.ServerID;
                this.txtIp.Text = ms.ServerID.Split(':')[0];
                this.txtport.Text = ms.ServerID.Split(':')[1];
                this.txtServerDescr.Text = ms.ServerDescr;
                this.txtServerName.Text = ms.ServerName;
                if (ms.Valid == "1")
                {
                    this.rdoYes.Checked = true;
                }
                else
                {
                    this.rdoNo.Checked = true;
                }
            }
            this.btnSave.Text = "修改";
            this.btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(serverid))
            {
                if (MessageBox.Show("确定删除服务器" + serverid + "吗?", "提示信息", MessageBoxButtons.YesNo).ToString() == "Yes")
                {
                    string sql = "delete  from tb_MemServer where ServerID='" + serverid + "'";
                    int rs = SQLiteHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                    if (rs >= 0)
                        Clear();
                    BindServer();
                    frm.BindMemServer();
                }
            }
        }

        private void dgvServerList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.dgvServerList.Rows.Count != 0)
            {
                for (int i = 0; i < this.dgvServerList.Rows.Count; )
                {
                    this.dgvServerList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Wheat;
                    i += 2;
                }
            }
        }
    }
}
