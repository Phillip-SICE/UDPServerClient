using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UDPCommGUI.ViewModels
{
    class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "Connected" ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is SolidColorBrush)
            {
                return ((SolidColorBrush)value).Color == Colors.Red ? "Disconnected" : "Connected";
            }
            return "Disconnected";
        }
    }
}
