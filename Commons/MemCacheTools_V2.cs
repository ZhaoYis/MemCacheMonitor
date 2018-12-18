using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons
{
    public class MemCacheTools_V2
    {

        static List<string> memcacheList = new List<string>();
        public static string[] serverlist = new string[1];
        static MemcachedClient mc;
        static SockIOPool pool;
        public MemCacheTools_V2(string[] server)
        {
            serverlist = server;
            pool = SockIOPool.GetInstance();
            pool.SetServers(serverlist);//添加服务器列表          
            pool.InitConnections = 3;//设置连接池初始数目          
            pool.MinConnections = 3;//设置最小连接数目          
            pool.MaxConnections = 5;//设置最大连接数目          
            pool.SocketConnectTimeout = 1000;//设置连接的套接字超时。        
            pool.SocketTimeout = 3000;//设置套接字超时读取           
            pool.MaintenanceSleep = 30;//设置维护线程运行的睡眠时间。如果设置为0，那么维护线程将不会启动;          
            //获取或设置池的故障标志。            
            //如果这个标志被设置为true则socket连接失败，            
            //将试图从另一台服务器返回一个套接字如果存在的话。          
            //如果设置为false，则得到一个套接字如果存在的话。否则返回NULL，如果它无法连接到请求的服务器。         
            pool.Failover = true;
            //如果为false，对所有创建的套接字关闭Nagle的算法。         
            pool.Nagle = false;
            pool.Initialize();
            string name = serverlist[0];
            SockIOPool pooltemp = SockIOPool.GetInstance(serverlist[0]);
            pooltemp.SocketTimeout = 10000;
            pooltemp.SocketConnectTimeout = 5000;
            pooltemp.SetServers(serverlist);
            pooltemp.Initialize();
            if (!memcacheList.Contains(name))
                memcacheList.Add(name);
        }
        private MemcachedClient NewsClient
        {
            get
            {
                mc = GetClientByName(serverlist[0]);
                return mc;
            }
        }
        public MemcachedClient GetClientByName(string name)
        {
            mc = new MemcachedClient(name);
            mc.DefaultEncoding = "utf-8";
            mc.EnableCompression = false;
            mc.PrimitiveAsString = true;
            mc.PoolName = name;


            pool.Initialize();
            return mc;
        }
        /// <summary>
        /// 添加一个缓存
        /// </summary>
        /// <param name="obj">缓存对象</param>
        /// <param name="sj">缓存时间</param>
        public void Add(string key, object obj, DateTime expiry)
        {
            NewsClient.Set(key, obj, expiry);
            //foreach (var c in memcacheList)
            //{
            //    var client = GetClientByName(c);
            //    if (!client.Add(key, obj, expiry))
            //    {
            //        client.Set(key, obj, expiry);
            //    }
            //}
        }

        public void Add(string key, object obj)
        {
            //foreach (var c in memcacheList)
            //{
            //    var client = GetClientByName(c);
            //    if (!client.Add(key, obj, 0))
            //    {
            //        client.Set(key, obj, 0);
            //    }
            //}
            NewsClient.Set(key, obj, 0);
        }
        public void Add(string key, object obj, List<string> memcacheList)
        {
            NewsClient.Set(key, obj, 0);
            //foreach (var c in memcacheList)
            //{
            //    var client = GetClientByName(c);
            //    if (!client.Add(key, obj, 0))
            //    {
            //        client.Set(key, obj, 0);
            //    }
            //}
        }

        public void Clear(string key)
        {
            NewsClient.Delete(key);
            //foreach (var c in memcacheList)
            //{
            //    var client = GetClientByName(c);
            //    client.Delete(key);
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            object obj = NewsClient.Get(key);
            return obj;
        }

    }
}

