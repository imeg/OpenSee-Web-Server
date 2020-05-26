using Microsoft.AspNetCore.SignalR;
using OpenSeesServer.Core.Helpers;
using OpenSeesServer.Core.Hubs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenSeesServer.Core.Infrastructures
{
    public class OpenSeesTextWriter : TextWriter
    {
        private readonly IHubContext<OpenSeesHub> hubContext;

        public string ConnectionId { get; set; }
        public override Encoding Encoding => Encoding.ASCII;

        public OpenSeesTextWriter(string connectionId, IHubContext<OpenSeesHub> hubContext)
        {
            ConnectionId = connectionId;
            this.hubContext = hubContext;
        }

        public override void Write(double value)
        {
            Console.Write($"{value}");
            this.hubContext.Write(ConnectionId, $"{value}").Wait();
        }

        public override void Write(string value)
        {
            if (value == "\n")
            {
                Console.Write($"{value}");
                this.hubContext.WriteLine(ConnectionId, "").Wait();
            }
            Console.Write($"{value}");
            this.hubContext.Write(ConnectionId, $"{value}").Wait();
        }

        public override void WriteLine(string value)
        {
            Console.WriteLine($"{value}");
            this.hubContext.WriteLine(ConnectionId, $"{value}").Wait();
        }

        public void SetFontColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
