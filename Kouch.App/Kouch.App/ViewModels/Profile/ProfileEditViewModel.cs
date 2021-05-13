using Kouch.App.Entities;
using Kouch.App.Extensions;
using Kouch.App.Models;
using Kouch.App.Services;
using Kouch.App.Validations;
using Kouch.App.Views.Modals;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class ProfileEditViewModel : AsyncBaseViewModel
    {
        private bool firstCitySelect = true;
        public ICommand EditProfileCommand => new Command(async () => await EditProfile());
        //private UserViewModel user;

        private bool isCountriesLoading;
        private bool isCitiesLoading;

        private ValidatableObject<Country> selectedCountry;
        private ValidatableObject<City> selectedCity;
        private ValidatableObject<string> firstName;
        private ValidatableObject<string> secondName;

        private ValidatableObject<string> vk;
        private ValidatableObject<string> facebook;
        private ValidatableObject<string> instagram;



        private ValidationCollection validities;


        private readonly User user;

        public ValidationCollection Validities
        {
            get { return validities; }
            set
            {
                validities = value;
                OnPropertyChanged();
            }
        }

        //public UserViewModel User
        //{
        //    get => user; set
        //    {
        //        user = value;
        //        OnPropertyChanged();
        //    }
        //}
        public ValidatableObject<Country> SelectedCountry
        {
            get => selectedCountry; set
            {
                selectedCountry = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<City> SelectedCity
        {
            get => selectedCity; set
            {
                selectedCity = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> FirstName
        {
            get => firstName; set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> SecondName
        {
            get => secondName; set
            {
                secondName = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> Vk
        {
            get => vk; set
            {
                vk = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> Facebook
        {
            get => facebook; set
            {
                facebook = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> Instagram
        {
            get => instagram; set
            {
                instagram = value;
                OnPropertyChanged();
            }
        }
        public bool IsCountriesLoading
        {
            get => isCountriesLoading; set
            {
                isCountriesLoading = value;
                OnPropertyChanged();
            }
        }
        public bool IsCitiesLoading
        {
            get => isCitiesLoading; set
            {
                isCitiesLoading = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Country> Countries { get; set; } = new ObservableCollection<Country>();
        public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();
        public ProfileEditViewModel(User user, INavigation navigation) : base(navigation)
        {
            //User = new UserViewModel(UserStorageService.Instance.User);
            SelectedCountry = new ValidatableObject<Country>(this)
            {
                new ExpressionRule(()=>{
                return SelectedCountry.Value==null;
                },"Заполните страну проживания")
            };
            SelectedCity = new ValidatableObject<City>(this)
            {
                new ExpressionRule(()=>SelectedCity.Value==null,"Заполните город проживания")
            };
            FirstName = new ValidatableObject<string>(this)
            {
               new IsNotNullOrEmptyRule("Заполните имя")
            };
            SecondName = new ValidatableObject<string>(this)
            {
               new IsNotNullOrEmptyRule("Заполните фамилию")
            };

            Vk = new ValidatableObject<string>(this) {
            new IsNullOrRule(new IsUrl("Укажите ссылку на Вашу страницу"))
            };
            Facebook = new ValidatableObject<string>(this);
            Instagram = new ValidatableObject<string>(this);

            Validities = new ValidationCollection(nameof(Validities), this)
            {
                SelectedCountry,SelectedCity,FirstName,SecondName,Vk,Facebook,Instagram
            };



            SelectedCountry.onChangeValue = () =>
            {
                SelectedCity.Value = null;
                LoadCities();
            };

            FirstName.Value = user.FirstName;
            SecondName.Value = user.LastName;
            Vk.Value = user.Vk;
            Instagram.Value = user.Instagram;
            Facebook.Value = user.Facebook;
            Validities.UpdateAll();
            Init();
            this.user = user;
        }
        private async void Init()
        {
            //await Navigation.PushPopupAsync(new LoadingModal());
            IsCountriesLoading = true;
            ApiResnonse<PaginationModel<Country>> countriesResponse = await ApLocationService.Instance.GetCountries();
            IsCountriesLoading = false;
            //await Navigation.PopPopupAsync();
            if (countriesResponse.IsSuccsess)
            {
                Countries.Clear();
                foreach (var item in countriesResponse.Result.Data)
                {
                    Countries.Add(item);
                }
                //if (user?.Country?.Id != null)
                //{
                //    SelectedCountry.Value = Countries.FirstOrDefault(x => x.Id == user.Country.Id);
                //}
            }
            else
            {
                ToastsService.Instance.ShowToast("Ошибка при заргузке списка стран");
                await Navigation.PopAsync();
            }
        }
        private async void LoadCities()
        {
            IsCitiesLoading = true;
            ApiResnonse<List<City>> countriesResponse = await ApLocationService.Instance.GetCities(selectedCountry.Value.Id);
            IsCitiesLoading = false;
            if (countriesResponse.IsSuccsess)
            {
                Cities.Clear();
                Cities.AddRang(countriesResponse.Result);
                //if (firstCitySelect && user.City != null)
                //{
                //    SelectedCity.Value = countriesResponse.Result.FirstOrDefault(x => x.Id == user.City.Id);
                //}
                firstCitySelect = false;
            }
            else
            {
                ToastsService.Instance.ShowToast("Ошибка при заргузке списка городов");
            }
        }
        private async Task EditProfile()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var editResponse = await ApiUserService.Instance.EditProfile(new UserEditModel
            {
                FirstName = FirstName.Value,
                LastName = SecondName.Value,
                CityId = SelectedCity.Value?.Id,
                CountryId = SelectedCountry.Value?.Id,
                Vk = Vk.Value,
                Instagram = Instagram.Value,
                Facebook = Facebook.Value
            });

            if (editResponse.IsSuccsess)
            {
                await ApiUserService.Instance.UpdateMe();
                await Navigation.PopPopupAsync();
                await Navigation.PopAsync();
            }
            else
            {
                await Navigation.PopPopupAsync();
                ToastsService.Instance.ShowToast(editResponse.Error);
            }
        }
    }
}
