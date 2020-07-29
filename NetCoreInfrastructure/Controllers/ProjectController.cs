using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Service.IService;

namespace OvenMonitoring.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectInfoService _projectInfoService;
        private readonly IHeatStepService _heatStepService;
        private readonly ITmSigService _tmSigService;
        private readonly IPidService _pidService;
        private readonly IOvenInfoService _ovenInfoService;
        public ProjectController(IProjectInfoService projectInfoService, IHeatStepService heatStepService, ITmSigService tmSigService, IPidService pidService, IOvenInfoService ovenInfoService)
        {
            _projectInfoService = projectInfoService;
            _heatStepService = heatStepService;
            _tmSigService = tmSigService;
            _pidService = pidService;
            _ovenInfoService = ovenInfoService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetProjectInfo()
        {
            List<Entity.ProjectInfo> projectInfos = _projectInfoService.Queryable().ToList();
            List<OvenInfo> ovenInfos = _ovenInfoService.Queryable().ToList();
            var ft = from a in projectInfos
                     join b in ovenInfos on a.oven_id equals b.id
                     select new
                     {
                        id = a.id,
                        name = a.name,
                        oven_name=b.oven_name,
                        project_status=a.project_status,
                        start_time=a.start_time,
                        end_time=a.end_time
                     };
            return Json(ft);
        }

        public IActionResult GetProjectDetailInfo(int id)
        {
            ProjectDetailInfo projectDetailInfo = new ProjectDetailInfo();
            Entity.ProjectInfo projectInfo = _projectInfoService.Queryable().Where(x => x.id == id).First();
            List<HeatStep> heatSteps = _heatStepService.Queryable().Where(x => x.project_id == projectInfo.id).ToList();
            List<TmSig> tmSigs = _tmSigService.Queryable().Where(x => x.project_id == projectInfo.id).ToList();
            List<Pid> pids = _pidService.Queryable().Where(x => x.project_id == projectInfo.id).ToList();

            var ParentType = typeof(Entity.ProjectInfo);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(projectDetailInfo, Propertie.GetValue(projectInfo, null), null);
                }
            }
            projectDetailInfo.heatStep1 = heatSteps[0];
            projectDetailInfo.heatStep2 = heatSteps[1];
            projectDetailInfo.heatStep3 = heatSteps[2];
            projectDetailInfo.heatStep4 = heatSteps[3];

            projectDetailInfo.tmSig1 = tmSigs[0];
            projectDetailInfo.tmSig2 = tmSigs[1];
            projectDetailInfo.tmSig3 = tmSigs[2];
            projectDetailInfo.tmSig4 = tmSigs[3];

            projectDetailInfo.pid1 = pids[0];
            projectDetailInfo.pid2 = pids[1];
            return Json(projectDetailInfo);
        }

        [HttpPost]
        public IActionResult AddProjectInfo([FromBody] Entity.ProjectInfo projectInfo)
        {
            bool res = false;
            int project_id = 0;
            if(projectInfo.oven_id > 0)
            {
                //设定初始参数
                project_id = _projectInfoService.InsertReturnIdentity(projectInfo);
                List<HeatStep> heatSteps = new List<HeatStep>();
                for (int i = 0; i < 4;i++)
                {
                    HeatStep heatStep = new HeatStep();
                    heatStep.project_id = project_id;
                    heatStep.order_id = i + 1;
                    heatStep.temperature = 0;
                    heatStep.duration = 0;
                    heatSteps.Add(heatStep);
                }
                List<TmSig> tmSigs = new List<TmSig>();
                for (int i = 0; i < 4; i++)
                {
                    TmSig tmSig = new TmSig();
                    tmSig.project_id = project_id;
                    tmSig.order_id = i + 1;
                    tmSig.sout = 0;
                    tmSig.ssn = 0;
                    tmSig.stm = 0;
                    tmSig.esn = 0;
                    tmSig.etm = 0;
                    tmSigs.Add(tmSig);
                }

                List<Pid> pids = new List<Pid>();
                for (int i = 0; i < 2; i++)
                {
                    Pid pid = new Pid();
                    pid.project_id = project_id;
                    pid.order_id = i + 1;
                    pid.p = 0;
                    pid.i = 0;
                    pid.d = 0;
                    pids.Add(pid);
                }

                res = _heatStepService.Insert(heatSteps);
                res &= _tmSigService.Insert(tmSigs);
                res &= _pidService.Insert(pids);

            }

            return Json(res);
        }


        [HttpPost]
        public IActionResult UpdateProjectInfo([FromBody] ProjectDetailInfo projectDetailInfo)
        {
            bool res = false;
            Entity.ProjectInfo projectInfo = projectDetailInfo;
            List<HeatStep> heatSteps = new List<HeatStep>();
            heatSteps.Add(projectDetailInfo.heatStep1);
            heatSteps.Add(projectDetailInfo.heatStep2);
            heatSteps.Add(projectDetailInfo.heatStep3);
            heatSteps.Add(projectDetailInfo.heatStep4);

            List<TmSig> tmSigs = new List<TmSig>();
            tmSigs.Add(projectDetailInfo.tmSig1);
            tmSigs.Add(projectDetailInfo.tmSig2);
            tmSigs.Add(projectDetailInfo.tmSig3);
            tmSigs.Add(projectDetailInfo.tmSig4);

            List<Pid> pids = new List<Pid>();
            pids.Add(projectDetailInfo.pid1);
            pids.Add(projectDetailInfo.pid2);

            res = _projectInfoService.Update(projectInfo);
            res &= _heatStepService.Update(heatSteps);
            res &= _tmSigService.Update(tmSigs);
            res &= _pidService.Update(pids);
            return Json(res);
        }
        //[HttpPost]
        //public IActionResult AddProjectInfo([FromBody] ProjectDetailInfo projectDetailInfo )
        //{
        //    bool res = false;
        //    if(projectDetailInfo.oven_id > 0)
        //    {
        //        int project_id = 0;
        //        Entity.ProjectInfo projectInfo = projectDetailInfo;
        //        List<HeatStep> heatSteps = new List<HeatStep>();
        //        List<TmSig> tmSigs = new List<TmSig>();
        //        List<Pid> pids = new List<Pid>();
        //        project_id = _projectInfoService.InsertReturnIdentity(projectInfo);
        //        if (project_id > 0)
        //        {                  
        //            heatSteps.Add(projectDetailInfo.heatStep1);
        //            heatSteps.Add(projectDetailInfo.heatStep2);
        //            heatSteps.Add(projectDetailInfo.heatStep3);
        //            heatSteps.Add(projectDetailInfo.heatStep4);
        //            tmSigs.Add(projectDetailInfo.tmSig1);
        //            tmSigs.Add(projectDetailInfo.tmSig2);
        //            tmSigs.Add(projectDetailInfo.tmSig3);
        //            tmSigs.Add(projectDetailInfo.tmSig4);
        //            pids.Add(projectDetailInfo.pid1);
        //            pids.Add(projectDetailInfo.pid2);
        //            res = _heatStepService.Insert(heatSteps);
        //            res &= _tmSigService.Insert(tmSigs);
        //            res &= _pidService.Insert(pids);
        //        }
        //    }
        //    return Json(res);
        //}



        public IActionResult DeleteProjectInfo(int id)
        {
            bool res = false;
            res = _projectInfoService.Delete(id);
            _heatStepService.Delete(x => x.project_id == id);
            _tmSigService.Delete(x => x.project_id == id);
            _pidService.Delete(x => x.project_id == id);
            return Json(res);
        }

        /// <summary>
        /// 开始专案
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult StartProject(int id)
        {
            bool res = false;
            //这边要写socket通知温控器及为采集器
            Entity.ProjectInfo projectInfo = new Entity.ProjectInfo();
            projectInfo.id = id;
            projectInfo.project_status = 1;
            projectInfo.start_time = DateTime.Now;
            res = _projectInfoService.Update(projectInfo,x=> new {x.start_time,x.project_status });
            return Json(res);
        }

        /// <summary>
        /// 结束专案
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult FinishProject(int id)
        {
            bool res = false;
            Entity.ProjectInfo projectInfo = new Entity.ProjectInfo();
            projectInfo.id = id;
            projectInfo.project_status = 2;
            projectInfo.end_time = DateTime.Now;
            res = _projectInfoService.Update(projectInfo, x => new { x.end_time, x.project_status });
            return Json(res);
        }
    }
}
