using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSemTestLibrary.Common
{
    public class TextWriterLogger : ILogger
    {
        private System.IO.TextWriter _textWriter;
        public TextWriterLogger(System.IO.TextWriter textWriter)
        {
            _textWriter = textWriter;
        }
        public void Log(string message)
        {
            _textWriter.WriteLine(message);
            _textWriter.Flush();
        }

        public void Error(string message)
        {
            Log("[!E] " + message);
        }

        public void Info(string message)
        {
            Log("[I] " + message);
        }

        public void Warning(string message)
        {
            Log("[W] " + message);
        }
    }
}
