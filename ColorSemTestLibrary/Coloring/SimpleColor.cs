using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ColorSemTestLibrary.Common;

namespace ColoringColorSemTestLibrary
{
    [EnduranceTest("Simple coloring test")]
    public class SimpleColor
    {
        ILogger _logWriter;
        private void Setup(System.IO.TextWriter logWriter)
        {
            if (_logWriter == null)
            {
                _logWriter = new TextWriterLogger(logWriter);
                _logWriter.Info($"*[SimpleColor] New Setup at {DateTime.Now.ToString("dd.MM hh:mm:ss.fff")}");
            }
            else
                _logWriter.Info($"[SimpleColor] org Setup at {DateTime.Now.ToString("dd.MM hh:mm:ss.fff")}");

        }
        [EnduranceTest]
        public void TestSimpleColor()
        {
            WaitSeconds(0.3451);
        }
        public void NoTested1() { }
        public void NoTested2() { }

        [EnduranceTest]
        public void BlendingNoDuration()
        {
            WaitSeconds(0.0497869);
        }
        [EnduranceTest]
        public void LoggerTest(System.IO.TextWriter logWriter)
        {
            Setup(logWriter);
            TextWriterLogger log = new TextWriterLogger(logWriter);
            _logWriter.Info("SimpleColor, LoggerTest ok");
            WaitSeconds(0.0197869, _logWriter);
        }

        private void WaitSeconds(double secs, ILogger log = null)
        {
            Stopwatch sw = Stopwatch.StartNew();
            string ws = string.Empty;
            int i = 0;
            do
            {
                i++;
                ws += "ElapsedTicks" + sw.ElapsedTicks.ToString();
                if (log != null && i % 100 == 0)
                    log.Info($"Elapse {sw.Elapsed.TotalSeconds} seconds {i}");
            }
            while (sw.Elapsed.TotalSeconds < secs);
        }
    }
}
