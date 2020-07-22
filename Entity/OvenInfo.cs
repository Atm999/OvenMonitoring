using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class OvenInfo : BasicEntity
    {
        /// <summary>
        /// 烤箱名称(唯一)
        /// </summary>
        public string oven_name { get; set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        public int creation_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime creation_time { get; set; }
        /// <summary>
        /// COM口(唯一)
        /// </summary>
        public string serial_port { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public SerialPortParameter.SerialPortBaudRates baud_rate { get; set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public SerialPortParameter.SerialPortDatabits data_bit{get;set;}
        /// <summary>
        /// 停止位
        /// </summary>
        public SerialPortParameter.SerialStopBits stop_bit { get; set; }
        /// <summary>
        /// 奇偶校验
        /// </summary>
        public SerialPortParameter.SerialParity parity { get; set; }
        
        /// <summary>
        /// 烤箱状态
        /// </summary>
        public OvenParameter.OvenState oven_state { get; set; }
        /// <summary>
        /// 烤箱门状态
        /// </summary>
        public OvenParameter.DoorState door_state { get; set; }

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
        /// 更新者id
        /// </summary>
        public int update_id { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime update_time { get; set; }
    }
}
