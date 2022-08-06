using System;
using System.Windows.Data;

namespace Lastik_Otel.Data
{
    public class datepickerNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object aDateTime = (DateTime)value;

            if ((DateTime)aDateTime == null || (DateTime)aDateTime == DateTime.MinValue)
            {
                return DateTime.Now;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object aDateTime = (DateTime)value;

            if ((DateTime)aDateTime== null || (DateTime)aDateTime == DateTime.MinValue)
            {
                return DateTime.Today;
            }
            return value;
        }
    }
}
