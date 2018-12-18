using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Memcached.ClientLibrary;
using Ionic.Zlib;
using MemCacheMonitor;


public class MemcachedTools
{
    static List<string> memcacheList = new List<string>();

    public static string Servers
    {
        get
        {
            return MemServer.GetAllServerID();
        }
    }
    static MemcachedTools()
    {
        string[] serverarr = Servers.Split(',');
        if (serverarr.Length > 0)
        {
            String[] serverlist = new string[] { serverarr[0] };
            SockIOPool pool = SockIOPool.GetInstance("news");
            pool.SocketTimeout = 10000;
            pool.SocketConnectTimeout = 5000;
            pool.SetServers(serverlist);
            pool.Initialize();
            for (int i = 0; i < serverarr.Length; i++)
            {
                string name = "news" + i.ToString();
                SockIOPool pooltemp = SockIOPool.GetInstance("news" + i.ToString());
                serverlist = new string[] { serverarr[i] };
                pooltemp.SocketTimeout = 10000;
                pooltemp.SocketConnectTimeout = 5000;
                pooltemp.SetServers(serverlist);
                pooltemp.Initialize();
                memcacheList.Add(name);
            }
        }
    }


    private static MemcachedClient NewsClient
    {
        get
        {
            MemcachedClient mc = new MemcachedClient();
            mc.DefaultEncoding = "utf-8";
            mc.EnableCompression = false;
            mc.PrimitiveAsString = true;
            mc.PoolName = "news";
            return mc;
        }
    }

    private static MemcachedClient NewsClientGB
    {
        get
        {
            MemcachedClient mc = new MemcachedClient();
            mc.DefaultEncoding = "gb2312";
            mc.EnableCompression = false;
            mc.PrimitiveAsString = true;
            mc.PoolName = "news";
            return mc;
        }
    }

    private static MemcachedClient NewsClientImg
    {
        get
        {
            MemcachedClient mc = new MemcachedClient();
            mc.EnableCompression = false;
            mc.PrimitiveAsString = true;
            mc.PoolName = "news";
            return mc;
        }
    }

    public static MemcachedClient GetClientByName(string name)
    {
        MemcachedClient mc = new MemcachedClient();
        mc.DefaultEncoding = "utf-8";
        mc.EnableCompression = false;
        mc.PrimitiveAsString = true;
        mc.PoolName = name;
        return mc;
    }

    public static MemcachedClient GetClientGBByName(string name)
    {
        MemcachedClient mc = new MemcachedClient();
        mc.DefaultEncoding = "gb2312";
        mc.EnableCompression = false;
        mc.PrimitiveAsString = true;
        mc.PoolName = name;
        return mc;
    }

    /// <summary>
    /// 添加一个缓存
    /// </summary>
    /// <param name="obj">缓存对象</param>
    /// <param name="sj">缓存时间</param>
    public static void Add(string key, object obj, DateTime expiry)
    {
        foreach (var c in memcacheList)
        {
            var client = GetClientByName(c);
            if (!client.Add(key, obj, expiry))
            {
                client.Set(key, obj, expiry);
            }
        }
    }

    public static void Add(string key, object obj)
    {
        foreach (var c in memcacheList)
        {
            var client = GetClientByName(c);
            if (!client.Add(key, obj, 0))
            {
                client.Set(key, obj, 0);
            }
        }
    }
    public static void Add(string key, object obj, List<string> memcacheList)
    {
        foreach (var c in memcacheList)
        {
            var client = GetClientByName(c);
            if (!client.Add(key, obj, 0))
            {
                client.Set(key, obj, 0);
            }
        }
    }

    public static void Clear(string key)
    {
        foreach (var c in memcacheList)
        {
            var client = GetClientByName(c);
            client.Delete(key);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static object Get(string key)
    {
        object obj = NewsClient.Get(key);
        if (obj == null)
        {
            foreach (var c in memcacheList)
            {
                var client = GetClientByName(c);
                obj = client.Get(key);
                if (obj != null)
                {
                    break;
                }
            }
        }
        return obj;
    }

    public static object Get(string key, List<string> memcacheList)
    {
        object obj = NewsClient.Get(key);
        if (obj == null)
        {
            foreach (var c in memcacheList)
            {
                var client = GetClientByName("news");
                obj = client.Get(key);
                if (obj != null)
                {
                    break;
                }
            }
        }
        return obj;
    }
    public static object[] Get(string[] keys)
    {
        object[] objs = NewsClient.GetMultipleArray(keys);
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i] == null)
            {
                objs[i] = NewsClient.Get(keys[i]);
                if (objs[i] == null)
                {
                    foreach (var c in memcacheList)
                    {
                        var client = GetClientByName(c);
                        objs[i] = client.Get(keys[i]);
                        if (objs[i] != null)
                        {
                            break;
                        }
                    }
                }
            }
        }
        return objs;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static object GetHQ(string key)
    {
        object obj = NewsClientGB.Get(key);
        if (obj == null)
        {
            foreach (var c in memcacheList)
            {
                var client = GetClientGBByName(c);
                obj = client.Get(key);
                if (obj != null)
                {
                    break;
                }
            }
        }
        return obj;
    }

    public static object GetImg(string key)
    {
        object obj = NewsClientImg.Get(key);
        if (obj == null)
        {
            foreach (var c in memcacheList)
            {
                var client = GetClientByName(c);
                obj = client.Get(key);
                if (obj != null)
                {
                    break;
                }
            }
        }
        return obj;
    }

    public static object[] GetHQ(string[] keys)
    {
        object[] objs = NewsClientGB.GetMultipleArray(keys);
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i] == null)
            {
                objs[i] = NewsClientGB.Get(keys[i]);
                if (objs[i] == null)
                {
                    foreach (var c in memcacheList)
                    {
                        var client = GetClientGBByName(c);
                        objs[i] = client.Get(keys[i]);
                        if (objs[i] != null)
                        {
                            break;
                        }
                    }
                }
            }
        }
        return objs;
    }

    static String MemoryStreamToString(System.IO.MemoryStream ms)
    {
        byte[] ByteArray = ms.ToArray();
        //return System.Text.Encoding.ASCII.GetString(ByteArray);

        StringBuilder sb = new StringBuilder();
        //sb.Append(System.Text.UnicodeEncoding.Default.GetChars(ByteArray));
        sb.Append(System.Text.UTF8Encoding.UTF8.GetChars(ByteArray));
        return sb.ToString();
    }

    static void CopyStream(System.IO.Stream src, System.IO.Stream dest)
    {
        byte[] buffer = new byte[1024 * 1024];            //这里大小=====>为memcache中value的最大限制值
        int len = src.Read(buffer, 0, buffer.Length);
        while (len > 0)
        {
            dest.Write(buffer, 0, len);
            len = src.Read(buffer, 0, buffer.Length);
        }
        dest.Flush();
    }
}

