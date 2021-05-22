using Kouch.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        private readonly LoadingViewModel loadingViewModel;
        public LoadingPage()
        {
            InitializeComponent();
            loadingViewModel = new LoadingViewModel(Navigation)
            {
                IsLoading = true
            };
            BindingContext = loadingViewModel;
            //Thread.Sleep(5000);
            //loadingViewModel.IsLoading = false;
        }
    }
}