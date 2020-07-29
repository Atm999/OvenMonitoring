using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Service
{
    public class OvenHistoryDataService: OvenHistoryDataRepository, IOvenHistoryDataService
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public OvenHistoryDataService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
    }
}
