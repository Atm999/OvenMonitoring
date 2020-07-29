using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    [SugarTable("email_config")]
    public class EmailConfig:BasicEntity
    {
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 发件人密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 是否启用ssl
        /// </summary>
        public bool enable_ssl { get; set; }
        /// <summary>
        /// 是否启用邮件警报
        /// </summary>
        public bool enabled_alert { get; set; }


    }
}
