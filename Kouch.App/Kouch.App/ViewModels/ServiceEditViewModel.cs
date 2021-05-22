using Kouch.App.Constants;
using Kouch.App.Entities;
using Kouch.App.Views.Modals;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class ServiceEditViewModel : BaseViewModel
    {

        private readonly Service service;
        private CategoryViewModel selectedCategory;



        #region Properties
        public int CategoryId
        {
            get => service.CategoryId;
            set
            {
                if (service.CategoryId != value)
                {
                    service.CategoryId = value;
                    OnPropertyChanged();
                    OnPropertyChanged("SelectedCategory");
                }
            }
        }
        public bool IsOnline
        {
            get => service.IsOnline;
            set
            {
                if (service.IsOnline != value)
                {
                    service.IsOnline = value;
                    OnPropertyChanged();
                }
            }
        }
        public ServiceTypeDisplay ServiceType
        {
            get => ServiceTypeDictionary.Data.FirstOrDefault(x => x.ServiceType == service.ServiceType);
            set
            {
                if (service.ServiceType != value.ServiceType)
                {
                    service.ServiceType = value.ServiceType;
                    OnPropertyChanged();
                }
            }
        }
        public CategoryViewModel SelectedCategory
        {
            get => selectedCategory; set
            {
                if (selectedCategory == null || selectedCategory.Id != value.Id)
                {
                    selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands
        public ICommand SelectCategoryCommand => new Command(async () =>
                                                               {
                                                                   await Navigation.PushPopupAsync(new CategorySelectorModal(category =>
                                                                   {
                                                                       SelectedCategory = category;
                                                                   }, SelectedCategory));
                                                               });
        public ICommand AddDescriptionCommand => new Command(() =>
                                                               {
                                                                   ServiceDescriptions.Add(new ServiceDescription());
                                                               });
        #endregion
        public ObservableCollection<Language> Languages
        {
            get;
            set;
        }
        public List<Language> AvailableLanguages => Languages.Where(x => !ServiceDescriptions.Any(u => u.Language?.Id == x.Id)).ToList();
        public ObservableCollection<ServiceDescription> ServiceDescriptions { get; set; }

        public ServiceEditViewModel(INavigation navigation) : base(navigation)
        {
            Languages = new ObservableCollection<Language>();
            ServiceDescriptions = new ObservableCollection<ServiceDescription>();
            service = new Service();


            Languages.CollectionChanged += (e, s) =>
            {
                OnPropertyChanged(nameof(AvailableLanguages));
            };
            ServiceDescriptions.CollectionChanged += (e, s) =>
            {
                OnPropertyChanged(nameof(AvailableLanguages));
            };
            Init();
        }
        private async void Init()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            await LoadLanguages();
            await Navigation.PopPopupAsync();
        }
        private async Task LoadLanguages()
        {
            await Task.Delay(500);
            Languages.Add(new Language
            {
                Id = 1,
                Name = "Ru"
            });
            Languages.Add(new Language
            {
                Id = 2,
                Name = "En"
            });
            Languages.Add(new Language
            {
                Id = 2,
                Name = "Kz"
            });

        }
    }
}
