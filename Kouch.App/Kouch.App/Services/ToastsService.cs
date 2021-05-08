using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kouch.App.Services
{
    public class ToastsService
    {
        private static ToastsService _instance;
        public static ToastsService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ToastsService();
                }

                return _instance;
            }
        }
        private ToastsService()
        {

        }
        public void ShowToast(string message, ToastLength toastLength = ToastLength.Short)
        {
            CrossToastPopUp.Current.ShowToastMessage(message,toastLength);
        }
    }
}
