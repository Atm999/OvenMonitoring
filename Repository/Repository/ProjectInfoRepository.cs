using Entity;
using Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class ProjectInfoRepository : BaseRepository<ProjectInfo>, IProjectInfoRepository
    {
        public ProjectInfoRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }
    }
}
