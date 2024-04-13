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
        public async Task SendMessage(string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message);
        }
       
        //public override Task OnConnectedAsync()
        //{
        //    if (Context.User.Identity.Name != null)
        //    {
        //        return Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
        //    }
        //    return base.OnConnectedAsync();
        //}

    }
}
