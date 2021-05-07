using Kouch.App.Constants;
using Kouch.App.Entities;
using Kouch.App.Services;
using Kouch.App.Views.Modals;
using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class RegisterViewModel : AsyncBaseViewModel
    {
        private ApiAuthService apiAuthServicev = new ApiAuthService();
        private RegisterEmailModel RegisterEmailModel { get; set; } = new RegisterEmailModel();
        private RegisterCodeModel RegisterCodeModel { get; set; } = new RegisterCodeModel();

        private string emailError;

        public string EmailError
        {
            get { return emailError; }
            set {
                if (emailError != value) {
                    emailError = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool isImailError;

        public bool IsEmailError
        {
            get { return isImailError; }
            set
            {
                if (isImailError != value)
                {
                    isImailError = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => RegisterEmailModel.Email;
            set
            {
                if (RegisterEmailModel.Email != value)
                {
                    RegisterEmailModel.Email = value;
                    OnPropertyChanged();
                    ValidateEmailModel();
                }
            }
        }
        public string Password
        {
            get => RegisterEmailModel.Password;
            set
            {
                if (RegisterEmailModel.Password != value)
                {
                    RegisterEmailModel.Password = value;
                    OnPropertyChanged();
                    ValidateEmailModel();
                }
            }
        }
        public string RepeatPassword
        {
            get => RegisterEmailModel.RepeatPassword;
            set
            {
                if (RegisterEmailModel.RepeatPassword != value)
                {
                    RegisterEmailModel.RepeatPassword = value;
                    OnPropertyChanged();
                    ValidateEmailModel();
                }
            }
        }
        public string code
        {
            get => RegisterCodeModel.Code;
            set
            {
                if (RegisterCodeModel.Code != value)
                {
                    RegisterCodeModel.Code = value;
                    OnPropertyChanged();
                    ValidateEmailModel();
                }
            }
        }
        public ICommand SendSmsCodeCommand{ get; set; }
        public ICommand ReturnEmailInputCommand{ get; set; }
        public ICommand VerifyAccountCommand{ get; set; }

        private RegisterState currentState;
        public ObservableCollection<RegisterState> States{ get; set; }

        public RegisterState CurrentState
        {
            get => currentState; set
            {
                if (currentState == null)
                {
                    currentState = value;
                    OnPropertyChanged("CurrentState");
                }
                else if (currentState.State != value.State)
                {
                    currentState = value;
                    OnPropertyChanged("CurrentState");
                }
            }
        }
        public RegisterViewModel(INavigation navigation) : base(navigation)
        {
           
            States = new ObservableCollection<RegisterState>
            {
                new RegisterState
                {
                    State = RegistrationPageConstants.EMAIL,
                    Model = this
                },
                new RegisterState
                {
                    State = RegistrationPageConstants.CODE,
                    Model = this
                }
            };
            CurrentState = States[0];
            SendSmsCodeCommand = new Command(async () => await SendSmsCode());
            ReturnEmailInputCommand = new Command(async () => await ReturnEmailInput());
            VerifyAccountCommand = new Command(async () => await VerifyAccount());
        }
        private async Task SendSmsCode()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var registerResponse = await apiAuthServicev.Register(RegisterEmailModel);
            await Navigation.PopPopupAsync();

            if (registerResponse.IsSuccsess)
            {
                Password = "";
                RepeatPassword = "";
                RegisterCodeModel.Email = RegisterEmailModel.Email;
                CurrentState = States[1];
            }
            else
            {
                CrossToastPopUp.Current.ShowToastMessage(registerResponse.Error);
            }
        }
        private async Task VerifyAccount()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var verifyResponse = await apiAuthServicev.VerifyAccount(RegisterCodeModel);
            await Navigation.PopPopupAsync();

            if (verifyResponse.IsSuccsess)
            {
                CrossToastPopUp.Current.ShowToastMessage("Все внатуре четко");
            }
            else
            {
                CrossToastPopUp.Current.ShowToastMessage(verifyResponse.Error);
            }
        }
        private async Task ReturnEmailInput()
        {
            CurrentState = States[0];
        }
        private void ValidateEmailModel()
        {
            if (string.IsNullOrEmpty(RegisterEmailModel.Email))
            {
                IsEmailError = true;
                EmailError = "Введите номер телефона";
            }else if (string.IsNullOrEmpty(RegisterEmailModel.Password))
            {
                IsEmailError = true;
                EmailError = "Введите пароль";
            }
            if (string.IsNullOrEmpty(RegisterEmailModel.RepeatPassword))
            {
                IsEmailError = true;
                EmailError = "Повторите пароль";
            }
            else if (RegisterEmailModel.RepeatPassword != RegisterEmailModel.Password)
            {
                IsEmailError = true;
                EmailError = "Введенные пароли не совпадают";
            }
            else
            {
                IsEmailError = false;
                EmailError = "";
            }
        }
        public class RegisterState
        {
            public string State { get; set; }
            public RegisterViewModel  Model { get; set; }
        }
    }
}
