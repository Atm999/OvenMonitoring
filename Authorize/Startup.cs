using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Authorize.Authorize;
using Authorize.IService;
using Authorize.Service;
using CoreExtention;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Authorize
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // 添加文档信息
                c.SwaggerDoc("JWT", new OpenApiInfo { Title = "JWT权限认证", Version = "JWT" });   //分组显示

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                    //In = "header",
                    //Type = "apiKey"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {  new OpenApiSecurityScheme
                   {
                    Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                   },
                    new string[] { }
                }
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Authorize.xml");

                c.IncludeXmlComments(xmlPath);
                //启用数据注解
                c.EnableAnnotations();
            });
            services.InjectAuthentication(Configuration);
            services.AddSqlSugar(Configuration, ServiceLifetime.Scoped);        //注入sqlsugar
            ServiceExtension.RegisterAssembly(services, "Service");   //服务注入
            ServiceExtension.RegisterAssembly(services, "Repository");//仓库注入
            services.AddScoped<IUserLoginService, UserLoginService>();          //用户登录接口对象注入
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/JWT/swagger.json", "JWT权限认证");

            });

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
