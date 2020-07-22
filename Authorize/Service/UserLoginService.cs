using Authorize.Authorize;
using Authorize.Entity;
using Authorize.IService;
using Entity;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreExtention;

namespace Authorize.Service
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserLoginService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        public UserToken LoginAndGetToken(string user_name, string password)
        {
            UserInfo userInfo = _userService.QueryableToEntity(x => x.user_name.Equals(user_name));
            UserToken userToken = new UserToken();
            if (userInfo == null)
            {
                userToken.ErrorMessage = "未查找到用户";
            }
            else if (!userInfo.pass_word.EncodeBase64().Equals(password))
            {
                userToken.ErrorMessage = "用户密码错误";
            }
            else
            {
                userToken.AuthorizeType = "Bearer";
                userToken.UserName = userInfo.user_name;
                userToken.Role = userInfo.Role;
                userToken.Token = _tokenService.GetToken(userInfo);
                userToken.expire = DateTime.Now.AddHours(_tokenService.TokenSetting.ExpireHours);
            }
            return userToken;
        }
    }
}
