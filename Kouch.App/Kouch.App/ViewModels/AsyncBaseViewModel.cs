using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class AsyncBaseViewModel : BaseViewModel
    {
        private readonly bool showLoadingModal;
        private bool isLoading;
        private bool isError;
        private string error;
        public AsyncBaseViewModel(INavigation navigation) : base(navigation)
        {
        }
        public bool IsError
        {
            get => isError; set
            {
                if (isError != value)
                {
                    isError = value;
                    OnPropertyChanged("IsError");
                }
            }
        }
        public string Error
        {
            get => error; set
            {
                if (error != value)
                {
                    error = value;
                    OnPropertyChanged("Error");
                }
            }
        }


        public bool IsLoading
        {
            get => isLoading;
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged("IsLoading");
                }
            }
        }
    }
}
