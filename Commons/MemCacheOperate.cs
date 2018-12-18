using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Commons
{
    public static class MemCacheOperate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverList"></param>
        /// <returns></returns>
        public static ArrayList GetAllItems(ArrayList serverList)
        {
            ArrayList itemarr = new ArrayList();
            ArrayList arr = MemCachedManager.GetStats(serverList, MemCachedManager.Stats.Items, null);
            int host = Convert.ToInt32(serverList[0].ToString().Split(':')[1]);
            if (arr == null)
            {
                return null;
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
                    if (!find && item_id > 0 && item_id != host)
                        itemarr.Add(item_id);
                }
            }
            return itemarr;
        }
        /// <summary>
        /// 获取MemCache 详细信息
        /// </summary>
        /// <param name="serverList"></param>
        /// <returns></returns>
        public static ArrayList GetMemServerDetials(ArrayList serverList)
        {
            ArrayList arrlist = MemCachedManager.GetStats(serverList, MemCachedManager.Stats.Detail, null);
            return arrlist;
        }

        /// <summary>
        /// 获取一个ItemKey
        /// </summary>
        /// <param name="serverList">MemCache服务器</param>
        /// <param name="item">Item</param>
        /// <param name="count">个数，0表示所有</param>
        /// <returns></returns>
        public static List<string> GetOneItemKey(ArrayList serverList, int item, int count)
        {
            List<string> allKeys = new List<string>();
            ArrayList itemKey = MemCachedManager.GetStats(serverList, MemCachedManager.Stats.CachedDump, "" + item + " " + count + "");
            foreach (string key in itemKey)
            {
                if (key == serverList[0].ToString())
                    continue;
                allKeys.Add(key.Split(':')[0]);
            }
            return allKeys;
        }
        public static int GetTotalCountBySlab(ArrayList serverList, int item)
        {
            ArrayList items = MemCachedManager.GetStats(serverList, MemCachedManager.Stats.CachedDump, "" + item + " 0");
            return items.Count;
        }
        /// <summary>
        /// 获取Memcache 数据
        /// </summary>
        /// <param name="keys"> </param>
        /// <param name="serverList">memcache</param>
        /// <returns></returns>
        public static Dictionary<string, object> Get(List<string> keys, ArrayList serverList)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string[] serverlist = (string[])serverList.ToArray(typeof(string));
            MemCacheTools_V2 tools = new MemCacheTools_V2(serverlist);
            foreach (string key in keys)
            {
                object obj = tools.Get(key);
                if (!dic.ContainsKey(key) && obj != null)
                    dic.Add(key, obj);
            }
            return dic;
        }
    }
}
