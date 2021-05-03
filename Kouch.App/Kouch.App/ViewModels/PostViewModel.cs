using Kouch.App.Entities;
using Kouch.App.Services;
using Kouch.App.Views.Modals;
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
    public class PostViewModel : BaseViewModel
    {
        private readonly ApiPostsService apiPostsService = new ApiPostsService();
        public ICommand LoadCommentsCommand { get; set; }
        public ICommand AddCommentCommand { get; set; }
        public ICommand AddBookmarkCommand { get; set; }
        public ICommand RemoveBookmarkCommand { get; set; }

        private Post post;
        private bool isCommentsLoading;

        private ObservableCollection<Comment> comments;
        private bool isCommentsLoadingError;

        public bool IsCommentsLoadingError
        {
            get { return isCommentsLoadingError; }
            set
            {
                if (isCommentsLoadingError != value)
                {
                    isCommentsLoadingError = value;
                    OnPropertyChanged("IsCommentsLoadingError");
                }
            }
        }

        public ObservableCollection<Comment> Comments
        {
            get => comments; set
            {
                if (comments != value)
                {
                    comments = value;
                    OnPropertyChanged("Comments");
                }
            }
        }
        public bool IsCommentsLoading
        {
            get { return isCommentsLoading; }
            set
            {
                if (isCommentsLoading != value)
                {
                    isCommentsLoading = value;
                    OnPropertyChanged("IsCommentsLoading");
                }
            }
        }

        public Post Post
        {
            get => post; set
            {
                if (post != value)
                {
                    post = value;
                    OnPropertyChanged("Post");
                }
            }
        }
        public bool IsBookmark
        {
            get => Post.IsBookmark; set
            {
                if (Post.IsBookmark != value)
                {
                    Post.IsBookmark = value;
                    OnPropertyChanged("IsBookmark");
                    OnPropertyChanged("IsNotBookmark");
                }
            }
        }
        public bool IsNotBookmark => !IsBookmark;
        public PostViewModel(Post post) : base(null)
        {
            Post = post;
            LoadCommentsCommand = new Command(async () => await LoadComments());
            AddCommentCommand = new Command<int>(async (int commentId) => await AddCommentClick(commentId));
            AddBookmarkCommand = new Command(async () => await AddBookmark());
            RemoveBookmarkCommand = new Command(async () => await RemoveBookmark());
            Comments = new ObservableCollection<Comment>();

            Init();
        }
        public async void Init()
        {
            await LoadComments();
        }
        public async Task LoadComments()
        {
            IsCommentsLoadingError = false;
            IsCommentsLoading = true;
            var commentsResponse = await apiPostsService.GetComments(Post.Id);
            IsCommentsLoading = false;
            Comments.Clear();
            if (commentsResponse.IsSuccsess)
            {
                IsCommentsLoading = false;
                foreach (var item in commentsResponse.Result.Data)
                {
                    Comments.Add(item);
                }
            }
            else
            {
                IsCommentsLoadingError = true;
            }
        }

        public async Task AddCommentClick(int commentId)
        {
            await Navigation.PushPopupAsync(new LoadingModal());
        }
        private async Task AddBookmark()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            await Task.Delay(1000);
            await Navigation.PopPopupAsync();
            IsBookmark = true;
        }
        private async Task RemoveBookmark()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            await Task.Delay(1000);
            await Navigation.PopPopupAsync();
            IsBookmark = false;
        }
    }
}
