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
            await Clients.Caller.SendAsync("opensees-prompt", "");
            await Clients.Caller.SendAsync("opensees-prompt", "OpenSees -- Open System For Earthquake Engineering Simulation");
            await Clients.Caller.SendAsync("opensees-prompt", "Pacific Earthquake Engineering Research Center");
            await Clients.Caller.SendAsync("opensees-prompt", "Version 3.2.0");
            await Clients.Caller.SendAsync("opensees-prompt", "(c) Copyright 1999-2016 The Regents of the University of California");
            await Clients.Caller.SendAsync("opensees-prompt", "All Rights Reserved");
            await Clients.Caller.SendAsync("opensees-prompt", "(Copyright and Disclaimer @ http://www.berkeley.edu/OpenSees/copyright.html)");

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
