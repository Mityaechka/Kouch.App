using Kouch.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        private readonly AuthViewModel viewModel;
        public AuthPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AuthViewModel(Navigation);
        }
        protected override bool OnBackButtonPressed()
        {
            if (viewModel.OpenRegisterPageCommand.CanExecute(null))
            {
                viewModel.OpenRegisterPageCommand.Execute(null);
            }
            return false;
        }
    }
}