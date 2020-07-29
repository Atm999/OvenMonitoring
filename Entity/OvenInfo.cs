using Entity.Enums;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    [SugarTable("oven_info")]
    public class OvenInfo : BasicEntity
    {
        /// <summary>
        /// 烤箱名称(唯一)
        /// </summary>
        public string oven_name { get; set; }
        /// <summary>
        /// 是否启用该烤炉
        /// </summary>
        public bool oven_enable { set; get; }
        /// <summary>
        /// 是否启用host
        /// </summary>
        public bool host_enable { set; get; }
        /// <summary>
        /// 创建者用户名
        /// </summary>
        public string creation_name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime creation_time { get; set; }
        #region   采集器通讯参数
        /// <summary>
        /// 采集器采集频率
        /// </summary>
        public decimal collector_frequency { get; set; }   
        /// <summary>
        /// COM口
        /// </summary>
        public int collector_port { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public SerialPortParameter.SerialPortBaudRates collector_baud_rate { get; set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public SerialPortParameter.SerialPortDatabits collector_data_bit { get;set;}
        /// <summary>
        /// 停止位
        /// </summary>
        public SerialPortParameter.SerialStopBits collector_stop_bit { get; set; }
        /// <summary>
        /// 奇偶校验
        /// </summary>
        public SerialPortParameter.SerialParity collector_parity { get; set; }
        /// <summary>
        /// 双工
        /// </summary>
        public SerialPortParameter.SerialPortDuplex collector_duplex { set; get; }
        #endregion

        #region   温控器通讯参数
        /// <summary>
        /// COM口
        /// </summary>
        public int thermostat_port { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public SerialPortParameter.SerialPortBaudRates thermostat_baud_rate { get; set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public SerialPortParameter.SerialPortDatabits thermostat_data_bit { get; set; }
        /// <summary>
        /// 停止位
        /// </summary>
        public SerialPortParameter.SerialStopBits thermostat_stop_bit { get; set; }
        /// <summary>
        /// 奇偶校验
        /// </summary>
        public SerialPortParameter.SerialParity thermostat_parity { get; set; }
        /// <summary>
        /// 双工
        /// </summary>
        public SerialPortParameter.SerialPortDuplex thermostat_duplex { set; get; }
        #endregion


        #region 烤箱设定
        /// <summary>
        /// 补偿温度
        /// </summary>
        public decimal compensate_p1 { get; set; } = 0;

        public decimal compensate_p2 { get; set; } = 0;

        public decimal compensate_p3 { get; set; } = 0;

        public decimal compensate_p4 { get; set; } = 0;

        public decimal compensate_p5 { get; set; } = 0;

        public decimal compensate_p6 { get; set; } = 0;
        /// <summary>
        /// 温度下限
        /// </summary>
        public decimal lower_limit { set; get; }
        /// <summary>
        /// 温度上限
        /// </summary>
        public decimal upper_limit { set; get; }
        #endregion
        /// <summary>
        /// 更新者 用户名
        /// </summary>
        public string update_name { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime update_time { get; set; }
    }
}
