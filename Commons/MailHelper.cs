using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Commons
{
    public class MailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="cc">抄送人</param>
        /// <param name="sbuject">主题</param>
        /// <param name="content">内容</param>    
        /// <returns></returns>
        public static bool SendEmail(string to, string cc, string sbuject, string content)
        {
            string host = System.Configuration.ConfigurationManager.AppSettings["host"];
            int port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port"]);
            string user = System.Configuration.ConfigurationManager.AppSettings["user"];
            string password = System.Configuration.ConfigurationManager.AppSettings["password"];
            bool b = false;
            string message = string.Empty;
            MailMessage mm = new MailMessage();
            if (!string.IsNullOrEmpty(to))
            {
                foreach (var m in to.Split(','))
                {
                    mm.To.Add(m);//收件人
                }
            }
            if (!string.IsNullOrEmpty(cc))
            {
                foreach (var m in cc.Split(','))
                {
                    mm.CC.Add(m); ////抄送
                }
            }
            //发件人
            mm.From = new MailAddress(user);
            mm.SubjectEncoding = Encoding.GetEncoding(936);//这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
            mm.Subject = sbuject; //邮件标题
            mm.IsBodyHtml = true; //邮件正文是否是HTML格式
            mm.BodyEncoding = Encoding.GetEncoding(936); //邮件正文的编码， 设置不正确， 接收者会收到乱码
            //-------------------------------------------------------------------------
            mm.Body = content;//邮件正文
            mm.Priority = MailPriority.High; //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
            SmtpClient smtp = new SmtpClient(); //实例化一个SmtpClient
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //将smtp的出站方式设为 Network
            smtp.EnableSsl = true;//smtp服务器是否启用SSL加密
            smtp.Host = host; //指定 smtp 服务器地址
            smtp.Port = port;//指定 smtp 服务器的端口，默认是25
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(user, password);//认证          
            Util.SetCertificatePolicy();//禁止证书验证
            try
            {
                smtp.Send(mm);
                b = true;
                message = "发送成功!";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                b = false;
                message = "发送失败!" + ex.Message.ToString();
            }
            return b;
        }
    }

    public static class Util
    {
        /// <summary>
        /// Sets the cert policy.
        /// </summary>
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }

        /// <summary>
        /// Remotes the certificate validate.
        /// </summary>
        private static bool RemoteCertificateValidate(
           object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            // trust any certificate!!!
            System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }

    }
}
