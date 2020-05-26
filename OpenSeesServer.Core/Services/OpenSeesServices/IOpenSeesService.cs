using OpenSees.Tcl;
using OpenSeesServer.Core.Models.RequestModels;
using OpenSeesServer.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSeesServer.Core.Services.OpenSeesServices
{
    public interface IOpenSeesService
    {
        public static Dictionary<string, TclWrapper> TclWrappers { get; private set; } = new Dictionary<string, TclWrapper>();
        Task Initialize(string connectionId);
        Task<TclExecutionResultViewModel> Execute(ExecutionCommandRequest model);
    }
}
