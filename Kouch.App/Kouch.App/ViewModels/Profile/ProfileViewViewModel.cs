using Kouch.App.Entities;
using Kouch.App.ImageCropper;
using Kouch.App.Models;
using Kouch.App.Services;
using Kouch.App.Views.Modals;
using Kouch.App.Views.Pages;
using Rg.Plugins.Popup.Extensions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class ProfileViewViewModel : AsyncBaseViewModel
    {
        public ICommand RefreshCommand => new Command(async () => await Refresh());
        public ICommand EditProfileCommand => new Command(async () => await EditProfile());
        public ICommand ChangeAvatarImageCommand => new Command(async () => await ChangeAvatarImage());
        private UserViewModel user;

        public UserViewModel User
        {
            get => user; set
            {
                if (user == null)
                {
                    user = value;
                    OnPropertyChanged();
                }
                if (user.Id != value.Id)
                {
                    user = value;
                    OnPropertyChanged();
                }
            }
        }
        public ProfileViewViewModel(INavigation navigation) : base(navigation)
        {
            User = new UserViewModel(UserStorageService.Instance.User);
            UserStorageService.Instance.OnUserChahged += (user) =>
            {

                User.Id = user.Id;
                User.Email = user.Email;

                User.FirstName = user.FirstName;
                User.LastName = user.LastName;
                User.Avatar = user.Avatar;
                User.Country = user.Country?.Name;
                User.City = user.City?.Name;

                User.Vk = user.Vk;
                User.Instagram = user.Instagram;
                User.Facebook = user.Facebook;
            };
        }
        public async Task Refresh()
        {
            IsLoading = true;
            var userResponse = await ApiUserService.Instance.GetMe();
            IsLoading = false;
            if (userResponse.IsSuccsess)
            {
                UserStorageService.Instance.User = userResponse.Result;
            }
            else
            {
                ToastsService.Instance.ShowToast(userResponse.Error);
            }
        }
        public async Task ChangeAvatarImage()
        {
            try
            {
                FilePickerFileType customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "*/*" } }
                });
                PickOptions options = new PickOptions
                {
                    FileTypes = customFileType,
                };

                FileResult result = await FilePicker.PickAsync(options);
                if (result != null)
                {

                    Stream stream = await result.OpenReadAsync();
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);
                    var array = ms.ToArray();
                    await Navigation.PushPopupAsync(new ImageCropperModal(array, async (s) => await OnAvaterSelect(s)));

                }

            }
            catch (Exception ex)
            {
            }
        }
        private async Task OnAvaterSelect(byte[] array)
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            var response = await ApiUserService.Instance.EditAvatar(array);
            await Navigation.PopPopupAsync();

            if (response.IsSuccsess)
            {
                await ApiUserService.Instance.UpdateMe();
            }
            else
            {
                ToastsService.Instance.ShowToast(response.Error);
            }
        }
        private async Task EditProfile()
        {
            //await Navigation.PushPopupAsync(new FullLoadingModal());
            var p = new ProfileEditPage(UserStorageService.Instance.User);
            await Navigation.PushAsync(p);
        }
    }
    public class UserViewModel : BaseViewModel
    {
        public string FullName => new[] { FirstName, LastName }.All(x => string.IsNullOrEmpty(x)) ? "отсуствует" : $"{FirstName} {LastName}";
        public string FullAddress => new[] { Country, City}.All(x => string.IsNullOrEmpty(x)) ? "отсуствует" : $"{Country} {City}";

        private int id;
        private string email;
        private string firstName;
        private string lastName;
        private string avatar;
        private string country;
        private string city;

        private string vk;
        private string facebook;
        private string instagram;

        public int Id
        {
            get => id; set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => email; set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => firstName; set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => lastName; set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Avatar
        {
            get => avatar; set
            {
                if (avatar != value)
                {
                    avatar = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Country
        {
            get => country; set
            {
                if (country != value)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }
        public string City
        {
            get => city; set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Instagram
        {
            get => instagram; set
            {
                if (instagram != value)
                {
                    instagram = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Vk
        {
            get => vk; set
            {
                if (vk != value)
                {
                    vk = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Facebook
        {
            get => facebook; set
            {
                if (facebook != value)
                {
                    facebook = value;
                    OnPropertyChanged();
                }
            }
        }
        public UserViewModel(User user) : base(null)
        {
            if (user != null)
            {
                Email = user.Email;
                Id = user.Id;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Avatar = user.Avatar;
                Country = user.Country?.Name;
                City = user.City?.Name;

                Vk = user.Vk;
                Facebook = user.Facebook;
                Instagram = user.Instagram;
            }
        }
    }
}
