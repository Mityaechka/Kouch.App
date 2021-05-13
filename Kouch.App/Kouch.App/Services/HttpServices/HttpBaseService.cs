using Kouch.App.Entities;
using Kouch.App.Extensions;
using Kouch.App.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kouch.App.Services
{
    public class HttpBaseService
    {
        private static HttpClient http = new HttpClient();
        private readonly JsonSerializerSettings serializerSettings;

        private static HttpBaseService _instance;
        public static HttpBaseService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HttpBaseService();
                }

                return _instance;
            }
        }

        private HttpBaseService()
        {
            serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
        public async Task<ApiResnonse<T>> Get<T>(string url, bool needToken = true)
        {
            return await SendRequest<T>(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{AppSettingsService.Instance["ApiUrl"]}/{url}")
            });
        }
        public async Task<ApiResnonse> Get(string url, bool needToken = true)
        {
            return await SendRequest(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{AppSettingsService.Instance["ApiUrl"]}/{url}")
            }, needToken);
        }
        public async Task<ApiResnonse> Send<T>(HttpMethod method, string url, T data = null, bool needToken = true) where T : class
        {
            StringContent content = null;
            if (data != null)
            {
                var d = JsonConvert.SerializeObject(data);
                content = new StringContent(JsonConvert.SerializeObject(data, serializerSettings), Encoding.UTF8, "application/json");
            }
            return await SendRequest(new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{AppSettingsService.Instance["ApiUrl"]}/{url}"),
                Content = content
            }, needToken);
        }
        public async Task<ApiResnonse<U>> Send<T, U>(HttpMethod method, string url, T data = null, bool needToken = true) where T : class
        {
            StringContent content = null;
            if (data != null)
            {
                content = new StringContent(JsonConvert.SerializeObject(data, serializerSettings), Encoding.UTF8, "application/json");
            }
            return await SendRequest<U>(new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{AppSettingsService.Instance["ApiUrl"]}/{url}"),
                Content = content
            }, needToken);
        }
        public async Task<ApiResnonse> SendContent(HttpMethod method, string url, HttpContent content, bool needToken = true) 
        {
            return await SendRequest(new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{AppSettingsService.Instance["ApiUrl"]}/{url}"),
                Content = content
            }, needToken);
        }
        private async Task<ApiResnonse<U>> SendRequest<U>(HttpRequestMessage requestMessage, bool needToken = true)
        {
            try
            {
                if (needToken)
                {
                    TokenModel token = await TokenStorageService.Instance.GetToken();
                    if (token == null)
                    {
                        return new ApiResnonse<U> { Error = "Ошибка при авторизации запроса. Попробуйте позже", IsSuccsess = false };
                    }
                    else
                    {
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Access);
                    }
                }
                var response = await http.SendAsync(requestMessage).WaitOrCancel(App.CancelToken);
                string content = await response.Content.ReadAsStringAsync();
                System.Net.HttpStatusCode status = response.StatusCode;
                switch ((int)status)
                {
                    case 200:
                        return new ApiResnonse<U> { Result = JsonConvert.DeserializeObject<U>(content), IsSuccsess = true };
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
            }
            catch (Exception e)
            {
                return new ApiResnonse<U> { Error = "Ошибка сервера", IsSuccsess = false };
            }
        }
        public async Task<ApiResnonse> SendRequest(HttpRequestMessage requestMessage, bool needToken = true)
        {

            try
            {
                if (needToken)
                {
                    TokenModel token = await TokenStorageService.Instance.GetToken();
                    if (token == null)
                    {
                        return new ApiResnonse { Error = "Ошибка при авторизации запроса. Попробуйте позже", IsSuccsess = false };
                    }
                    else
                    {
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Access);
                    }
                }

                var response = await http.SendAsync(requestMessage).WaitOrCancel(App.CancelToken);
                string content = await response.Content.ReadAsStringAsync();
                System.Net.HttpStatusCode status = response.StatusCode;
                switch ((int)status)
                {
                    case 200:
                        return new ApiResnonse { IsSuccsess = true };
                    case 400:
                        var jObject = JObject.Parse(content);
                        var error = "Ошибка сервера";

                        if (jObject.ContainsKey("error"))
                        {
                            error = jObject["error"].ToString();
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
        public void SetToken(string jwt)
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(jwt)));
        }
    }
}
