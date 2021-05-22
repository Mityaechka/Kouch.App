using Kouch.App.Entities;
using Kouch.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileEditPage : ContentPage
    {
        private readonly User user;

        public ProfileEditPage(User user)
        {
            InitializeComponent();
            this.user = user;
            BindingContext = new ProfileEditViewModel(user, Navigation);
        }

    }
}