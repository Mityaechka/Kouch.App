using Kouch.App.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            await Task.Delay(3000);
            IsLoading = false;
            OpenRegisterPage();

        }
        public void OpenRegisterPage()
        {
            App.Current.MainPage = new RegisterPage();
           //await App.Page.PushAsync(new RegisterPage());
            //await Navigation.PushAsync(new RegisterPage());
        }
    }
}
