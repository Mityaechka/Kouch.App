using Kouch.App.Constants;
using Kouch.App.Entities;
using Kouch.App.Services;
using Kouch.App.Validations;
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

        private ValidatableObject<string> email;
        private ValidatableObject<string> password;
        private ValidatableObject<string> repeatPassword;



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
        public ValidatableObject<string> RepeatPassword
        {
            get => repeatPassword;
            set
            {
                repeatPassword = value;
                OnPropertyChanged();
            }
        }
        
        //public string code
        //{
        //    get => RegisterCodeModel.Code;
        //    set
        //    {
        //        if (RegisterCodeModel.Code != value)
        //        {
        //            RegisterCodeModel.Code = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        public ICommand SendSmsCodeCommand { get; set; }
        public ICommand ReturnEmailInputCommand { get; set; }
        public ICommand VerifyAccountCommand { get; set; }


        private RegisterState currentState;
        public ObservableCollection<RegisterState> States { get; set; }

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
            Email = new ValidatableObject<string>(this)
            {
                new IsNotNullOrEmptyRule<string>("Заполните почту")
            };
            Password = new ValidatableObject<string>(this)
            {
                new IsNotNullOrEmptyRule<string>("Заполните пароль")
            };
            RepeatPassword = new ValidatableObject<string>(this)
            {
                new IsNotNullOrEmptyRule<string>("Повторите пароль")
            };

            validatableObjects.Add(Email);
            validatableObjects.Add(Password);
            validatableObjects.Add(RepeatPassword);
            
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
            //await Navigation.PushPopupAsync(new LoadingModal());
            //var registerResponse = await apiAuthServicev.Register(RegisterEmailModel);
            //await Navigation.PopPopupAsync();

            //if (registerResponse.IsSuccsess)
            //{
            //    Password.Value = "";
            //    RepeatPassword.Value = "";
            //    RegisterCodeModel.Email = RegisterEmailModel.Email;
            //    CurrentState = States[1];
            //}
            //else
            //{
            //    CrossToastPopUp.Current.ShowToastMessage(registerResponse.Error);
            //}
        }
        private async Task VerifyAccount()
        {
            //await Navigation.PushPopupAsync(new LoadingModal());
            //var verifyResponse = await apiAuthServicev.VerifyAccount(RegisterCodeModel);
            //await Navigation.PopPopupAsync();

            //if (verifyResponse.IsSuccsess)
            //{
            //    CrossToastPopUp.Current.ShowToastMessage("Все внатуре четко");
            //}
            //else
            //{
            //    CrossToastPopUp.Current.ShowToastMessage(verifyResponse.Error);
            //}
        }
        private async Task ReturnEmailInput()
        {
            CurrentState = States[0];
        }

        public class RegisterState
        {
            public string State { get; set; }
            public RegisterViewModel Model { get; set; }
        }
    }
}
