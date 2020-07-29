using Repository.Repository;
using Service.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Service
{
    public class EmailReceiverService : EmailReceiverRepository, IEmailReceiverService
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public EmailReceiverService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
    }
}
