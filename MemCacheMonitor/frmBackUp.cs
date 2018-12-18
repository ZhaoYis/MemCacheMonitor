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
    public partial class frmBackUp : Form
    {
        frmMain frm;
        ArrayList serverList;
        private Thread m_thread;
        private Dictionary<int, WorkThread> dic = new Dictionary<int, WorkThread>();

        public frmBackUp(frmMain frm)
        {
            InitializeComponent();
            this.btnBackUp.Enabled = false;
            BindMemServer();
            this.frm = frm;
        }
        public string BackPath = string.Empty;
        private void btnBackUp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BackPath))
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(BackPath);
                if (di.GetFiles().Length + di.GetDirectories().Length != 0)
                {
                    MessageBox.Show("当前文件目录不为空，请先清空目录或者新建一个空的备份目录！");
                    return;
                }
                GetItems();
                ReadState();
                this.Close();
                this.frm.备份ToolStripMenuItem1.Enabled = false;
                this.frm.还原ToolStripMenuItem.Enabled = false;
            }
        }
        public void ReadState()
        {
            m_thread = new Thread(new ThreadStart(Check));
            m_thread.Start();
        }
        public void WriteState(string msg)
        {
            frm.WriteState(msg);
        }

        private void GetItems()
        {
            ArrayList itemarr = new ArrayList();
            ArrayList arr = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                arr = MemCachedManager.GetStats(serverList, MemCachedManager.Stats.Items, null);
                if (arr.Count == 0)
                {
                    this.frm.备份ToolStripMenuItem1.Enabled = true;
                    this.frm.还原ToolStripMenuItem.Enabled = true;
                    WriteState("没有找到数据！");
                    return;
                }
                if (arr == null)
                {
                    WriteState("连接服务器异常！");
                    return;
                }
            }
            catch (Exception ex)
            {
                WriteState(ex.Message.ToString());
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
            WriteState("获取Item 成功" + itemarr.Count + "个Item！");
            foreach (int item in itemarr)
            {
                WorkThread wt = null;
                wt = new WorkThread(item, new Help(0, 1, 0, true));
                wt.frm = this;
                wt.arrayList = serverList;
                dic.Add(item, wt);
                wt.Run();
            }
        }

        public void Check()
        {
            while (true)
            {
                for (; ; )
                {
                    bool allCompleted = true;
                    foreach (var s in dic)
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
                this.frm.备份ToolStripMenuItem1.Enabled = true;
                this.frm.还原ToolStripMenuItem.Enabled = true;
                Thread.Sleep(1000 * 10);
                WriteState("备份数据完成，线程退出！");
                dic.Clear();
                m_thread.Join();

            }
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
        private void btnChoose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择文件备份目录";
            fbd.RootFolder = System.Environment.SpecialFolder.Desktop;
            string selectPath = string.Empty;
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                BackPath = fbd.SelectedPath;
                if (BackPath != "")
                {
                    this.txtBackPath.Text = BackPath;
                    this.btnBackUp.Enabled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="key">Memchche Key</param>
        /// <param name="value"> value</param>
        /// <param name="path"></param>
        /// <param name="filePath"></param>
        public void WriteFile(string key, string value, int itemTotalNum, int item, string path, string filePath, string indexPath)
        {
            StreamWriter sw;
            if (!File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                using (sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    sw.Write(value);
                    WriteState("正在备份Item " + item + " 【key=" + key + "】");
                    WriteFileIndex(path, indexPath, filePath);
                }
            }
            catch (Exception ex)
            {
                WriteState(ex.Message.ToString());
            }
        }
        /// <summary>
        /// 写入文件索引
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <param name="filePath"></param>
        public void WriteFileIndex(string path, string indexFilePath, string filePath)
        {
            StreamWriter sw;
            if (!File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                string file = indexFilePath + ".txt";
                if (!File.Exists(file))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Create))
                    {
                        using (sw = new StreamWriter(fs))
                        {
                            sw.Write(filePath + "\r\n");
                        }
                    }
                }
                else
                {
                    using (sw = new StreamWriter(file, true, Encoding.UTF8))
                    {
                        sw.Write(filePath + "\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteState(ex.Message.ToString());
            }
        }

        private void cbMemServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            serverList = new ArrayList();
            serverList.Add(this.cbMemServer.SelectedValue.ToString());
        }
    }
}
