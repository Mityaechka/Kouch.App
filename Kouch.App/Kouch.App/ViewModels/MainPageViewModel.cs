using Kouch.App.Models;
using Kouch.App.Views.Modals;
using Kouch.App.Views.Pages;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class MainPageViewModel : AsyncBaseViewModel
    {
        private UserViewModel user;

        public UserViewModel User
        {
            get => user; set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LogoutCommand => new Command(async () => await Logout());
        public ICommand OpenMyProfileCommand => new Command(async () => await OpenMyProfile());
        public ICommand OpenMyContactsCommand => new Command(async () => await OpenMyContacts());
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            User = new UserViewModel(UserStorageService.Instance.User);

            UserStorageService.Instance.OnUserChahged += (user) =>
            {
                User.Id = user.Id;
                User.Email = user.Email;

                User.FirstName = user.FirstName;
                User.LastName = user.LastName;
                User.Avatar = user.Avatar;

            };
        }
        private async Task Logout()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            Entities.TokenModel token = await TokenStorageService.Instance.GetToken();
            TokenStorageService.Instance.ClearAuthData();
            TokenStorageService.Instance.ClearToken();

            //if (token == null)
            //{
            await Navigation.PopPopupAsync();
            App.Current.MainPage = new AuthPage();
            //}
            //else
            //{
            //    var logoutResponse = await ApiAuthService.Instance.Logout(token.Refresh);
            //    await Navigation.PopPopupAsync();
            //    if (logoutResponse.IsSuccsess)
            //    {
            //            App.Current.MainPage = new AuthPage();
            //    }
            //    else
            //    {
            //        ToastsService.Instance.ShowToast(logoutResponse.Error);
            //    }
            //}
        }
        private async Task OpenMyProfile()
        {
            if (typeof(MainPage) == App.Current.MainPage.GetType())
            {
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new ProfileViewPage());
                await Task.Delay(500);
                (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            }

        }
        private async Task OpenMyContacts()
        {
            if (typeof(MainPage) == App.Current.MainPage.GetType())
            {
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new ContactsViewPage());
                await Task.Delay(500);
                (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            }

        }
    }
}
