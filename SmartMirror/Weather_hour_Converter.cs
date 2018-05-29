using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace SmartMirror
{
    [ValueConversion(typeof(String), typeof(String))]
    public class Weather_hour_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val=value as string;
            int hour = Int32.Parse(val);
            int hour_1 = hour - 3;
            string result = hour_1.ToString()+"시 - "+hour.ToString()+"시";
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Cant convert back");
        }
    }
}
