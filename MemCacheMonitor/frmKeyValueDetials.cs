using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Commons;
using MemCacheMoniter;

namespace MemCacheMonitor
{
    public partial class frmKeyValueDetials : Form
    {
        ArrayList memcacheServer;
        string oldValue = string.Empty;
        DataTable dtSource;
        int posIndex = 0;
        public DataGridView dgv;
        public frmMain frm;
        public frmKeyValueDetials(string key, string value, ArrayList serverList, DataTable dt, int rowIndex)
        {
            InitializeComponent();
            this.txtKey.Text = key;
            this.txtKey.Enabled = false;
            this.txtValue.Text = value;
            memcacheServer = serverList;
            this.groupBox1.Text = "Key:" + key;
            this.Text = "Key:" + key;
            oldValue = value;
            dtSource = dt;
            posIndex = rowIndex;
            if (posIndex == 0)
            {
                this.lbPrev.Enabled = false;
            }
            if (posIndex == dtSource.Rows.Count - 1)
            {
                this.lbNext.Enabled = false;
            }
            string errmsg = string.Empty;
            if (JsonFormat.CanFormatJson(value, out errmsg))
            {
                this.lbFormat.Enabled = true;
            }
            else
            {
                this.lbFormat.Enabled = false;
            }
            this.lbCancel.Enabled = false;
        }
        private void lbFormat_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string value = JsonFormat.FormatJson(this.txtValue.Text);
                this.txtValue.Text = value;
                this.lbCancel.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lbCancel_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.txtValue.Text = oldValue;
        }

        private void lbModify_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] serverlist = (string[])memcacheServer.ToArray(typeof(string));
            MemCacheTools_V2 tools = new MemCacheTools_V2(serverlist);
            string key = this.txtKey.Text;
            string value = this.txtValue.Text;
            tools.Add(key, value);
            MessageBox.Show("修改成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void lbDelete_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] serverlist = (string[])memcacheServer.ToArray(typeof(string));
            MemCacheTools_V2 tools = new MemCacheTools_V2(serverlist);
            string key = this.txtKey.Text;
            if (MessageBox.Show("确定要删除key=【" + key + "】吗？", "提示信息", MessageBoxButtons.YesNo).ToString() == "Yes")
            {
                tools.Clear(key);
                MessageBox.Show("删除成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            string text = this.txtValue.Text;
            string oldValue = this.txtOldValue.Text;
            string newValue = this.txtNewValue.Text;
            if (!string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
            {
                var value = text.Replace(oldValue, newValue);
                this.txtValue.Text = value;
            }
        }

        private void frmKeyValueDetials_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
                GetNext();
            if (e.KeyCode == Keys.PageUp)
                GetPrev();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetPrev()
        {
            posIndex--;
            if (posIndex == 0)
            {
                this.lbNext.Enabled = true;
                this.lbPrev.Enabled = false;
            }
            else
            {
                this.lbNext.Enabled = true;
                this.lbPrev.Enabled = true;
            }
            GetInfo();
        }

        private void GetNext()
        {
            posIndex++;
            if (posIndex == dtSource.Rows.Count - 1)
            {
                this.lbNext.Enabled = false;
                this.lbPrev.Enabled = true;
            }
            else
            {
                this.lbNext.Enabled = true;
                this.lbPrev.Enabled = true;
            }
            GetInfo();
        }

        private void lbPrev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetPrev();
        }

        private void lbNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetNext();
        }

        public void GetInfo()
        {
            this.txtKey.Text = dtSource.Rows[posIndex]["key"].ToString();
            this.txtValue.Text = dtSource.Rows[posIndex]["value"].ToString();
            frm.ChangeRowSelect(dgv, posIndex);
        }
    }
}
