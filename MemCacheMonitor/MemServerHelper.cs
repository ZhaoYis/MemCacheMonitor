using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MemCacheMonitor
{
    public class MemServerHelper
    {
        private static XDocument doc = new XDocument();

        public static string filePath = ".\\MemServerInfo.xml";

        public string ServerID { get; set; }
        public string ServerName { get; set; }
        public string Valid { get; set; }
        public string ServerDescr { get; set; }

        public MemServerHelper()
        {

            doc = XDocument.Load(filePath);
        }

        public MemServerHelper(string filepath)
            : this()
        {

            filePath = filepath;

        }
        public bool Add()
        {

            XElement db = new XElement("MemServer",
               new XAttribute("ServerID", ServerID),
               new XElement("ServerName", new XAttribute("value", ServerName)),
               new XElement("Valid", new XAttribute("value", Valid)),
               new XElement("ServerDescr", new XAttribute("value", ServerDescr))
               );

            try
            {

                doc.Element("MemServer").Add(db);
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Remove(string id)
        {
            XElement xe = (from db in doc.Element("myxml").Elements("MemServer") where db.Attribute("id").Value == id select db).Single() as XElement;
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
        public bool Modify()
        {

            XElement xe = (from db in doc.Element("myxml").Elements("MemServer") where db.Attribute("id").Value.ToString() == ServerID select db).Single();
            try
            {
                xe.Element("ServerName").Attribute("ServerName").Value = ServerName;
                xe.Element("Valid").Attribute("Valid").Value = Valid;
                xe.Element("ServerDescr").Attribute("ServerDescr").Value = ServerDescr;
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<MemServerHelper> GetAll()
        {
            List<MemServerHelper> dbs = (from db in doc.Element("myxml").Elements("MemServer")
                                         select new MemServerHelper
                                         {
                                             ServerID = db.Attribute("id").Value.ToString(),
                                             ServerName = db.Element("company").Attribute("value").Value.ToString(),
                                             ServerDescr = db.Element("server").Attribute("value").Value.ToString(),
                                             Valid = db.Element("database").Attribute("value").Value.ToString()
                                         }).ToList();
            return dbs;
        }
    }
}
