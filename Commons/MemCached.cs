using System.Collections;

using Commons;
using System.Configuration;
using MemCacheMonitor;
using System;


/// <summary>
/// MemCache管理操作类
/// </summary>
public sealed class MemCachedManager
{
    #region 静态方法和属性
    private static MemcachedClient mc = null;

    private static SockIOPool pool = null;

    //private static MemCachedConfigInfo memCachedConfigInfo = MemCachedConfigs.GetConfig();    
    public static string[] serverList = MemServer.GetAllServerID().Split(',');

    static MemCachedManager()
    {
        CreateManager();
    }

    private static void CreateManager()
    {
        try
        {
            SockIOPool pool = SockIOPool.GetInstance("news");
            pool.SocketTimeout = 10000;
            pool.SocketConnectTimeout = 5000;
            pool.SetServers(serverList);
            pool.Initialize();
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// 缓存服务器地址列表
    /// </summary>
    public static string[] ServerList
    {
        set
        {
            if (value != null)
                serverList = value;
        }
        get { return serverList; }
    }

    /// <summary>
    /// 客户端缓存操作对象
    /// </summary>
    public static MemcachedClient CacheClient
    {
        get
        {
            if (mc == null)
                CreateManager();

            return mc;
        }
    }



    /// <summary>
    /// 获取服务器端缓存的数据信息
    /// </summary>
    /// <returns>返回信息</returns>
    public static ArrayList GetStats()
    {
        ArrayList arrayList = new ArrayList();
        foreach (string server in serverList)
        {
            arrayList.Add(server);
        }
        return GetStats(arrayList, Stats.Default, null);
    }

    /// <summary>
    /// 获取服务器端缓存的数据信息
    /// </summary>
    /// <param name="serverArrayList">要访问的服务列表</param>
    /// <returns>返回信息</returns>
    public static ArrayList GetStats(ArrayList serverArrayList, Stats statsCommand, string param)
    {
        ArrayList statsArray = new ArrayList();
        param = string.IsNullOrEmpty(param) ? "" : param.ToLower();

        string commandstr = "stats";
        //转换stats命令参数
        switch (statsCommand)
        {
            case Stats.Reset: { commandstr = "stats reset"; break; }
            case Stats.Malloc: { commandstr = "stats malloc"; break; }
            case Stats.Maps: { commandstr = "stats maps"; break; }
            case Stats.Sizes: { commandstr = "stats sizes"; break; }
            case Stats.Slabs: { commandstr = "stats slabs"; break; }
            case Stats.Items: { commandstr = "stats items"; break; }
            case Stats.States: { commandstr = "stats states"; break; }
            case Stats.CachedDump:
                {
                    string[] statsparams = Utils.SplitString(param, " ");
                    if (statsparams.Length == 2)
                        if (Utils.IsNumericArray(statsparams))
                            commandstr = "stats cachedump  " + param;

                    break;
                }
            case Stats.Detail:
                {
                    if (string.Equals(param, "on") || string.Equals(param, "off") || string.Equals(param, "dump"))
                        commandstr = "stats detail " + param.Trim();

                    break;
                }
            default: { commandstr = "stats"; break; }
        }
        //加载返回值
        string message = "";
        Hashtable stats = MemCachedManager.CacheClient.Stats(serverArrayList, commandstr, out message);
        if (message != "")
        {
            return null;
        }
        foreach (string key in stats.Keys)
        {
            statsArray.Add(key);
            Hashtable values = (Hashtable)stats[key];
            foreach (string key2 in values.Keys)
            {
                statsArray.Add(key2 + ":" + values[key2]);
            }
        }
        return statsArray;
    }

    /// <summary>
    /// Stats命令行参数
    /// </summary>
    public enum Stats
    {
        /// <summary>
        /// stats : 显示服务器信息, 统计数据等
        /// </summary>
        Default = 0,
        /// <summary>
        /// stats reset : 清空统计数据
        /// </summary>
        Reset = 1,
        /// <summary>
        /// stats malloc : 显示内存分配数据
        /// </summary>
        Malloc = 2,
        /// <summary>
        /// stats maps : 显示"/proc/self/maps"数据
        /// </summary>
        Maps = 3,
        /// <summary>
        /// stats sizes
        /// </summary>
        Sizes = 4,
        /// <summary>
        /// stats slabs : 显示各个slab的信息,包括chunk的大小,数目,使用情况等
        /// </summary>
        Slabs = 5,
        /// <summary>
        /// stats items : 显示各个slab中item的数目和最老item的年龄(最后一次访问距离现在的秒数)
        /// </summary>
        Items = 6,
        /// <summary>
        /// stats cachedump slab_id limit_num : 显示某个slab中的前 limit_num 个 key 列表
        /// </summary>
        CachedDump = 7,
        /// <summary>
        /// stats detail [on|off|dump] : 设置或者显示详细操作记录   on:打开详细操作记录  off:关闭详细操作记录 dump: 显示详细操作记录(每一个键值get,set,hit,del的次数)
        /// </summary>
        Detail = 8,
        /// <summary>
        /// 基本状态信息
        /// </summary>
        States = 9
    }
    #endregion
}

/// <summary>
/// MemCache缓存策略类
/// </summary>
public class MemCachedStrategy : ICacheStrategy
{

    /// <summary>
    /// 添加指定ID的对象
    /// </summary>
    /// <param name="objId"></param>
    /// <param name="o"></param>
    public void AddObject(string objId, object o)
    {
        RemoveObject(objId);
        if (TimeOut > 0)
        {
            MemCachedManager.CacheClient.Set(objId, o, System.DateTime.Now.AddMinutes(TimeOut));
        }
        else
        {
            MemCachedManager.CacheClient.Set(objId, o);
        }
    }

    /// <summary>
    /// 添加指定ID的对象(关联指定文件组)
    /// </summary>
    /// <param name="objId"></param>
    /// <param name="o"></param>
    /// <param name="files"></param>
    public void AddObjectWithFileChange(string objId, object o, string[] files)
    {
        ;
    }

    /// <summary>
    /// 添加指定ID的对象(关联指定键值组)
    /// </summary>
    /// <param name="objId"></param>
    /// <param name="o"></param>
    /// <param name="dependKey"></param>
    public void AddObjectWithDepend(string objId, object o, string[] dependKey)
    {
        ;
    }

    /// <summary>
    /// 移除指定ID的对象
    /// </summary>
    /// <param name="objId"></param>
    public void RemoveObject(string objId)
    {
        if (MemCachedManager.CacheClient.KeyExists(objId))
            MemCachedManager.CacheClient.Delete(objId);
    }

    /// <summary>
    /// 返回指定ID的对象
    /// </summary>
    /// <param name="objId"></param>
    /// <returns></returns>
    public object RetrieveObject(string objId)
    {
        return MemCachedManager.CacheClient.Get(objId);
    }

    /// <summary>
    /// 到期时间
    /// </summary>
    public int TimeOut { set; get; }
}


