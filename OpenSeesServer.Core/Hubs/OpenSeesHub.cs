using Microsoft.AspNetCore.SignalR;
using OpenSeesServer.Core.Models.RequestModels.SignalRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSeesServer.Core.Hubs
{
    public class OpenSeesHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("get-hub-info")]
        public async Task GetHubInfo()
        {
            await Clients.Caller.SendAsync("hub-info", new HubInfoViewModel {
                ConnectionId = Context.ConnectionId,
            });
            return;
        }
    }

    public class ClientModel
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
    }
}
