using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreInfrastructure.Models;
using Service.IService;
using Entity;
using CoreExtention;
using Authorize.Entity;
using OvenMonitoring;
using Newtonsoft.Json;
using OvenMonitoring.Tool;

namespace NetCoreInfrastructure.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IHttpClientFactoryHelper _httpClientFactoryHelper;
        CookieHelper cookieHelper = new CookieHelper();

        public HomeController(ILogger<HomeController> logger, IUserService userService,IHttpClientFactoryHelper httpClientFactoryHelper)
        {
            _logger = logger;
            _userService = userService;
            _httpClientFactoryHelper = httpClientFactoryHelper;
        }

        public IActionResult Index()
        {
            string user_name = cookieHelper.GetCookies(HttpContext, "user_name");
            if (user_name != "")
            {
                UserInfo userInfo = new UserInfo();
                userInfo.user_name = user_name;
                userInfo.Role = cookieHelper.GetCookies(HttpContext, "user_role");
                return View(userInfo);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Check(string username, string password)
        {
            string url = GlobalVar.Author_url + "/JWT/Login?user_name=" + username + "&password=" + password;
            var postData = new { user_name = username, password = password };
            string postData_str = JsonConvert.SerializeObject(postData);
            UserToken userToken = await _httpClientFactoryHelper.GetJsonResult<UserToken>(url, postData_str, System.Net.Http.HttpMethod.Post);
            if (userToken != null && userToken.UserName != null)
            {
                cookieHelper.SetCookies(HttpContext, "user_name", userToken.UserName, userToken.expire);
                cookieHelper.SetCookies(HttpContext, "user_role", userToken.Role, userToken.expire);
                cookieHelper.SetCookies(HttpContext, "Token", userToken.Token, userToken.expire);
                return Json("Success");
            }
            return Json("Fail");
        }
    }
}
