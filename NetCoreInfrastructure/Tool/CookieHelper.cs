using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvenMonitoring.Tool
{
    public class CookieHelper
    {
        public void SetCookies(HttpContext httpContext, string key, string value, DateTime expire)
        {
            httpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = expire,
            });
        }
        public void DeleteCookies(HttpContext httpContext, string key)
        {
            httpContext.Response.Cookies.Delete(key);
        }
        public string GetCookies(HttpContext httpContext, string key)
        {
            httpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
    }
}
