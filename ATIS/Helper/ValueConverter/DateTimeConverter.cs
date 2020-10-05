using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace ATIS.Ui.Helper.ValueConverter
{
    /// Kurze Datums+Zeit Darstellung. Verwendet CurrentCulture und nicht CurrentUICulture
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToString("g", Thread.CurrentThread.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value.ToString();
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out resultDateTime))
            {
                return resultDateTime;
            }
            return value;
        }
    }
}
