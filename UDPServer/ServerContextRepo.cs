using System.Threading.Tasks;
using UDPCommGUI;
using System.Linq;
using System;

namespace Sice.PoC.UDPServer
{
    public class ServerContextRepo
    {
        public ServerContext context { get; set; }
        public ServerContextRepo(ServerContext context)
        {
            this.context = context;
        }

        public async Task<int> AddReceivedMessage(ReceivedMessage message)
        {
            context.ReceivedMessages.Add(message);
            return await context.SaveChangesAsync();
        }

        public int GetControllerIDFromLogin(ClientLoginEvent credential)
        {
            var username = credential.Username;
            var password = credential.Password;
            var query = from login in context.Login
                        where login.Username == username
                        select login;
            foreach (var items in query)
            {
                if (items.Username == username && BCrypt.Net.BCrypt.Verify(password, items.PasswordHash))
                {
                    return items.ControllerID;
                }
            }
            throw new ArgumentException();
        }

        public string GetControllerInfo(int controllerID)
        {
            var query = from controller in context.Controllers
                        where controller.ControllerID == controllerID
                        select controller;
            foreach (var item in query)
            {
                if (item.ControllerID == controllerID)
                {
                    return item.ControllerInfo;
                }
            }
            throw new ArgumentException();
        }
    }
}