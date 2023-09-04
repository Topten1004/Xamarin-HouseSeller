using System;
using System.Globalization;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Converters
{
    public class IsEmptyArrayToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(value.ToString()) == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
