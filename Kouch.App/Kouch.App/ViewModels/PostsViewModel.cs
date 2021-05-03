using Kouch.App.Entities;
using Kouch.App.Models;
using Kouch.App.Services;
using Kouch.App.Views.Modals;
using Kouch.App.Views.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    class PostsViewModel : AsyncBaseViewModel
    {
        private readonly ApiPostsService apiPostsService = new ApiPostsService();
        public ICommand ScrolledEvent { get; set; }
        private Post selectedPost;
        private readonly int PageSize;
        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();
        private bool isEnd;
        

        public bool IsEnd
        {
            get { return isEnd; }
            set
            {
                if (isEnd != value)
                {
                    isEnd = value;
                    OnPropertyChanged("IsEnd");
                }
            }
        }
        
        public Post SelectedPost
        {
            get => selectedPost;
            set
            {
                Post tempPost = value;
                selectedPost = null;
                OpenPost(tempPost);
                OnPropertyChanged("SelectedPost");
            }
        }
        public PostsViewModel(INavigation navigation) : base(navigation)
        {
            PageSize = AppSettingsService.Settings.GetValue<int>("PageSize");
            ScrolledEvent = new Command(async () => await LoadNextPage());

            Init();
        }
        private async void Init()
        {
            await LoadNextPage();
        }
        private async Task LoadNextPage()
        {
            if (IsEnd || IsLoading)
            {
                return;
            }
            IsLoading = true;
            ApiResnonse<PaginationModel<Post>> posts = await apiPostsService.GetPosts(Posts.Count, PageSize);
            IsLoading = false;
            if (posts.IsSuccsess)
            {
                IsEnd = posts.Result.IsEnd;
                foreach (var item in posts.Result.Data)
                {
                    Posts.Add(item);
                }
                IsError = false;
            }
            else
            {
                IsError = true;
                Error = posts.Error;
            }
        }
        private async void OpenPost(Post post)
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var postResponse = await apiPostsService.GetPost(post.Id);
            await Navigation.PopPopupAsync();
            await Navigation.PushAsync(new PostPage(postResponse.Result));
        }
    }
}
