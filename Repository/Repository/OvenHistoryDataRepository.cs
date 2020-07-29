using Entity;
using Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class OvenHistoryDataRepository : BaseRepository<OvenHistoryData>, IOvenHistoryDataRepository
    {
        public OvenHistoryDataRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }
    }
}
