//using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using OpenSees.Components;
using OpenSees.Handlers;
using OpenSees.Tcl;
using OpenSeesServer.Core.Hubs;
using OpenSeesServer.Core.Infrastructures;
using OpenSeesServer.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSeesServer.Core.Services.OpenSeesServices
{
    public partial class OpenSeesService : IOpenSeesService
    {
        //private readonly ILogger<OpenSeesService> logger;
        private OpenSeesTextWriter writer;
        private TclWrapper tclInterp;
        private readonly IMapper mapper;
        private readonly IHubContext<OpenSeesHub> hubContext;

        /*ILogger<OpenSeesService> logger*/
        public OpenSeesService(IMapper mapper, IHubContext<OpenSeesHub> hubContext)
        {
            //this.logger = logger;
            this.mapper = mapper;
            this.hubContext = hubContext;
            Initialize();
        }

        public void Initialize()
        {
            try
            {
                this.writer = new OpenSeesTextWriter("1");
                this.tclInterp = new TclWrapper(new RedirectStreamWrapper(this.writer));
                this.tclInterp.Init();
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "Unable to create opensees interpreter instance");
            }
        }

        public async Task<TclExecutionResultViewModel> Execute(string connectionId)
        {
            var sourcefile = @"d:\test-model.tcl";
            var commands = System.IO.File.ReadAllLines(sourcefile);
            foreach(var command in commands)
            {
                var execResult = tclInterp.Execute(command);
                await hubContext.Clients.Client(connectionId).SendAsync("recived-execution-message", new TclExecutionResultViewModel
                {
                    Command = command,
                    DateTime = DateTime.UtcNow,
                    ErrorMessage = execResult.ErrorMessage,
                    ExecutionStatus = execResult.ExecutionStatus,
                    Result = execResult.Result,
                });
            }
            
            var ret = tclInterp.Execute("wipe");
            return new TclExecutionResultViewModel()
            {
                Command = "wipe",
                DateTime = DateTime.UtcNow,
                ErrorMessage = ret.ErrorMessage,
                ExecutionStatus = ret.ExecutionStatus,
                Result = ret.Result
            };
        }

        private DomainWrapper Domain
        {
            get
            {
                return tclInterp.GetActiveDomain();
            }
        }
    }
}
