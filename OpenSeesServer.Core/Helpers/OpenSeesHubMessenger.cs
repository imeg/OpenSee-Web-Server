using Microsoft.AspNetCore.SignalR;
using OpenSeesServer.Core.Hubs;
using OpenSeesServer.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSeesServer.Core.Helpers
{
    public static class OpenSeesHubMessenger
    {
        public static async Task WriteLine(this IHubContext<OpenSeesHub> hub, string connId, string message, string @class = null)
        {
            await hub.Clients.Client(connId).SendAsync("write", new OutputViewModel
            {
                Class = @class,
                Message = message,
                EndLine = true,
            });
        }

        public static async Task Write(this IHubContext<OpenSeesHub> hub, string connId, string message)
        {
            await hub.Clients.Client(connId).SendAsync("write", new OutputViewModel
            {
                Message = message,
                EndLine = false,
            });
        }
    }
}
