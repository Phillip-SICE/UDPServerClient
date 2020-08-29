using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using UDPServer;

namespace UDPServer.ViewModels
{
    class StringToColorConverter : IValueConverter
    {
        public StringToColorConverter()
        {
            this.GreenString = Properties.Resource.Listening;
            this.YellowString = Properties.Resource.Connected;
            this.RedString = Properties.Resource.Stopped;
        }

        public readonly string YellowString;
        public readonly string GreenString;
        public readonly string RedString;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush color = new SolidColorBrush();
            var status = value.ToString();
            if (status == GreenString) color = new SolidColorBrush(Colors.Green);
            if (status == YellowString) color = new SolidColorBrush(Colors.Yellow);
            if (status == RedString) color = new SolidColorBrush(Colors.Red);
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Color = (value as SolidColorBrush)?.Color;
            var ConnectionStatus = (Color == Colors.Red) ? RedString : GreenString;
            return ConnectionStatus;
        }
    }

}
