using Microsoft.AspNetCore.SignalR;
using OpenSeesServer.Core.Models.RequestModels.SignalRModels;
using OpenSeesServer.Core.Services.OpenSeesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSeesServer.Core.Hubs
{
    public class OpenSeesHub : Hub
    {
        private readonly IOpenSeesService openSeesService;

        public OpenSeesHub(IOpenSeesService openSeesService)
        {
            this.openSeesService = openSeesService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (IOpenSeesService.TclWrappers.ContainsKey(Context.ConnectionId))
            {
                try
                {
                    var tclInterp = IOpenSeesService.TclWrappers[Context.ConnectionId];
                    try
                    {
                        IOpenSeesService.TclWrappers.Remove(Context.ConnectionId);
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        tclInterp.Execute("wipe");
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        //tclInterp.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                }
                catch (Exception)
                {

                }
            }
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("get-connection-info")]
        public async Task GetConnectionInfo()
        {
            await Clients.Caller.SendAsync("connection-info", new HubInfoViewModel
            {
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
