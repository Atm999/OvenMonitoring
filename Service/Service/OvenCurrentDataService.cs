using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Service
{
    public class OvenCurrentDataService : OvenCurrentDataRepository, IOvenCurrentDataService
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public OvenCurrentDataService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
    }
}
