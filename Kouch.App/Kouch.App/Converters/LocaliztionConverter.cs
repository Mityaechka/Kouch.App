using Kouch.App.Services;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Kouch.App.Converters
{
    public class LocaliztionConverter : IValueConverter
    {
        public string Path { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return "Translate Error";
            }
            return LocalizationService.Instance[(string)parameter];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
