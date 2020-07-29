using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Service
{
    public class EmailConfigService : EmailConfigRepository, IEmailConfigService
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public EmailConfigService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
    }
}
