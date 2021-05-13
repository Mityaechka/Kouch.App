
using Kouch.App.Services;
using Kouch.App.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        

        public INavigation Navigation { get; set; }
        public BaseViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
        public void OnPropertyChanged([CallerMemberName] string propName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string this[string path]
        {
            get
            {
                return LocalizationService.Instance[path];
            }
        }

    }


}
