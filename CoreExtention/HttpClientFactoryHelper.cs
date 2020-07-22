using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreExtention
{

    public class HttpClientFactoryHelper : IHttpClientFactoryHelper
    {

        private readonly IHttpClientFactory _clientFactory;

        public HttpClientFactoryHelper(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }

        public async Task<T> GetJsonResult<T>(string url, string postData, HttpMethod method, string authorization = null, string token = null)
        {
            T resp = default(T);
            try
            {
                var request = new HttpRequestMessage(method, url);

                //判断是否需要认证
                if (!string.IsNullOrEmpty(authorization))
                {
                    request.Headers.Add("Authorization", authorization);
                }
                //将token传入到Header中，以便API调用
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Add("Authorization", token);
                }

                var client = _clientFactory.CreateClient();

                HttpResponseMessage response;

                if (!string.IsNullOrEmpty(postData))
                {
                    HttpContent httpContent = new StringContent(postData, Encoding.UTF8);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    request.Content = httpContent;
                }
                response = await client.SendAsync(request);
                if (response != null && response.IsSuccessStatusCode)
                {
                    resp = await response.Content.ReadAsAsync<T>();
                    return resp;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //LogHelper.Info($"MyHttpClientHelper结束，url={client.BaseAddress},action={Utils.GetEnumDescription(action)},postData={paramStr} ,jrclientguid={jrclientguid}---------");
            }
            return resp;
        }

    }
}
