using Kouch.App.Entities;
using Kouch.App.Models;
using Kouch.App.Services;
using Kouch.App.Views.Modals;
using Kouch.App.Views.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class MainPageViewModel : AsyncBaseViewModel
    {
        private CategoryViewModel category;
        public ICommand LogoutCommand { get; set; }
        public ICommand SelectCategoryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PushPopupAsync(new CategorySelectorModal(c=> {
                        category = c;
                    },category));
                });
            }
        }
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            LogoutCommand = new Command(async () => await Logout());
        }
        private async Task Logout()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var token = await TokenStorageService.Instance.GetToken();
            TokenStorageService.Instance.ClearAuthData();
            TokenStorageService.Instance.ClearToken();

            if (token == null)
            {
                await Navigation.PopPopupAsync();
                App.Current.MainPage = new LoadingPage();
            }
            else
            {
                var logoutResponse = await ApiAuthService.Instance.Logout(token.Refresh);
                await Navigation.PopPopupAsync();
                if (logoutResponse.IsSuccsess)
                {
                    App.Current.MainPage = new LoadingPage();
                }
                else
                {
                    ToastsService.Instance.ShowToast(logoutResponse.Error);
                }
            }
        }
    }
}
