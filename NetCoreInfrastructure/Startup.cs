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
            //ע�����ݿ����
            services.AddSqlSugar(Configuration, ServiceLifetime.Scoped);        //ע��sqlsugar
            ServiceExtension.RegisterAssembly(services, "Service");   //����ע��
            ServiceExtension.RegisterAssembly(services, "Repository");//�ֿ�ע��
            services.AddHttpClientFactoryHelper();//http���󹤳�ע��
            GlobalVar.Author_url = Configuration.GetValue<string>("Author_url").ToString();//��ȡȨ����֤api��ַ
            //��ʼ���������
            services.AddInitDataTable();
            services.AddSwaggerGen(c =>
            {
                // ����ĵ���Ϣ
                c.SwaggerDoc("Common", new OpenApiInfo { Title = "�ӿ�", Version = "Common" });   //������ʾ
                //c.SwaggerDoc("Dashboard", new OpenApiInfo { Title = "Dashboard����Դ", Version = "Dashboard" });   //������ʾ

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(basePath, "OvenMonitoring.xml");
                c.IncludeXmlComments(xmlPath);
                //��������ע��
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
            // ����SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Common/swagger.json", "�ӿ�");
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
