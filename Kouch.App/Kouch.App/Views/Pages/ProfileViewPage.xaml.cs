using Kouch.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileViewPage : ContentPage
    {
        public ProfileViewPage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewViewModel(Navigation);
        }
    }
}