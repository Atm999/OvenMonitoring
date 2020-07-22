using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using Repository.IRepository;
using SqlSugar;

namespace Repository.Repository
{
    public class UserRepository : BaseRepository<UserInfo>, IUserRepository
    {
        public UserRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }
    }
}
