using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Commons;
using System.Collections;
using MemCacheMonitor;
using System.Net;
using System.Threading;

namespace MemCacheMoniter
{
    public partial class frmMain : Form
    {
        private delegate void WriteStateDelegate(string log);//日志信息
        public delegate void Asyncdelegate();
        private Thread m_thread;
        int sleep = 0;
        private void CallbackMethod(IAsyncResult ar)
        {
            Asyncdelegate dlgt = (Asyncdelegate)ar.AsyncState;
            dlgt.EndInvoke(ar);
        }

        List<MemServer> list;
        List<MemServerDetitals> listDetials;

        public frmMain()
        {
            InitializeComponent();
            this.tabControl1.TabPages.Remove(this.tp7);
            Control.CheckForIllegalCrossThreadCalls = false;
            string sqliteconnString = "data source=" + Application.StartupPath + @"\MemServerMointorDB.db" + ";Pooling=true;FailIfMissing=false";
            listDetials = MemServerDetitals.GetMemServerDetitalsList(sqliteconnString);
            sleep = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Sleep"]) * 1000 * 60;
            BindMemServer();
            GetMemServerDetials();
            this.btnStop.Enabled = false;
            this.lbltime.Visible = false;
            m_thread = new Thread(new ThreadStart(CheckMemServerState));
            m_thread.Start();
        }

        public void BindMemServer()
        {
            list = MemServer.GetServerList();
            this.cbMemServer.DisplayMember = "ServerName";
            this.cbMemServer.ValueMember = "ServerID";
            this.cbMemServer.DataSource = list;
            if (list != null && list.Count > 0)
            {
                serverList = new ArrayList();
                serverList.Add(this.cbMemServer.SelectedValue.ToString());
                TabControlBind(this.tabControl1.SelectedIndex.ToString());
                this.tabControl1.Enabled = true;
                this.工具ToolStripMenuItem.Enabled = true;
            }
            else
            {
                MessageBox.Show("请先添加服务器，再进行其他操作！");
                this.tabControl1.Enabled = false;
                this.工具ToolStripMenuItem.Enabled = false;
                new frmServer(this).Show();
            }
        }

        ArrayList serverList = new ArrayList();

        void BindSlabSource()
        {
            DataTable dt = CreateDataTable();
            ArrayList items = null;
            try
            {
                items = MemCacheOperate.GetAllItems(serverList);
                if (items == null)
                {
                    MessageBox.Show("服务器连接异常，正在尝试重新连接！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务器连接异常！" + ex.Message.ToString());
                return;
            }
            DataRow dr = null;
            items.Sort();
            foreach (var s in items)
            {
                dr = dt.NewRow();
                dr["key"] = "Slab:" + s;
                dr["value"] = s;
                dt.Rows.Add(dr);
            }
            this.cbSlabs.DisplayMember = "key";
            this.cbSlabs.ValueMember = "value";
            this.cbSlabs.DataSource = dt;
        }

        private void cbMemServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            serverList = new ArrayList();
            string server = this.cbMemServer.SelectedValue.ToString();
            serverList.Add(server);
            TabControlBind(this.tabControl1.SelectedIndex.ToString());
        }

