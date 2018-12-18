using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using MemCacheMonitor;
using Commons;

public class WriteThread
{
    private string _path { get; set; }
    public frmRecover frm { get; set; }
    public Help help { get; set; }
    public string[] serverList;
    private Thread wt_thread;
    public WriteThread(string path, Help help)
    {
        this._path = path;
        this.help = help;
    }

    public bool IsCompleted()
    {
        //if (t != null)
        //    return t.IsCompleted;
        //else return true;
        if (wt_thread != null)
            return !wt_thread.IsAlive;
        else return true;
    }

    public void Join()
    {
        wt_thread.Join();
    }

    public void Start()
    {
        wt_thread = new Thread(() => this.Process());
        wt_thread.Start();
    }

    private static string GetFileName(string FullName)
    {
        Regex re = new Regex(@"\\[^\\]*$");
        Match ma = re.Match(FullName);
        if (ma.Success)
            return ma.Value.Substring(1);
        else
            return "";
    }

    private void ReadValue(string filePath, string index)
    {
        StreamReader sr;
        string key = string.Empty;
        string value = string.Empty;
        MemCacheTools_V2 tools = new MemCacheTools_V2(serverList);
        using (sr = new StreamReader(filePath, Encoding.UTF8))
        {
            key = GetFileName(filePath).Replace(".txt", "");
            value = sr.ReadToEnd();
            help.totalNum++;

            frm.WriteState("正在还原Item" + index + "【key=" + key + "】，已处理" + help.totalNum + "个");
            tools.Add(key, value);
        }
    }
    public void Process()
    {
        //string path = frm.FileIndexPath;
        string filePath = string.Empty;
        string[] arr = _path.Split('\\');
        string index = arr[arr.Length - 1];
        _path = _path + "\\" + index + ".txt";
        StreamReader sr;
        if (!File.Exists(_path))
        {
            frm.WriteState("没有找到文件路径" + _path + "！");
            return;
        }

        using (sr = new StreamReader(_path, Encoding.UTF8))
        {
            while ((filePath = sr.ReadLine()) != null)
            {
                ReadValue(filePath, index);
            }
            frm.WriteState(index + " 还原数据完成，总共写入文件" + help.totalNum + "个！");
            wt_thread.Abort();
        }
    }
}

