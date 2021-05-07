using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kouch.App.ViewModels
{
    public class AppStorageService
    {
        public void SaveData(string key,string data) 
        {
            CrossSettings.Current.AddOrUpdateValue(key, data);
        }
    }
}
