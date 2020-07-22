using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorize.Entity
{
    public class UserToken
    {
        public string UserName { set; get; }
        /// <summary>
        /// 用户权限角色
        /// </summary>
        public string Role { set; get; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime expire { set; get; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { set; get; }
        /// <summary>
        /// 授权类型
        /// </summary>
        public string AuthorizeType { set; get; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { set; get; }
    }
}
