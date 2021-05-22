
using Kouch.App.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {


        public INavigation Navigation { get; set; }
        public BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string this[string path] => LocalizationService.Instance[path];

    }


}
