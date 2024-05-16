using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Hubs
{
    public class Notihub : Hub
    {
        public async Task NotiMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
       
    }
}
