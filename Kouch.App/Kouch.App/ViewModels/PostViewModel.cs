using Kouch.App.Entities;
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
        public ICommand LoadCommentsCommand { get; set; }
        public ICommand AddCommentCommand { get; set; }

        private Post post;
        private bool isCommentsLoading;
        
        private ObservableCollection<Comment> comments;
        private bool isCommentsLoadingError;

        public bool IsCommentsLoadingError
        {
            get { return isCommentsLoadingError; }
            set {
                if (isCommentsLoadingError != value)
                {
                    isCommentsLoadingError = value;
                    OnPropertyChanged("IsCommentsLoadingError");
                }
            }
        }

        public ObservableCollection<Comment> Comments { get => comments; set  {
                if (comments != value)
                {
                    comments = value;
                    OnPropertyChanged("Comments");
                }
            } }
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
        public PostViewModel() : base(null)
        {
            LoadCommentsCommand = new Command(async () => await LoadComments());
            AddCommentCommand = new Command<int>(async (int commentId) => await AddCommentClick(commentId));

        }

        public async Task LoadComments()
        {
            IsCommentsLoading = true;
            await Task.Delay(1000);
            IsCommentsLoading = false;

            Comments = new ObservableCollection<Comment>();
            Comments.Add(new Comment
            {
                Name = "Юзер 1",
                Content = "Хороший пост"
            });
            Comments.Add(new Comment
            {
                Name = "Юзер 2",
                Content = "Пост говно",
                Children = new List<Comment>
                {
                    new Comment
                    {
                        Name = "Юзер 3",
                        Content = "Не согласен"
                    },
                    new Comment
                    {
                        Name = "Юзер 4",
                        Content="Маму в кино водил",
                        Children = new List<Comment>
                        {
                            new Comment
                            {
                                Name="Юзер 2",
                                Content="Саси"
                            }
                        }
                    }
                }
            });
        }

        public async Task AddCommentClick(int commentId )
        {
            await Navigation.PushPopupAsync(new LoadingModal());
        }
    }
}
