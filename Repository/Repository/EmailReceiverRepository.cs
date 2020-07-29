using Entity;
using Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class EmailReceiverRepository : BaseRepository<EmailReceiver>, IEmailReceiverRepository
    {
        public EmailReceiverRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }
    }
}
