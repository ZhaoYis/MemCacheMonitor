using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using MemCacheMoniter;
using System.IO;

namespace MemCacheMonitor
{
    public partial class frmRecover : Form
    {
        frmMain frmMain;
        int MemchcheCount = 10000;
        string recoverPath = string.Empty;
        private Dictionary<string, WriteThread> dicWirte = new Dictionary<string, WriteThread>();
        private Thread m_thread;
        ArrayList serverList;

        public frmRecover(frmMain frm)
        {
            InitializeComponent();
            this.btnRecover.Enabled = false;
            BindMemServer();
            frmMain = frm;
        }
        public void BindMemServer()
        {
            List<MemServer> list = MemServer.GetServerList();
            this.cbMemServer.DisplayMember = "ServerName";
            this.cbMemServer.ValueMember = "ServerID";
            this.cbMemServer.DataSource = list;
            serverList = new ArrayList();
            serverList.Add(this.cbMemServer.SelectedValue.ToString());
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void WriteState(string msg)
        {
            frmMain.WriteState(msg);
        }
        private void btnRecover_Click(object sender, EventArgs e)
        {
            BackUpRun();
            ReadState();
            this.Close();
            this.frmMain.备份ToolStripMenuItem1.Enabled = false;
            this.frmMain.还原ToolStripMenuItem.Enabled = false;
        }

        public void ReadState()
        {
            m_thread = new Thread(new ThreadStart(CheckWriteThread));
            m_thread.Start();
        }
        /// <summary>
        /// 备份文件线程
        /// </summary>
        void BackUpRun()
        {
            int count = 0;
            while (true)
            {
                GetBackUpItems(out count);
                if (count < 0)
                {
                    WriteState("连接失败！");
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }
            if (count > MemchcheCount)
            {
                WriteState("Memcache 已经有数据" + count + "条，大于设置恢复的条数" + MemchcheCount + "条，当前不需要更新！");
            }
            else
            {
                if (System.IO.Directory.Exists(recoverPath))
                {
                    if (System.IO.Directory.Exists(recoverPath))
                    {
                        string[] dirs = Directory.GetDirectories(recoverPath);
                        string[] servers = (string[])serverList.ToArray(typeof(string));
                        //遍历本地文件，写入memcache
                        WriteThread wt = null;
                        foreach (string path in dirs)
                        {
                            wt = new WriteThread(path, new Help(0, 1, 0, true));
                            wt.frm = this;
                            wt.serverList = servers;
                            dicWirte.Add(path, wt);
                            wt.Start();
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }
        public void CheckWriteThread()
        {
            while (true)
            {
                for (; ; )
                {
                    bool allCompleted = true;
                    foreach (var s in dicWirte)
                    {
                        if (!s.Value.IsCompleted())
                        {
                            allCompleted = false;
                        }
                        else
                        {
                            s.Value.Join();
                        }
                    }
                    if (allCompleted)
                        break;
                    else
                    {
                        Thread.Sleep(1000);
                        allCompleted = true;
                    }
                }
                this.frmMain.备份ToolStripMenuItem1.Enabled = true;
                this.frmMain.还原ToolStripMenuItem.Enabled = true;
                Thread.Sleep(1000 * 10);
                WriteState("还原数据完成，线程退出！");
                dicWirte.Clear();
                m_thread.Join();
            }
        }


        public void GetBackUpItems(out int count)
        {
            count = -1;
            ArrayList itemarr = new ArrayList();
            ArrayList arr = MemCachedManager.GetStats(serverList, MemCachedManager.Stats.Items, null);
            if (arr == null)
            {
                count = -1;
                return;
            }
            foreach (string a in arr)
            {
                string[] tmparr = a.Split(':');
                if (tmparr.Length > 1)
                {
                    int item_id = 0;
                    int.TryParse(tmparr[1], out item_id);
                    bool find = false;
                    foreach (int item in itemarr)
                    {
                        if (item == item_id)
                            find = true;
                    }
                    if (!find && item_id > 0 && item_id != 11211)
                        itemarr.Add(item_id);
                }
            }
            int total = 0;

            foreach (int item in itemarr)
            {
                WriteState("正在获取Item" + item + " 数据！");
                ArrayList cachearr = MemCachedManager.GetStats(serverList, MemCachedManager.Stats.CachedDump, "" + item + " 0");
                WriteState("获取Item" + item + " 数据" + cachearr.Count + "条！");
                total += cachearr.Count;
                if (total > MemchcheCount)
                {
                    WriteState("Memcache 已有超过 " + MemchcheCount + " 数据项！不恢复操作");
                    count = total;
                    return;
                }
            }
            WriteState("获取Item 成功" + itemarr.Count + "个Item");
            count = total;
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择要还原的文件目录";
            fbd.RootFolder = System.Environment.SpecialFolder.Desktop;
            string selectPath = string.Empty;
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                recoverPath = fbd.SelectedPath;
                if (recoverPath != "")
                {
                    this.txtRecoverPath.Text = recoverPath;
                    this.btnRecover.Enabled = true;
                }
            }
        }

        private void cbMemServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            serverList = new ArrayList();
            serverList.Add(this.cbMemServer.SelectedValue.ToString());
        }
    }
}
