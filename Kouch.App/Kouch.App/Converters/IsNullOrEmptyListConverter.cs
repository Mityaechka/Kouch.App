using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Kouch.App.Converters
{
    public class IsNullOrEmptyListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return true;
            }else if (value.GetType() != typeof(IEnumerable<object>))
            {
                return true;
            }else
            {
                IEnumerable<object> list = (IEnumerable<object>)value;
                if (list.Count() == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
