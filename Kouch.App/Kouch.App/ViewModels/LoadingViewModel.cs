using Kouch.App.Entities;
using Kouch.App.Models;
using Kouch.App.Services;
using Kouch.App.Views.Pages;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class LoadingViewModel : AsyncBaseViewModel
    {
        private bool firstConnect = true;
        private bool firstCheck = true;
        private bool isInternetAvailable = false;
        public LoadingViewModel(INavigation navigation) : base(navigation)
        {
            Connectivity.ConnectivityChanged += OnConnectionChanged;
            Init();
        }
        public async void Init()
        {
            NetworkAccess current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                if (firstConnect)
                {
                    //ToastsService.Instance.ShowToast("Отсуствует подключение к интернету");
                    firstConnect = false;
                }
                return;
            }
            isInternetAvailable = true;

            while (!await CheckServerAvailable())
            {
                if (firstCheck)
                {
                    //ToastsService.Instance.ShowToast("Не удалось подключиться к серверу. Повторная попытка через 10 секунд",ToastLength.Long);
                    firstCheck = false;
                }
                await Task.Delay(1000);
            }
            TokenStorageService.Instance.ClearToken();
            TokenModel token = await TokenStorageService.Instance.GetToken();
            if (token == null)
            {
                App.Current.MainPage = new AuthPage();
            }
            else
            {
                ApiResnonse<User> userResponse = await ApiUserService.Instance.GetMe();
                if (userResponse.IsSuccsess)
                {
                    UserStorageService.Instance.User = userResponse.Result;
                }
                App.Current.MainPage = new MainPage();
            }
        }
        private void OnConnectionChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (isInternetAvailable)
            {
                return;
            }
            Init();
        }
        private async Task<bool> CheckServerAvailable()
        {
            ApiResnonse resonse = await ApiAuthService.Instance.CheckConnection();
            return resonse.IsSuccsess;
        }
    }
}
