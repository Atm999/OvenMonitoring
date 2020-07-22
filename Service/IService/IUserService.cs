using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Entity;

namespace Service.IService
{
    public interface IUserService : IBaseRepository<UserInfo>
    {
        UserInfo Check(string username, string password);
    }
}
