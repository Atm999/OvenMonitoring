using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreExtention
{
    public interface IHttpClientFactoryHelper
    {
        Task<T> GetJsonResult<T>(string url, string postData, HttpMethod method, string authorization = null, string token = null);
    }
}
