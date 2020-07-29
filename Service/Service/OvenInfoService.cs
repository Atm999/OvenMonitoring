using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Service
{
    public class OvenInfoService: OvenInfoRepository, IOvenInfoService
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public OvenInfoService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
    }
}
