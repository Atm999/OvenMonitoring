using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Enums
{
    public class OvenParameter
    {
        /// <summary>
        /// 烤箱门状态(0=关闭,1=开启)
        /// </summary>
        public enum DoorState : int
        {
            Open = 1,
            Close = 0
        }
        /// <summary>
        /// 烤箱状态(0=禁用,1=闲置,2=运行,-1=异常)
        /// </summary>
        public enum OvenState : int
        {
            Disable=0,
            Idle = 1,
            Run = 2,
            Alarm = -1
        }
    }
}
