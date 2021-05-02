using Kouch.App.Entities;
using Kouch.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.ViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostViewCell : ViewCell
    {

        public PostViewCell()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            //if (BindingContext.GetType() == typeof(Post))
            //{
            //    Post post = (Post)BindingContext;
            //    PostViewModel = new PostViewModel()
            //    {
            //        Post = post
            //    };
            //    BindingContext = PostViewModel;
            //    base.OnBindingContextChanged();
            //}
            base.OnBindingContextChanged();
        }
    }
}