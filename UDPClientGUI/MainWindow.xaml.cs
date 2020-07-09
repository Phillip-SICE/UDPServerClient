﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace UDPCommGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MyUDPClient client = new MyUDPClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void IPChanged(object sender, TextChangedEventArgs e)
        {
            client.ConnectionIP = IPbox.Text;
        }

        private void PortChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                client.ConnectionPort = Int32.Parse(PortBox.Text);
            }
            finally
            {

            }
            
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            client.Connect(client.ConnectionIP, client.ConnectionPort);
        }

        private void MessageChanged(object sender, TextChangedEventArgs e)
        {
            client.InputMessage = MessageBox.Text;
        }

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            client.SendMessage();
        }

        private void DisConnectClicked(object sender, RoutedEventArgs e)
        {
            client.Close();
        }
    }
}
