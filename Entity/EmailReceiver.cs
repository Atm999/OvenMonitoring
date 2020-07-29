using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 收件人地址
    /// </summary>
    [SugarTable("email_receiver")]
    public class EmailReceiver:BasicEntity
    {
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string address { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { set; get; }
    }
}
