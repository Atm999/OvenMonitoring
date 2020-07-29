using Entity.Enums;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 当前的烤箱温度数据 最新
    /// </summary>
    [SugarTable("oven_current_data")]
    public class OvenCurrentData
    {
        [SugarColumn(IsPrimaryKey = true)]
        /// <summary>
        /// 烤箱id
        /// </summary>
        public int oven_id { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 温度点1
        /// </summary>
        public decimal point1 { get; set; }
        /// <summary>
        /// 温度点2
        /// </summary>
        public decimal point2 { get; set; }
        /// <summary>
        /// 温度点3
        /// </summary>
        public decimal point3 { get; set; }
        /// <summary>
        /// 温度点4
        /// </summary>
        public decimal point4 { get; set; }
        /// <summary>
        /// 温度点5
        /// </summary>
        public decimal point5 { get; set; }
        /// <summary>
        /// 温度点6
        /// </summary>
        public decimal point6 { get; set; }
        /// <summary>
        /// 采集器状态
        /// </summary>
        public string dam_state { get; set; }
        /// <summary>
        /// 烤箱门状态
        /// </summary>
        public string door_state { get; set; }
        /// <summary>
        /// 烤箱状态
        /// </summary>
        public string oven_state { get; set; }
        /// <summary>
        /// 温控器状态
        /// </summary>
        public string tc_state { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime update_time { get; set; }
    }
}
