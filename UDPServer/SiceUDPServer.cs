using Caliburn.Micro;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPServer;
using System.Linq;
using UDPCommGUI;
using Newtonsoft.Json;

namespace Sice.PoC.UDPServer
{
    public class SiceUDPServer : IServerInterface, IHandle<ServerCommandEvent>
    {
        private IEventAggregator eventAggregator;
        private UdpClient client;
        private CancellationTokenSource source;
        private CancellationToken token;
        public IPAddress ConnectionIP { get; set; }
        public int ConnectionPort { get; set; }
        public string ReceivedData { get; set; }
        public bool ConnectionStatus { get; set; }
        public ReceivedMessage ReceivedMessage { get; set; }
        public int DataAvailable => client.Available;
        public bool HasLoggedIn { get; set; }
        public int ConnectedControllerID { get; set; }
        public string ConnectedControllerInfo { get; set; }

        public SiceUDPServer(IEventAggregator eventAggregator)
        {
            this.ConnectionStatus = false;
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
        }

        public void Connect()
        {
            if(!ConnectionStatus)
            {
                client = new UdpClient(ConnectionPort);
                source = new CancellationTokenSource();
                token = source.Token;
                Task.Run(() => Listen(), token);
                this.ConnectionStatus = true;
                eventAggregator.PublishOnUIThread(new ServerStatusChangedEvent(ServerStatus.Connected));
            }
        }

        private async Task Listen()
        {
            while (true)
            {
                if (ConnectionStatus && DataAvailable != 0)
                {
                    if(HasLoggedIn)
                    {
                        await GetData();
                    }
                    else
                    {
                        await GetLogin();
                    }
                }
            }
        }

        public void Disconnect()
        {
            if(ConnectionStatus)
            {
                this.ConnectionStatus = false;
                client.Close();
                source.Cancel();
                eventAggregator.PublishOnUIThread(new ServerStatusChangedEvent(ServerStatus.Stopped));
            }
        }

        public async Task GetData()
        {
            var remoteIpEndPoint = new IPEndPoint(ConnectionIP, ConnectionPort);
            var data = await client.ReceiveAsync();
            ReceivedData = Encoding.ASCII.GetString(data.Buffer);
            ReceivedMessage = new ReceivedMessage(DateTime.Now.ToString(), data.RemoteEndPoint.Address.ToString(), ReceivedData, ConnectedControllerID);
            this.eventAggregator.PublishOnUIThread(ReceivedMessage);
            using (var db = new ServerContext())
            {
                db.ReceivedMessages.Add(ReceivedMessage);
                await db.SaveChangesAsync();
            }
        }

        public async Task GetLogin()
        {
            var remoteIpEndPoint = new IPEndPoint(ConnectionIP, ConnectionPort);
            var data = await client.ReceiveAsync();
            ReceivedData = Encoding.ASCII.GetString(data.Buffer);
            //var credential = JsonSerializer.Deserialize(ReceivedData, ClientLoginEvent, default);
            var credential = JsonConvert.DeserializeObject<ClientLoginEvent>(ReceivedData);
            string username = credential.Username;
            string password = credential.Password;
            using (var db = new ServerContext())
            {
                var query = from login in db.Login
                            where login.Username == username
                            select login;
                foreach (var items in query)
                {
                    if(items.Username == username && BCrypt.Net.BCrypt.Verify(password, items.PasswordHash))
                    {
                        HasLoggedIn = true;
                        ConnectedControllerID = items.ControllerID;
                        GetControllerInfo(ConnectedControllerID);
                        eventAggregator.PublishOnUIThread(new ServerStatusChangedEvent(ServerStatus.Listening, ConnectedControllerInfo));
                    }
                }
            }
        }

        public void GetControllerInfo(int controllerID)
        {
            using (var db = new ServerContext())
            {
                var query = from controller in db.Controllers
                            where controller.ControllerID == controllerID
                            select controller;
                foreach (var item in query)
                {
                    if(item.ControllerID == controllerID)
                    {
                        this.ConnectedControllerInfo = item.ControllerInfo;
                    }
                }

            }
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public void Handle(ServerCommandEvent command)
        {
            if(command.Command)
            {
                ConnectionIP = command.ConnectionIP;
                ConnectionPort = command.ConnectionPort;
                this.Connect();
            }
            else
            {
                this.Disconnect();
            }
        }
    }
}
