using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreExtention;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OvenMonitoring;

namespace NetCoreInfrastructure
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
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddRazorRuntimeCompilation();
            //注入数据库操作
            services.AddSqlSugar(Configuration, ServiceLifetime.Scoped);        //注入sqlsugar
            ServiceExtension.RegisterAssembly(services, "Service");   //服务注入
            ServiceExtension.RegisterAssembly(services, "Repository");//仓库注入
            services.AddHttpClientFactoryHelper();//http请求工厂注入
            GlobalVar.Author_url = Configuration.GetValue<string>("Author_url").ToString();//获取权限认证api地址
            //初始化创建表格
            services.AddInitDataTable();
            services.AddSwaggerGen(c =>
            {
                // 添加文档信息
                c.SwaggerDoc("Common", new OpenApiInfo { Title = "接口", Version = "Common" });   //分组显示
                //c.SwaggerDoc("Dashboard", new OpenApiInfo { Title = "Dashboard数据源", Version = "Dashboard" });   //分组显示

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "OvenMonitoring.xml");
                c.IncludeXmlComments(xmlPath);
                //启用数据注解
                c.EnableAnnotations();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseSwagger();
            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Common/swagger.json", "接口");
            });


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware(typeof(AuthMiddleWare));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
