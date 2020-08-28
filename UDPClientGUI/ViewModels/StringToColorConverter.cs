using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UDPCommGUI.ViewModels
{
    public class StringToColorConverter : IValueConverter
    {
        public StringToColorConverter()
        {
            this.GreenString = Properties.Resources.Connected;
            this.RedString = Properties.Resources.Disconnected;
        }

        public StringToColorConverter(string GreenString, string RedString)
        {
            this.GreenString = GreenString;
            this.RedString = RedString;
        }

        public readonly string GreenString;
        public readonly string RedString;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == GreenString ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (value as SolidColorBrush)?.Color;
            var connectionStatus = (color == Colors.Red) ? RedString : GreenString;
            return connectionStatus;
        }
    }
}
