using System;
using System.Globalization;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Converters
{
    public class IsHeaderToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return (Color)App.Current.Resources["ColorBlueMain"];
            }
            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
