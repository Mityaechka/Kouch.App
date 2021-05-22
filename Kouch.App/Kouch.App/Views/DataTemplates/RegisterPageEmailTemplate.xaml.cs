
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.DataTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPageEmailTemplate : ContentView
    {
        public RegisterPageEmailTemplate()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            //RegisterStateViewModel registerStateViewModel = (RegisterStateViewModel)BindingContext;
            //registerStateViewModel.Model?.EmailValidationCollection?.UpdateAll();
            base.OnBindingContextChanged();
        }
    }
}