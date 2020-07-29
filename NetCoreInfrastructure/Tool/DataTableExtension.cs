using Microsoft.Extensions.DependencyInjection;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreInfrastructure
{
    public static class DataTableExtension
    {
        public static IServiceCollection AddInitDataTable(this IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            //创建表格
            var UserRepository = serviceProvider.GetRequiredService<IUserRepository>();
            var OvenInfoRepository = serviceProvider.GetRequiredService<IOvenInfoRepository>();
            var OvenCurrentDataRepository = serviceProvider.GetRequiredService<IOvenCurrentDataRepository>();
            var OvenHistoryDataRepository = serviceProvider.GetRequiredService<IOvenHistoryDataRepository>();
            var EmailConfigDataRepository = serviceProvider.GetRequiredService<IEmailConfigRepository>();
            var EmailReceiverRepository = serviceProvider.GetRequiredService<IEmailReceiverRepository>();

            var ProjectInfoRepository = serviceProvider.GetRequiredService<IProjectInfoRepository>();
            var HeatStepRepository = serviceProvider.GetRequiredService<IHeatStepRepository>();
            var TmSigRepository = serviceProvider.GetRequiredService<ITmSigRepository>();
            var PidRepository = serviceProvider.GetRequiredService<IPidRepository>();

            OvenInfoRepository.InitTables();
            UserRepository.InitTables();
            OvenCurrentDataRepository.InitTables();
            OvenHistoryDataRepository.InitTables();
            EmailConfigDataRepository.InitTables();
            EmailReceiverRepository.InitTables();
            ProjectInfoRepository.InitTables();
            HeatStepRepository.InitTables();
            TmSigRepository.InitTables();
            PidRepository.InitTables();
            return services;
        }
    }
}
