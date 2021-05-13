using Kouch.App.Constants;
using Kouch.App.Entities;
using Kouch.App.Models;
using Kouch.App.Services;
using Kouch.App.Validations;
using Kouch.App.Views.Modals;
using Kouch.App.Views.Pages;
using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public partial class RegisterViewModel : AsyncBaseViewModel
    {
        private ApiAuthService apiAuthServicev = ApiAuthService.Instance;
        private Timer timer;
        private int sendCodeSeconds;

        public int SendCodeSeconds
        {
            get => sendCodeSeconds;
            set
            {
                if (sendCodeSeconds != value)
                {
                    sendCodeSeconds = value;
                    OnPropertyChanged();
                }
            }
        }

        private ValidatableObject<string> email;
        private ValidatableObject<string> password;
        private ValidatableObject<string> repeatPassword;

        private ValidatableObject<string> code;

        private ValidationCollection emailValidationCollection;
        private ValidationCollection codeValidationCollection;
        public ValidationCollection EmailValidationCollection
        {
            get => emailValidationCollection;
            set
            {
                emailValidationCollection = value;
                OnPropertyChanged();
            }
        }
        public ValidationCollection CodeValidationCollection
        {
            get => codeValidationCollection;
            set
            {
                codeValidationCollection = value;
                OnPropertyChanged();
            }
        }
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
        public ValidatableObject<string> Code
        {
            get => code;
            set
            {
                code = value;
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
        public ICommand ResendVerificationCodeCommand { get; set; }


        private RegisterStateViewModel currentState;
        public ObservableCollection<RegisterStateViewModel> States { get; set; }

        public RegisterStateViewModel CurrentState
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
                new IsNotNullOrEmptyRule("Заполните почту"),
                new IsEmailRule("Введите Вашу почту")
            };
            Password = new ValidatableObject<string>(this)
            {
                new IsNotNullOrEmptyRule("Заполните пароль")
            };
            RepeatPassword = new ValidatableObject<string>(this)
            {
                new IsNotNullOrEmptyRule("Повторите пароль"),
                new ExpressionRule(()=>Password?.Value!=RepeatPassword.Value,"Пароли должны совпадать")
            };
            Code = new ValidatableObject<string>(this)
            {
                new IsNotNullOrEmptyRule("Введите код"),
                new LengthRule(4,"Длинна пароля должна быть 4")
            };
            EmailValidationCollection = new ValidationCollection(nameof(EmailValidationCollection), this)
            {
                Email,Password,RepeatPassword
            };
            CodeValidationCollection = new ValidationCollection(nameof(CodeValidationCollection), this)
            {
                Code
            };
            States = new ObservableCollection<RegisterStateViewModel>
            {
                new RegisterStateViewModel
                {
                    State = RegistrationPageConstants.EMAIL,
                    Model = this
                },
                new RegisterStateViewModel
                {
                    State = RegistrationPageConstants.CODE,
                    Model = this
                }
            };
            CurrentState = States[0];
            SendSmsCodeCommand = new Command(async () => await SendSmsCode(), () => EmailValidationCollection.IsValid);
            ReturnEmailInputCommand = new Command(async () => await ReturnEmailInput());
            VerifyAccountCommand = new Command(async () => await VerifyAccount(),()=>CodeValidationCollection.IsValid);
            ResendVerificationCodeCommand = new Command(async () => await ResendSmsCode());

            EmailValidationCollection.UpdateAll();
            CodeValidationCollection.UpdateAll();
        }
        private async Task SendSmsCode()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var registerResponse = await apiAuthServicev.Register(new RegisterEmailModel
            {
                Email = Email.Value,
                Password = Password.Value,
                RepeatPassword = Password.Value
            });
            await Navigation.PopPopupAsync();

            if (registerResponse.IsSuccsess)
            {
                RestartCodeTimer();

                CurrentState = States[1];
                Code.Value = "";
                CodeValidationCollection.UpdateAll();
            }
            else
            {
                ToastsService.Instance.ShowToast(registerResponse.Error);
            }
        }
        private async Task ResendSmsCode()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var registerResponse = await apiAuthServicev.ResendVerificationCode(Email.Value);
            await Navigation.PopPopupAsync();

            if (registerResponse.IsSuccsess)
            {
                RestartCodeTimer();
                CrossToastPopUp.Current.ShowToastMessage("Новый код успешно отправлен на почту");
            }
            else
            {
                CrossToastPopUp.Current.ShowToastMessage(registerResponse.Error);
            }
        }
        private async Task VerifyAccount()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var verifyResponse = await apiAuthServicev.VerifyAccount(new RegisterCodeModel
            {
                Code = code.Value,
                Email = email.Value
            });
            if (verifyResponse.IsSuccsess)
            {
                var loginResponse = await apiAuthServicev.Login(new LoginRequestModel
                {
                    Email = Email.Value,
                    Password = Password.Value
                });
                
                if (loginResponse.IsSuccsess)
                {
                    TokenStorageService.Instance.SaveToken(loginResponse.Result.Tokens);
                    TokenStorageService.Instance.SaveAuthData(new LoginRequestModel
                    {
                        Email = Email.Value,
                        Password = Password.Value
                    });
                    var userResponse = await ApiUserService.Instance.GetMe();
                    if (userResponse.IsSuccsess)
                    {
                        UserStorageService.Instance.User = userResponse.Result;
                    }
                    await Navigation.PopPopupAsync();
                    App.Current.MainPage =new MainPage();
                }
                else
                {
                    await Navigation.PopPopupAsync();
                    CrossToastPopUp.Current.ShowToastMessage(loginResponse.Error);
                }
            }
            else
            {
                await Navigation.PopPopupAsync();
                CrossToastPopUp.Current.ShowToastMessage(verifyResponse.Error);
            }
        }
        private async Task ReturnEmailInput()
        {
            SendCodeSeconds = 0;
            timer?.Stop();
            Code.Value = "";
            Password.Value = "";
            RepeatPassword.Value = "";

            CurrentState = States[0];
        }
        private void RestartCodeTimer()
        {
            timer?.Stop();
            SendCodeSeconds = 5;
            timer = new Timer
            {
                Interval = 1000
            };
            timer.Elapsed += (sender, e) =>
            {
                SendCodeSeconds--;
                if (SendCodeSeconds <= 0)
                {
                    timer.Stop();
                }
            };


            timer.Enabled = true;
        }
    }
}
