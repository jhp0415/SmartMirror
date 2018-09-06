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
    public class WeatherConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string boundWord = value as string;
            string imageSource = null;

            switch (boundWord.ToLower().Trim())
            {
                case "clear":
                    imageSource = "Image\\Weather\\Clear - Day Time.png";
                    break;
                case "partly cloudy":
                    imageSource = "Image\\Weather\\Partly Cloudy - Day Time.png";
                    break;
                case "mostly cloudy":
                    imageSource = "Image\\Weather\\Mostly Cloudy - Day Time.png";
                    break;
                case "cloudy":
                    imageSource = "Image\\Weather\\Cloudy.png";
                    break;
                case "rain":
                    imageSource = "Image\\Weather\\Rain.png";
                    break;
                case "snow":
                    imageSource = "Image\\Weather\\Snow.png";
                    break;
            }

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Cant convert back");
        }
    }
}
