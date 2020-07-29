using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace OvenMonitoring.Controllers
{
    public class TemperatureHistoryController : Controller
    {
        private readonly IOvenHistoryDataService _ovenHistoryDataService;
        public TemperatureHistoryController(IOvenHistoryDataService ovenHistoryDataService)
        {
            _ovenHistoryDataService = ovenHistoryDataService;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取该时间段内的数据
        /// </summary>
        /// <returns></returns>
        public IActionResult GetHistoryData(int oven_id,DateTime start_time,DateTime end_time)
        {
            List<OvenHistoryData> list = _ovenHistoryDataService.Queryable()
                                           .Where(x => x.oven_id == oven_id)
                                           .Where(x => x.insert_time >= start_time && x.insert_time <= end_time)
                                           .ToList();
            return Json(list);
        }

    }
}
