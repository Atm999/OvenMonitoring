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
            UserRepository.InitTables();
            return services;
        }
    }
}
