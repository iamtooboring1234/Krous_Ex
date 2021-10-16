using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Krous_Ex.Hubs
{
    public class ChatHub : Hub
    {
        public void send(string message)
        {
            Clients.All.addMessage(message);
        }
    }
}