using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Kouch.App.Entities;
using Kouch.App.Models;

namespace Kouch.App.Services
{
    public class ApiUserService
    {
        private static ApiUserService _instance;
        public static ApiUserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiUserService();
                }

                return _instance;
            }
        }
        private ApiUserService()
        {

        }
        public async Task<ApiResnonse<User>> GetMe()
        {
            return await HttpBaseService.Instance.Get<User>("users/me/");
        }
        public async Task UpdateMe()
        {
            ApiResnonse<User> response = await GetMe();
            if (response.IsSuccsess)
            {
                UserStorageService.Instance.User = response.Result;
            }
        }
        public async Task<ApiResnonse> EditAvatar(byte[] avatar)
        {
            MultipartFormDataContent dataContent = new MultipartFormDataContent();
            dataContent.Add(new ByteArrayContent(avatar), "image", "avatar.png");
            return await HttpBaseService.Instance.SendContent(HttpMethod.Post, "users/me/edit/upload-profile-avatar/", dataContent);
        }
        public async Task<ApiResnonse> EditProfile(UserEditModel userEditModel)
        {
            return await HttpBaseService.Instance.Send<object>(HttpMethod.Post, "users/me/edit/", new
            {
                profile = userEditModel
            });
        }
    }
}