        private void btnSearchItems_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum.Text))
            {
                MessageBox.Show("请输入要查询的行数！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string server = this.cbMemServer.SelectedValue.ToString();
            var num = Convert.ToInt32(this.txtNum.Text);
            int item = Convert.ToInt32(this.cbSlabs.SelectedValue);
            List<string> keys = MemCacheOperate.GetOneItemKey(serverList, item, num);
            Dictionary<string, object> dicSource = MemCacheOperate.Get(keys, serverList);
            DataTable dt = CreateDataTable();
            DataRow dr;
            foreach (var s in dicSource)
            {
                dr = dt.NewRow();
                dr["key"] = s.Key;
                dr["value"] = s.Value;
                dt.Rows.Add(dr);
            }
            this.dgvSource.DataSource = dt;
        }

        DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            DataColumn dtc = new DataColumn("key", typeof(string));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("value", typeof(string));
            dt.Columns.Add(dtc);
            return dt;
        }

        private void cbSlabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int item = Convert.ToInt32(this.cbSlabs.SelectedValue);
            int totalcount = MemCacheOperate.GetTotalCountBySlab(serverList, item);
            this.lblSlabCount.Text = "共" + totalcount + "条";
        }

        void ShootingMemServer()
        {
            DataTable dt = GetServerDetials();
            decimal get1 = 0, get2 = 0, get3 = 0,
                delete1 = 0, delete2 = 0, delete3 = 0,
                cas1 = 0, cas2 = 0, cas3 = 0,
                incr1 = 0, incr2 = 0, incr3 = 0,
                decr1 = 0, decr2 = 0, decr3 = 0;
            foreach (var s in dicDetials)
            {
                if (s.Key == "cmd_get")
                {
                    this.cmd_get.Text = s.Value;
                    get1 = Convert.ToDecimal(s.Value);
                }
                if (s.Key == "get_hits")
                {
                    this.get_hits.Text = s.Value;
                    get2 = Convert.ToDecimal(s.Value);
                }
                if (s.Key == "get_misses")
                {
                    this.get_misses.Text = s.Value;
                    get3 = Convert.ToDecimal(s.Value);
                }

                if (s.Key == "delete_hits")
                {
                    this.delete_hits.Text = s.Value;
                    delete2 = Convert.ToDecimal(s.Value);
                }
                if (s.Key == "delete_misses")
                {
                    this.delete_misses.Text = s.Value;
                    delete3 = Convert.ToDecimal(s.Value);
                }

                if (s.Key == "decr_hits")
                {
                    this.decr_hits.Text = s.Value;
                    decr2 = Convert.ToDecimal(s.Value);
                }
                if (s.Key == "decr_misses")
                {
                    this.decr_misses.Text = s.Value;
                    decr3 = Convert.ToDecimal(s.Value);
                }
                if (s.Key == "cas_hits")
                {
                    this.cas_hits.Text = s.Value;
                    cas2 = Convert.ToDecimal(s.Value);
                }
                if (s.Key == "cas_misses")
                {
                    this.cas_misses.Text = s.Value;
                    cas3 = Convert.ToDecimal(s.Value);
                }


                if (s.Key == "incr_hits")
                {
                    this.incr_hits.Text = s.Value;
                    incr2 = Convert.ToDecimal(s.Value);
                }
                if (s.Key == "incr_misses")
                {
                    this.incr_misses.Text = s.Value;
                    incr3 = Convert.ToDecimal(s.Value);
                }

            }
            delete1 = delete2 + delete3;
            cas1 = cas2 + cas3;
            incr1 = incr2 + incr3;
            decr1 = decr2 + decr3;
            this.cmd_cas.Text = cas1.ToString();
            this.cmd_decr.Text = decr1.ToString();
            this.cmd_incr.Text = incr1.ToString();
            this.cmd_delete.Text = delete1.ToString();
            this.get_hitting.Text = get1 == 0 ? "0%" : ((get2 / get1) * 100).ToString("0.00") + "%";
            this.delete_hitting.Text = delete1 == 0 ? "0%" : ((delete2 / delete1) * 100).ToString("0.00") + "%";
            this.cas_hitting.Text = cas1 == 0 ? "0%" : ((cas2 / cas1) * 100).ToString("0.00") + "%";
            this.incr_hitting.Text = incr1 == 0 ? "0%" : ((incr2 / incr1) * 100).ToString("0.00") + "%";
            this.decr_hitting.Text = decr1 == 0 ? "0%" : ((decr2 / decr1) * 100).ToString("0.00") + "%";
        }

        void MemServerMoniter()
        {
            DataTable dt = GetServerDetials();
            if (dt != null)
            {
                foreach (var s in dicDetials)
                {
                    if (s.Key == "curr_connections")
                        this.label6.Text = s.Value;
                    if (s.Key == "total_connections")
                        this.label7.Text = s.Value;
                    if (s.Key == "cmd_get")
                        this.label8.Text = s.Value;
                    if (s.Key == "cmd_set")
                        this.label9.Text = s.Value;
                    if (s.Key == "total_items")
                        this.label10.Text = s.Value;
                    if (s.Key == "cmd_flush")
                        this.label11.Text = s.Value;
                    if (s.Key == "bytes_read")
                        this.label12.Text = Help.GetFileSize(long.Parse(s.Value));
                    if (s.Key == "bytes_written")
                        this.label13.Text = Help.GetFileSize(long.Parse(s.Value));
                    if (s.Key == "accepting_conns")
                        this.label14.Text = s.Value;
                    if (s.Key == "evictions")
                        this.label15.Text = s.Value;
                    if (s.Key == "curr_items")
                        this.label16.Text = s.Value;
                    if (s.Key == "bytes")
                        this.label17.Text = Help.GetFileSize(long.Parse(s.Value));
                    if (s.Key == "conn_yields")
                        this.label18.Text = s.Value;
                    if (s.Key == "listen_disabled_num")
                        this.label19.Text = s.Value;
                }
            }
        }

        void InitLableCss()
        {
            this.label6.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;

            this.label7.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;

            this.label8.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Red;

            this.label9.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Red;

            this.label10.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Red;

            this.label11.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Red;

            this.label12.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Red;

            this.label13.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Red;

            this.label14.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;

            this.label15.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Red;

            this.label16.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Red;

            this.label17.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Red;

            this.label18.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.Red;

            this.label19.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Red;

        }

        void GetMemServerDetials()
        {
            if (serverList == null || serverList.Count == 0)
                return;
            DataTable dt = GetServerDetials();
            this.dgvDetails.DataSource = dt;
        }

        Dictionary<string, string> dicDetials = null;

        DataTable GetServerDetials()
        {
            dicDetials = new Dictionary<string, string>();
            if (serverList == null || serverList.Count == 0)
                return null;
            ArrayList list = MemCacheOperate.GetMemServerDetials(serverList);
            if (list == null)
            {
                MessageBox.Show("连接到服务器失败！请稍候重试！正在切换服务器~！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BindMemServer();
                if (t != null)
                    t.Stop();
                return null;
            }
            DataTable dt = new DataTable();
            DataColumn dtc = new DataColumn("value", typeof(string));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("zh-cn", typeof(string));
            dt.Columns.Add(dtc);
            dtc = new DataColumn("en-us", typeof(string));
            dt.Columns.Add(dtc);
            string[] tempStr = new string[2];
            DataRow dr;
            foreach (var s in list)
            {
                tempStr = s.ToString().Split(':');
                foreach (var t in listDetials)
                {
                    if (t.en == tempStr[0])
                    {
                        dicDetials.Add(tempStr[0], tempStr[1]);
                        dr = dt.NewRow();
                        if (t.en == "limit_maxbytes" || t.en == "bytes" || t.en == "bytes_written" || t.en == "bytes_read")
                        {
                            dr["value"] = Help.GetFileSize(long.Parse(tempStr[1]));
                        }
                        else
                        {
                            dr["value"] = tempStr[1];
                        }
                        dr["zh-cn"] = t.cn;
                        dr["en-us"] = tempStr[0];
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        private void btnSearchByKey_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvkeysSource.DataSource;
            if (dt != null)
            {
                dt.Rows.Clear();
                dgvkeysSource.DataSource = dt;
            }
            dt = CreateDataTable();
            string searchKey = this.txtKeys.Text;
            List<string> keys = new List<string>();
            if (!string.IsNullOrEmpty(searchKey))
            {
                string[] allKeys = searchKey.Split(',');
                keys = new List<string>(allKeys);
            }
            Dictionary<string, object> dicSource = MemCacheOperate.Get(keys, serverList);

            DataRow dr;
            foreach (var s in dicSource)
            {
                dr = dt.NewRow();
                dr["key"] = s.Key;
                dr["value"] = s.Value;
                dt.Rows.Add(dr);
            }
            this.dgvkeysSource.DataSource = dt;
        }

        public void TabControlBind(string tp)
        {
            switch (tp)
            {
                case "0":
                    GetMemServerDetials();
                    break;
                case "1":
                    InitLableCss();
                    MemServerMoniter();
                    break;
                case "2":
                    Asyncdelegate isgt = new Asyncdelegate(BindSlabSource);
                    IAsyncResult ar = isgt.BeginInvoke(new AsyncCallback(CallbackMethod), isgt);
                    break;
                case "3":
                    break;
                case "4":
                    ShootingMemServer();
                    break;
                case "5":
                    BindAllMemServerDetials();
                    break;
                case "6":
                    break;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.tabControl1.SelectedIndex;
            TabControlBind(index.ToString());
        }

        System.Windows.Forms.Timer t;

        private void btnStart_Click(object sender, EventArgs e)
        {
            string time = this.txtTime.Text;
            if (string.IsNullOrEmpty(time))
            {
                MessageBox.Show("请输入刷新频率！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int interval = Convert.ToInt32(time) * 1000;
            t = new System.Windows.Forms.Timer();
            t.Interval = interval;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            this.cbMemServer.Enabled = false;
            this.lbltime.Visible = true;
        }

        void t_Tick(object sender, EventArgs e)
        {
            MemServerMoniter();
            this.lbltime.Text = "上一次刷新时间：" + DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.cbMemServer.Enabled = true;
            t.Stop();
            this.lbltime.Text = "";
        }


        DataTable CreateTalbe(string[] cols)
        {
            DataTable dt = new DataTable();
            DataColumn dc = null;
            foreach (var s in cols)
            {
                dc = new DataColumn(s, typeof(string));
                dt.Columns.Add(dc);
            }
            return dt;
        }

        void BindAllMemServerDetials()
        {
            ArrayList delialsList;
            ArrayList serverList = new ArrayList();
            string[] cols = { "serverid", "pid", "uptime", "version", "curr_connections", "cmd_get", "cmd_set", "cmd_flush", "delete_hits", "bytes_read", "bytes_written", "limit_maxbytes", "threads", "bytes" };
            DataTable dt = CreateTalbe(cols);
            DataRow dr;
            foreach (var server in list)
            {
                serverList = new ArrayList();
                serverList.Add(server.ServerID);
                delialsList = MemCacheOperate.GetMemServerDetials(serverList);
                if (delialsList == null)
                {

                    MessageBox.Show(serverList[0] + "连接到服务器失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }
                string[] tempStr = new string[2];
                dr = dt.NewRow();
                foreach (var t in delialsList)
                {
                    tempStr = t.ToString().Split(':');
                    dr["serverid"] = server.ServerID;
                    if (tempStr[0] == "pid")
                        dr["pid"] = tempStr[1];
                    if (tempStr[0] == "uptime")
                        dr["uptime"] = tempStr[1];
                    if (tempStr[0] == "version")
                        dr["version"] = tempStr[1];
                    if (tempStr[0] == "curr_connections")
                        dr["curr_connections"] = tempStr[1];
                    if (tempStr[0] == "cmd_get")
                        dr["cmd_get"] = tempStr[1];
                    if (tempStr[0] == "cmd_set")
                        dr["cmd_set"] = tempStr[1];
                    if (tempStr[0] == "cmd_flush")
                        dr["cmd_flush"] = tempStr[1];
                    if (tempStr[0] == "delete_hits")
                        dr["delete_hits"] = tempStr[1];
                    if (tempStr[0] == "bytes_read")
                        dr["bytes_read"] = Help.GetFileSize(long.Parse(tempStr[1]));
                    if (tempStr[0] == "bytes_written")
                        dr["bytes_written"] = Help.GetFileSize(long.Parse(tempStr[1]));
                    if (tempStr[0] == "limit_maxbytes")
                        dr["limit_maxbytes"] = Help.GetFileSize(long.Parse(tempStr[1]));
                    if (tempStr[0] == "threads")
                        dr["threads"] = tempStr[1];
                    if (tempStr[0] == "bytes")
                        dr["bytes"] = Help.GetFileSize(long.Parse(tempStr[1]));
                }
                dt.Rows.Add(dr);
            }
            this.dgvAllServerDetials.DataSource = dt;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            MemCacheTools_V2 tools = new MemCacheTools_V2((string[])serverList.ToArray(typeof(string)));
            string key = this.txtkey.Text;
            string value = this.txtValue.Text;
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Key不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Value不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tools.Add(key, value);
            MessageBox.Show("写入数据成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.txtkey.Text = this.txtValue.Text = "";
        }

        private void 添加MemcacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmServer fs = new frmServer(this);
            fs.ShowDialog();
        }

        private void dgvSource_DoubleClick(object sender, EventArgs e)
        {
            if (dgvSource.CurrentRow == null)
            {
                return;
            }
            int rowIndex = this.dgvSource.CurrentRow.Index;//获取行的索引
            string key = this.dgvSource.Rows[rowIndex].Cells[0].Value.ToString();
            string value = this.dgvSource.Rows[rowIndex].Cells[1].Value.ToString();
            DataTable dt = (DataTable)dgvSource.DataSource;
            frmKeyValueDetials fkvd = new frmKeyValueDetials(key, value, serverList, dt, rowIndex);
            fkvd.dgv = dgvSource;
            fkvd.frm = this;
            fkvd.ShowDialog();
        }

        private void dgvkeysSource_DoubleClick(object sender, EventArgs e)
        {
            if (dgvkeysSource.CurrentRow == null)
            {
                return;
            }
            DataTable dt = (DataTable)dgvkeysSource.DataSource;
            int rowIndex = this.dgvkeysSource.CurrentRow.Index;//获取行的索引
            string key = this.dgvkeysSource.Rows[rowIndex].Cells[0].Value.ToString();
            string value = this.dgvkeysSource.Rows[rowIndex].Cells[1].Value.ToString();
            frmKeyValueDetials fkvd = new frmKeyValueDetials(key, value, serverList, dt, rowIndex);
            fkvd.dgv = dgvkeysSource;
            fkvd.frm = this;
            fkvd.ShowDialog();
        }

        private void 备份ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBackUp fb = new frmBackUp(this);
            fb.ShowDialog();
        }

        private void 还原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecover fr = new frmRecover(this);
            fr.ShowDialog();
        }
        public void WriteState(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new WriteStateDelegate(WriteState), new object[] { text });
            }
            else
            {
                this.lblStateMessage.Text = text;
            }
        }

        void GetMailUser(string serverId, out string toUser, out string copyUser)
        {
            toUser = "";
            copyUser = "";
            string iniPath = Application.StartupPath + "\\" + "UserMailConfig.ini";
            INIHelper ini = new INIHelper(iniPath);
            string section = "ServerID=" + serverId + "";
            toUser = ini.IniReadValue(section, "ToUser");
            copyUser = ini.IniReadValue(section, "CoypUser");
        }

        public void CheckMemServerState()
        {
            while (true)
            {
                List<MemServer> allServer = MemServer.GetAllValidServer();
                List<IPEndPoint> list = new List<IPEndPoint>();
                if (allServer == null)
                    return;
                string ip = string.Empty;
                int port = 0;
                foreach (var s in allServer)
                {
                    ip = s.ServerID.Split(':')[0];
                    port = Convert.ToInt32(s.ServerID.Split(':')[1]);
                    list.Add(new IPEndPoint(IPAddress.Parse(ip), port));
                }
                MemcachedMonitor monitor = new MemcachedMonitor(list);
                List<MemcachedServerStats> resultList = monitor.GetAllServerStats();
                string result = string.Empty;
                string toUser = string.Empty;
                string copyUser = string.Empty;
                string errIp = string.Empty;
                string serverId = string.Empty;
                int errPort = 0;
                string content = "MemCache 服务器{0}在{1} 检测连接失败，请远程登录查看！";
                foreach (MemcachedServerStats stats in resultList)
                {
                    errIp = stats.IPEndPoint.Address.ToString();
                    errPort = stats.IPEndPoint.Port;
                    serverId = errIp + ":" + errPort;
                    if (!stats.IsReachable)
                    {
                        GetMailUser(serverId, out toUser, out copyUser);
                        //发送邮件
                        WriteState(serverId + "测试连接失败，正在发送邮件！");
                        MailHelper.SendEmail(toUser, copyUser, serverId + "检测连接失败", string.Format(content, serverId, DateTime.Now));
                        WriteState(serverId + "发送邮件成功！");
                    }
                    else
                    {
                        WriteState(serverId + "测试连接成功！");
                    }
                }
                Thread.Sleep(sleep);
                WriteState("消息：");
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出程序吗？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                Application.ExitThread();
                System.Environment.Exit(System.Environment.ExitCode);
                Application.Exit();
                this.Close();
            }
            else
                e.Cancel = true;
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }
        }

        private void btnSaveToMemCache_Click(object sender, EventArgs e)
        {

            MemCacheTools_V2 tools = new MemCacheTools_V2((string[])serverList.ToArray(typeof(string)));
            string key = this.txtKeyAdd.Text;
            string value = this.txtValueAdd.Text;
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Key不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Value不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确定要添加数据到服务器" + serverList[0].ToString() + "?", "提示信息", MessageBoxButtons.YesNo).ToString() == "Yes")
            {
                tools.Add(key, value);
                MessageBox.Show("写入数据成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtKeyAdd.Text = this.txtValueAdd.Text = "";
            }
        }

        Color color = System.Drawing.Color.Wheat;
        private void dgvDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.dgvDetails.Rows.Count != 0)
            {
                for (int i = 0; i < this.dgvDetails.Rows.Count;)
                {
                    this.dgvDetails.Rows[i].DefaultCellStyle.BackColor = color;
                    i += 2;
                }
            }

        }

        private void dgvSource_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.dgvSource.Rows.Count != 0)
            {
                for (int i = 0; i < this.dgvSource.Rows.Count;)
                {
                    this.dgvSource.Rows[i].DefaultCellStyle.BackColor = color;
                    i += 2;
                }
            }
        }

        private void dgvAllServerDetials_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.dgvAllServerDetials.Rows.Count != 0)
            {
                for (int i = 0; i < this.dgvAllServerDetials.Rows.Count;)
                {
                    this.dgvAllServerDetials.Rows[i].DefaultCellStyle.BackColor = color;
                    i += 2;
                }
            }
        }

        private void dgvkeysSource_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.dgvkeysSource.Rows.Count != 0)
            {
                for (int i = 0; i < this.dgvkeysSource.Rows.Count;)
                {
                    this.dgvkeysSource.Rows[i].DefaultCellStyle.BackColor = color;
                    i += 2;
                }
            }
        }

        public void ChangeRowSelect(DataGridView dgv, int posIndex)
        {
            dgv.ClearSelection();
            dgv.Rows[posIndex].Selected = true;
        }

    }
}
