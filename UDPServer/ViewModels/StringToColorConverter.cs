using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UDPServer.ViewModels
{
    class StringToColorConverter : IValueConverter
    {
<<<<<<< Updated upstream
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "Stopped" ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
=======
        public StringToColorConverter()
        {
            this.GreenString = Properties.Resources.Listening;
            this.YellowString = Properties.Resources.Connected;
            this.RedString = Properties.Resources.Stopped;
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
>>>>>>> Stashed changes
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
<<<<<<< Updated upstream
            if(value is SolidColorBrush)
            {
                return ((SolidColorBrush)value).Color == Colors.Red ? "Stopped" : "Listening";
            }
            return "Stopped";
=======
            var Color = (value as SolidColorBrush)?.Color;
            var ConnectionStatus = (Color == Colors.Red) ? RedString : GreenString;
            return ConnectionStatus;
>>>>>>> Stashed changes
        }
    }

}
