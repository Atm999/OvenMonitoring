using Authorize.Entity;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorize.IService
{
    public interface IUserLoginService
    {
        UserToken LoginAndGetToken(string user_name,string password);
    }
}
