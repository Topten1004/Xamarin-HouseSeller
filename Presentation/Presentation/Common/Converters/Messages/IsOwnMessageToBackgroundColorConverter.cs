using System;
using System.Globalization;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Converters.Messages
{
    public class IsOwnMessageToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isOwnMessage = (bool)value;

            if (isOwnMessage)
            {
                return (Color)App.Current.Resources["ColorBlueMain"];
            }
            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
