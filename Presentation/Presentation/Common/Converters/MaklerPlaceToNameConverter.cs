using System;
using System.Globalization;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Converters
{
    public class MaklerPlaceToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var place = int.Parse(value.ToString());

            switch (place)
            {
                case 1:
                    return "Makler A";
                case 2:
                    return "Makler B";
                case 3:
                    return "Makler C";
                case 4:
                    return "Makler D";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
