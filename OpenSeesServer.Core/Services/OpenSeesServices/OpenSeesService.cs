//using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using OpenSees.Components;
using OpenSees.Handlers;
using OpenSees.Tcl;
using OpenSeesServer.Core.Helpers;
using OpenSeesServer.Core.Hubs;
using OpenSeesServer.Core.Infrastructures;
using OpenSeesServer.Core.Models.RequestModels;
using OpenSeesServer.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSeesServer.Core.Services.OpenSeesServices
{
    public partial class OpenSeesService : IOpenSeesService
    {
        //private OpenSeesTextWriter writer;
        //private TclWrapper tclInterp;
        private readonly IMapper mapper;
        private readonly IHubContext<OpenSeesHub> hubContext;

        public OpenSeesService(IMapper mapper, IHubContext<OpenSeesHub> hubContext)
        {
            this.mapper = mapper;
            this.hubContext = hubContext;
        }

        public async Task Initialize(string connectionId)
        {
            try
            {
                var writer = new OpenSeesTextWriter(connectionId, hubContext);
                var tclInterp = new TclWrapper(new RedirectStreamWrapper(writer));
                tclInterp.Init();
                IOpenSeesService.TclWrappers.Add(connectionId, tclInterp);
                await PromptOpenSeesCopyRight(connectionId);
            }
            catch (Exception ex)
            {
                throw new Exception("error in initialization", ex);
            }
        }

        public async Task<TclExecutionResultViewModel> Execute(ExecutionCommandRequest model)
        {
            var ret = getTclWrapper(model.ConnectionId).Execute(model.Command);
            return await Task.FromResult(new TclExecutionResultViewModel()
            {
                Command = model.Command,
                DateTime = DateTime.UtcNow,
                ErrorMessage = ret.ErrorMessage,
                ExecutionStatus = ret.ExecutionStatus,
                Result = ret.Result
            });

            //var sourcefile = @"d:\test-model.tcl";
            //var commands = System.IO.File.ReadAllLines(sourcefile);
            //foreach(var command in commands)
            //{
            //    var execResult = tclInterp.Execute(command);
            //    await hubContext.Clients.Client(connectionId).SendAsync("recived-execution-message", new TclExecutionResultViewModel
            //    {
            //        Command = command,
            //        DateTime = DateTime.UtcNow,
            //        ErrorMessage = execResult.ErrorMessage,
            //        ExecutionStatus = execResult.ExecutionStatus,
            //        Result = execResult.Result,
            //    });
            //}

            //var ret = tclInterp.Execute("wipe");
            //return new TclExecutionResultViewModel()
            //{
            //    Command = "wipe",
            //    DateTime = DateTime.UtcNow,
            //    ErrorMessage = ret.ErrorMessage,
            //    ExecutionStatus = ret.ExecutionStatus,
            //    Result = ret.Result
            //};
        }

        private TclWrapper getTclWrapper(string connId) {
            return IOpenSeesService.TclWrappers[connId];
        }

        private async Task PromptOpenSeesCopyRight(string connectionId)
        {
            await hubContext.WriteLine(connectionId, "" , HtmlStylingClass.TextCenter);
            await hubContext.WriteLine(connectionId, "OpenSees -- Open System For Earthquake Engineering Simulation", HtmlStylingClass.TextCenter);
            await hubContext.WriteLine(connectionId, "Pacific Earthquake Engineering Research Center", HtmlStylingClass.TextCenter);
            await hubContext.WriteLine(connectionId, "Version 3.2.0", HtmlStylingClass.TextCenter);
            await hubContext.WriteLine(connectionId, "(c) Copyright 1999-2016 The Regents of the University of California", HtmlStylingClass.TextCenter);
            await hubContext.WriteLine(connectionId, "All Rights Reserved", HtmlStylingClass.TextCenter);
            await hubContext.WriteLine(connectionId, "(Copyright and Disclaimer @ http://www.berkeley.edu/OpenSees/copyright.html)", HtmlStylingClass.TextCenter);
        }
    }
}
