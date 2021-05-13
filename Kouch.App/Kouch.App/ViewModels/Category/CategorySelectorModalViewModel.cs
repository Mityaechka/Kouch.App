using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class CategorySelectorModalViewModel : BaseViewModel
    {
        public ICommand SelectCategoryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    onClose?.Invoke(category);
                    await Navigation.PopPopupAsync();
                });
            }
        }
        private CategoryViewModel category;
        private readonly Action<CategoryViewModel> onClose;

        public CategoryViewModel Category
        {
            get => category; set
            {
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged();
                }
            }
        }

        public CategorySelectorModalViewModel(INavigation navigation,Action<CategoryViewModel> onClose,CategoryViewModel category=null) : base(navigation)
        {
            this.onClose = onClose;
            Category = category;
        }
    }
}
