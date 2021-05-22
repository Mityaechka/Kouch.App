using Kouch.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategorySelectorComponent : ContentView
    {
        private readonly CategorySelectorViewModel CategorySelectorViewModel;
        public static BindableProperty SelectedCategoryProperty = BindableProperty.Create(nameof(SelectedCategory),
            typeof(CategoryViewModel),
            typeof(CategorySelectorComponent),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: SelectedCategoryPropertyChanged,
            defaultValue: null);

        private static void SelectedCategoryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
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
            get => (CategoryViewModel)GetValue(SelectedCategoryProperty);
            set => SetValue(SelectedCategoryProperty, value);
        }
        public CategorySelectorComponent()
        {
            InitializeComponent();
            BindingContext = CategorySelectorViewModel = new CategorySelectorViewModel(null, Navigation);
            CategorySelectorViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler((s, e) =>
            {
                if (e.PropertyName == "SelectedCategory")
                {
                    SelectedCategory = CategorySelectorViewModel.SelectedCategory;
                }
            });
        }

    }
}