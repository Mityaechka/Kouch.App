using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingModal : PopupPage
    {
        public LoadingModal()
        {
            InitializeComponent();
        }
    }
}