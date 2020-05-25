using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSeesServer.Core.Models.ViewModels
{
    public class TclExecutionResultViewModel
    {
        public int ExecutionStatus { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
        public string Command { get; set; }
        public DateTime DateTime { get; set; }
    }
}
