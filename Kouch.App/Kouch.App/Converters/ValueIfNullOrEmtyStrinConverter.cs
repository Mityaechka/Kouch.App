using System;
using System.Globalization;
using Xamarin.Forms;

namespace Kouch.App.Converters
{
    public class ValueIfNullOrEmtyStrinConverter : IValueConverter
    {
        public object Value { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Value;
            }

            return string.IsNullOrWhiteSpace(value.ToString()) ? Value : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class IsNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return true;
            }

            return string.IsNullOrWhiteSpace(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
