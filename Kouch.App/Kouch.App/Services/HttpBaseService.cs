using Kouch.App.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kouch.App.Services
{
    public class HttpBaseService
    {
        private HttpClient http = new HttpClient();
        private string baseUrl = "";
        public HttpBaseService()
        {

        }
        public async Task<ApiResnonse<T>>Get<T>(string url)
        {
            return await SendRequest<T>(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri =new Uri($"{baseUrl}/{url}")
            });
        }
        public async Task<ApiResnonse<U>> Send<T,U>(HttpMethod method,string url,U data = null) where U : class
        {
            StringContent content = null;
            if (data != null)
            {
                content = new StringContent(JsonConvert.SerializeObject(data));
            }
            return await SendRequest<U>(new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{baseUrl}/{url}"),
                Content = content
            });
        }
        private async Task<ApiResnonse<U>> SendRequest<U>(HttpRequestMessage requestMessage)
        {
            try
            {
                var response = await http.SendAsync(requestMessage);
                string content = await response.Content.ReadAsStringAsync();
                System.Net.HttpStatusCode status = response.StatusCode;
                switch ((int)status)
                {
                    case 200:
                        return new ApiResnonse<U> { Data = JsonConvert.DeserializeObject<U>(content) };
                    case 400:
                        var jObject = JObject.Parse(content);
                        var error = "Ошибка сервера";

                        if (jObject.ContainsKey("details"))
                        {
                            error = jObject["details"].ToString();
                        }
                        return new ApiResnonse<U> { Error = error, IsSuccsess = false };
                    default:
                        return new ApiResnonse<U> { Error = "Ошибка сервера", IsSuccsess = false };
                }
            }catch(Exception e)
            {
                return new ApiResnonse<U> { Error = "Ошибка сервера", IsSuccsess = false };
            }
        }
    }
}
