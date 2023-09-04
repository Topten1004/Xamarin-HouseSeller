using System;
using System.Globalization;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Converters
{
    class StyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Style)App.Current.Resources[(string)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Style).BaseResourceKey;
        }
    }
}
