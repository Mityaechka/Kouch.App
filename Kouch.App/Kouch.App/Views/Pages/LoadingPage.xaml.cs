using Kouch.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        LoadingViewModel loadingViewModel;
        public LoadingPage()
        {
            InitializeComponent();
            loadingViewModel = new LoadingViewModel(Navigation)
            {
                IsLoading = true
            };
            BindingContext = loadingViewModel;
            //Thread.Sleep(5000);
            //loadingViewModel.IsLoading = false;
        }
    }
}