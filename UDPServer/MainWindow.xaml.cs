using System;
using System.Collections.Generic;
using System.Linq;
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

        var server = new MyUDPServer();
        Thread listeningThread;

        public MainWindow()
        {
            InitializeComponent();
            ListBox1.ItemsSource = server.MessageReceived;
            this.DataContext = server;
        }

        private void IPChanged(object sender, TextChangedEventArgs e)
        {
            server.ConnectionIP = IPBox.Text;
        }

        private void PortChanged(object sender, TextChangedEventArgs e)
        {
            server.ConnectionPort = Int32.Parse(PortBox.Text);
        }

        private void ConnectButtonClicked(object sender, RoutedEventArgs e)
        {
            if(!server.ConnectionStatus)
            {
                server.Connect();
                listeningThread = new Thread(new ThreadStart(GetData));
                listeningThread.Start();
                this.ChangeStatus();
            }            
        }

        private void GetData()
        {
            while (true)
            {
                if(server.DataAvailable != 0)
                {
                    this.Dispatcher.Invoke(async () =>
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
                listeningThread.Abort();
                server.Disconnect();
                this.ChangeStatus();
            }            
        }

        private void ChangeStatus()
        {
            if (server.ConnectionStatus)
            {
                this.Dispatcher.Invoke(() =>
                {
                    ListeningStatus.Text = "Listening";
                    ListeningStatus.Foreground = new SolidColorBrush(Colors.Green); ;
                });
            }
            else
            {
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
