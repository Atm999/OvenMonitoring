using Entity;
using Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class OvenInfoRepository: BaseRepository<OvenInfo>, IOvenInfoRepository
    {
        public OvenInfoRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }
    }
}
