using Kouch.App.Entities;
using Kouch.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kouch.App.Services
{
    public class ApiPostsService
    {
        HttpBaseService httpBaseService = new HttpBaseService();

        public async Task<ApiResnonse<PaginationModel<Post>>> GetPosts(int skip, int take)
        {
            return await httpBaseService.Get<PaginationModel<Post>>($"posts?skip={skip}&take={take}");
        }
        public async Task<ApiResnonse<Post>> GetPost(int id)
        {
            return await httpBaseService.Get<Post>($"posts/{id}");
        }
        public async Task<ApiResnonse<PaginationModel<Comment>>> GetComments(int id)
        {
            return await httpBaseService.Get<PaginationModel<Comment>>($"posts/{id}//posts/{id}/load-comments/");
        }

        internal Task GetPosts()
        {
            throw new NotImplementedException();
        }
    }
}
