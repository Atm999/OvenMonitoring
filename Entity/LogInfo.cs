using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    [SugarTable("log_info")]
    public class LogInfo:BasicEntity
    {
        /// <summary>
        /// 异常内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 异常发生时间
        /// </summary>
        public string happen_time { get; set; }

    }
}
