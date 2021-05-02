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
        public PostPage()
        {
            InitializeComponent();
            BindingContext = new PostViewModel
            {
                Post = new Entities.Post
                {
                    Title = "Lorem Ipsum",
                    Body = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Preview = "logo.png",
                    Files = new List<File>
                    {
                        new File
                        {
                            Path="https://secure.gravatar.com/avatar/ffd6ee1204cd468d96b1bed855c1d6d0/?default=https%3A%2F%2Fvanillicon.com%2F0d67df115d67bf481f39cb3051e746c3_200.png&rating=g&size=130"
                        },
                        new File
                        {
                            Path="logo.png"
                        },
                        new File
                        {
                            Path="logo.png"
                        },
                        new File
                        {
                            Path="logo.png"
                        }
                    }
                }
            };
        }
    }
}