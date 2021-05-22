using System;
using System.Globalization;
using Xamarin.Forms;

namespace Kouch.App.Converters
{
    public class SubstringConverter : IValueConverter
    {
        public int Length { get; set; } = 5;
        public bool AddDots { get; set; } = true;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Length < Length)
            {
                return value;
            }
            return $"{value.ToString().Substring(0, Length)}{(AddDots ? "..." : "")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
