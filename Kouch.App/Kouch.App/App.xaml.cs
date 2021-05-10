using Kouch.App.Views.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using I18NPortable;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;

namespace Kouch.App
{
    public partial class App : Application
    {
        private static readonly CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public static CancellationToken CancelToken => cancelTokenSource.Token;

        public App()
        {
            InitializeComponent();
            



            //var page = new NavigationPage(new PostsPage());
            var page = new MainPage();
            MainPage = page;
            Connectivity.ConnectivityChanged += ConnectivityChanged;
            Init();
        }
        private async void Init()
        {
            try
            {
                await Task.Delay(1000000, CancelToken);
            }
            catch (Exception e)
            {

            }
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
        private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.None)
            {
                cancelTokenSource.Cancel();
            }
        }
    }

}
