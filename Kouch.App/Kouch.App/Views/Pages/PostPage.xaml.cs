using Kouch.App.Entities;
using Kouch.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostPage : ContentPage
    {
        public PostPage(Post post)
        {
            InitializeComponent();
            BindingContext = new PostViewModel(post);
        }
    }
}