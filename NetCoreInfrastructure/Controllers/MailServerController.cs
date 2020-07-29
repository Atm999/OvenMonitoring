using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using CoreExtention;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace OvenMonitoring.Controllers
{
    public class MailServerController : Controller
    {
        private readonly IEmailConfigService _emailConfigService;
        private readonly IEmailReceiverService _emailReceiverService;
        private readonly IHttpClientFactoryHelper _httpClientFactoryHelper;
        public MailServerController(IEmailConfigService emailConfigService, IEmailReceiverService emailReceiverService, IHttpClientFactoryHelper httpClientFactoryHelper)
        {
            _emailConfigService = emailConfigService;
            _emailReceiverService = emailReceiverService;
            _httpClientFactoryHelper = httpClientFactoryHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Get()
        {
            EmailConfig emailConfig = _emailConfigService.Queryable().First();
            List<EmailReceiver> emailReceivers = _emailReceiverService.Queryable().ToList();
            return Json(new { emailConfig, emailReceivers });
        }

        [HttpPost]
        public IActionResult EditConfig([FromBody]EmailConfig emailConfig)
        {
            bool res = false;
            EmailConfig ec = _emailConfigService.Queryable().First();
            if(ec == null)
            {
                res = _emailConfigService.Insert(emailConfig);
            }
            else
            {
                res = _emailConfigService.Update(emailConfig);
            }
            return Json(res);
        }
        [HttpPost]
        public IActionResult AddEmailReceiver([FromBody]EmailReceiver emailReceiver)
        {
            emailReceiver.create_time = DateTime.Now;
            bool res = _emailReceiverService.Insert(emailReceiver); 
            return Json(res);
        }

        [HttpPost]
        public IActionResult DeleteEmailReceiver([FromBody]EmailReceiver emailReceiver)
        {
            bool res = _emailReceiverService.Delete(x=>x.address == emailReceiver.address);
            return Json(res);
        }



        //public IActionResult GetEmailReceiver()
        //{
        //    List<EmailReceiver> emailReceivers = _emailReceiverService.Queryable().ToList();
        //    return Json(emailReceivers);
        //}
    }
}
