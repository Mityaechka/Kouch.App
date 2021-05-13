using System;
using System.Globalization;
using Xamarin.Forms;

namespace Kouch.App.Converters
{
    public class ValueIfNullConverter : IValueConverter
    {
        public object Value { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value??Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
