using System.Configuration;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sice.PoC.UDPCommGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly SiceUDPClient client = new SiceUDPClient();
        bool ipValid;
        bool portValid;
        bool msgValid;

        public MainWindow()
        {
            ipValid = false;
            portValid = false;
            msgValid = false;
            InitializeComponent();
            IPbox.Text = ConfigurationManager.AppSettings["IPAddress"];
            PortBox.Text = ConfigurationManager.AppSettings["PortNumber"];
        }

        private void IPChanged(object sender, TextChangedEventArgs e)
        {
            ipValid = IPAddress.TryParse(IPbox.Text, out IPAddress parseOut);
            if (ipValid)
            {
                client.ConnectionIP = parseOut;
            }
        }

        private void PortChanged(object sender, TextChangedEventArgs e)
        {
            portValid = int.TryParse(PortBox.Text, out int parseOut);
            if (portValid)
            {
                client.ConnectionPort = parseOut;
            }
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            if (!ipValid)
            {
                MessageBox.Show("Invalid IP");
                return;
            }
            if (!portValid)
            {
                MessageBox.Show("Invalid port");
                return;
            }
            client.Connect();
            this.Dispatcher.Invoke(() =>
            {
                ConStatus.Text = "Connected";
                ConStatus.Foreground = new SolidColorBrush(Colors.Green); ;
            });
        }

        private void MessageChanged(object sender, TextChangedEventArgs e)
        {
            msgValid = !string.IsNullOrEmpty(MsgBox.Text);
            if(msgValid)
            {
                client.InputMessage = MsgBox.Text;
            }
        }

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            if(!msgValid)
            {
                MessageBox.Show("Message empty");
                return;
            }
            client.SendMessage();
        }

        private void DisconnectClicked(object sender, RoutedEventArgs e)
        {
            client.Disconnect();
            client.Dispose();
            this.Dispatcher.Invoke(() =>
            {
                ConStatus.Text = "Disconnected";
                ConStatus.Foreground = new SolidColorBrush(Colors.Red);
            });
        }
    }
}
