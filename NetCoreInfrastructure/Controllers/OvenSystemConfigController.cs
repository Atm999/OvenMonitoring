using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;
using OvenMonitoring.Tool;
using Service.IService;
using Service.Service;
using static Entity.Enums.SerialPortParameter;

namespace OvenMonitoring.Controllers
{
    public class OvenSystemConfigController : Controller
    {
        private readonly IOvenInfoService _ovenInfoService;
        CookieHelper cookieHelper = new CookieHelper();

        public OvenSystemConfigController(IOvenInfoService ovenInfoService)
        {
            _ovenInfoService = ovenInfoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有烤炉信息
        /// </summary>
        /// <returns></returns>
        public IActionResult GetOvenInfo()
        {
            List<OvenInfo> list = _ovenInfoService.Queryable().ToList();
            return Json(list);
        }
        /// <summary>
        /// 根据名字查询烤炉信息
        /// </summary>
        /// <param name="oven_name"></param>
        /// <returns></returns>
        public IActionResult GetOvenInfoByName(string oven_name)
        {
            OvenInfo entity = _ovenInfoService.Queryable().Where(x => x.oven_name == oven_name)?.First();
            return Json(entity);
        }


        /// <summary>
        /// 新增烤炉信息 别问为啥不传对象 问就是不知道
        /// </summary>
        /// <param name="oven_name"></param>
        /// <returns></returns>
        public IActionResult AddOvenInfo(string oven_name)
        {
            bool res = false;
            OvenInfo oven = _ovenInfoService.Queryable().Where(x => x.oven_name == oven_name)?.First();
            //非同名的烤炉名字 才可以被插入
            if(oven == null)
            {
                //初始化数据
                oven = new OvenInfo();
                oven.oven_name = oven_name;
                oven.collector_baud_rate = SerialPortBaudRates.BaudRate_9600;
                oven.collector_data_bit = SerialPortDatabits.EightBits;
                oven.collector_duplex = SerialPortDuplex.None;
                oven.collector_frequency = 1;
                oven.collector_parity = SerialParity.None;
                oven.collector_port = 1;
                oven.collector_stop_bit = SerialStopBits.One;

                oven.thermostat_duplex = SerialPortDuplex.None;
                oven.thermostat_baud_rate = SerialPortBaudRates.BaudRate_9600;
                oven.thermostat_data_bit = SerialPortDatabits.EightBits;
                oven.thermostat_parity = SerialParity.None;
                oven.thermostat_port = 2;
                oven.thermostat_stop_bit = SerialStopBits.One;

                oven.creation_name = cookieHelper.GetCookies(HttpContext, "user_name");
                oven.update_name = cookieHelper.GetCookies(HttpContext,"user_name");
                oven.creation_time = DateTime.Now;
                oven.update_time = DateTime.Now;
                oven.upper_limit = 5;
                oven.lower_limit = 5;
                oven.oven_enable = true;
                oven.host_enable = false;
                oven.compensate_p1 = 1;
                oven.compensate_p2 = 1;
                oven.compensate_p3 = 1;
                oven.compensate_p4 = 1;
                oven.compensate_p5 = 1;
                oven.compensate_p6 = 1;
                res = _ovenInfoService.Insert(oven);
            }
            return Json(res);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateOvenInfo([FromBody] OvenInfo entity)
        {
            bool res = false;
            res = _ovenInfoService.Update(entity);
            return Json(res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="oven_name"></param>
        /// <returns></returns>
        public IActionResult DeleteOvenInfo(string oven_name)
        {
            bool res = false;
            res = _ovenInfoService.Delete(x=>x.oven_name== oven_name);
            return Json(res);
        }

    }
}
