using OpenSeesServer.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSeesServer.Core.Services.OpenSeesServices
{
    public interface IOpenSeesService
    {
        Task<TclExecutionResultViewModel> Execute(string connectionId);
    }
}
