using Kouch.App.Views.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Kouch.App
{
    public partial class App : Application
    {
        private static readonly CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public static CancellationToken CancelToken => cancelTokenSource.Token;

        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);

            Connectivity.ConnectivityChanged += ConnectivityChanged;

            MainPage = new LoadingPage();

            Init();
        }
        private async void Init()
        {
            try
            {
                await Task.Delay(1000000, CancelToken);
            }
            catch (Exception)
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
