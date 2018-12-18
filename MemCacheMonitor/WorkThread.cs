using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using MemCacheMonitor;

public class WorkThread
{
    public frmBackUp frm;
    public int number;
    public ArrayList arrayList;
    public Help help;
    public WorkThread(int item, Help help)
    {
        this.number = item;
        this.help = help;
    }
    // Task t;
    Thread thread;
    public void Run()
    {
        //t = new Task(() => this.Process());
        //t.Start();
        thread = new Thread(() => this.Process());
        thread.Start();
    }

    public void Join()
    {
        thread.Join();
    }
    public bool IsCompleted()
    {
        //if (t != null)
        //    return t.IsCompleted;
        //else return true;
        if (thread != null)
            return !thread.IsAlive;
        else return true;
    }
    public void Process()
    {
        string path = frm.BackPath;
        string IndexPath = frm.BackPath;//文件索引地址
        string filePath = "";
        ArrayList cachearr = MemCachedManager.GetStats(arrayList, MemCachedManager.Stats.CachedDump, "" + number + " 0");
        if (cachearr.Count > 0)
        {
            IndexPath = path + "\\" + number + "\\" + number;
            for (var i = 0; i < cachearr.Count; i++)
            {
                path = frm.BackPath;
                path = path + "\\" + number + "\\" + help.count;
                string key = cachearr[i].ToString().Split(':')[0];
                object value = MemcachedTools.Get(key);
                if (value != null && value.ToString() != "")
                {
                    filePath = path + "\\" + key + ".txt";
                    frm.WriteFile(key, value.ToString(), help.totalNum, number, path, filePath, IndexPath);
                    help.totalNum++;
                    help.num++;
                    if (help.num == 1000)
                    {
                        help.count++;
                        help.num = 0;
                    }
                    filePath = "";
                }
            }
            frm.WriteState("Item " + number + " 备份数据完成，总共写入文件" + help.totalNum + "个！");
            thread.Abort();
        }
    }
}

