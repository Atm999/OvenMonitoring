using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorize.Entity;
using CoreExtention;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OvenMonitoring.Tool;
using Service.IService;

namespace OvenMonitoring.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpClientFactoryHelper _httpClientFactoryHelper;
        CookieHelper cookieHelper = new CookieHelper();

        public UserController( IUserService userService, IHttpClientFactoryHelper httpClientFactoryHelper)
        {
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




        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetUserListAsync()
        {
            string token = cookieHelper.GetCookies(HttpContext, "Token");
            List<UserInfo> re = new List<UserInfo>();
            if (token != null)
            {
                string url = GlobalVar.Author_url + "/JWT/GetUser?token=" + token ;
                re = await _httpClientFactoryHelper.GetJsonResult<List<UserInfo>>(url, "", System.Net.Http.HttpMethod.Get);
                return Json(re);
            }
            return Json(re);
        }



        /// <summary>
        /// 创建新账户
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateUserAsync(string username, string password,string role)
        {
            string token = cookieHelper.GetCookies(HttpContext, "Token");
            if(token != null)
            {
                string url = GlobalVar.Author_url + "/JWT/CreateUser?token="+ token + "&user_name=" + username + "&password=" + password + "&role=" + role;
                bool re = await _httpClientFactoryHelper.GetJsonResult<bool>(url, "", System.Net.Http.HttpMethod.Get);
                return Json(re);
            }
            return Json(false);
        }

        public async Task<IActionResult> DeleteUserAsync(string username)
        {
            string token = cookieHelper.GetCookies(HttpContext, "Token");
            if (token != null)
            {
                string url = GlobalVar.Author_url + "/JWT/DeleteUser?token=" + token + "&user_name=" + username;
                bool re = await _httpClientFactoryHelper.GetJsonResult<bool>(url, "", System.Net.Http.HttpMethod.Get);
                return Json(re);
            }
            return Json(false);
        }
        public async Task<IActionResult> UpdatePasswordAsync(string pre_password,string new_password)
        {
            string token = cookieHelper.GetCookies(HttpContext, "Token");
            if (token != null)
            {
                string url = GlobalVar.Author_url + "/JWT/UpdatePassword?token="+ token + "&pre_password=" + pre_password + "&new_password=" + new_password;
                bool re = await _httpClientFactoryHelper.GetJsonResult<bool>(url,"", System.Net.Http.HttpMethod.Get);
                if(re)
                {
                    cookieHelper.DeleteCookies(HttpContext, "Token");
                    cookieHelper.DeleteCookies(HttpContext, "user_name");
                    cookieHelper.DeleteCookies(HttpContext, "user_role");
                }
                return Json(re);
            }
            return Json(false);
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="pre_password"></param>
        /// <param name="new_password"></param>
        /// <returns></returns>
        public IActionResult LogOut()
        {
            cookieHelper.DeleteCookies(HttpContext, "Token");
            cookieHelper.DeleteCookies(HttpContext, "user_name");
            cookieHelper.DeleteCookies(HttpContext, "user_role");
            return Json(true);
        }
    }
}
