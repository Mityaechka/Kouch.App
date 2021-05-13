using Kouch.App.Entities;
using Kouch.App.Models;
using Kouch.App.Services;
using Kouch.App.Validations;
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
    public class AuthViewModel : AsyncBaseViewModel
    {
        private ValidatableObject<string> email;
        private ValidatableObject<string> password;

        private ValidationCollection authCollection;

        public ICommand AuthCommand { get; set; }
        public ICommand OpenRegisterPageCommand { get; set; }
        public ValidatableObject<string> Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        public ValidationCollection AuthCollection
        {
            get => authCollection;
            set
            {
                authCollection = value;
                OnPropertyChanged();
            }
        }
        public AuthViewModel(INavigation navigation) : base(navigation)
        {
            Email = new ValidatableObject<string>(this)
            {
                new IsEmailRule("Введите Вашу почту")
            };
            Password = new ValidatableObject<string>(this)
            {
                new IsNotNullOrEmptyRule("Введите пароль")
            };
            AuthCollection = new ValidationCollection(nameof(AuthCollection),this)
            {
                Email,Password
            };

            AuthCommand = new Command(async () => await Auth(),()=>AuthCollection.IsValid);
            OpenRegisterPageCommand = new Command(() => OpenRegisterPage());

            AuthCollection.UpdateAll();
        }
        private async Task Auth()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var authResponse = await ApiAuthService.Instance.Login(new LoginRequestModel
            {
                Email = Email.Value,
                Password = Password.Value
            });
            
            if (authResponse.IsSuccsess)
            {
                TokenStorageService.Instance.SaveToken(authResponse.Result.Tokens);
                TokenStorageService.Instance.SaveAuthData(new LoginRequestModel
                {
                    Email = Email.Value,
                    Password = Password.Value
                });
                var userResponse = await ApiUserService.Instance.GetMe();
                await Navigation.PopPopupAsync();
                if (userResponse.IsSuccsess)
                {
                    UserStorageService.Instance.User = userResponse.Result;
                }
                App.Current.MainPage = new MainPage();
            }
            else
            {
                await Navigation.PopPopupAsync();
                ToastsService.Instance.ShowToast(authResponse.Error);
            }
        }
        private void OpenRegisterPage()
        {
            App.Current.MainPage = new RegisterPage();
        }
    }
}
