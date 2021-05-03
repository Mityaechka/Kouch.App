using Kouch.App.Views.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //var page = new NavigationPage(new PostsPage());
            var page = new RegisterPage();
            MainPage = page;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
