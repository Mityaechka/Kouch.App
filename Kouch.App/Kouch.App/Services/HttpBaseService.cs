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
        public HttpBaseService()
        {

        }
        public async Task<ApiResnonse<T>> Get<T>(string url)
        {
            return await SendRequest<T>(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{AppSettingsService.Settings["ApiUrl"]}/{url}")
            });
        }
        public async Task<ApiResnonse> Get(string url)
        {
            return await SendRequest(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{AppSettingsService.Settings["ApiUrl"]}/{url}")
            });
        }
        public async Task<ApiResnonse> Send<T>(HttpMethod method,string url,T data = null) where T : class
        {
            StringContent content = null;
            if (data != null)
            {
                content = new StringContent(JsonConvert.SerializeObject(data));
            }
            return await SendRequest(new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{AppSettingsService.Settings["ApiUrl"]}/{url}"),
                Content = content
            });
        }
        public async Task<ApiResnonse<U>> Send<T, U>(HttpMethod method, string url, T data = null) where T : class
        {
            StringContent content = null;
            if (data != null)
            {
                content = new StringContent(JsonConvert.SerializeObject(data));
            }
            return await SendRequest<U>(new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{AppSettingsService.Settings["ApiUrl"]}/{url}"),
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
                        return new ApiResnonse<U> { Result = JsonConvert.DeserializeObject<U>(content),IsSuccsess = true };
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
        private async Task<ApiResnonse> SendRequest(HttpRequestMessage requestMessage)
        {
            try
            {
                var response = await http.SendAsync(requestMessage);
                string content = await response.Content.ReadAsStringAsync();
                System.Net.HttpStatusCode status = response.StatusCode;
                switch ((int)status)
                {
                    case 200:
                        return new ApiResnonse { IsSuccsess=true };
                    case 400:
                        var jObject = JObject.Parse(content);
                        var error = "Ошибка сервера";

                        if (jObject.ContainsKey("details"))
                        {
                            error = jObject["details"].ToString();
                        }
                        return new ApiResnonse { Error = error, IsSuccsess = false };
                    default:
                        return new ApiResnonse { Error = "Ошибка сервера", IsSuccsess = false };
                }
            }
            catch (Exception e)
            {
                return new ApiResnonse { Error = "Ошибка сервера", IsSuccsess = false };
            }
        }
    }
}
