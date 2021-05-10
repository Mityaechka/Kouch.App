using System.Threading.Tasks;
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
        public async Task<ApiResnonse> GetMe()
        {
            return await HttpBaseService.Instance.Get("users/me/");
        }
    }
}
