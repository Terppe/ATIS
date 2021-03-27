using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ATIS.Ui.Helper.ValueConverter
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new BitmapImage();
            }

            try
            {
                var strmImg = new MemoryStream((byte[])value);
                var myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.StreamSource = strmImg;
                myBitmapImage.EndInit();
                return myBitmapImage;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return new BitmapImage();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
