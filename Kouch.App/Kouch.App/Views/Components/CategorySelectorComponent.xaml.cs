using Kouch.App.Entities;
using Kouch.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategorySelectorComponent : ContentView
    {
        private CategorySelectorViewModel CategorySelectorViewModel;
        public static BindableProperty SelectedCategoryProperty = BindableProperty.Create(nameof(SelectedCategory),
            typeof(CategoryViewModel),
            typeof(CategorySelectorComponent),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: SelectedCategoryPropertyChanged,
            defaultValue: null);

        static void SelectedCategoryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            CategorySelectorComponent instance = bindable as CategorySelectorComponent;
            if (instance != null)
            {
                instance.CategorySelectorViewModel.SelectedCategory = newValue as CategoryViewModel;
                instance.SelectedCategory = newValue as CategoryViewModel;
            }
        }
        public CategoryViewModel SelectedCategory
        {
            get { return (CategoryViewModel)GetValue(SelectedCategoryProperty); }
            set { SetValue(SelectedCategoryProperty, value); }
        }
        public CategorySelectorComponent()
        {
            InitializeComponent();
            BindingContext = CategorySelectorViewModel = new CategorySelectorViewModel(null, Navigation);
            CategorySelectorViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler((s,e) =>
            {
                if (e.PropertyName == "SelectedCategory")
                {
                    SelectedCategory = CategorySelectorViewModel.SelectedCategory;
                }
            });
        }

    }
}