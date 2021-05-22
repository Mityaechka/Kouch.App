using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class AsyncBaseViewModel : BaseViewModel
    {
        private bool isLoading;
        public AsyncBaseViewModel(INavigation navigation) : base(navigation)
        {
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
