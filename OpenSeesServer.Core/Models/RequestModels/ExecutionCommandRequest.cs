using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSeesServer.Core.Models.RequestModels
{
    public class ExecutionCommandRequest
    {
        public string ConnectionId { get; set; }
        public string Command { get; set; }
    }
}
