using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    [SugarTable("project_Info")]
    public class ProjectInfo:BasicEntity
    {
        /// <summary>
        /// 工程名称
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 烤炉id
        /// </summary>
        public int oven_id { set; get; }
        /// <summary>
        /// 项目状态 0:等待  1:开始  2:结束
        /// </summary>
        public int project_status { set; get; }
        /// <summary>
        /// 专案开始时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? start_time { set; get; }
        /// <summary>
        /// 专案结束时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? end_time { set; get; }
        /// <summary>
        /// 升温报警
        /// </summary>
        public bool rise_temp_alarm { set; get; }
        /// <summary>
        /// 常温报警
        /// </summary>
        public bool const_temp_alarm { set; get; }
        /// <summary>
        /// 温度上下限
        /// </summary>
        public decimal tolerance { set; get; }
    }

    public class ProjectDetailInfo: ProjectInfo
    {
        /// <summary>
        /// 温段
        /// </summary>
        public HeatStep heatStep1 { set; get; }
        public HeatStep heatStep2 { set; get; }
        public HeatStep heatStep3 { set; get; }
        public HeatStep heatStep4 { set; get; }
        /// <summary>
        /// tmsig 我也不知道这是什么 用就完事了
        /// </summary>
        public TmSig tmSig1 { set; get; }
        public TmSig tmSig2 { set; get; }
        public TmSig tmSig3 { set; get; }
        public TmSig tmSig4 { set; get; }
        /// <summary>
        /// pid
        /// </summary>
        public Pid pid1 { set; get; }
        public Pid pid2 { set; get; }
    }
    [SugarTable("heat_step")]
    /// <summary>
    /// 升温段
    /// </summary>
    public class HeatStep:BasicEntity
    {
        /// <summary>
        /// 专案id
        /// </summary>
        public int project_id { set; get; }
        /// <summary>
        /// 序号 在同一专案下进行排序
        /// </summary>
        public int order_id { set; get; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public decimal duration { set; get; }
        /// <summary>
        /// 温度
        /// </summary>
        public decimal temperature { set; get; }
    }
    [SugarTable("tm_sig")]
    public class TmSig:BasicEntity
    {
        /// <summary>
        /// 专案id
        /// </summary>
        public int project_id { set; get; }
        /// <summary>
        /// 序号 在同一专案下进行排序
        /// </summary>
        public int order_id { set; get; }
        public int sout{set;get;}
        public int ssn { set; get;}
        public int stm { set; get; }
        public int esn { set; get; }
        public int etm { set; get; }
    }

    /// <summary>
    /// pid
    /// </summary>
    [SugarTable("pid")]
    public class Pid:BasicEntity
    {
        /// <summary>
        /// 专案id
        /// </summary>
        public int project_id { set; get; }
        /// <summary>
        /// 序号 在同一专案下进行排序
        /// </summary>
        public int order_id { set; get; }
        public decimal p { set; get; }
        public decimal i { set; get; }
        public decimal d { set; get; }
    }

}
