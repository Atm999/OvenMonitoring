using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorize.Authorize
{
    public interface IIdentityService
    {
        string GetUserName();
        string GetUserRole();
    }
}
