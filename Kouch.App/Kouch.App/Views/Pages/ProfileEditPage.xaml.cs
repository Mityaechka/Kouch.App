using Kouch.App.Entities;
using Kouch.App.ViewModels;
using Kouch.App.Views.Modals;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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