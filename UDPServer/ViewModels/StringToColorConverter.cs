using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UDPServer.ViewModels
{
    class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "Stopped" ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is SolidColorBrush)
            {
                return ((SolidColorBrush)value).Color == Colors.Red ? "Stopped" : "Listening";
            }
            return "Stopped";
        }
    }
}
