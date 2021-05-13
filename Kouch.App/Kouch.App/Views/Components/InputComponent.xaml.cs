using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputComponent : ContentView
    {

        public static BindableProperty IsLoadingProperty = BindableProperty.Create(nameof(IsLoading),
                                                                                            typeof(bool),
                                                                                            typeof(CategorySelectorComponent),
                                                                                            defaultBindingMode: BindingMode.TwoWay,
                                                                                            propertyChanged: IsLoadingPropertyChanged,
                                                                                            defaultValue: null);

        static void IsLoadingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InputComponent instance = bindable as InputComponent;
            if (instance != null)
            {
                instance.IsLoading = (bool)newValue;
                if (instance.IsLoading)
                {
                    instance.LoadingIndicator.IsVisible = true;
                    instance.LoadingIndicator.IsRunning  = true;
                    instance.BodyContent.IsVisible = false;
                }
                else
                {
                    instance.LoadingIndicator.IsVisible = false;
                    instance.LoadingIndicator.IsRunning = false;
                    instance.BodyContent.IsVisible = true;
                }
            }
        }
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static BindableProperty IsErrorProperty = BindableProperty.Create(nameof(IsError),
                                                                                            typeof(bool),
                                                                                            typeof(CategorySelectorComponent),
                                                                                            defaultBindingMode: BindingMode.TwoWay,
                                                                                            propertyChanged: IsErrorPropertyChanged,
                                                                                            defaultValue: null);

        static void IsErrorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InputComponent instance = bindable as InputComponent;
            if (instance != null)
            {
                instance.IsError = (bool)newValue;
                instance.State();
            }
        }
        public bool IsError
        {
            get { return (bool)GetValue(IsErrorProperty); }
            set { SetValue(IsErrorProperty, value); }
        }

        public static BindableProperty ErrorProperty = BindableProperty.Create(nameof(Error),
                                                                                            typeof(string),
                                                                                            typeof(CategorySelectorComponent),
                                                                                            defaultBindingMode: BindingMode.TwoWay,
                                                                                            propertyChanged: ErrorPropertyChanged,
                                                                                            defaultValue: null);

        static void ErrorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InputComponent instance = bindable as InputComponent;
            if (instance != null)
            {
                instance.Error = (string)newValue;
                instance.State();
            }
        }
        public string Error
        {
            get { return (string)GetValue(ErrorProperty); }
            set { SetValue(ErrorProperty, value); }
        }


        public static BindableProperty HelpTextProperty = BindableProperty.Create(nameof(HelpText),
                                                                                    typeof(string),
                                                                                    typeof(CategorySelectorComponent),
                                                                                    defaultBindingMode: BindingMode.TwoWay,
                                                                                    propertyChanged: HelpTextChanged,
                                                                                    defaultValue: null);

        static void HelpTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InputComponent instance = bindable as InputComponent;
            if (instance != null)
            {
                instance.HelpText = (string)newValue;
                instance.State();
            }
        }
        public string HelpText
        {
            get { return (string)GetValue(HelpTextProperty); }
            set { SetValue(HelpTextProperty, value); }
        }

        public static BindableProperty TitleTextProperty = BindableProperty.Create(nameof(TitleText),
                                                                            typeof(string),
                                                                            typeof(CategorySelectorComponent),
                                                                            defaultBindingMode: BindingMode.TwoWay,
                                                                            propertyChanged: TitleTextChanged,
                                                                            defaultValue: null);

        static void TitleTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InputComponent instance = bindable as InputComponent;
            if (instance != null)
            {
                instance.HelpText = (string)newValue;
                instance.HeaderLabel.Text = (string)newValue;
                if (string.IsNullOrEmpty((string)newValue))
                {
                    instance.HeaderLabel.IsVisible = false;
                }
                else
                {
                    instance.HeaderLabel.IsVisible = true;
                }
            }
        }
        public string TitleText
        {
            get { return (string)GetValue(HelpTextProperty); }
            set { SetValue(HelpTextProperty, value); }
        }
        public View Body
        {
            get => BodyContent.Content;
            set => BodyContent.Content = value;
        }
        public InputComponent()
        {
            InitializeComponent();
        }

        private void State()
        {
            if (IsError)
            {
                ErrorLabel.Text = $"*{Error}";
                ErrorLabel.Style = (Style)App.Current.Resources["ErrorLabel"];
            }
            else
            {
                ErrorLabel.Text = HelpText;
                ErrorLabel.Style = (Style)App.Current.Resources["HelpLabel"];

            }
        }
    }
}