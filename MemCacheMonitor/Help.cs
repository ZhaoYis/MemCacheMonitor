using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Help
{
    public int num { get; set; }
    public int count { get; set; }
    public int totalNum { get; set; }
    public bool IsWork = false;//状态标识,是否工作
    public Help(int num = 1000, int count = 1, int totalNum = 0, bool isWork = true)
    {
        this.num = num;
        this.count = count;
        this.totalNum = totalNum;
        this.IsWork = isWork;
    }

    public static string GetFileSize(long len)
    {
        float result;
        string re = "";
        if (len >= 1073741824)
        {
            result = len / 1073741824.00F;
            re = "GB";
        }
        else if (len >= 1048576)
        {
            result = len / 1048576.00F;
            re = "MB";
        }
        else
        {
            result = len / 1024.00F;
            re = "KB";
        }
        string File_Size = result.ToString("0.00");
        return (File_Size + re);
    }
}


