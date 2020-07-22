using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorize.Authorize;
using Authorize.Entity;
using Authorize.IService;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using CoreExtention;
using System.Text;

namespace Authorize.Controllers
{
    [Route("[Controller]/[Action]")]
    [ApiController]
    public class JWTController : Controller
    {
        private readonly IUserLoginService _userLoginService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        public JWTController(IUserLoginService userLoginService,ITokenService tokenService, IUserService userService)
        {
            _userLoginService = userLoginService;
            _tokenService = tokenService;
            _userService = userService;
        }
        /// <summary>
        /// 登录并获取token
        /// </summary>
        /// <param name="user_name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(string user_name, string password)
        {
            UserToken userToken = _userLoginService.LoginAndGetToken(user_name, password);
            return new JsonResult(userToken);
        }

        [HttpGet]
        public IActionResult ValidateToken(string AuthToken)//验证token结果
        {
            string token = AuthToken;
            if (token.Contains("Bearer"))
            {
                token = token.Replace("Bearer", string.Empty);
                token = token.Trim();
            }
            UserToken tokenParseResult = _tokenService.ValidateToken(token);
            return new JsonResult(tokenParseResult);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUser(string token)
        {
            List<UserInfo> list = new List<UserInfo>();
            if (token.Contains("Bearer"))
            {
                token = token.Replace("Bearer", string.Empty);
                token = token.Trim();
            }
            UserToken res = _tokenService.ValidateToken(token);
            list = _userService.Queryable().ToList();
            list.ForEach(x => x.pass_word = "");              
            return new JsonResult(list);
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="user_name">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="role">权限 admin guest</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateUser(string token,string user_name,String password,string role)
        {
            bool res = false;
            if (token.Contains("Bearer"))
            {
                token = token.Replace("Bearer", string.Empty);
                token = token.Trim();
            }
            UserToken userToken = _tokenService.ValidateToken(token);
            if (userToken.Role == "admin")
            {
                UserInfo userInfo = _userService.Queryable().Where(x => x.user_name == user_name)?.First();
                if(userInfo == null)
                {
                    userInfo = new UserInfo();
                    userInfo.user_name = user_name;
                    userInfo.pass_word = password.EncodeBase64(Encoding.UTF8);
                    userInfo.Role = role;
                    userInfo.creation_time = DateTime.Now;
                    res = _userService.Insert(userInfo);
                }
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 删除账号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="user_name"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteUser(string token, string user_name)
        {
            bool res = false;
            if (token.Contains("Bearer"))
            {
                token = token.Replace("Bearer", string.Empty);
                token = token.Trim();
            }
            UserToken userToken = _tokenService.ValidateToken(token);
            if (userToken.Role == "admin" && userToken.UserName != user_name)
            {
                res = _userService.Delete(x => x.user_name == user_name);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="pre_password"></param>
        /// <param name="new_password"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdatePassword(string token, string pre_password,string new_password)
        {
            bool res = false;
            if (token.Contains("Bearer"))
            {
                token = token.Replace("Bearer", string.Empty);
                token = token.Trim();
            }
            UserToken userToken = _tokenService.ValidateToken(token);
            if (userToken.UserName != "")
            {
                UserInfo userInfo = _userService.Queryable().Where(x => x.user_name == userToken.UserName)?.First();
                if (userInfo != null)
                {
                    if(userInfo.pass_word == pre_password.EncodeBase64(Encoding.UTF8))
                    {
                        userInfo.pass_word = new_password.EncodeBase64(Encoding.UTF8);
                        res = _userService.Update(userInfo);
                    }
                }
            }
            return new JsonResult(res);
        }

    }
}
