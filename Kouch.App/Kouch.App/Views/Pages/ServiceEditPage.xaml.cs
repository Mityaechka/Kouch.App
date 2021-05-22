using Kouch.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceEditPage : ContentPage
    {
        public ServiceEditPage()
        {
            InitializeComponent();
            BindingContext = new ServiceEditViewModel(Navigation);
        }
    }
}