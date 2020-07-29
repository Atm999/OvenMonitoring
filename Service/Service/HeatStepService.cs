using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Service
{
    public class HeatStepService : HeatStepRepository, IHeatStepService
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public HeatStepService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
    }
}
