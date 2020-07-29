using Entity;
using Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class OvenCurrentDataRepository : BaseRepository<OvenCurrentData>, IOvenCurrentDataRepository
    {
        public OvenCurrentDataRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }
    }
}
