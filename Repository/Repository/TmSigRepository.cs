using Entity;
using Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class TmSigRepository : BaseRepository<TmSig>, ITmSigRepository
    {
        public TmSigRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }
    }
}
