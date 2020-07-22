using Authorize.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorize.Authorize
{
    public static class AuthenticationExtention
    {
        /// <summary>
        /// 注入jwt授权
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection InjectAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSetting = new JwtSetting();
            Configuration.Bind("JwtSetting", jwtSetting);//读取配置

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));//将读取到的配置注入

            services.AddHttpContextAccessor();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidIssuer = jwtSetting.Issuer,
                       ValidAudience = jwtSetting.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),
                       // 默认 300s
                       ClockSkew = TimeSpan.Zero
                   };
               });
            return services;
        }

        public static IServiceCollection AddJwtAuthenticationWithProtectedCookie(
            this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddAuthentication()
            //.AddCookie(cfg = &gt; cfg.SlidingExpiration = true)
            //.AddJwtBearer(options = &gt;
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //    };
            //});

            return services;
        }
    }
}
