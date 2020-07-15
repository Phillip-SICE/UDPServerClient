using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UDPServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly SiceUDPServer server = new SiceUDPServer();
        bool portValid;
        bool ipValid;
        CancellationTokenSource ctSource;
        CancellationToken token;

        public MainWindow()
        {
            portValid = false;
            ipValid = false;
            InitializeComponent();
            ListBox1.ItemsSource = server.MessageReceived;
            this.DataContext = server;
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
                ctSource = new CancellationTokenSource();
                token = ctSource.Token;
                Task.Run(() => GetData(), token);
                this.Dispatcher.Invoke(() =>
                {
                    ListeningStatus.Text = "Listening";
                    ListeningStatus.Foreground = new SolidColorBrush(Colors.Green); ;
                });
            }            
        }

        private async Task GetData()
        {
            while (true)
            {
                if(server.DataAvailable != 0)
                {
                    await this.Dispatcher.Invoke(async () =>
                    {
                        await server.GetData();
                    });
                }
            }
        }

        private void DisconnectButtonClicked(object sender, RoutedEventArgs e)
        {
            if(server.ConnectionStatus)
            {
                server.Disconnect();
                ctSource.Cancel();
                server.Dispose();
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
                server.ClearMessage();
            });
        }
    }
}
