using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Commons
{
    public class MemcachedMonitor
    {
        /// <summary>  
        /// 连接Memcached的超时时间  
        /// </summary>  
        public TimeSpan ConnectionTimeout { get; set; }
        /// <summary>  
        /// 接收Memcached返回数据的超时时间  
        /// </summary>  
        public TimeSpan ReceiveTimeout { get; set; }
        private List<IPEndPoint> serverList;
        public MemcachedMonitor(ICollection<IPEndPoint> list)
        {
            ConnectionTimeout = TimeSpan.FromSeconds(100);
            ReceiveTimeout = TimeSpan.FromSeconds(200);
            serverList = new List<IPEndPoint>();
            serverList.AddRange(list);
        }

        public List<MemcachedServerStats> GetAllServerStats()
        {
            List<MemcachedServerStats> resultList = new List<MemcachedServerStats>();
            foreach (IPEndPoint endPoint in serverList)
            {
                resultList.Add(GetServerStats(endPoint, ConnectionTimeout, ReceiveTimeout));
            }
            return resultList;
        }

        public static MemcachedServerStats GetServerStats(IPEndPoint ip, TimeSpan connectionTimeout, TimeSpan receiveTimeout)
        {
            MemcachedSocket socket = new MemcachedSocket(ip, connectionTimeout, receiveTimeout);
            MemcachedServerStats stats = socket.GetStats();
            return stats;
        }

        public static IPEndPoint Parse(string hostName, int port)
        {
            IPHostEntry host = Dns.GetHostEntry(hostName);
            IPEndPoint endPoint = null;
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    endPoint = new IPEndPoint(ip, port);
                    break;
                }
            }
            return endPoint;
        }

    }
}
