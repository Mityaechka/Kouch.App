using Kouch.App.Entities;
using Kouch.App.ViewModels;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Kouch.App.Converters
{
    class PostToViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new PostViewModel { Post = (Post)value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((PostViewModel)value).Post;
        }
    }
    class PostListToViewModelListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<Post> collection = (ObservableCollection<Post>)value;
            ObservableCollection<PostViewModel> postViewModels = new ObservableCollection<PostViewModel>(collection.Select(x => new PostViewModel { Post = x }));
            return postViewModels;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((PostViewModel)value).Post;
        }
    }
}
