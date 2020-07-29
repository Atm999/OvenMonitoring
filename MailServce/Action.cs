using MailServce.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace MailServce
{
    public class Action
    {
        EmailServer _email_info;
        public Action(EmailServer email_info)
        {
            _email_info = email_info;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="host">主机电子</param>
        /// <param name="port">端口号</param>
        /// <param name="fm_user">发件人账号</param>
        /// <param name="fm_password">发件人密码</param>
        /// <param name="theme">发送的主题</param>
        /// <param name="rs_users">接收人队列</param>
        /// <param name="cc_users">抄送人队列</param>
        /// <param name="content">发送的内容</param>
        /// <returns></returns>
        public bool Send(string host,int port,string fm_user,string fm_password,string theme, List<string> rs_users,List<string> cc_users,string content,bool enable_ssl=false)
        {
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();
            //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
            mailMessage.Priority = MailPriority.Normal;
            //发件人邮箱地址。
            mailMessage.From = new MailAddress(fm_user);
            //收件人邮箱地址。需要群发就写多个
            //拆分邮箱地址
            for (int i = 0; i < rs_users.Count; i++)
            {
                mailMessage.To.Add(new MailAddress(rs_users[i]));  //收件人邮箱地址。
            }
            //抄送 队列
            for (int i = 0; i < cc_users.Count; i++)
            {
                //邮件的抄送者，支持群发
                mailMessage.CC.Add(new MailAddress(cc_users[i]));
            }
            //邮件正文是否是HTML格式
            mailMessage.IsBodyHtml = false;
            //邮件标题。
            mailMessage.Subject = theme;
            //邮件内容。
            mailMessage.Body = content;
            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();
            #region 设置邮件服务器地址
            //在这里我使用的是163邮箱，所以是smtp.163.com，如果你使用的是qq邮箱，那么就是smtp.qq.com。
            // client.Host = "smtp.163.com";
            client.Host = host;
            client.Port = port;
            client.EnableSsl = enable_ssl;
            #endregion
            //使用安全加密连接。
            client.EnableSsl = true;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;

            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential(fm_user, fm_password);

            //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //发送
            client.Send(mailMessage);
            return true;
        }
    }
}
