using System.Collections.ObjectModel;
using System.Configuration;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sice.PoC.UDPServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly SiceUDPServer server = new SiceUDPServer();
        public ObservableCollection<string> ReceivedMessages;
        bool portValid;
        bool ipValid;

        public MainWindow()
        {
            portValid = false;
            ipValid = false;
            server.MessageReceivedEventHandler += server_MessageReceived;
            ReceivedMessages = new ObservableCollection<string>();
            InitializeComponent();
            ListBox1.ItemsSource = ReceivedMessages;
            this.DataContext = this;
            IPBox.Text = ConfigurationManager.AppSettings["IPAddress"];
            PortBox.Text = ConfigurationManager.AppSettings["PortNumber"];
        }

        private void IPChanged(object sender, TextChangedEventArgs e)
        {
            ipValid = IPAddress.TryParse(IPBox.Text, out IPAddress parseOut);
            if (ipValid)
            {
                server.ConnectionIP = parseOut;
            }
        }

        private void PortChanged(object sender, TextChangedEventArgs e)
        {
            portValid = int.TryParse(PortBox.Text, out int parseOut);
            if (portValid)
            {
                server.ConnectionPort = parseOut;
            }
        }

        private void ConnectButtonClicked(object sender, RoutedEventArgs e)
        {
            if(!ipValid)
            {
                MessageBox.Show("Invalid IP");
                return;
            }
            if(!portValid)
            {
                MessageBox.Show("Invalid port");
                return;
            }
            if(!server.ConnectionStatus)
            {
                server.Connect();
                this.Dispatcher.Invoke(() =>
                {
                    ListeningStatus.Text = "Listening";
                    ListeningStatus.Foreground = new SolidColorBrush(Colors.Green); ;
                });
            }            
        }

        private void DisconnectButtonClicked(object sender, RoutedEventArgs e)
        {
            if(server.ConnectionStatus)
            {
                server.Disconnect();
                this.Dispatcher.Invoke(() =>
                {
                    ListeningStatus.Text = "Stoped";
                    ListeningStatus.Foreground = new SolidColorBrush(Colors.Red);
                });
            }            
        }

        private void ClearButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                ReceivedMessages.Clear();
            });
        }

        public void server_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                ReceivedMessages.Add(e.ReceivedMessage.Message);
            });
        }
    }
}
