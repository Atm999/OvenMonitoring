using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace OvenMonitoring.Controllers
{
    public class TemperatureMonitorController : Controller
    {

        private readonly IProjectInfoService _projectInfoService;
        private readonly IOvenCurrentDataService _ovenCurrentDataService;
        private readonly IOvenInfoService _ovenInfoService;

        public TemperatureMonitorController(IProjectInfoService projectInfoService, IOvenCurrentDataService ovenCurrentDataService, IOvenInfoService ovenInfoService)
        {
            _projectInfoService = projectInfoService;
            _ovenCurrentDataService = ovenCurrentDataService;
            _ovenInfoService = ovenInfoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCurrentData()
        {
            //第一步 查询所有创建的烤炉
            List<OvenInfo> ovenInfos = _ovenInfoService.Queryable().ToList();
            //第二步 查看所有专案信息
            List<ProjectInfo> projectInfos = _projectInfoService.Queryable().ToList();
            //第三步 查看实时数据
            List<OvenCurrentData> ovenCurrentDatas = _ovenCurrentDataService.Queryable().ToList();
            if (ovenInfos !=null)
            {
                var cd = from a in ovenInfos
                         join b in ovenCurrentDatas on a.id equals b.oven_id into ab
                         //from c in ab.DefaultIfEmpty(new OvenCurrentData())
                         join d in projectInfos on a.id equals d.oven_id  into ad
                         from e in ab.DefaultIfEmpty(new OvenCurrentData())
                         from f in ad.DefaultIfEmpty(new ProjectInfo())

                         select new
                         {
                             id = a.id,
                             name = a.oven_name,
                             p1 = e.point1,
                             p2 = e.point2,
                             p3 = e.point3,
                             p4 = e.point4,
                             p5 = e.point5,
                             p6 = e.point6,
                             dam_state = e.dam_state,
                             door_state = e.door_state,
                             tc = e.tc_state,
                             oven_state = e.oven_state,
                             project_status = f.project_status
                         };
                return Json(cd);
            }
            return Json(null);
        }

    }
}
