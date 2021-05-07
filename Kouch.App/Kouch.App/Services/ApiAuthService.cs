using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kouch.App.Entities;
using Kouch.App.Models;

namespace Kouch.App.Services
{
    public class ApiAuthService
    {
        private readonly HttpBaseService httpBaseService = new HttpBaseService();
        public async Task<ApiResnonse> Register(RegisterEmailModel model)
        {
            return await httpBaseService.Send(HttpMethod.Post, "auth/register/", model);
        }
        public async Task<ApiResnonse> VerifyAccount(RegisterCodeModel model)
        {
            return await httpBaseService.Send(HttpMethod.Post, "verify-account/", model);
        }

    }
}
