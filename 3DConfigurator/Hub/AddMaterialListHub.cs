using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace _3DConfigurator.Hub
{
    public class AddMaterialListHub : DynamicHub
    {
        public Task SendMessageToAll(string message)
        {
           return Clients.All.SendAsync("ReceiveMessage", message);

        }
    }
}
