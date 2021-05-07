using Kouch.App.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentElement : ContentView
    {
        public CommentElement()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            //Comment comment = (Comment)BindingContext;
            //if (comment.Children!=null&&comment.Children.Count != 0)
            //{
            //    TreeLayout.Children.Add(new CommentsTreeElement
            //    {
            //        BindingContext = comment.Children
            //    });
            //}
            base.OnBindingContextChanged();
        }
    }
}