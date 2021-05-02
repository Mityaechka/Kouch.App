using Kouch.App.Services;
using Kouch.App.Views.Pages;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class LoadingViewModel : AsyncBaseViewModel
    {
       
        public LoadingViewModel(INavigation navigation) : base(navigation)
        {
            Init();
        }
        public async void Init()
        {
            while (!await CheckConnection())
            {
                 CrossToastPopUp.Current.ShowToastMessage("Ошибка сервера. Повторная попытка через 5 секунд", ToastLength.Long);
                await Task.Delay(5000);
            }
            OpenRegisterPage();
        }
        public async Task<bool> CheckConnection()
        {
            var result = await new HttpBaseService().Get("");
            return result.IsSuccsess;
        }
        public void OpenRegisterPage()
        {
            Application.Current.MainPage = new RegisterPage();
        }
    }
}
