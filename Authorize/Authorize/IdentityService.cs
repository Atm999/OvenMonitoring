using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorize.Authorize
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;
        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string GetUserName()
        {
            string user_name = _context.HttpContext.User.FindFirst("user_name")?.Value;

            return user_name;
        }

        public string GetUserRole()
        {
            string user_role = _context.HttpContext.User.FindFirst("user_role")?.Value;

            return user_role;
        }
    }
}
