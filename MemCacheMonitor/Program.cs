using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MemCacheMoniter;

namespace MemCacheMonitor
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //处理未捕获的异常
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                bool createdNew;
                Mutex instance = new Mutex(true, Application.ProductName, out createdNew);

                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new frmMain());

                    instance.ReleaseMutex();
                }
                else
                {
                    MessageBox.Show(@"程序已经在运行，请勿重复启动程序！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffff") + "\r\n";

                string errMsg = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                    ex.GetType().Name, ex.Message, ex.StackTrace);

                MessageBox.Show(errMsg, @"系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 处理UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string errMsg = "";
            string strDateInfo = "出现应用程序未处理的UI线程异常：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffff") + "\r\n";
            if (e.Exception is Exception error)
            {
                errMsg = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                    error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                errMsg = string.Format("应用程序UI线程异常：{0}", e);
            }

            MessageBox.Show(errMsg, @"UI线程异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 处理非UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string errMsg = "";
            string strDateInfo = "出现应用程序未处理的非UI线程异常：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffff") + "\r\n";
            if (e.ExceptionObject is Exception error)
            {
                errMsg = string.Format(strDateInfo + "应用程序非UI线程异常：{0}\r\n堆栈信息：{1}", error.Message, error.StackTrace);
            }
            else
            {
                errMsg = string.Format("应用程序非UI线程异常：{0}", e);
            }

            MessageBox.Show(errMsg, @"非UI线程异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
