using System;
using System.Globalization;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Converters
{
    public class IntentionToSellToIsUrgentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value >= 8;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
