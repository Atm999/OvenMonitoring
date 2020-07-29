using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Service
{
    public class ProjectInfoService : ProjectInfoRepository, IProjectInfoService
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public ProjectInfoService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
    }
}
