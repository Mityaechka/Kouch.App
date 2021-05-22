using Kouch.App.Entities;
using Kouch.App.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kouch.App.Services
{
    public class ApiAuthService
    {
        private static ApiAuthService _instance;
        public static ApiAuthService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiAuthService();
                }

                return _instance;
            }
        }
        private ApiAuthService()
        {

        }
        public async Task<ApiResnonse> Register(RegisterEmailModel model)
        {
            return await HttpBaseService.Instance.Send(HttpMethod.Post, "auth/register/", model, false);
        }
        public async Task<ApiResnonse> ResendVerificationCode(string email)
        {
            return await HttpBaseService.Instance.Send(HttpMethod.Post, "auth/resend-verification-code/", new { email }, false);
        }
        public async Task<ApiResnonse> VerifyAccount(RegisterCodeModel model)
        {
            return await HttpBaseService.Instance.Send(HttpMethod.Post, "auth/verify-account/", model, false);
        }
        public async Task<ApiResnonse<LoginResponseModel>> Login(LoginRequestModel model)
        {
            return await HttpBaseService.Instance.Send<LoginRequestModel, LoginResponseModel>(HttpMethod.Post, "auth/login/", model, false);
        }
        public async Task<ApiResnonse<TokenModel>> RefreshToken(string refresh)
        {
            return await HttpBaseService.Instance.Send<object, TokenModel>(HttpMethod.Post, "auth/login/refresh/", new { refresh }, false);
        }
        public async Task<ApiResnonse> CheckConnection()
        {
            return await HttpBaseService.Instance.Get("health-check/", false);
        }
        public async Task<ApiResnonse> Logout(string refresh)
        {
            return await HttpBaseService.Instance.Send(HttpMethod.Post, "auth/logout/", new { refresh }, true);
        }
    }
}
