using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenSeesServer.Core.Infrastructures
{
    public class OpenSeesTextWriter : TextWriter
    {
        public string SessionId { get; set; }
        public override Encoding Encoding => Encoding.ASCII;

        public OpenSeesTextWriter(string sessionId)
        {
            SessionId = sessionId;
        }

        public override void Write(double value)
        {
            Console.Write($"{SessionId}> {value}");
        }

        public override void Write(string value)
        {
            if (value == "\n")
            {
                Console.Write($"{value}");
                return;
            }
            Console.Write($"{SessionId}> {value}");
        }

        public override void WriteLine(string value)
        {
            Console.WriteLine($"{SessionId}> {value}");
        }

        public void SetFontColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
