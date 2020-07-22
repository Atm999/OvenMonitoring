using CoreExtention;
using Entity;
using Repository;
using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Service
{
    public class UserService : UserRepository, IUserService
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public UserService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
        public UserInfo Check(string username ,string password)
        {
            UserInfo userInfo = new UserInfo();
            //密码加密
            string pw = SharedHelpers.EncodeBase64(password);
            List<UserInfo> list = Queryable().ToList();
            UserInfo User = list.Where(x => x.user_name == username && x.pass_word == pw).FirstOrDefault();
            if(User != null)
            {
                userInfo = User;
            }
            return userInfo;
        }
    }
}
