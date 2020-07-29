using Entity;
using Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class EmailConfigRepository : BaseRepository<EmailConfig>, IEmailConfigRepository
    {
        public EmailConfigRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }
    }
}
