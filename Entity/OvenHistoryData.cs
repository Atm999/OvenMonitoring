using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 当前的烤箱温度数据 最新
    /// </summary>
    [SugarTable("oven_history_data")]
    public class OvenHistoryData : BasicEntity
    {
        /// <summary>
        /// 烤箱id
        /// </summary>
        public int oven_id { get; set; }
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
        /// 插入的时间
        /// </summary>
        public DateTime insert_time { get; set; }
    }
}
