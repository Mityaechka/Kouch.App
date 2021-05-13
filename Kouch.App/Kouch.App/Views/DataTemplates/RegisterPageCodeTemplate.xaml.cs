using Kouch.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.DataTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPageCodeTemplate : ContentView
    {
        public RegisterPageCodeTemplate()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            //RegisterStateViewModel registerStateViewModel = (RegisterStateViewModel)BindingContext;
            //registerStateViewModel.Model?.CodeValidationCollection?.UpdateAll();
            base.OnBindingContextChanged();
        }
    }
}