using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Swashbuckle.AspNetCore.Annotations;

namespace OvenMonitoring.Controllers
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [SwaggerTag("数据接收接口")]
    [Route("api/[controller]")]
    [ApiController]
    public class DataReceiveController : Controller
    {
        private readonly IOvenCurrentDataService _ovenCurrentDataService;
        private readonly IOvenHistoryDataService _ovenHistoryDataService;
        private readonly IOvenInfoService _ovenInfoService;

        public DataReceiveController(IOvenCurrentDataService ovenCurrentDataService, IOvenHistoryDataService ovenHistoryDataService, IOvenInfoService ovenInfoService)
        {
            _ovenCurrentDataService = ovenCurrentDataService;
            _ovenHistoryDataService = ovenHistoryDataService;
            _ovenInfoService = ovenInfoService;
        }
        [HttpPost]
        public ActionResult Get(List<CollectorData> list)
        {
            List<OvenCurrentData> ovenCurrentDatas = new List<OvenCurrentData>();
            List<OvenHistoryData> ovenHistoryDatas = new List<OvenHistoryData>();
            List<OvenInfo> ovenInfos = _ovenInfoService.Queryable().ToList();
            if (list !=null)
            {
                //遍历接收的数据队列
                foreach(CollectorData collectorData in list)
                {
                    OvenInfo ovenInfo = ovenInfos
                                        .Where(x => x.collector_port == collectorData.Com)
                                        .FirstOrDefault();
                    //这边需要加一个开启专案的判定
                    //只有配置过的端口才会接收
                    if(ovenInfo != null)
                    {
                        OvenCurrentData ovenCurrentData = new OvenCurrentData();
                        OvenHistoryData ovenHistoryData = new OvenHistoryData();
                        ovenCurrentData.door_state = collectorData.DoorState;
                        ovenCurrentData.oven_id = ovenInfo.id;
                        ovenCurrentData.dam_state = collectorData.DAMState;
                        ovenCurrentData.port = collectorData.Com;
                        ovenCurrentData.tc_state = collectorData.TC;
                        ovenCurrentData.point1 = collectorData.tmp1;
                        ovenCurrentData.point2 = collectorData.tmp2;
                        ovenCurrentData.point3 = collectorData.tmp3;
                        ovenCurrentData.point4 = collectorData.tmp4;
                        ovenCurrentData.point5 = collectorData.tmp5;
                        ovenCurrentData.point6 = collectorData.tmp6;
                        ovenCurrentData.oven_state = collectorData.Oventate;
                        ovenCurrentData.update_time = DateTime.Now;
                        ovenCurrentDatas.Add(ovenCurrentData);


                        ovenHistoryData.oven_id = ovenInfo.id;
                        ovenHistoryData.point1 = collectorData.tmp1;
                        ovenHistoryData.point2 = collectorData.tmp2;
                        ovenHistoryData.point3 = collectorData.tmp3;
                        ovenHistoryData.point4 = collectorData.tmp4;
                        ovenHistoryData.point5 = collectorData.tmp5;
                        ovenHistoryData.point6 = collectorData.tmp6;
                        ovenHistoryData.insert_time = DateTime.Now;
                        ovenHistoryDatas.Add(ovenHistoryData);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                _ovenCurrentDataService.Saveable(ovenCurrentDatas);
                _ovenHistoryDataService.Insert(ovenHistoryDatas);
            }
            return Json(true);
        }
    }
}
