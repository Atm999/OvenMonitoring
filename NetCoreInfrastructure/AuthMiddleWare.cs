using CoreExtention;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvenMonitoring
{
    public class AuthMiddleWare
    {
        private readonly RequestDelegate next;
        //private readonly IHttpClientFactoryHelper _httpClientFactoryHelper;
        public AuthMiddleWare(RequestDelegate next)
        {
            this.next = next;
            //_httpClientFactoryHelper = httpClientFactoryHelper;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context.Request.Path.Value != "/Login")
                {
                    context.Request.Cookies.TryGetValue("token", out string token);
                    if (string.IsNullOrEmpty(token))
                    {
                        //string url = GlobalVar.Author_url + "/JWT/ValidateToken?AuthToken=" + token;
                        //bool re = await _httpClientFactoryHelper.GetJsonResult<bool>(url, "", System.Net.Http.HttpMethod.Get);
                        //if (re)
                        //{
                        //    //放行
                        //    await next(context);
                        //}
                    }
                    context.Request.Path = "/Login";
                }
                await next(context);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
