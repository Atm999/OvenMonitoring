using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Service
{
    public class PidService : PidRepository, IPidService
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public PidService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
    }
}
