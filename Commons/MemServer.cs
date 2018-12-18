using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Commons;
using System.Collections;
using System.Net;
using System.Xml.Linq;

namespace MemCacheMonitor
{
    public class MemServer
    {
        private static XDocument doc = new XDocument();
        public string filePath = "MemServerInfo.xml";
        public string ServerName { get; set; }
        public String ServerID { get; set; }
        public String ServerDescr { get; set; }
        public string Valid { get; set; }
        public DateTime? UpdateTime { get; set; }

        public static List<MemServer> GetAllValidServer()
        {
            //string sql = "select * from tb_MemServer where valid=1";
            //DataTable dt = SQLiteHelper.ExecuteQuery(CommandType.Text, sql, null).Tables[0];
            //List<MemServer> list = ConvertHelper<MemServer>.ConvertToList(dt);
            //return list;  
            return GetAll().FindAll(x=>x.Valid=="1");
        }

        public static List<MemServer> GetServerList()
        {
            //string sql = "select * from tb_MemServer where valid=1";
            //DataTable dt = SQLiteHelper.ExecuteQuery(CommandType.Text, sql, null).Tables[0];
            //List<MemServer> list = ConvertHelper<MemServer>.ConvertToList(dt);
            List<MemServer> list = GetAll().FindAll(x => x.Valid == "1");
            List<MemServer> successlist = new List<MemServer>();
            foreach (var s in list)
            {
                if (GetOneMemServerState(s.ServerID))
                {
                    successlist.Add(s);
                }
            }
            return successlist;
        }

        //首次加载判断服务器是否有效
        public static bool GetOneMemServerState(string ipall)
        {
            bool flag = false;
            List<IPEndPoint> list = new List<IPEndPoint>();
            string ip = ipall.Split(':')[0];
            int port = Convert.ToInt32(ipall.Split(':')[1]);
            list.Add(new IPEndPoint(IPAddress.Parse(ip), port));
            MemcachedMonitor monitor = new MemcachedMonitor(list);
            List<MemcachedServerStats> resultList = monitor.GetAllServerStats();
            foreach (MemcachedServerStats stats in resultList)
            {
                flag = stats.IsReachable;
                break;
            }
            return flag;
        }
        public static String GetAllServerID()
        {
            string strServer = string.Empty;
            List<MemServer> servers = MemServer.GetServerList();
            //GetMemServerState(servers);
            foreach (var s in servers)
            {
                strServer += s.ServerID + ",";
            }
            return strServer.TrimEnd(",".ToCharArray());
        }

        public MemServer()
        {
            doc = XDocument.Load(filePath);
        }
        public MemServer(string filepath)
            : this()
        {

            filePath = filepath;
        }
        public bool Add(string ServerID, string ServerName, string Valid, string ServerDescr)
        {

            XElement db =
                new XElement("MemServer",
               new XElement("ServerID", ServerID),
               new XElement("ServerName", ServerName),
               new XElement("Valid", Valid),
               new XElement("ServerDescr", ServerDescr)
               );
            try
            {
                doc.Element("myxml").Add(db);
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(string id)
        {
            XElement xe = (from db in doc.Descendants("MemServer") where db.Element("ServerID").Value == id select db).Single() as XElement;
            try
            {
                xe.Remove();
                doc.Save(filePath);
                return true;
            }
            catch
            {

                return false;
            }
        }
        public bool Modify(string ServerID, string ServerName, string Valid, string ServerDescr)
        {

            XElement xe = (from db in doc.Descendants("MemServer") where db.Element("ServerID").Value.ToString() == ServerID select db).Single();
            try
            {
                xe.Element("ServerName").Value = ServerName;
                xe.Element("Valid").Value = Valid;
                xe.Element("ServerDescr").Value = ServerDescr;
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static List<MemServer> GetAll()
        {
            try
            {
                doc = XDocument.Load("MemServerInfo.xml");
                List<MemServer> dbs = (from db in doc.Descendants("MemServer")
                                       select new MemServer
                                             {
                                                 ServerID = db.Element("ServerID").Value.ToString(),
                                                 ServerName = db.Element("ServerName").Value.ToString(),
                                                 ServerDescr = db.Element("ServerDescr").Value.ToString(),
                                                 Valid = db.Element("Valid").Value.ToString()
                                             }).ToList();
                return dbs;
            }
            catch
            {

            }
            return null;
        }

    }

    public class MemServerDetitals
    {
        public string id { get; set; }
        public string en { get; set; }
        public string cn { get; set; }

        public static List<MemServerDetitals> GetMemServerDetitalsList(string connstring)
        {
            string sql = "select * from tb_MemServerDetials";
            DataTable dt = SQLiteHelper.ExecuteQuery(connstring,CommandType.Text, sql, null).Tables[0];
            List<MemServerDetitals> list = ConvertHelper<MemServerDetitals>.ConvertToList(dt);
            return list;
        }
    }
}
