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

        MyUDPServer server = new MyUDPServer();
        Thread listeningThread;

        public MainWindow()
        {
            InitializeComponent();
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
            server.Connect();
            listeningThread = new Thread(new ThreadStart(GetData));
            listeningThread.Start();
        }

        private void GetData()
        {
            while(true)
            {
                server.GetData();
                this.Dispatcher.Invoke(() =>
                {
                    ListBox1.Items.Add(server.ReceivedData);
                });

            }
        }

        private void DisconnectButtonClicked(object sender, RoutedEventArgs e)
        {
            listeningThread.Abort();
        }
    }
}
