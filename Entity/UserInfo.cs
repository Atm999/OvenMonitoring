using SqlSugar;
using System;

namespace Entity
{
    [SugarTable("user_info")]
    public class UserInfo:BasicEntity
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [SugarColumn(Length = 100)]
        public string user_name { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [SugarColumn(Length = 200)]
        public string pass_word { get; set; }
        /// <summary>
        /// 用户权限(admin,user)
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        //public string mail_address { get; set; }
        ///// <summary>
        ///// 是否需要接受警报邮件
        ///// </summary>
        //public bool accept_alert_mail { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime creation_time { get; set; }


    }
}
