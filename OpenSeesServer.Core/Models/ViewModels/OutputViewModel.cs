using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSeesServer.Core.Models.ViewModels
{
    public class OutputViewModel
    {
        public string Message { get; set; }
        public string @Class { get; set; }
        public bool EndLine { get; set; }
    }
}
