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
                // ����ĵ���Ϣ
                c.SwaggerDoc("JWT", new OpenApiInfo { Title = "JWTȨ����֤", Version = "JWT" });   //������ʾ

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "���������Bearer��Token",
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
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(basePath, "Authorize.xml");

                c.IncludeXmlComments(xmlPath);
                //��������ע��
                c.EnableAnnotations();
            });
            services.InjectAuthentication(Configuration);
            services.AddSqlSugar(Configuration, ServiceLifetime.Scoped);        //ע��sqlsugar
            ServiceExtension.RegisterAssembly(services, "Service");   //����ע��
            ServiceExtension.RegisterAssembly(services, "Repository");//�ֿ�ע��
            services.AddScoped<IUserLoginService, UserLoginService>();          //�û���¼�ӿڶ���ע��
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
            // ����SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/JWT/swagger.json", "JWTȨ����֤");

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
