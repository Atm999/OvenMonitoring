using System;
using System.Collections.Generic;
using System.Text;

namespace MailServce.Entity
{
    public class EmailServer
    {
        /// <summary>
        /// 主机ip
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
