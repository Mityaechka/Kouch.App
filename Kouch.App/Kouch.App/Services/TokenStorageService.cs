using Kouch.App.Entities;
using Kouch.App.Services;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Threading.Tasks;

namespace Kouch.App.Models
{
    public class TokenStorageService
    {
        private static TokenStorageService _instance;
        public static TokenStorageService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TokenStorageService();
                }

                return _instance;
            }
        }
        private DateTime TokenReceiveTime;
        private TokenStorageService()
        {
        }
        public void ClearToken() {
            TokenReceiveTime = new DateTime();
            CrossSettings.Current.AddOrUpdateValue("HttpTokens", "");
        }
        public void ClearAuthData()
        {
            CrossSettings.Current.AddOrUpdateValue("AuthData", "");
        }
        public async Task<TokenModel> GetToken()
        {
            string rawToken = CrossSettings.Current.GetValueOrDefault("HttpTokens", "");
            string rawAuthData = CrossSettings.Current.GetValueOrDefault("AuthData", "");


            if (string.IsNullOrEmpty(rawToken))
            {
                if (string.IsNullOrEmpty(rawAuthData))
                {
                    return null;
                }
                else
                {
                    LoginRequestModel loginRequestModel = JsonConvert.DeserializeObject<LoginRequestModel>(rawAuthData);
                    ApiResnonse<LoginResponseModel> loginResponse = await ApiAuthService.Instance.Login(loginRequestModel);
                    if (loginResponse.IsSuccsess)
                    {
                        SaveToken(loginResponse.Result.Tokens);
                        return loginResponse.Result.Tokens;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                var tokenData = JsonConvert.DeserializeObject<TokenModel>(rawToken);
                if (TokenReceiveTime.AddSeconds(3) > DateTime.Now)
                {
                    return tokenData;
                }
                else
                {
                    ApiResnonse<TokenModel> refreshResponse = await ApiAuthService.Instance.RefreshToken(tokenData.Refresh);
                    if (refreshResponse.IsSuccsess)
                    {
                        TokenModel tokenModel = refreshResponse.Result;
                        tokenModel.Refresh = tokenData.Refresh;

                        SaveToken(tokenModel);
                        return tokenModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public void SaveToken(TokenModel tokenModel)
        {
            TokenReceiveTime = DateTime.Now;
            CrossSettings.Current.AddOrUpdateValue("HttpTokens", JsonConvert.SerializeObject(tokenModel));
        }
        public void SaveAuthData(LoginRequestModel model)
        {
            CrossSettings.Current.AddOrUpdateValue("AuthData", JsonConvert.SerializeObject(model));
        }
    }
}
